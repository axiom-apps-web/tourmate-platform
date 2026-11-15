FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY Tourmate.Platform/*.csproj Tourmate.Platform/
RUN dotnet restore ./Tourmate.Platform
COPY . .
RUN dotnet publish ./Tourmate.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Tourmate.Platform.dll"]