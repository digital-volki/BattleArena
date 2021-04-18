#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine-arm64v8 AS base
WORKDIR /app
#RUN cd /usr/lib && ln -s libgdiplus.so gdiplus.dll && ln -s libhostpolicy.so /app/libhostpolicy.so 
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BattleArena/BattleArena.csproj", "BattleArena/"]
COPY ["BattleArena.Application/BattleArena.Application.csproj", "BattleArena.Application/"]
COPY ["BattleArena.Application.Domain/BattleArena.Application.Domain.csproj", "BattleArena.Application.Domain/"]
COPY ["BattleArena.Application.Services/BattleArena.Application.Services.csproj", "BattleArena.Application.Services/"]
COPY ["BattleArena.Common/BattleArena.Common.csproj", "BattleArena.Common/"]
COPY ["BattleArena.Core.PostgreSQL/BattleArena.Core.PostgreSQL.csproj", "BattleArena.Core.PostgreSQL/"]
RUN dotnet restore "BattleArena/BattleArena.csproj" -r linux-arm64
COPY . .
WORKDIR "/src/BattleArena"
RUN ASPNETCORE_ENVIRONMENT="Production" \
	dotnet build "BattleArena.csproj" -c Release -o /app/build -r linux-musl-arm64

FROM build AS publish
RUN dotnet publish "BattleArena.csproj" -c Release -o /app/publish -r linux-musl-arm64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apk add libgdiplus --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted
ENTRYPOINT ["dotnet", "BattleArena.dll"]
