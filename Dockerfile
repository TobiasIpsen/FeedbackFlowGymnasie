# Build Code
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY /feedbackFlowAPI/feedbackFlowAPI.csproj .
RUN dotnet restore "feedbackFlowAPI.csproj"

COPY ./feedbackFlowAPI .
RUN dotnet publish "feedbackFlowAPI.csproj" --output /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "feedbackFlowAPI.dll" ]