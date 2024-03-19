FROM hub.nexdev.net/base/dotnet/core/sdk:8.0-alpine AS builder
WORKDIR /src

COPY ./Fanex.NonSport.Service.ID41/Fanex.NonSport.Business.ID41 ./Fanex.NonSport.Business.ID41
COPY ./Fanex.NonSport.Service.ID41/Fanex.NonSport.Service.ID41/Fanex.NonSport.Service.ID41.csproj ./Fanex.NonSport.Service.ID41/Fanex.NonSport.Service.ID41.csproj

RUN ls /src -l
WORKDIR /src/Fanex.NonSport.Service.ID41
RUN dotnet nuget add source https://nuget.nexdev.net/api/v2
RUN --mount=type=cache,target=/root/.nuget/packages 
RUN dotnet restore -v n

COPY ./Fanex.NonSport.Service.ID41/Fanex.NonSport.Service.ID41 .

RUN --mount=type=cache,target=/root/.nuget/packages 
RUN dotnet publish -r linux-x64 --self-contained false -c Release -o /app

FROM hub.nexdev.net/base/dotnet/core/aspnet:8.0-nap-mail-core

# Don’t run as root
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app && chown -R dotnetuser /tmp
USER dotnetuser
#

ENV TZ="America/La_Paz"
ENV COMPlus_EnableDiagnostics=0

WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "Fanex.NonSport.Service.ID41.dll"]
