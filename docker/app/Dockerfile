FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["src/", "src/"]
COPY ["tests/", "tests/"]

COPY ["Reminy.Core.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Build.targets", "./"]
COPY ["Directory.Packages.props", "./"]

RUN dotnet build -restore Reminy.Core.sln
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
RUN apt-get update && apt-get install -y nano
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT Production

EXPOSE 5000

ENTRYPOINT ["dotnet", "Reminy.Core.Host.dll"]
