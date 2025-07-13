FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Presentation/TicTacToe.WebAPI/TicTacToe.WebAPI.csproj", "Presentation/TicTacToe.WebAPI/"]
COPY ["Infrastructure/TicTacToe.Persistence/TicTacToe.Persistence.csproj", "Infrastructure/TicTacToe.Persistence/"]
COPY ["Core/TicTacToe.Domain/TicTacToe.Domain.csproj", "Core/TicTacToe.Domain/"]
COPY ["Core/TicTacToe.Application/TicTacToe.Application.csproj", "Core/TicTacToe.Application/"]
RUN dotnet restore "Presentation/TicTacToe.WebAPI/TicTacToe.WebAPI.csproj"

COPY . .
WORKDIR "/src/Presentation/TicTacToe.WebAPI"
RUN dotnet build "TicTacToe.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToe.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.WebAPI.dll"]