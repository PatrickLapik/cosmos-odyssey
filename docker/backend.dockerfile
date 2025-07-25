FROM mcr.microsoft.com/dotnet/aspnet:9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /backend

COPY /backend/* ./
