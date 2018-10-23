FROM microsoft/aspnetcore:1.1 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:1.1 AS build
WORKDIR /src
COPY ["SMP.MVC/SMP.MVC.csproj", "SMP.MVC/"]
COPY ["SMP.Models/SMP.Models.csproj", "SMP.Models/"]
COPY ["SMP.Service/SMP.Service.csproj", "SMP.Service/"]
COPY ["SMP.DAL/SMP.DAL.csproj", "SMP.DAL/"]
RUN dotnet restore "SMP.MVC/SMP.MVC.csproj"
COPY . .
WORKDIR "/src/SMP.MVC"
RUN dotnet build "SMP.MVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SMP.MVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SMP.MVC.dll"]