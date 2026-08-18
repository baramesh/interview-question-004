FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /source
COPY InterviewQuestion004.slnx dotnet-tools.json ./
COPY src/api/InterviewQuestion004.Api.csproj src/api/
RUN dotnet restore src/api/InterviewQuestion004.Api.csproj
COPY src/api/ src/api/
RUN dotnet publish src/api/InterviewQuestion004.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app
RUN apk add --no-cache krb5-libs
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Example.InterviewQuestion004.Api.dll"]
