# Используем образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Копируем все проекты
COPY ["src/", "src/"]

# Копируем все тесты
COPY ["tests/", "tests/"]

# Копируем файлы решения и конфигурации
COPY ["Reminy.Core.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Build.targets", "./"]
COPY ["Directory.Packages.props", "./"]

# Восстанавливаем зависимости для всех проектов
RUN dotnet build -restore Reminy.Core.sln

# Сборка всех проектов
RUN dotnet build -c Release -o /app/build

# Публикация
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Используем образ рантайма для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Staging

EXPOSE 5000

ENTRYPOINT ["dotnet", "Reminy.Core.Host.dll"]
