# Script to set user secrets for BackOffice project
# Run this from the solution directory

Write-Host "Setting user secrets for BackOffice project..." -ForegroundColor Green

# Navigate to the BackOffice project directory
Set-Location "PlaceOfInterest.BackOffice"

# Set the Azure connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=tcp:placeofinterestserverdb.database.windows.net,1433;Initial Catalog=poi_db;Persist Security Info=False;User ID=admindbWorker;Password=WPEUzZWNZ9LvKPDXHs;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Return to original directory
Set-Location ".."

Write-Host "User secrets configured successfully!" -ForegroundColor Green
Write-Host "The BackOffice project will now use the same Azure database as the API." -ForegroundColor Yellow