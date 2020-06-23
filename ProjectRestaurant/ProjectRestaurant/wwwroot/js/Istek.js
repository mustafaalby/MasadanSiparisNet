"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SessionRequest").build();
var CostumerConnectionId;
var RestaurantConnectionId;
var RestaurantConnectionId;


connection.on("receive", function (message, CostumerConnectionId) {
    var node = document.createElement("LI");                 // Create a <li> node
    var textnode = document.createTextNode(message + " " + CostumerConnectionId);         // Create a text node
        
    node.appendChild(textnode);
    alert(CostumerConnectionId)
    var htmlAccept = '<div class="row" id='+CostumerConnectionId+'><button class="btn btn-success" id="AcceptButton" >Kabul Et</button><button id="DeclineButton" class="btn btn-danger">Reddet</button></div>'
    document.getElementById("RequestList").innerHTML += htmlAccept;
});

connection.on("Request", function (message)
{
    var node = document.createElement("LI");                 // Create a <li> node
    var textnode = document.createTextNode(message);         // Create a text node
    node.appendChild(textnode);
    document.getElementById("RequestList").append(node);
});

connection.on("SendLink", function (message) {
    alert(message);
});

connection.on("UserConnected", function (ConnectionId) {
    CostumerConnectionId = ConnectionId;    
});



connection.start().then(function () {
    
    
}).catch(function (err) {
    
    return console.error(err.toString());
});


if (document.getElementById("sendButton") != null)  {
    document.getElementById("sendButton").addEventListener("click", function (event) {

        var message = document.getElementById("messageInput").value;
        connection.invoke("BroadcastMessage", message, CostumerConnectionId).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

}


if (document.getElementById("temp") != null) {
    document.getElementById("temp").addEventListener("click", function (event) {
        if (event.target.id == "AcceptButton") {
            var conId = document.getElementById("AcceptButton").parentElement.id
            connection.invoke("SendActivationLink", "deneem", conId);
        }
        
    });
}
