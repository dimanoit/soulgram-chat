FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Soulgram.Chat.Api/Soulgram.Chat.Api.csproj", "Soulgram.Chat.Api/"]
RUN dotnet restore "Soulgram.Chat.Api/Soulgram.Chat.Api.csproj"
COPY . .
WORKDIR "/src/Soulgram.Chat.Api"
RUN dotnet build "Soulgram.Chat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Soulgram.Chat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Soulgram.Chat.Api.dll"]
