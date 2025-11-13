FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/learning-center-webapi/learning-center-webapi.csproj", "src/learning-center-webapi/"]
RUN dotnet restore "src/learning-center-webapi/learning-center-webapi.csproj"
COPY . .
WORKDIR "/src/src/learning-center-webapi"
RUN dotnet build "learning-center-webapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "learning-center-webapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "learning_center_webapi.dll"]
