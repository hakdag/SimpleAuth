# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "SimpleAuth.Api.dll", "--configuration", "Release"]