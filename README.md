# BlackJack

Backend api version of a BlackJack game, developed in .Net Core Web Api 2.

## Getting Started

1. Download the project and run in Visual Studio.
2. Import the 'PostmanTest.json' file in a Postman client to perform the tests.

## Consuming

To play the game against the api it's necessary to call the following endpoints in the given order:

1. post /api/game/initialize to initialize a new game
2. post /api/game/hit to perform a hit for the Guest (peek up a new card)
3. post /api/game/stand to perform a stand so the Guest pass the turn to the Dealer (server)

The state of the game is persisted in a encrypted token that is sent back and forth in the comunication with the client.
It's returned to the client every call in the response payload and must be given back to the server through a 'X-Token' header attribute.

OBS: The token approach was implemented to avoid the need to use a cache server (i.e. Redis) to control the game state.
