# #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Championship.API/Championship.API.csproj", "Championship.API/"]
COPY ["Championship.Infrastructure/Championship.Infrastructure.csproj", "Championship.Infrastructure/"]
COPY ["Championship.Domain/Championship.Domain.csproj", "Championship.Domain/"]
COPY ["Championship.Application/Championship.Application.csproj", "Championship.Application/"]
RUN dotnet restore "Championship.API/Championship.API.csproj"
COPY . .
WORKDIR "/src/Championship.API"
RUN dotnet build "Championship.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Championship.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Championship.API.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Championship.API.dll