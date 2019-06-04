const uri = 'ws://' + location.hostname + ':10081';
var ws;

var text_message = document.getElementById("text_message")
function log(msg) {
    text_message.innerText += msg
    text_message.innerText += "\n"
}

function connect() {
    log("connect() uri=" + uri)

    ws = new WebSocket(uri);
    ws.addEventListener('open', function (evt) {
        log("ws.onopen");
    });

    ws.addEventListener('close', function (evt) {
        log("ws.onclose");
        setTimeout(connect, 3000);
    });

    ws.addEventListener('error', function (evt) {
        log("ws.onerror");
    });

    ws.addEventListener('message', function (message) {
        log("ws.onmessage : message = " + message.data);
    });
}

function on_click_button() {
    if (ws.readyState == WebSocket.OPEN) {
        ws.send('hello');
        log("on_click_button() : send()");
    }
    else {
        log("on_click_button() : readyState=" + ws.readyState);
    }
}

var button_test = document.getElementById("button_test")
button_test.onclick = function () {
    on_click_button();
}

connect();
