# ReadMe

## Instructions

### Using appsettings
Each developer:
1. Create appsettings.Development.json locally.
2. Copy/paste contents from appsettings.Example.json into appsettings.Development.json.
3. Replace DIN_LOKALA_NYCKEL with actual api key.
4. appsettings.Development.json >> Properties >> Copy to Output Directory >> "Copy if newer".

## Examples of fetching data from appsettings
- string baseUrl = App.Config["ExternalApi:BaseUrl"];
- string apiKey  = App.Config["ExternalApi:ApiKey"];