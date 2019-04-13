FROM mcr.microsoft.com/dotnet/core/sdk:2.2

EXPOSE 80
WORKDIR /app
COPY bin/Release/netcoreapp2.2/publish /app

ENTRYPOINT ["dotnet", "/app/michaelphillips.dev.dll"]
