WilliamPL

Install Redis-x64-3.0.504 (https://github.com/MicrosoftArchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.msi)

To configure the Redis server locate the connection string "ConnectionRedis"
in the appsettings.json

To create a Roulette POST .../api/Roulette/CreateRoulette
To Open a Roulette PUT ..../api/Roulette/OpenRoulette/{id}
To Close a Roulette PUT .../api/Roulette/CloseRoulette/{id}
To List a Roulette GET .../api/Roulette/AllRoulette/

To create a Bet POST ../api/Bet/CreateBet/
Header: Key => idUser
Body:
{
	"Id":"0",
	"NumberBet":30,
	"TypeBet":1,
	"ValueBet":5000,
	"IdRoulette":"{Id de la Ruleta}",
	"User":"idUser"
}

