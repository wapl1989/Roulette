WilliamPL

configurar servidor Redis
Cadena de conexion ConnectionRedis en el appsettings.json

Para crear Ruleta POST http://localhost:58489/api/Roulette/CreateRoulette
Para Abrir Ruleta PUT http://localhost:58489/api/Roulette/OpenRoulette/{id}
Para Cerrar Ruleta PUT 

Para crear Apuesta POST http://localhost:58489/api/Bet/CreateBet/
Header: Key = idUser
Body:
{
	"Id":"0",
	"NumberBet":30,
	"ColorBet":0,
	"TypeBet":1,
	"ValueBet":5000,
	"IdRoulette":"45e79e21-693e-4923-8609-d2bdbbfd61e5",
	"EarnedMoney":0,
	"User":"idUser"
}

