FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7045/tcp
EXPOSE 5045/tcp
COPY ./publish /app/
COPY ./aspnetapp.pfx /https/aspnetapp.pfx

ENTRYPOINT ["dotnet", "WebApi.dll"]
