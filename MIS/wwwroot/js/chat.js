"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

var myId;

connection.on("UserConnected", function (lastConnected) {
    var usersConnected = document.getElementById("usersConnected").innerHTML;
    usersConnected = lastConnected + 1;
    
    document.getElementById("usersConnected").innerHTML = usersConnected;
})

connection.on("UserDisconnected", function () {
    var usersConnected = document.getElementById("usersConnected").innerHTML;
    usersConnected--;
    console.log("hi")
    document.getElementById("usersConnected").innerHTML = usersConnected;
})

connection.on("ReceiveMessage", function (content, name, id) {
    if (id != myId) {
        var date = new Date;
        var hour = date.getHours();
        var min = date.getMinutes();
        var html = `<div class="message-container">
        <div class="message message-received">
            <div class="message-receiver-name">${name}</div>
            <div class="message-received-flex">
            <span class="message-content message-content-received">${content}</span>
            <span class="message-date message-date-received">${hour}:${min}</span>
        </div>
        </div>
        </div>`
        document.getElementById("chat-body").innerHTML = document.getElementById("chat-body").innerHTML + html
    }

});


connection.start().then(function () {
    var usersConnected = document.getElementById("usersConnected").innerHTML;
    $.get('/Meeting/UserConnected?lastConnected=' + usersConnected);
}).catch(function (err) {
    return console.error(err.toString());
});

function sendMessage(event, userName, userId) {
    var content = document.getElementById("chatInput").value;
    myId = userId;
    name = userName;
    var date = new Date;
    var hour = date.getHours();
    var min = date.getMinutes();

    var html = `<div class="message-container message-container-received">
        <div class="message message-sent">
            <span class="message-content">${content}</span>
            <span class="message-date">${hour}:${min}</span>
        </div>
    </div>`
    document.getElementById("chat-body").innerHTML = document.getElementById("chat-body").innerHTML + html
    $.get('/Meeting/SendMessage?content=' + content + '&name=' + userName + '&id=' + userId)


    event.preventDefault();
};

