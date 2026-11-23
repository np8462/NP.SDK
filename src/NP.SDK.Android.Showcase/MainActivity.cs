using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Widget;
using NP.SDK.Android.Controls;

namespace NP.SDK.Android.Showcase
{
    [Activity(Label = "NP.SDK Android Showcase", MainLauncher = true,
        Icon = "@android:drawable/sym_def_app_icon",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation |
                               Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        private LinearLayout _tabBar;
        private FrameLayout _content;
        private readonly string[] _tabs =
            { "Confirm", "Buttons", "Progress", "Gestures", "Samples" };

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
            subtitle.Text = "Reusable custom controls • Interactive samples";
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
                {
                    button.SetTextColor(i == index
                        ? Color.Rgb(105, 220, 170) : Color.White);
                }
            }

            _content.RemoveAllViews();
            View page;
            switch (index)
            {
                case 0: page = BuildConfirmPage(); break;
                case 1: page = BuildFuturePage("Buttons",
                    "Button controls are the next planned group.",
                    "Planned samples: icon button, loading button, outlined button, " +
                    "toggle button and long-press action."); break;
                case 2: page = BuildFuturePage("Progress",
                    "Progress controls are reserved for the next iteration.",
                    "Planned samples: linear progress, circular progress, " +
                    "indeterminate loader and step progress."); break;
                case 3: page = BuildFuturePage("Gestures",
                    "Gesture controls will share this tab.",
                    "Planned samples: swipe actions, swipe-to-dismiss, " +
                    "drag handle and gesture surface."); break;
                default: page = BuildSamplesPage(); break;
            }

            ScrollView scroll = new ScrollView(this);
            scroll.FillViewport = true;
            scroll.AddView(page);
            _content.AddView(scroll);
        }

        private View BuildConfirmPage()
        {
            LinearLayout page = CreatePage();
            AddHeading(page, "Slide to Confirm");
            AddBody(page, "A reusable confirmation control. Drag the round handle " +
                "to the right to confirm an action.");

            SlideConfirmView slider = new SlideConfirmView(this);
            slider.ConfirmThreshold = 0.85f;
            slider.PromptText = "Slide to commit";
            slider.ConfirmedText = "Committed!";
            LinearLayout.LayoutParams sliderParams =
                new LinearLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent, Dp(56));
            sliderParams.TopMargin = Dp(18);
            sliderParams.BottomMargin = Dp(8);
            page.AddView(slider, sliderParams);

            TextView status = new TextView(this);
            status.Text = "Status: waiting for confirmation";
            status.TextSize = 14;
            status.SetTextColor(Color.Rgb(70, 80, 90));
            status.SetPadding(0, Dp(12), 0, Dp(8));
            page.AddView(status);

            slider.Confirmed += delegate
            {
                status.Text = "Status: confirmed — event raised";
                status.SetTextColor(Color.Rgb(35, 125, 85));
            };

            Button reset = new Button(this);
            reset.Text = "Reset control";
            reset.SetAllCaps(false);
            reset.Click += delegate
            {
                slider.Reset();
                status.Text = "Status: waiting for confirmation";
                status.SetTextColor(Color.Rgb(70, 80, 90));
            };
            page.AddView(reset);

            AddSection(page, "Current features");
            AddBullet(page, "Native Android View rendered with Canvas");
            AddBullet(page, "Adjustable confirmation threshold");
            AddBullet(page, "Confirmed event is raised once per confirmation");
            AddBullet(page, "Reset() makes the control reusable");
            AddBullet(page, "No third-party UI dependency");

            AddLinkButton(page, "Open SlideConfirmView.cs",
                "https://github.com/np8462/NP.SDK/blob/" + Branch +
                "/src/NP.SDK.Android.Controls/Controls/SlideConfirmView.cs");
            return page;
        }

        private View BuildSamplesPage()
        {
            LinearLayout page = CreatePage();
            AddHeading(page, "C# Samples");
            AddBody(page, "Add a project reference to NP.SDK.Android.Controls, " +
                "then create and configure the control:");

            TextView code = new TextView(this);
            code.Text =
                "var slider = new SlideConfirmView(this);\n" +
                "slider.ConfirmThreshold = 0.85f;\n" +
                "slider.PromptText = \"Slide to commit\";\n" +
                "slider.ConfirmedText = \"Done!\";\n\n" +
                "slider.Confirmed += delegate\n" +
                "{\n" +
                "    CommitOperation();\n" +
                "};\n\n" +
                "layout.AddView(slider);\n\n" +
                "// To reuse it:\n" +
                "slider.Reset();";
            code.Typeface = Typeface.Monospace;
            code.TextSize = 12;
            code.SetTextColor(Color.Rgb(35, 55, 70));
            code.SetBackgroundColor(Color.Rgb(230, 235, 239));
            code.SetPadding(Dp(12), Dp(12), Dp(12), Dp(12));
            page.AddView(code);

            AddBody(page, "The sample above is also documented in the repository. " +
                "Use the links below to inspect the implementation and project.");

            AddLinkButton(page, "Control source: SlideConfirmView.cs",
                "https://github.com/np8462/NP.SDK/blob/" + Branch +
                "/src/NP.SDK.Android.Controls/Controls/SlideConfirmView.cs");
            AddLinkButton(page, "Controls library",
                "https://github.com/np8462/NP.SDK/tree/" + Branch +
                "/src/NP.SDK.Android.Controls");
            AddLinkButton(page, "Showcase project and README",
                "https://github.com/np8462/NP.SDK/tree/" + Branch +
                "/src/NP.SDK.Android.Showcase");
            AddLinkButton(page, "NP.SDK repository",
                "https://github.com/np8462/NP.SDK");
            return page;
        }

        private View BuildFuturePage(string heading, string title, string detail)
        {
            LinearLayout page = CreatePage();
            AddHeading(page, heading);
            AddBody(page, title);
            AddBody(page, detail);
            AddSection(page, "Showcase structure");
            AddBullet(page, "A dedicated tab for this control family");
            AddBullet(page, "Interactive preview will be added with the control");
            AddBullet(page, "C# sample and direct GitHub source link");
            AddBody(page, "This tab is intentionally a roadmap placeholder. " +
                "Only Slide to Confirm is implemented in the current version.");
            AddLinkButton(page, "Browse Controls library on GitHub",
                "https://github.com/np8462/NP.SDK/tree/" + Branch +
                "/src/NP.SDK.Android.Controls");
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

        private void AddSection(LinearLayout page, string text)
        {
            TextView view = new TextView(this);
            view.Text = text;
            view.TextSize = 15;
            view.SetTextColor(Color.Rgb(40, 65, 85));
            view.SetPadding(0, Dp(18), 0, Dp(4));
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

        private void AddBullet(LinearLayout page, string text)
        {
            AddBody(page, "•  " + text);
        }

        private void AddLinkButton(LinearLayout page, string caption, string url)
        {
            Button button = new Button(this);
            button.Text = caption;
            button.SetAllCaps(false);
            button.Click += delegate { OpenUrl(url); };
            page.AddView(button);
        }

        private int Dp(int value)
        {
            return (int)(value * Resources.DisplayMetrics.Density + 0.5f);
        }

        private const string Branch = "feature/android-controls-showcase";

        private void OpenUrl(string url)
        {
            try
            {
                Intent intent = new Intent(Intent.ActionView, Uri.Parse(url));
                StartActivity(intent);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Unable to open GitHub link.",
                    ToastLength.Short).Show();
            }
        }
    }
}