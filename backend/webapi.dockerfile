FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_ConnectionStrings__Default=Host=postgres;Database=TreniniDb;Username=tdbuser;Password=tdbpass
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG Configuration=Release
WORKDIR /
COPY . .
#COPY ["TreniniDotNet.sln", "./"]
#COPY ["Common/Common.csproj", "./Common"]
#COPY ["src/SharedKernel/SharedKernel.csproj", "./SharedKernel"]
#COPY ["Domain/Domain.csproj", "./Domain"]
#COPY ["Application/Application.csproj", "./Application"]
#COPY ["Infrastructure/Infrastructure.csproj", "./Infrastructure"]
#COPY ["Web/Web.csproj", "./Web"]
#COPY ["GrpcServices/GrpcServices.csproj", "./GrpcServices"]
#RUN dotnet restore "TreniniDotNet.sln"
RUN pwd
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TreniniDotNet.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]