#Main app dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BlogApp.Presentation/BlogApp.Presentation.csproj", "src/BlogApp.Presentation/"]
COPY ["src/BlogApp.Infrastructure/BlogApp.Infrastructure.csproj", "src/BlogApp.Infrastructure/"]
COPY ["src/BlogApp.Application/BlogApp.Application.csproj", "src/BlogApp.Application/"]
COPY ["src/BlogApp.Domain/BlogApp.Domain.csproj", "src/BlogApp.Domain/"]
RUN dotnet restore "src/BlogApp.Presentation/BlogApp.Presentation.csproj"
COPY . .
WORKDIR "/src/src/BlogApp.Presentation"
RUN dotnet build "BlogApp.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogApp.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogApp.Presentation.dll"]