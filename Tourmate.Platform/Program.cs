using Tourmate.Platform.Iam.Application.Acl;
using Tourmate.Platform.Iam.Application.CommandServices;
using Tourmate.Platform.Iam.Application.Internal.CommandServices;
using Tourmate.Platform.Iam.Application.Internal.OutboundServices;
using Tourmate.Platform.Iam.Application.Internal.QueryServices;
using Tourmate.Platform.Iam.Application.QueryServices;
using Tourmate.Platform.Iam.Domain.Repositories;
using Tourmate.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using Tourmate.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using Tourmate.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Tourmate.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using Tourmate.Platform.Iam.Interfaces.Acl;
using Tourmate.Platform.Iam.Resources;
using Tourmate.Platform.FeedbackAndTourReviews.Application.CommandServices;
using Tourmate.Platform.FeedbackAndTourReviews.Application.Internal.CommandServices;
using Tourmate.Platform.FeedbackAndTourReviews.Application.Internal.QueryServices;
using Tourmate.Platform.FeedbackAndTourReviews.Application.QueryServices;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Repositories;
using Tourmate.Platform.FeedbackAndTourReviews.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Repositories;

using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Resources.Shared;
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Interfaces.AspNetCore.OpenApi;
using Tourmate.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;
using Tourmate.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scalar.AspNetCore;
// Added for ProblemDetailsFactory
// Added for base ProblemDetailsFactory
// Added for IamMessages
// Added for ProfilesMessages
using ProblemDetailsFactory = Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails and ExceptionHandler services
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Database Connection

// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .AddInterceptors(serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Explicitly register IStringLocalizer for ErrorMessages and Commons
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services
    .AddSingleton<IStringLocalizer<CommonMessages>,
        StringLocalizer<CommonMessages>>(); // Corrected from Common to Commons
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>(); // Added for IamMessages


// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();


// OpenAPI / Scalar documentation
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    options.AddOperationTransformer<IamBearerSecurityOperationTransformer>();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();


// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();


// FeedbackAndTourReviews Bounded Context Injection Configuration
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(ExceptionHandlingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(TransactionCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    [typeof(Program)]);


var app = builder.Build();

// Apply pending migrations on startup (safe to call even when schema is up to date)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "Learning Center Platform API v1"));
    app.MapScalarApiReference();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();