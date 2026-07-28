//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebSocketSharp;
//using WebSocketSharp.Server;

//namespace NP.Services.Server
//{

//private class BridgeBehavior :
//    WebSocketBehavior
//{
//    private readonly RuntimeSocketServer _runtime;

//    public BridgeBehavior(
//        RuntimeSocketServer runtime)
//    {
//        _runtime = runtime;
//    }

//    //--------------------------------------------------

//    protected override void OnOpen()
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                "WebSocket Connected");
//        }

//        base.OnOpen();
//    }

//    //--------------------------------------------------

//    protected override void OnClose(
//        CloseEventArgs e)
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                "WebSocket Closed");
//        }

//        base.OnClose(e);
//    }

//    //--------------------------------------------------

//    protected override void OnError(
//        WebSocketSharp.ErrorEventArgs e)
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                e.Message);
//        }

//        base.OnError(e);
//    }

//    //--------------------------------------------------

//    protected override void OnMessage(
//        MessageEventArgs e)
//    {
//        try
//        {
//            if (_runtime.MessageReceived != null)
//            {
//                _runtime.MessageReceived(
//                    e.Data);
//            }

//            if (_runtime._router != null)
//            {
//                _runtime._router.RouteLog(
//                    "WS <= " + e.Data);
//            }

//            //------------------------------------------------
//            // فعلاً فقط Echo
//            //------------------------------------------------

//            Send("OK");
//        }
//        catch (Exception ex)
//        {
//            Send(ex.ToString());
//        }
//    }
//}

