# Script to manually apply EF Core migrations
# Run this from the solution directory

Write-Host "Applying EF Core migrations..." -ForegroundColor Green

Write-Host "Migrating API database..." -ForegroundColor Yellow
# Navigate to the API project directory
Set-Location "PlaceOfInterest.ClientAPI"

# Apply migrations for API
dotnet ef database update --verbose

# Return to solution directory
Set-Location ".."

Write-Host "Migrating BackOffice databases..." -ForegroundColor Yellow
# Navigate to the BackOffice project directory
Set-Location "PlaceOfInterest.BackOffice"

# Apply Identity migrations for BackOffice
dotnet ef database update --context ApplicationDbContext --verbose

# Apply PlaceOfInterest migrations for BackOffice (using the EFCore project)
dotnet ef database update --project "../PlaceOfInterest.EFCore" --context PlaceOfInterestContext --verbose

# Return to original directory
Set-Location ".."

Write-Host "All migrations completed!" -ForegroundColor Green