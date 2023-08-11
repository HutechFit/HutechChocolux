FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt-get update && apt-get install -y clang
WORKDIR /src
COPY ["Hutech.Presentation/Hutech.Presentation.csproj", "Hutech.Presentation/"]
COPY ["Hutech.Application/Hutech.Application.csproj", "Hutech.Application/"]
COPY ["Hutech.Infrastructure/Hutech.Infrastructure.csproj", "Hutech.Infrastructure/"]
COPY ["Hutech.Domain/Hutech.Domain.csproj", "Hutech.Domain/"]
RUN dotnet restore "Hutech.Presentation/Hutech.Presentation.csproj"
COPY . .
WORKDIR "/src/Hutech.Presentation"
RUN dotnet build "Hutech.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hutech.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hutech.Presentation.dll"]