"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SessionRequest").build();
var CostumerConnectionId;
connection.on("UserConnected", function (ConnectionId) {
    CostumerConnectionId = ConnectionId;
});


