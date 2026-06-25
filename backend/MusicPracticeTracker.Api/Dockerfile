FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["backend/MusicPracticeTracker.Api/MusicPracticeTracker.Api.csproj", "backend/MusicPracticeTracker.Api/"]
RUN dotnet restore "backend/MusicPracticeTracker.Api/MusicPracticeTracker.Api.csproj"
COPY . .
RUN dotnet publish "backend/MusicPracticeTracker.Api/MusicPracticeTracker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MusicPracticeTracker.Api.dll"]
