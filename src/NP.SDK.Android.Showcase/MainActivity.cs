using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using NP.SDK.Android.Controls.Testing;

namespace NP.SDK.Android.Showcase
{
	/*
    [Activity(Label = "NP.SDK Android Showcase", MainLauncher = true,
        Icon = "@android:drawable/sym_def_app_icon",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation |
                               Android.Content.PM.ConfigChanges.ScreenSize)]
*/
	[Activity(Label = "NP.SDK Android Showcase", MainLauncher = true,
	Icon = "@android:drawable/sym_def_app_icon",
	Theme = "@android:style/Theme.Holo.Light.NoActionBar",
	ConfigurationChanges = global::Android.Content.PM.ConfigChanges.Orientation |
						   global::Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
    {
        private LinearLayout _tabBar;
        private FrameLayout _content;
        private readonly string[] _tabs =
            { "SlideConfirm", "Buttons", "Progress", "Gestures", "Samples" };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            BuildShell();
            SelectTab(0);
        }

        private void BuildShell()
        {
            LinearLayout root = new LinearLayout(this);
            root.Orientation = Orientation.Vertical;
            root.SetBackgroundColor(Color.Rgb(245, 247, 249));

            TextView title = new TextView(this);
            title.Text = "NP.SDK Android";
            title.TextSize = 21;
            title.SetTextColor(Color.White);
            title.SetPadding(Dp(16), Dp(13), Dp(16), Dp(1));

            TextView subtitle = new TextView(this);
            subtitle.Text = "Reusable custom controls • Interactive test laboratory";
            subtitle.TextSize = 12;
            subtitle.SetTextColor(Color.Rgb(220, 232, 240));
            subtitle.SetPadding(Dp(16), 0, Dp(16), Dp(12));

            LinearLayout header = new LinearLayout(this);
            header.Orientation = Orientation.Vertical;
            header.SetBackgroundColor(Color.Rgb(28, 49, 68));
            header.AddView(title);
            header.AddView(subtitle);
            root.AddView(header);

            HorizontalScrollView tabScroll = new HorizontalScrollView(this);
            tabScroll.HorizontalScrollBarEnabled = false;
            tabScroll.FillViewport = true;
            tabScroll.SetBackgroundColor(Color.Rgb(42, 67, 88));
            _tabBar = new LinearLayout(this);
            _tabBar.Orientation = Orientation.Horizontal;

            for (int i = 0; i < _tabs.Length; i++)
            {
                int tabIndex = i;
                Button tab = new Button(this);
                tab.Text = _tabs[i];
                tab.TextSize = 11;
                tab.SetAllCaps(false);
                tab.SetPadding(Dp(10), 0, Dp(10), 0);
                tab.Click += delegate { SelectTab(tabIndex); };
                _tabBar.AddView(tab, new LinearLayout.LayoutParams(
                    ViewGroup.LayoutParams.WrapContent, Dp(48)));
            }

            tabScroll.AddView(_tabBar);
            root.AddView(tabScroll);

            _content = new FrameLayout(this);
            root.AddView(_content, new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, 0, 1f));
            SetContentView(root);
        }

        private void SelectTab(int index)
        {
            for (int i = 0; i < _tabBar.ChildCount; i++)
            {
                Button button = _tabBar.GetChildAt(i) as Button;
                if (button != null)
                    button.SetTextColor(i == index ? Color.Rgb(105, 220, 170) : Color.White);
            }

            _content.RemoveAllViews();
            View page;
            switch (index)
            {
                case 0:
                    page = new SlideConfirmLabView(this);
                    _content.AddView(page, new FrameLayout.LayoutParams(
                        ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
                    return;
                case 1:
                    page = BuildFuturePage("Buttons",
                        "Planned controls: icon, loading, outlined and toggle buttons.");
                    break;
                case 2:
                    page = BuildFuturePage("Progress",
                        "Planned controls: linear/circular progress and loading indicators.");
                    break;
                case 3:
                    page = BuildFuturePage("Gestures",
                        "Planned controls: swipe actions, swipe-to-dismiss and drag surfaces.");
                    break;
                default:
                    page = BuildSamplesPage();
                    break;
            }

            ScrollView scroll = new ScrollView(this);
            scroll.FillViewport = true;
            scroll.AddView(page);
            _content.AddView(scroll);
        }

        private View BuildFuturePage(string heading, string detail)
        {
            LinearLayout page = CreatePage();
            AddHeading(page, heading);
            AddBody(page, detail);
            AddBody(page, "This tab is an extension point. It will become interactive when the corresponding controls are implemented.");
            AddLinkButton(page, "Browse Controls library",
                "https://github.com/np8462/NP.SDK/tree/" + Branch +
                "/src/NP.SDK.Android.Controls");
            return page;
        }

        private View BuildSamplesPage()
        {
            LinearLayout page = CreatePage();
            AddHeading(page, "C# Samples");
            AddBody(page, "The SlideConfirm tab contains a live property editor, event monitor and generated C# snippet. The same test page is also available from RuntimeClient.");

            TextView code = new TextView(this);
            code.Text = "var slider = new SlideConfirmView(this);\n" +
                "slider.ConfirmThreshold = 0.85f;\n" +
                "slider.PromptText = \"Slide to commit\";\n" +
                "slider.ConfirmedText = \"Done!\";\n\n" +
                "slider.Confirmed += delegate\n{\n    CommitOperation();\n};\n\n" +
                "parentLayout.AddView(slider);\nslider.Reset();";
            code.Typeface = Typeface.Monospace;
            code.TextSize = 12;
            code.SetTextColor(Color.Rgb(35, 55, 70));
            code.SetBackgroundColor(Color.Rgb(230, 235, 239));
            code.SetPadding(Dp(12), Dp(12), Dp(12), Dp(12));
            page.AddView(code);

            AddLinkButton(page, "SlideConfirmView source",
                "https://github.com/np8462/NP.SDK/blob/" + Branch +
                "/src/NP.SDK.Android.Controls/Controls/SlideConfirmView.cs");
            AddLinkButton(page, "Shared test page source",
                "https://github.com/np8462/NP.SDK/blob/" + Branch +
                "/src/NP.SDK.Android.Controls/Testing/SlideConfirmLabView.cs");
            AddLinkButton(page, "Controls library",
                "https://github.com/np8462/NP.SDK/tree/" + Branch +
                "/src/NP.SDK.Android.Controls");
            AddLinkButton(page, "RuntimeClient integration",
                "https://github.com/np8462/NP.SDK/tree/" + Branch +
                "/src/NP.SDK.Android.RuntimeClient");
            AddLinkButton(page, "NP.SDK repository",
                "https://github.com/np8462/NP.SDK");
            return page;
        }

        private LinearLayout CreatePage()
        {
            LinearLayout page = new LinearLayout(this);
            page.Orientation = Orientation.Vertical;
            page.SetPadding(Dp(18), Dp(18), Dp(18), Dp(24));
            return page;
        }

        private void AddHeading(LinearLayout page, string text)
        {
            TextView view = new TextView(this);
            view.Text = text;
            view.TextSize = 20;
            view.SetTextColor(Color.Rgb(30, 48, 64));
            page.AddView(view);
        }

        private void AddBody(LinearLayout page, string text)
        {
            TextView view = new TextView(this);
            view.Text = text;
            view.TextSize = 13;
            view.SetTextColor(Color.Rgb(85, 96, 106));
            view.SetPadding(0, Dp(9), 0, Dp(4));
            page.AddView(view);
        }

        private void AddLinkButton(LinearLayout page, string caption, string url)
        {
            Button button = new Button(this);
            button.Text = caption;
            button.SetAllCaps(false);
            button.Click += delegate
            {
                try { StartActivity(new Intent(Intent.ActionView,global::Android.Net.Uri.Parse(url))); }
                catch (Exception) { Toast.MakeText(this, "Unable to open GitHub link.", ToastLength.Short).Show(); }
            };
            page.AddView(button);
        }

        private int Dp(int value)
        {
            return (int)(value * Resources.DisplayMetrics.Density + 0.5f);
        }

        private const string Branch = "feature/android-controls-showcase";
    }
}
