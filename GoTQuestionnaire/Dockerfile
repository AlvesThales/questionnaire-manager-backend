FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["QuestionnaireManager.Rest/QuestionnaireManager.Rest.csproj", "QuestionnaireManager.Rest/"]
RUN dotnet restore "QuestionnaireManager.Rest/QuestionnaireManager.Rest.csproj"
COPY . .
WORKDIR "/src/QuestionnaireManager.Rest"
RUN dotnet build "QuestionnaireManager.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuestionnaireManager.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuestionnaireManager.Rest.dll"]
