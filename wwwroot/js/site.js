
$(function () {
    const placeholderAvatar = "https://t4.ftcdn.net/jpg/05/42/36/11/360_F_542361185_VFRJWpR2FH5OiAEVveWO7oZnfSccZfD3.jpg";
    let userId;
    let messages = [];

    var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

    function getCurrentScrollHeight() {
        var div = $('#chat-messages');

        return div.prop('scrollHeight') - div.height() - div.scrollTop();
    }

    function scrollDown() {
        var div = $('#chat-messages');
        div.scrollTop(div.prop('scrollHeight') - div.height());
    }

    const format = (user, message, i) => {
        if (user.id !== userId) {
            if (i > 0 && user.id == messages[i - 1].userId)
                return `
                <div class="sep same">
                    <p class="message">${message}</p>
                </div>`

            return `
            <div class="sep">
                <img src="${user.avatar || placeholderAvatar}" class="avatar">
                <div>
                    <p class="name">${user.userName}</p>
                    <p class="message">${message}</p>
                </div>
            </div>`
        }

        return `
        <div class="sep own">
            <p class="message">${message}</p>
        </div>`
    }

    connection.on("ChatHistory", function (history) {
        messages = history;
        userId = Number($("#userId").val());
        $("#userId").remove();

        for (var i = 0; i < history.length; i++) {
            $("#chatArea").append(format(history[i].user, history[i].message, i));
        }

        scrollDown();
    });

    connection.on("ReceiveMessage", function (user, message) {
        const scrollPosition = getCurrentScrollHeight();
        const i = messages.length;

        messages.push({ userId: user.id })

        if (scrollPosition > 100) {
            $("#chatArea").append(format(user, message, i));
            $("#new-messages").show();
        }
        else {
            $("#chatArea").append(format(user, message, i));
            scrollDown()
        }
    });

    connection.start().catch(function (err) {
        console.error(err.toString());
    });

    $("#new-messages").click(function () {
        scrollDown();
        $("#new-messages").hide();
    });

    $("#send-button").click(async function () {
        const message = $("#message-input").val();

        if (message.length <= 0) return;

        await connection.invoke("SendMessage", userId, message).catch(function (err) {
            console.error(err.toString());
        });

        $("#message-input").val("")
        scrollDown();
    });

    $("#chat-messages").on("scroll", function () {
        const scrollPosition = getCurrentScrollHeight();

        if (scrollPosition < 21)
            $("#new-messages").hide();
    });
});