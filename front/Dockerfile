FROM mcr.microsoft.com/dotnet/core/sdk:3.1

COPY ca-bundle.pem /usr/local/share/ca-certificates/ca-bundle.crt
RUN update-ca-certificates

WORKDIR /usr/src/app

COPY . ./



ENV ASPNETCORE_URLS=http://+:5000


CMD dotnet run