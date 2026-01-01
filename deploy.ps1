# Quick Deploy Script for eShopLite to Azure Container Apps

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "eShopLite Azure Container Apps Deployment" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Ensure we're in the right directory (where azure.yaml is located)
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptPath
Write-Host "Working directory: $(Get-Location)" -ForegroundColor Gray
Write-Host ""

# Check if azd is installed
Write-Host "Checking for Azure Developer CLI (azd)..." -ForegroundColor Yellow
$azdExists = Get-Command azd -ErrorAction SilentlyContinue
if (-not $azdExists) {
    Write-Host "ERROR: Azure Developer CLI (azd) is not installed." -ForegroundColor Red
    Write-Host "Install it with: winget install microsoft.azd" -ForegroundColor Yellow
    exit 1
}
Write-Host "? Azure Developer CLI found" -ForegroundColor Green
Write-Host ""

# Check if Docker is running
Write-Host "Checking for Docker..." -ForegroundColor Yellow
$dockerExists = Get-Command docker -ErrorAction SilentlyContinue
if (-not $dockerExists) {
    Write-Host "ERROR: Docker is not installed or not in PATH." -ForegroundColor Red
    Write-Host "Install Docker Desktop from: https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
    exit 1
}

try {
    docker info | Out-Null
    Write-Host "? Docker is running" -ForegroundColor Green
} catch {
    Write-Host "ERROR: Docker is not running. Please start Docker Desktop." -ForegroundColor Red
    exit 1
}
Write-Host ""

# Login to Azure
Write-Host "Logging in to Azure..." -ForegroundColor Yellow
azd auth login
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Failed to login to Azure" -ForegroundColor Red
    exit 1
}
Write-Host "? Successfully logged in to Azure" -ForegroundColor Green
Write-Host ""

# Deploy
Write-Host "Starting deployment..." -ForegroundColor Yellow
Write-Host "This will provision Azure resources and deploy your application." -ForegroundColor Yellow
Write-Host "The process may take 5-10 minutes." -ForegroundColor Yellow
Write-Host ""

azd up

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "Deployment Successful! ??" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Your application is now running in Azure Container Apps." -ForegroundColor Green
    Write-Host "Check the output above for your application URL." -ForegroundColor Green
    Write-Host ""
    Write-Host "Useful commands:" -ForegroundColor Cyan
    Write-Host "  azd monitor --overview  - View application dashboard" -ForegroundColor White
    Write-Host "  azd monitor --logs      - View application logs" -ForegroundColor White
    Write-Host "  azd deploy              - Redeploy after code changes" -ForegroundColor White
    Write-Host "  azd down                - Delete all Azure resources" -ForegroundColor White
} else {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "Deployment Failed" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Check the error messages above for details." -ForegroundColor Yellow
    Write-Host "For help, see DEPLOYMENT.md" -ForegroundColor Yellow
    exit 1
}
