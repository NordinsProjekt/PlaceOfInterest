# BackOffice Deployment Guide

## Overview
This guide will help you configure the BackOffice project to use the same Azure database as your API project.

## Prerequisites
- Azure SQL Database already set up (same as API project)
- Connection string available

## Local Setup Steps

### 1. Configure User Secrets
Run the setup script to configure the Azure connection string:
```powershell
.\setup-backoffice-secrets.ps1
```

### 2. Apply Migrations (Optional - Automatic on startup)
If you want to manually apply migrations:
```powershell
.\migrate-database.ps1
```

### 3. Test Locally
1. Start the BackOffice project
2. Check console output for migration success messages
3. Verify it connects to the Azure database

## Azure Deployment Configuration

### In Azure App Service Configuration:
Add the following application settings:

**Connection String:**
- **Name:** `DefaultConnection`
- **Value:** `Server=tcp:placeofinterestserverdb.database.windows.net,1433;Initial Catalog=poi_db;Persist Security Info=False;User ID=admindbWorker;Password=WPEUzZWNZ9LvKPDXHs;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;`
- **Type:** SQLServer

## Key Changes Made

1. **Fixed MARS Setting**: Changed `MultipleActiveResultSets=true` to `false` to match API project
2. **Added Automatic Migration**: Both Identity and PlaceOfInterest databases migrate automatically on startup
3. **Added Connection Logging**: See which connection string is being used in logs
4. **Shared Database**: BackOffice now uses the same `poi_db` database as the API

## Database Tables

The BackOffice project uses two sets of tables in the same database:
- **Identity Tables**: For user authentication (AspNetUsers, AspNetRoles, etc.)
- **PlaceOfInterest Tables**: Shared with API (PlaceOfInterests, StartLocations, EndLocations)

## Verification

After deployment, check:
1. Application logs show successful migration messages
2. Database contains both Identity and PlaceOfInterest tables
3. BackOffice can display and manage places of interest
4. Authentication works properly

## Troubleshooting

If you encounter issues:
1. Check Azure App Service logs for migration errors
2. Verify connection string is correctly set in Azure configuration
3. Ensure database firewall allows Azure services
4. Check that the database user has sufficient permissions for DDL operations (for migrations)