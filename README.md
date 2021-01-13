# TicTacToe

Тестировочный аккаунт
test1@gmail.com/123456

Для просмотра доступный контроллеров предусмотрен swagger. Kestrel - localhost:5000 / ISS - localhost:port

Для того что начать игру требуется

1. Создать пользователей
2. Авторизироваться
3. Создать игру
4. Отправить ход игрока 1
5. Отправить ход игрока 2. Если игра с ботом то ничего отправлять не требуется
6. Посмотреть ход предыдущего игрока.
7. Повторять предыдущие пункты до завершения игры

Разница между играми с ботом и с пользователем
Бот
Создание игры имеет формат
{
    "gameId": "Guid",
    "player1Id": "Guid",
    "player2Id": null,
    "isPlayer2Bot": true,
    "isGameFinished": false
}
Ход
{
  "gameId": "Guid",
  "playerId": "Guid",
  "isBot": false,
  "xAxis": 0,
  "yAxis": 0,
  "moveDate": "2021-01-13T10:30:10.254Z"
}
Ход бота выполняется автоматически

Пользователь
Создание игры имеет формат
{
    "gameId": "Guid",
    "player1Id": "Guid",
    "player2Id": "Guid",
    "isPlayer2Bot": false,
    "isGameFinished": false
}
Ход
{
  "gameId": "Guid",
  "playerId": "Guid",
  "isBot": false,
  "xAxis": 0,
  "yAxis": 0,
  "moveDate": "2021-01-13T10:30:10.254Z"
}

Пример отправки запроса через curl
curl -X GET "https://localhost:44366/api/Games" -H "accept: */*" -H "Authorization: Bearer token"
