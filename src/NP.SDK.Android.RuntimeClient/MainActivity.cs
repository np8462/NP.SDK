using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using NP.SDK.Android.RuntimeClient.Clients;

namespace NP.SDK.Android.RuntimeClient
{
    [Activity(Label = "NP.SDK Runtime Client", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private EditText _txtServerAddress;
        private EditText _txtMessage;
        private Button _btnConnect;
        private Button _btnSend;
        private Button _btnDisconnect;
        private TextView _txtLog;
        private AndroidRuntimeClient _client;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _txtServerAddress = FindViewById<EditText>(Resource.Id.txtServerAddress);
            _txtMessage = FindViewById<EditText>(Resource.Id.txtMessage);
            _btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            _btnSend = FindViewById<Button>(Resource.Id.btnSend);
            _btnDisconnect = FindViewById<Button>(Resource.Id.btnDisconnect);
            _txtLog = FindViewById<TextView>(Resource.Id.txtLog);

            _txtServerAddress.Text = "ws://192.168.1.14:5051/runtime";
            _btnConnect.Click += BtnConnect_Click;
            _btnSend.Click += BtnSend_Click;
            _btnDisconnect.Click += BtnDisconnect_Click;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(0, 1001, 0, "Controls Lab");
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 1001)
            {
                StartActivity(new Intent(this, typeof(ControlsLabActivity)));
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string address = _txtServerAddress.Text.Trim();
                _client = new AndroidRuntimeClient();
                _client.LogReceived += AddLog;
                _client.Start(address);
            }
            catch (Exception ex) { AddLog("Connect Error : " + ex.Message); }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (_client == null)
                {
                    AddLog("Client is not connected.");
                    return;
                }
                _client.SendMessage(_txtMessage.Text.Trim());
            }
            catch (Exception ex) { AddLog("Send Error : " + ex.Message); }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_client != null)
                {
                    _client.Stop();
                    _client.LogReceived -= AddLog;
                    _client = null;
                }
                AddLog("Client disconnected.");
            }
            catch (Exception ex) { AddLog("Disconnect Error : " + ex.Message); }
        }

        private void AddLog(string text)
        {
            RunOnUiThread(delegate
            {
                _txtLog.Append(DateTime.Now.ToString("HH:mm:ss") + " " + text + Environment.NewLine);
            });
        }

        protected override void OnDestroy()
        {
            if (_client != null)
            {
                _client.Stop();
                _client.LogReceived -= AddLog;
                _client = null;
            }
            base.OnDestroy();
        }
    }
}
