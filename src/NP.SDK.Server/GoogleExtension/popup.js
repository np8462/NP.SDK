const ws =
    new WebSocket(
        "ws://127.0.0.1:5051/runtime");

ws.onopen = function ()
{
    console.log("Connected");

    ws.send(
        JSON.stringify({
            Command: "ping",
            Data: "Hello From Browser"
        }));
};

ws.onmessage = function (e)
{
    console.log(e.data);
};

ws.onclose = function ()
{
    console.log("Closed");
};