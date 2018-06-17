# BlackJack

Version for backend api, developed in .Net Core Web Api 2 using a simple the data send back to the client to persist state instead of using a cache server.

## Getting Started

Download the project and run in Visual Studio.

## Consuming

To play it's necessary to call the following endpoints:

1. post /api/game/initialize to initialize a new game
2. post /api/game/hit to perform a hit
3. post /api/game/stand to perform a stand so you pass the turn to the guest (server)

All the time the state is exchanged between the client and the server through a token attribute.
It's sent to the client via response payload and requested through a 'X-Token' header attribute

OBS: The file 'PostmanTest.json' can be imported in a Postman client to perform the tests.