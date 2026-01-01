# Deploy eShopLite to Azure Container Apps

This guide will help you deploy the eShopLite Blazor application to Azure Container Apps using Azure Developer CLI (azd).

## Prerequisites

1. **Install Azure Developer CLI (azd)**
   - Windows: `winget install microsoft.azd`
   - macOS: `brew tap azure/azd && brew install azd`
   - Linux: `curl -fsSL https://aka.ms/install-azd.sh | bash`

2. **Install Docker Desktop**
   - Download from: https://www.docker.com/products/docker-desktop

3. **Azure Subscription**
   - Make sure you have an active Azure subscription

## Deployment Steps

### 1. Login to Azure

```bash
azd auth login
```

This will open a browser window for you to authenticate with Azure.

### 2. Deploy Using Automated Script

**Windows (PowerShell):**
```powershell
.\deploy.ps1
```

**Linux/macOS:**
```bash
chmod +x deploy.sh
./deploy.sh
```

The script will guide you through the deployment process.

### 3. Manual Deployment (Alternative)

If you prefer to run commands manually:

```bash
azd up
```

When prompted:
- **Environment name**: Choose a name (e.g., `dev`, `prod`)
- **Azure subscription**: Select your subscription from the list
- **Azure location**: Choose a region (e.g., `eastus`, `westus2`, `centralus`)

This command will:
- Provision all Azure resources (Container Registry, Container Apps Environment, Container App, Log Analytics, Application Insights)
- Build the Docker container
- Push the container to Azure Container Registry
- Deploy the container to Azure Container Apps

The process may take 5-10 minutes.

### 4. Access Your Application

After deployment completes, azd will output the URL of your deployed application:
```
SERVICE_WEB_URI: https://ca-web-xxxxx.azurecontainerapps.io
```

Open this URL in your browser to access your application.

## Managing Your Deployment

### View Application Logs

```bash
azd monitor --logs
```

### View Monitoring Dashboard

```bash
azd monitor --overview
```

### Redeploy After Code Changes

```bash
azd deploy
```

### Update Infrastructure

If you modify Bicep files:

```bash
azd provision
```

### Clean Up Resources

To delete all Azure resources created by this deployment:

```bash
azd down
```

## Architecture

The deployment creates the following Azure resources:

- **Resource Group**: Contains all resources (e.g., `rg-dev`)
- **Container Registry**: Stores your Docker images (e.g., `crxxxxxx`)
- **Container Apps Environment**: Managed environment for your containers (e.g., `cae-xxxxx`)
- **Container App**: Runs your Blazor application (e.g., `ca-web-xxxxx`)
- **Log Analytics Workspace**: Collects logs and metrics (e.g., `log-xxxxx`)
- **Application Insights**: Provides application monitoring (e.g., `appi-xxxxx`)

## Configuration

The deployment is configured through:
- `azure.yaml`: Defines the service and host type
- `infra/main.bicep`: Main infrastructure template
- `infra/main.parameters.json`: Parameter mapping for azd
- `infra/app/web.bicep`: Container App configuration
- `src/eShopLite.StoreFx/Dockerfile`: Container image definition

## Troubleshooting

### Error: "The 'location' property must be specified"

If you see this error:
```
ERROR CODE: InvalidDeployment
The 'location' property must be specified for 'xxx'. Please see https://aka.ms/arm-deployment-subscription for usage details.
```

**Solution**: Ensure you're running `azd up` from the repository root directory (where `azure.yaml` is located). The updated deployment scripts automatically handle this.

### Docker Not Running

```
ERROR: Docker is not running. Please start Docker Desktop.
```

**Solution**: Start Docker Desktop and wait for it to fully initialize before running the deployment script.

### Azure Login Issues

```
ERROR: Failed to login to Azure
```

**Solution**: 
- Make sure you have an active Azure subscription
- Try logging out and back in: `azd auth logout` then `azd auth login`
- Check if you can access the Azure portal

### Container Build Failures

If the Docker build fails:

1. Test the build locally:
   ```bash
   cd src/eShopLite.StoreFx
   docker build -t test:latest .
   ```

2. Check Docker logs for specific errors

3. Ensure all NuGet packages are accessible

### View Detailed Logs

For detailed deployment logs:
```bash
azd provision --debug
```

For detailed deployment logs:
```bash
azd deploy --debug
```

### Reset Environment

If you need to start fresh:

```bash
azd down --force --purge
azd up
```

### Check Resource Status

View all resources in the Azure Portal:
1. Go to https://portal.azure.com
2. Search for your resource group (e.g., `rg-dev`)
3. View all deployed resources and their status

## Environment Variables

The application is configured with:
- `ASPNETCORE_ENVIRONMENT`: Set to "Production"
- `ASPNETCORE_HTTP_PORTS`: Set to "8080"

Additional environment variables can be added in `infra/app/web.bicep` under the `env` section.

## Scaling

The Container App is configured to:
- Minimum replicas: 1
- Maximum replicas: 10
- Scale based on HTTP requests (100 concurrent requests per instance)
- CPU: 0.5 cores per instance
- Memory: 1.0 Gi per instance

Modify these settings in `infra/app/web.bicep` if needed.

## Cost Optimization

To minimize costs during development:
- The Container Apps Environment uses consumption-based pricing (pay only for what you use)
- Container Registry uses Basic SKU ($5/month)
- Log Analytics uses pay-as-you-go pricing
- Resources are only charged when running

**To stop incurring charges:**
```bash
azd down
```

This will delete all resources. You can redeploy anytime with `azd up`.

## Advanced Configuration

### Custom Domain

To add a custom domain, see: https://learn.microsoft.com/azure/container-apps/custom-domains

### HTTPS Certificates

Azure Container Apps automatically provides HTTPS with managed certificates.

### Database Persistence

The current deployment uses SQLite with ephemeral storage. For production:
1. Consider using Azure SQL Database or Cosmos DB
2. Add the database to `infra/main.bicep`
3. Update connection strings in `infra/app/web.bicep`

## Support Resources

- **Azure Developer CLI**: https://learn.microsoft.com/azure/developer/azure-developer-cli/
- **Azure Container Apps**: https://learn.microsoft.com/azure/container-apps/
- **Bicep Documentation**: https://learn.microsoft.com/azure/azure-resource-manager/bicep/
- **Docker Documentation**: https://docs.docker.com/

## Next Steps

After successful deployment:

1. ? Access your application at the provided URL
2. ? Set up monitoring alerts in Azure Portal
3. ? Configure custom domain (optional)
4. ? Set up CI/CD with GitHub Actions (see `.github/workflows/azure-deploy.yml`)
5. ? Review and adjust scaling settings based on usage
