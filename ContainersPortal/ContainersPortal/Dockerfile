FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

RUN apt-get update \
    && apt-get install -y sqlite3 libsqlite3-dev openssh-client sshpass putty-tools \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir /Db \
    && mkdir /Docker

EXPOSE 5161 

ENTRYPOINT ["dotnet", "ContainersPortal.dll"]