#Main app dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BlogApp.Auth.Presentation/BlogApp.Auth.Presentation.csproj", "src/BlogApp.Auth.Presentation/"]
COPY ["src/BlogApp.Auth.Infrastructure/BlogApp.Auth.Infrastructure.csproj", "src/BlogApp.Auth.Infrastructure/"]
COPY ["src/BlogApp.Auth.Application/BlogApp.Auth.Application.csproj", "src/BlogApp.Auth.Application/"]
COPY ["src/BlogApp.Auth.Domain/BlogApp.Auth.Domain.csproj", "src/BlogApp.Auth.Domain/"]
RUN dotnet restore "src/BlogApp.Auth.Presentation/BlogApp.Auth.Presentation.csproj"
COPY . .
WORKDIR "/src/src/BlogApp.Auth.Presentation"
RUN dotnet build "BlogApp.Auth.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogApp.Auth.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogApp.Auth.Presentation.dll"]