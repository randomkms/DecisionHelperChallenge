FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DecisionHelper.API/DecisionHelper.API.csproj", "DecisionHelper.API/"]
RUN dotnet restore "DecisionHelper.API/DecisionHelper.API.csproj"
COPY . .
WORKDIR "/src/DecisionHelper.API"
RUN dotnet build "DecisionHelper.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DecisionHelper.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DecisionHelper.API.dll"]