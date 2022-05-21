#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Events/Events.csproj", "Events/"]
RUN dotnet restore "Events/Events.csproj"
COPY . .
WORKDIR "/src/Events"
RUN dotnet build "Events.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Events.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Events.dll"]