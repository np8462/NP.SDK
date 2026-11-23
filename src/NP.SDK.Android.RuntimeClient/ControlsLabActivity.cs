using Android.App;
using Android.OS;
using NP.SDK.Android.Controls.Testing;

namespace NP.SDK.Android.RuntimeClient
{
    [Activity(Label = "NP.SDK Controls Lab")]
    public class ControlsLabActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(new SlideConfirmLabView(this));
        }
    }
}
