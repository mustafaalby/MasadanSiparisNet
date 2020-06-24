"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SessionRequest").build();
var CostumerConnectionId;
var RestaurantConnectionId;
var RestaurantConnectionId;


connection.on("receive", function (tableId, CostumerConnectionId) {

    var htmlAccept = '<div class="card"><p class=" cardText">Masa ID :' + tableId + ' Oturma Isteği </p><div class="card-footer ' + tableId + '" id=' + CostumerConnectionId + '><button class="btn btn-outline-success float-left" id="AcceptButton" >Kabul Et</button><button id="DeclineButton" class="btn btn-outline-danger float-right">Reddet</button></div></div>'
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

    if (message == "Decline") {
        alert("Maalesef şu anda bu masada hizmet veremiyoruz")
    } else {
        var x = message.split("-");
        
        window.location.href = '/Session/SessionRequest/'+x[1];
    }


    
});

connection.on("UserConnected", function (ConnectionId) {
    CostumerConnectionId = ConnectionId;    
});



connection.start().then(function () {
    
    
}).catch(function (err) {
    
    return console.error(err.toString());
});


if (document.getElementsByClassName("sendButton") != null) {
    var buttons = document.getElementsByClassName('sendButton');
    console.log(buttons[1]);
    for (var i = 0; i < buttons.length; i++) {        
        buttons[i].addEventListener("click", function (event) {
            var tableId = this.parentElement.parentElement.id;
             
            connection.invoke("BroadcastMessage", tableId, CostumerConnectionId).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }
}



if (document.getElementById("temp") != null) {
    document.getElementById("temp").addEventListener("click", function (event) {
        if (event.target.id == "AcceptButton") {
            var conId = event.target.parentElement.id
            var tableId = event.target.parentElement.classList[1];
            
            connection.invoke("SendActivationLink", "Accept-" + tableId, conId);
            event.target.parentElement.parentElement.remove();
        } else if (event.target.id == "DeclineButton") {
            var conId = event.target.parentElement.id
            
            connection.invoke("SendActivationLink", "Decline", conId);
            event.target.parentElement.parentElement.remove();
        }
    
        
    });
}
