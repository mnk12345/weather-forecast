# Weather Forecast Api

### To run from Visual Studio
Open solution and hit `F5`.
Open https://localhost:7172/swagger/ in browser.

### To run from Docker
`docker build -t wf .`
`docker run -d -p 7172:7172 --name wf wf`
Open https://localhost:7172/swagger/ in browser.

Example of API request:
https://localhost:7172/api/v1/forecast?City=Amsterdam&Date=2024-08-02

Replace OpenWeatherMap API key with correct one in *appsettings.json* on if my test key stopped working.