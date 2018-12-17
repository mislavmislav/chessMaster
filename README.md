# ChessMasted Dockered App

ChessMaster je testna aplikacija, za research i učenje docker engine-a, te integracjie raznih alata u kombinaciji. 

## Features

  - .NET Core Web Api
  - .NET Framework libraries
  - Redis as permament storage
  - Docker support for containerization

## Tech

## Env

  - Linux na Oracle Virtualbox sa docker engine-om
  - 192.168.32.29
  - ftp sa WinScp
  - ssh sa SecureCRT root/user
  - /home/mislav/chess/chessMaster/


## Automated Build

Na dockerhubu connectan github repo, te podešen Automated Deploy.

https://github.com/mislavmislav/chessMaster

Rezultat je automatski build i objava image-a u mom dockerhub repository-u. 

## Manual Build

### Web Api

Kopiran sadržaj kompletnog solutiona na /home/mislav/chess/chessMaster/
Komande :

  1)	cd /home/mislav/chess/chessMaster/Api
  2)    docker build -t chessimage .
  3)    docker tag chessimage mislavmislav/test:chessimage
  4)    docker push mislavmislav/test:chessimage


  - Dockerfile
```sh
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["RClient/RClient.csproj", "RClient/"]
COPY ["ChessMaster/ChessMaster.csproj", "ChessMaster/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
RUN dotnet build "Api/Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.dll"]
```

  - docker-compose.yml
```sh
version: '3.4'

services:

  rediscache:
    image: redis:alpine
    expose:
      - "6379"
    ports:
      - "6379:6379"
    container_name: rediscache

  chessmaster:
    image: mislavmislav/test:chessmaster
    environment:
      - RedisHost=rediscache:6379
    build:
      context: .
      dockerfile: Dockerfile
    expose:
      - "80"
      - "443"
    ports:
      - "5101:80"
    depends_on:
      - rediscache
    container_name: chessmaster
```

##  Publish & Installation

# TODO

### Persist redis date
### Extract redis container name to config
###	Add some frontend solution

