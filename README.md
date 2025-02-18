# ShoperBackend

This is the backend API for the **Shoper App** project, built with ASP.NET Core and .NET 9. It integrates with the Shoper.pl API to retrieve product data and provide a JSON endpoint for product suggestions.

## Features

- **Shoper API Integration:** Connects to Shoper.pl using Bearer Token authentication.
- **Autocomplete Support:** Accepts a search query (minimum three characters) to retrieve product suggestions.
- **Clean Architecture:** Organized into Controllers, Services, and Models.
- **Testing:** Includes both unit and integration tests to ensure reliability.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A valid Shoper.pl test store and API key.

### Environment Variables

Set the following environment variables (do not store these in the repository):

- **SHOPER__SHOPURL**: Base URL for the Shoper API (e.g., `https://yourtestshop.shoper.pl`)
- **SHOPER__APITOKEN**: Your Shoper API Bearer token

### Running the Application

1. **Open the Solution:**  
   Open the solution in Visual Studio and set the backend project as the startup project.

2. **Configure Launch Settings (Optional):**  
   Update the `launchSettings.json` to use your preferred ports (default examples: HTTP on `http://localhost:5000` and HTTPS on `https://localhost:5001`).

3. **Run the Application:**  
   Press F5 to run in Debug mode or Ctrl+F5 to run without debugging.

### API Endpoints

- **GET** `/api/products/search?query={searchTerm}`  
  Retrieves an array of products matching the search term.  
  **Note:** The search term must be at least 3 characters long.

### Testing

- **Unit Tests:** Located in `ShoperBackend.Tests/Services`
- **Integration Tests:** Located in `ShoperBackend.Tests/Controllers`
- **Run Tests:** Use Visual Studio Test Explorer or run via CLI:
  ```bash
  dotnet test
  ```

### Configuration and Architecture

- **Dependency Injection:** Utilizes ASP.NET Core DI and HttpClientFactory for API calls.
- **JSON Mapping:** Custom mapping using `JsonSerializerOptions` defined in `ProductServiceHelpers`.
- **Environment-Specific Settings:** Uses environment variables to keep sensitive data out of source control.

### Deployment

For production, ensure environment variables are correctly configured on the host system to secure API credentials.

### Contributing

Contributions are welcome! Please submit issues or pull requests for improvements.