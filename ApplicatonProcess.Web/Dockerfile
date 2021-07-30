#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Hahn.ApplicatonProcess.July2021.Data/Hahn.ApplicatonProcess.July2021.Data.csproj", "Hahn.ApplicatonProcess.July2021.Data/"]
RUN dotnet restore "Hahn.ApplicatonProcess.July2021.Data/Hahn.ApplicatonProcess.July2021.Data.csproj"
COPY . .
COPY ["Hahn.ApplicatonProcess.July2021.Domain/Hahn.ApplicatonProcess.July2021.Domain.csproj", "Hahn.ApplicatonProcess.July2021.Domain/"]
RUN dotnet restore "Hahn.ApplicatonProcess.July2021.Domain/Hahn.ApplicatonProcess.July2021.Domain.csproj"
COPY . .
COPY ["Hahn.ApplicatonProcess.July2021.Web.csproj", "Hahn.ApplicatonProcess.July2021.Web/"]
RUN dotnet restore "Hahn.ApplicatonProcess.July2021.Web/Hahn.ApplicatonProcess.July2021.Web.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicatonProcess.July2021.Web"
RUN dotnet build "Hahn.ApplicatonProcess.July2021.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicatonProcess.July2021.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.ApplicatonProcess.July2021.Web.dll"]