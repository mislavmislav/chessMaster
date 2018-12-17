FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["RClient/RClient.csproj", "RClient/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
RUN dotnet build "Api/Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.dll"]