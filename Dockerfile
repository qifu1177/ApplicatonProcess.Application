#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ApplicatonProcess.Data/ApplicatonProcess.Data.csproj", "ApplicatonProcess.Data/"]
RUN dotnet restore "ApplicatonProcess.Data/ApplicatonProcess.Data.csproj"
COPY . .
COPY ["ApplicatonProcess.Domain/ApplicatonProcess.Domain.csproj", "ApplicatonProcess.Domain/"]
RUN dotnet restore "ApplicatonProcess.Domain/ApplicatonProcess.Domain.csproj"
COPY . .
COPY ["ApplicatonProcess.Web/ApplicatonProcess.Web.csproj", "ApplicatonProcess.Web/"]
RUN dotnet restore "ApplicatonProcess.Web/ApplicatonProcess.Web.csproj"
COPY . .
WORKDIR "/src/ApplicatonProcess.Web"
RUN dotnet build "ApplicatonProcess.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApplicatonProcess.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ApplicatonProcess.Web.dll"]