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
    [Activity(Label = "NP.SDK Showcase", MainLauncher = true,
        Icon = "@android:drawable/sym_def_app_icon",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation |
                               Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        private LinearLayout _root;
        private LinearLayout _tabBar;
        private FrameLayout _content;
        private int _activeTab = -1;

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
            _root = new LinearLayout(this);
            _root.Orientation = Orientation.Vertical;
            _root.SetBackgroundColor(Color.Rgb(245, 247, 249));

            TextView title = new TextView(this);
            title.Text = "NP.SDK Android";
            title.TextSize = 22;
            title.SetTextColor(Color.White);
            title.SetPadding(Dp(16), Dp(14), Dp(16), Dp(2));

            TextView subtitle = new TextView(this);
            subtitle.Text = "Custom UI Controls • Interactive Showcase";
            subtitle.TextSize = 12;
            subtitle.SetTextColor(Color.Rgb(220, 232, 240));
            subtitle.SetPadding(Dp(16), 0, Dp(16), Dp(14));

            LinearLayout header = new LinearLayout(this);
            header.Orientation = Orientation.Vertical;
            header.SetBackgroundColor(Color.Rgb(28, 49, 68));
            header.AddView(title);
            header.AddView(subtitle);
            _root.AddView(header);

            _tabBar = new LinearLayout(this);
            _tabBar.Orientation = Orientation.Horizontal;
            _tabBar.SetBackgroundColor(Color.Rgb(42, 67, 88));

            for (int i = 0; i < _tabs.Length; i++)
            {
                int tabIndex = i;
                Button tab = new Button(this);
                tab.Text = _tabs[i];
                tab.TextSize = 10;
                tab.SetAllCaps(false);
                tab.SetPadding(Dp(2), 0, Dp(2), 0);
                tab.Click += delegate { SelectTab(tabIndex); };
                _tabBar.AddView(tab, new LinearLayout.LayoutParams(0,
                    Dp(48), 1f));
            }
            _root.AddView(_tabBar);

            _content = new FrameLayout(this);
            _root.AddView(_content, new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, 0, 1f));

            SetContentView(_root);
        }

        private void SelectTab(int index)
        {
            _activeTab = index;
            for (int i = 0; i < _tabBar.ChildCount; i++)
            {
                Button button = _tabBar.GetChildAt(i) as Button;
                if (button != null)
                {
                    button.SetTextColor(i == index
                        ? Color.Rgb(35, 120, 90) : Color.White);
                }
            }

            _content.RemoveAllViews();
            View page;

            switch (index)
            {
                case 0: page = BuildConfirmPage(); break;
                case 1: page = BuildInfoPage("Buttons",
                    "Custom buttons will be added here.",
                    "This tab is reserved for reusable button controls."); break;
                case 2: page = BuildInfoPage("Progress",
                    "Progress controls",
                    "Circular, linear and state-based progress indicators will be added here."); break;
                case 3: page = BuildInfoPage("Gestures",
                    "Gesture controls",
                    "Swipe actions, drag handles and gesture samples will be added here."); break;
                default: page = BuildSamplesPage(); break;
            }

            _content.AddView(page);
        }

        private View BuildConfirmPage()
        {
            LinearLayout page = CreatePage();
            AddHeading(page, "Slide to Confirm");
            AddBody(page, "Drag the round handle all the way to the right. " +
                "The Confirmed event fires once when the threshold is reached.");

            SlideConfirmView slider = new SlideConfirmView(this);
            slider.ConfirmThreshold = 0.85f;
            slider.PromptText = "Slide to commit";
            slider.ConfirmedText = "Committed!";
            LinearLayout.LayoutParams sliderParams =
                new LinearLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent, Dp(56));
            sliderParams.TopMargin = Dp(18);
            page.AddView(slider, sliderParams);

            TextView status = new TextView(this);
            status.Text = "Status: waiting for confirmation";
            status.TextSize = 14;
            status.SetTextColor(Color.Rgb(70, 80, 90));
            status.SetPadding(0, Dp(14), 0, Dp(8));
            page.AddView(status);

            slider.Confirmed += delegate
            {
                status.Text = "Status: confirmed • callback executed";
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

            AddBody(page, "Threshold: 85%   •   Native Canvas   •   No external UI package");
            Button source = new Button(this);
            source.Text = "Open C# source on GitHub";
            source.SetAllCaps(false);
            source.Click += delegate
            {
                OpenUrl("https://github.com/np8462/NP.SDK/blob/main/src/NP.SDK.Android.Controls/Controls/SlideConfirmView.cs");
            };
            page.AddView(source);

            return page;
        }

        private View BuildSamplesPage()
        {
            LinearLayout page = CreatePage();
            AddHeading(page, "Code Samples");
            AddBody(page, "Add the Controls project reference, then create the view:");
            TextView code = new TextView(this);
            code.Text = "var slider = new SlideConfirmView(this);\n" +
                "slider.Confirmed += delegate {\n" +
                "    CommitOperation();\n" +
                "};\n" +
                "layout.AddView(slider);";
            code.Typeface = Typeface.Monospace;
            code.TextSize = 12;
            code.SetTextColor(Color.Rgb(35, 55, 70));
            code.SetBackgroundColor(Color.Rgb(230, 235, 239));
            code.SetPadding(Dp(12), Dp(12), Dp(12), Dp(12));
            page.AddView(code);

            Button source = new Button(this);
            source.Text = "Browse NP.SDK on GitHub";
            source.SetAllCaps(false);
            source.Click += delegate { OpenUrl("https://github.com/np8462/NP.SDK"); };
            page.AddView(source);
            return page;
        }

        private View BuildInfoPage(string heading, string title, string detail)
        {
            LinearLayout page = CreatePage();
            AddHeading(page, heading);
            AddBody(page, title);
            AddBody(page, detail);
            AddBody(page, "This tab is part of the showcase structure and is ready for future controls.");
            return page;
        }

        private LinearLayout CreatePage()
        {
            LinearLayout page = new LinearLayout(this);
            page.Orientation = Orientation.Vertical;
            page.SetPadding(Dp(18), Dp(20), Dp(18), Dp(18));
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
            view.SetPadding(0, Dp(10), 0, Dp(4));
            page.AddView(view);
        }

        private int Dp(int value)
        {
            return (int)(value * Resources.DisplayMetrics.Density + 0.5f);
        }

        private void OpenUrl(string url)
        {
            try
            {
                Intent intent = new Intent(Intent.ActionView, Uri.Parse(url));
                StartActivity(intent);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Unable to open GitHub link.", ToastLength.Short).Show();
            }
        }
    }
}
