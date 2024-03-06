
$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

    const format = (userName, message) => {
        return `<div class="sep">
        <p class="name">${userName}</p>
        <p class="message">${message}</p>
        </div>`
    }

    connection.on("ChatHistory", function (history) {
        for (var i = 0; i < history.length; i++) {
            const userName = history[i].user?.userName || "Usuário Desconhecido";
            const message = history[i].message || "";
            $("#chatArea").append(format(userName, message));
        }
    });

    connection.on("ReceiveMessage", function (user, message) {
        $("#chatArea").append(format(user, message));
    });

    connection.start().catch(function (err) {
        console.error(err.toString());
    });

    $("#send-button").click(function () {
        var message = $("#message-input").val();
        var userId = Number($("#userId").val());

        if (message.length <= 0) return;

        connection.invoke("SendMessage", userId, message).catch(function (err) {
            console.error(err.toString());
        });

        $("#message-input").val("")
    });
});