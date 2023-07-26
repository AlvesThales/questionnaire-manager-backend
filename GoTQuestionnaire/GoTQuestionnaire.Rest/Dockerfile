FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GoTQuestionnaire.Rest/GoTQuestionnaire.Rest.csproj", "GoTQuestionnaire.Rest/"]
RUN dotnet restore "GoTQuestionnaire.Rest/GoTQuestionnaire.Rest.csproj"
COPY . .
WORKDIR "/src/GoTQuestionnaire.Rest"
RUN dotnet build "GoTQuestionnaire.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoTQuestionnaire.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoTQuestionnaire.Rest.dll"]
