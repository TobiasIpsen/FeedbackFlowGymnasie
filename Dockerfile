# Build Code
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY /feedbackFlowAPI/feedbackFlowAPI.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build /feedbackFlowAPI/feedbackFlowAPI.csproj --no-restore --configuration Release
RUN dotnet publish ./feedbackFlowAPI/feedbackFlowAPI.csproj --no-build --configuration Release --output /app

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "feedbackFlowAPI.dll" ]