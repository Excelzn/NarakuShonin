FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NarakuShonin.Web/NarakuShonin.Web.csproj", "NarakuShonin.Web/"]
RUN dotnet restore "NarakuShonin.Web/NarakuShonin.Web.csproj"
COPY . .
WORKDIR "/src/NarakuShonin.Web"
RUN dotnet build "./NarakuShonin.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NarakuShonin.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NarakuShonin.Web.dll"]
