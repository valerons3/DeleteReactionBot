FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DeleteReactionBot/DeleteReactionBot.csproj", "DeleteReactionBot/"]
RUN dotnet restore "DeleteReactionBot/DeleteReactionBot.csproj"
COPY . .
WORKDIR "/src/DeleteReactionBot"
RUN dotnet build "./DeleteReactionBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DeleteReactionBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeleteReactionBot.dll"]
