#Main app dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BlogApp.Authorization/BlogApp.Authorization.csproj", "src/BlogApp.Authorization/"]
RUN dotnet restore "src/BlogApp.Authorization/BlogApp.Authorization.csproj"
COPY . .
WORKDIR "/src/src/BlogApp.Authorization"
RUN dotnet build "BlogApp.Authorization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogApp.Authorization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogApp.Authorization.dll"]