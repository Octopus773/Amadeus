<p align="center">
    <img src="./assets/amadeus_logo.png" alt="Amadeus Logo" style="width: 400px">
</p>

# Amadeus
Amadeus is the a 3rd year Epitech project Dashboard.

Made with love by [@Clément Le Bihan](https://github.com/Octopus773) & [@Zoe Roux](https://github.com/AnonymusRaccoon)

Main repository: https://github.com/Octopus773/Amadeus

## In pictures
<img src="./assets/available_soon.png" alt="Available Soon" style="width: 400px;">

## Getting started
- Our hosted demo
- On your device
- With docker

## Services & Widget

3 services are available on Amadeus:
 - A weather service,
 - A covid service, and
 - An anime service (anilist)

Every service needs an API key to work properly. Api keys can be setup in the configuration file `appsettings.json` or by
specifing those environement variables:
 - WEATHERCONFIGURATION__APIKEY
 - COVIDCONFIGURATION__APIKEY
 - ANILISTOPTIONS__CLIENTID
 - ANILISTOPTIONS__CLIENTSECRET

The weather api key needs to be generated from [here](https://www.weatherapi.com/my/).
The covid api key from [here](https://rapidapi.com/Gramzivi/api/covid-19-data/).
And the anilist id and secrets from [here](https://anilist.co/home). During the configuration of this client, you need
to specify this url as the redirect url: http://localhost:8081/login/anilist


## Documentation
- User documentation
- Technical documentation

## Technical stack
- Backend: C#
- Frontend: Angular