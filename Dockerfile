FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY bin/Release/net5.0/publish/ myWebAPI/
WORKDIR /myWebAPI
CMD ASPNETCORE_URLS=http://*:$PORT dotnet myWebAPI.dll