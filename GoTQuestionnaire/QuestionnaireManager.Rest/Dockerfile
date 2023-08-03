FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Questionnaire.Rest/Questionnaire.Rest.csproj", "Questionnaire.Rest/"]
RUN dotnet restore "Questionnaire.Rest/Questionnaire.Rest.csproj"
COPY . .
WORKDIR "/src/Questionnaire.Rest"
RUN dotnet build "Questionnaire.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Questionnaire.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Questionnaire.Rest.dll"]
