FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/OzonEdu.MerchApi/OzonEdu.MerchApi.csproj", "src/OzonEdu.MerchApi/"]
RUN dotnet restore "src/OzonEdu.MerchApi/OzonEdu.MerchApi.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.MerchApi"
RUN dotnet build "OzonEdu.MerchApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.MerchApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 5000
EXPOSE 5001
EXPOSE 5004

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OzonEdu.MerchApi.dll"]