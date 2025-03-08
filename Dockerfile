# Use official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all files and build the application
COPY . ./
RUN dotnet publish -c Release -o /out

# Use a runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /out .

# Expose port for the application
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ElectionSystem.dll"]