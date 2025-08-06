FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /backend

COPY /backend/ ./

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet restore

RUN dotnet tool install --global dotnet-ef
