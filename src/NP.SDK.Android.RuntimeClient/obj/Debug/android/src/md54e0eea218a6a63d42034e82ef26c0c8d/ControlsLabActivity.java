package md54e0eea218a6a63d42034e82ef26c0c8d;


public class ControlsLabActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("NP.SDK.Android.RuntimeClient.ControlsLabActivity, NP.SDK.Android.RuntimeClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ControlsLabActivity.class, __md_methods);
	}


	public ControlsLabActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ControlsLabActivity.class)
			mono.android.TypeManager.Activate ("NP.SDK.Android.RuntimeClient.ControlsLabActivity, NP.SDK.Android.RuntimeClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
