# Use the official ASP.NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET Core SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project files to the container
COPY ["MyBlazorApp.csproj", "./"]
RUN dotnet restore "MyBlazorApp.csproj"

# Copy the rest of the application code to the container
COPY . .

# Build the application
RUN dotnet build "MyBlazorApp.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MyBlazorApp.csproj" -c Release -o /app/publish

# Use the base image and copy the published application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBlazorApp.dll"]
