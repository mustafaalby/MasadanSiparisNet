"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/oturumIstek").build();

connection.on("receive", function (message) {
    alert(message);
});

connection.start().then(function () {
   //alert("connection Successfull")
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
   // var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("BroadcastMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});