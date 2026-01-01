# eShopLite - Blazor Store Application

A modern e-commerce store application built with Blazor Server and .NET 9, ready to deploy to Azure Container Apps.

## Features

- ??? Product catalog browsing
- ?? Store information management
- ?? SQLite database for data persistence
- ?? Blazor Server for interactive UI
- ?? Docker containerization
- ?? Azure Container Apps ready

## Quick Start - Local Development

### Prerequisites
- .NET 9 SDK
- Visual Studio 2022 or VS Code

### Run Locally

1. Clone the repository
2. Navigate to the project directory:
   ```bash
   cd src/eShopLite.StoreFx
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser to `https://localhost:5001` (or the URL shown in the console)

## Deploy to Azure Container Apps

This application is configured for easy deployment to Azure Container Apps using Azure Developer CLI (azd).

### Quick Deploy (Automated)

**Windows (PowerShell):**
```powershell
.\deploy.ps1
```

**Linux/macOS:**
```bash
chmod +x deploy.sh
./deploy.sh
```

### Manual Deploy

For detailed deployment instructions, see [DEPLOYMENT.md](DEPLOYMENT.md).

**Quick steps:**
```bash
# Install Azure Developer CLI
winget install microsoft.azd

# Login to Azure
azd auth login

# Deploy
azd up
```

## Project Structure

```
??? src/
?   ??? eShopLite.StoreFx/          # Main Blazor application
?       ??? Components/              # Razor components
?       ??? Data/                    # Database context
?       ??? Models/                  # Data models
?       ??? Services/                # Business logic
?       ??? Program.cs               # Application entry point
?       ??? Dockerfile               # Container image definition
??? infra/                           # Azure infrastructure as code
?   ??? main.bicep                   # Main Bicep template
?   ??? app/                         # Application-specific resources
?   ??? shared/                      # Shared infrastructure resources
??? azure.yaml                       # Azure Developer CLI configuration
??? deploy.ps1                       # Windows deployment script
??? deploy.sh                        # Linux/macOS deployment script
??? DEPLOYMENT.md                    # Detailed deployment guide
```

## Technology Stack

- **Framework**: .NET 9
- **UI**: Blazor Server
- **Database**: SQLite (with Entity Framework Core)
- **Containerization**: Docker
- **Cloud Platform**: Azure Container Apps
- **Infrastructure as Code**: Bicep
- **CI/CD**: Azure Developer CLI (azd)

## Azure Resources

When deployed, the application creates:
- Azure Container Apps (for running the application)
- Azure Container Registry (for storing Docker images)
- Log Analytics Workspace (for logging)
- Application Insights (for monitoring)
- Container Apps Environment (managed environment)

## Development

### Building the Docker Image Locally

```bash
cd src/eShopLite.StoreFx
docker build -t eshoplite:latest .
docker run -p 8080:8080 eshoplite:latest
```

Access the app at `http://localhost:8080`

### Database

The application uses SQLite for simplicity. The database is automatically created and seeded on startup.

## CI/CD with GitHub Actions

A GitHub Actions workflow is included at `.github/workflows/azure-deploy.yml` for automated deployments.

To enable:
1. Set up Azure authentication for GitHub Actions
2. Configure the required secrets and variables in your repository
3. Push to the `main` or `deploy-azure` branch

See the [GitHub Actions documentation](https://docs.github.com/en/actions) for more details.

## Useful Commands

```bash
# Deploy or update the application
azd deploy

# View application logs
azd monitor --logs

# View monitoring dashboard
azd monitor --overview

# Clean up all Azure resources
azd down
```

## Cost Optimization

The deployment uses:
- Container Apps: Consumption-based pricing (pay per use)
- Container Registry: Basic tier
- Log Analytics: Pay-as-you-go

Run `azd down` when not using the application to avoid unnecessary costs.

## Support

For issues or questions:
- Check [DEPLOYMENT.md](DEPLOYMENT.md) for deployment help
- Review Azure Developer CLI docs: https://learn.microsoft.com/azure/developer/azure-developer-cli/
- Review Azure Container Apps docs: https://learn.microsoft.com/azure/container-apps/

## License

Copyright © 2025
