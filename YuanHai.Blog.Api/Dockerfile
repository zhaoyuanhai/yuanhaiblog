#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["YuanHai.Blog.Api/YuanHai.Blog.Api.csproj", "YuanHai.Blog.Api/"]
RUN dotnet restore "YuanHai.Blog.Api/YuanHai.Blog.Api.csproj"
COPY . .
WORKDIR "/src/YuanHai.Blog.Api"
RUN dotnet build "YuanHai.Blog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YuanHai.Blog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YuanHai.Blog.Api.dll"]