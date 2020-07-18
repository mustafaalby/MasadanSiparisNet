"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SessionRequest").build();
var CostumerConnectionId;
var RestaurantConnectionId;
var RestaurantConnectionId;


connection.on("receive", function (tableId, CostumerConnectionId) {
    var tableNumber = tableId.split("*")[0];
    var tableName = tableId.split("*")[1];
    
    var htmlAccept = '<div class="card oturmaIstegi"><p class=" cardText">' + tableName + ' Oturma Isteği </p><div class="card-footer ' + tableNumber + '" id=' + CostumerConnectionId + '><button class="btn btn-outline-success float-left" id="AcceptButton" >Kabul Et</button><button id="DeclineButton" class="btn btn-outline-danger float-right">Reddet</button></div></div>'
    document.getElementById("RequestList").innerHTML += htmlAccept;
});
connection.on("SendOrderResponse", function (tableId)
{
    var tableNumber = tableId.split("*")[0];
    var tableName = tableId.split("*")[1];
    var htmlAccept = '<div class="card oturmaIstegi"><p class=" cardText">' + tableName + ' Yeni Sipariş </p><div class="card-footer " style="text-align:center"><a class="btn btn-outline-light" target="_blank" href="/Restaurant/TableDetail/' + tableNumber + '">Yeni Sipariş</a></div></div>'
    document.getElementById("RequestList").innerHTML += htmlAccept;
});


connection.on("Request", function (message)
{
    var node = document.createElement("LI");                 // Create a <li> node
    var textnode = document.createTextNode(message);         // Create a text node
    node.appendChild(textnode);
    document.getElementById("RequestList").append(node);
});

connection.on("CheckoutResponse", function (tableId ) {
    
    var tableNumber = tableId.split("*")[0];
    var tableName = tableId.split("*")[1];
    var htmlAccept = '<div class="card oturmaIstegi"><p class=" cardText">' + tableName + ' Hesap İsteği </p><div class="card-footer " style="text-align:center"><a class="btn btn-outline-light" target="_blank" href="/Restaurant/TableDetail/' + tableNumber + '">Hesap İsteği</a></div></div>';
    document.getElementById("RequestList").innerHTML += htmlAccept;
});


connection.on("CloseSessionLink", function (tableId) {
    
    if (document.getElementsByClassName("tableId")[0].id.split("*")[0] == tableId) {
        
        window.location.href = '/MenuView/CloseSession';
    }
    else if (document.getElementsByClassName("tableId")[0].id == "TableDetail") {
        window.location.reload();
    }
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
if (document.getElementById("makeOrder") != null) {
    document.getElementById("makeOrder").addEventListener("click", function (event) {
        var tableId = document.getElementsByClassName("tableId")[0].id;
        connection.invoke("SendOrder", tableId);
        
    });
    
}

if (document.getElementById("checkoutReq") != null) {
    document.getElementById("checkoutReq").addEventListener("click", function (event) {
        var tableId = document.getElementsByClassName("tableId")[0].id;
        
        connection.invoke("CheckoutMessage", tableId, CostumerConnectionId);

    });

}
if (document.getElementById("closeSession") != null) {
    document.getElementById("closeSession").addEventListener("click", function (event) {
        if (event.target.id == "closeSession") {
            var tableId = event.target.parentElement.id;            
            connection.invoke("CloseSession", tableId);
        }
        

    });

}
