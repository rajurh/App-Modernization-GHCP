#!/bin/bash

# Quick Deploy Script for eShopLite to Azure Container Apps

echo "========================================"
echo "eShopLite Azure Container Apps Deployment"
echo "========================================"
echo ""

# Ensure we're in the right directory (where azure.yaml is located)
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"
echo "Working directory: $(pwd)"
echo ""

# Check if azd is installed
echo "Checking for Azure Developer CLI (azd)..."
if ! command -v azd &> /dev/null; then
    echo "ERROR: Azure Developer CLI (azd) is not installed."
    echo "Install it from: https://aka.ms/install-azd"
    exit 1
fi
echo "? Azure Developer CLI found"
echo ""

# Check if Docker is running
echo "Checking for Docker..."
if ! command -v docker &> /dev/null; then
    echo "ERROR: Docker is not installed or not in PATH."
    echo "Install Docker Desktop from: https://www.docker.com/products/docker-desktop"
    exit 1
fi

if ! docker info &> /dev/null; then
    echo "ERROR: Docker is not running. Please start Docker Desktop."
    exit 1
fi
echo "? Docker is running"
echo ""

# Login to Azure
echo "Logging in to Azure..."
azd auth login
if [ $? -ne 0 ]; then
    echo "ERROR: Failed to login to Azure"
    exit 1
fi
echo "? Successfully logged in to Azure"
echo ""

# Deploy
echo "Starting deployment..."
echo "This will provision Azure resources and deploy your application."
echo "The process may take 5-10 minutes."
echo ""

azd up

if [ $? -eq 0 ]; then
    echo ""
    echo "========================================"
    echo "Deployment Successful! ??"
    echo "========================================"
    echo ""
    echo "Your application is now running in Azure Container Apps."
    echo "Check the output above for your application URL."
    echo ""
    echo "Useful commands:"
    echo "  azd monitor --overview  - View application dashboard"
    echo "  azd monitor --logs      - View application logs"
    echo "  azd deploy              - Redeploy after code changes"
    echo "  azd down                - Delete all Azure resources"
else
    echo ""
    echo "========================================"
    echo "Deployment Failed"
    echo "========================================"
    echo ""
    echo "Check the error messages above for details."
    echo "For help, see DEPLOYMENT.md"
    exit 1
fi
