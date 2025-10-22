# Fixed Port Configuration

## Port Assignments

### Client (Blazor WebAssembly) - ClientWebassembly
- **HTTPS**: `https://localhost:7240`
- **HTTP**: `http://localhost:5176`

### API - PlaceOfInterest.ClientAPI
- **HTTPS**: `https://localhost:7001`
- **HTTP**: `http://localhost:5001`

## Configuration Files Updated

### 1. PlaceOfInterest.ClientAPI/Properties/launchSettings.json
- Changed API ports to 7001 (HTTPS) and 5001 (HTTP)

### 2. PlaceOfInterest.ClientAPI/Program.cs
- Updated CORS configuration to use fixed client ports:
  - `https://localhost:7240`
  - `http://localhost:5176`

### 3. ClientWebassembly/wwwroot/appsettings.json
- Already configured to use API at `https://localhost:7001`

### 4. ClientWebassembly/wwwroot/appsettings.Development.json
- Configured to use API at `https://localhost:7001`

## Running the Applications

### Start the API
```bash
cd PlaceOfInterest.ClientAPI
dotnet run --launch-profile https
```
The API will run on: `https://localhost:7001`

### Start the Client
```bash
cd ClientWebassembly
dotnet run --launch-profile https
```
The client will run on: `https://localhost:7240`

## Benefits
- ? No more changing CORS origins
- ? Consistent ports across team members
- ? Easy to remember ports (7001 for API, 7240 for client)
- ? Configuration matches across all environments
