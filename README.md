# pokemon-webapi
Sample Web API making a connection to pokeapi.co

## How to run it

The following instructions assume your OS is Windows, Linux or MacOS.
For windows users, instructions must be executed in git bash. If using cmd, please replace '/' with '\\' in file paths.

Make sure you have the follwoing installed:
- a recent version of git (>2.0.0)
- a recent version of docker (tested with Docker 20.10.8)
- dotnet SDK installed (tested with 5.0.401)

### Clone the repository:
```
git clone https://github.com/davidp443/pokemon-webapi
```

### Run the Web API using the development server

From the PokemonWebApi directory, execute:
```
docker run
```
This will execute a Kestrel development server. 

Using your browser (or curl/ wget), head to https://localhost:5001/pokemon/mewtwo or https://localhost:5001/pokemon/translated/mewtwo 
Your browser should display a security warning. Add a security exception for the self-signed certificate. 

### Run the Web API using docker

Alternately, the Web API can be ran using docker.

Build the image from the root of the git repository:
```
docker build -t pokemonwebapi -f PokemonWebApi/Docker file
```

Create and run the conatainer:
```
docker run -p 5000:80 pokemonwebapi
```
Using your browser, head to http://localhost:5000/pokemon/mewtwo or https://localhost:5000/pokemon/translated/mewtwo 

### Run the tests

From the PokemonWebApi.Tests directory, execute
```
dotnet test
```

## Anything I'd do differently for a production API

### Add a unit test layer
The tests are currently testing the almost all the service logic. The HttpClient is stubbed, but many classes are tested at once.
For a production system, we should add unit tests to cover more edge cases and error paths (different error codes for example, invalid response from any of the service being consumed, invalid charaters in Pokemon names...)

### Add a caching layer
Add a caching layer in front of api.funtranslations.com and pokeapi.co

### Use a premium service 
Use a paid plan for api.funtranslations.com and pokeapi.co, because free plan are rate limited
