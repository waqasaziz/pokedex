# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY src/WebAPI/*.csproj ./WebAPI/
COPY src/Domain/*.csproj ./Domain/	

WORKDIR /source/WebAPI
RUN dotnet restore


WORKDIR /source

# copy everything else and build app
COPY src/WebAPI/. ./WebAPI/
COPY src/Domain/. ./Domain/

WORKDIR /source/WebAPI
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebAPI.dll"]


