
$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

    const format = (name, message, nameColor) => {
        return `<div class="sep">
        <p class="name" style="color: ${nameColor};">${name}</p>
        <p class="message">${message}</p>
        </div>`
    }

    connection.on("ChatHistory", function (history) {
        for (var i = 0; i < history.length; i++) {
            $("#chatArea").append(format(history[i].name, history[i].message, history[i].nameColor));
        }
    });

    connection.on("ReceiveMessage", function (user, message, nameColor) {
        $("#chatArea").append(format(user, message, nameColor));
    });

    connection.start().catch(function (err) {
        console.error(err.toString());
    });

    $("#send-button").click(function () {
        var message = $("#message-input").val();
        var user = $("#username").val();
        var nameColor = $("#namecolor").val();

        if (message.length <= 0) return;

        connection.invoke("SendMessage", user, message, nameColor).catch(function (err) {
            console.error(err.toString());
        });
        
        $("#message-input").val("")
    });
});