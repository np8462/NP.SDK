using System;
using System.Text;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace NP.SDK.Android.Controls.Testing
{
    /// <summary>
    /// Interactive test page for configuring and exercising SlideConfirmView.
    /// This is a diagnostic/sample surface, not a production control.
    /// </summary>
    public class SlideConfirmLabView : LinearLayout
    {
        private readonly Context _context;
        private readonly SlideConfirmView _slider;
        private readonly TextView _status;
        private readonly TextView _eventLog;
        private readonly TextView _sampleCode;
        private readonly TextView _thresholdValue;
        private readonly EditText _promptInput;
        private readonly EditText _confirmedInput;
        private readonly FrameLayout _pageHost;
        private readonly Button[] _tabs = new Button[4];
        private readonly StringBuilder _events = new StringBuilder();

        public SlideConfirmLabView(Context context)
            : base(context)
        {
            _context = context;
            Orientation = Orientation.Vertical;
            SetBackgroundColor(Color.Rgb(245, 247, 249));

            _slider = new SlideConfirmView(context);
            _slider.ConfirmThreshold = 0.85f;
            _slider.PromptText = "Slide to confirm";
            _slider.ConfirmedText = "Confirmed!";
            _slider.Confirmed += OnConfirmed;
            _slider.ProgressChanged += delegate { _status.Text = GetStatusText(); };

            _status = new TextView(context);
            _eventLog = new TextView(context);
            _sampleCode = new TextView(context);
            _thresholdValue = new TextView(context);
            _promptInput = new EditText(context);
            _confirmedInput = new EditText(context);
            _pageHost = new FrameLayout(context);

            BuildShell();
            ShowTab(0);
            UpdateSample();
        }

        private void BuildShell()
        {
            TextView heading = new TextView(_context);
            heading.Text = "SlideConfirmView Lab";
            heading.TextSize = 20;
            heading.SetTextColor(Color.Rgb(30, 48, 64));
            heading.SetPadding(Dp(16), Dp(16), Dp(16), Dp(4));
            AddView(heading);

            TextView intro = new TextView(_context);
            intro.Text = "Change properties, try the gesture, inspect events and copy the matching C# sample.";
            intro.TextSize = 12;
            intro.SetTextColor(Color.Rgb(85, 96, 106));
            intro.SetPadding(Dp(16), 0, Dp(16), Dp(12));
            AddView(intro);

            HorizontalScrollView tabScroll = new HorizontalScrollView(_context);
            tabScroll.HorizontalScrollBarEnabled = false;
            tabScroll.SetBackgroundColor(Color.Rgb(42, 67, 88));
            LinearLayout tabBar = new LinearLayout(_context);
            tabBar.Orientation = Orientation.Horizontal;

            string[] names = { "Preview", "Properties", "C# Sample", "Events" };
            for (int i = 0; i < names.Length; i++)
            {
                int index = i;
                Button tab = new Button(_context);
                tab.Text = names[i];
                tab.TextSize = 11;
                tab.SetAllCaps(false);
                tab.Click += delegate { ShowTab(index); };
                _tabs[i] = tab;
                tabBar.AddView(tab, new LinearLayout.LayoutParams(
                    ViewGroup.LayoutParams.WrapContent, Dp(46)));
            }

            tabScroll.AddView(tabBar);
            AddView(tabScroll);

            AddView(_pageHost, new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, 0, 1f));
        }

        private void ShowTab(int index)
        {
            for (int i = 0; i < _tabs.Length; i++)
                _tabs[i].SetTextColor(i == index ? Color.Rgb(105, 220, 170) : Color.White);

            _pageHost.RemoveAllViews();
            View page;

            if (index == 0)
                page = BuildPreviewPage();
            else if (index == 1)
                page = BuildPropertiesPage();
            else if (index == 2)
                page = BuildSamplePage();
            else
                page = BuildEventsPage();

            ScrollView scroll = new ScrollView(_context);
            scroll.FillViewport = true;
            scroll.AddView(page);
            _pageHost.AddView(scroll);
        }

        private View BuildPreviewPage()
        {
            LinearLayout page = CreatePage();
            AddText(page, "Live control preview", 16, Color.Rgb(40, 65, 85));
            AddText(page, "Drag the circular handle to the right. A successful gesture raises Confirmed exactly once.", 13, Color.Rgb(85, 96, 106));

            LinearLayout.LayoutParams sliderParams = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, Dp(56));
            sliderParams.TopMargin = Dp(20);
            sliderParams.BottomMargin = Dp(10);
            page.AddView(_slider, sliderParams);

            _status.TextSize = 14;
            _status.SetTextColor(Color.Rgb(70, 80, 90));
            _status.Text = GetStatusText();
            page.AddView(_status);

            Button reset = MakeButton("Reset control");
            reset.Click += delegate
            {
                _slider.Reset();
                _status.Text = GetStatusText();
                AddEvent("Control reset.");
            };
            page.AddView(reset);

            Button eventTab = MakeButton("View event log");
            eventTab.Click += delegate { ShowTab(3); };
            page.AddView(eventTab);
            return page;
        }

        private View BuildPropertiesPage()
        {
            LinearLayout page = CreatePage();
            AddText(page, "Runtime properties", 16, Color.Rgb(40, 65, 85));
            AddText(page, "ConfirmThreshold controls how far the handle must travel (0–100%).", 13, Color.Rgb(85, 96, 106));

            _thresholdValue.TextSize = 14;
            _thresholdValue.SetTextColor(Color.Rgb(45, 60, 70));
            _thresholdValue.Text = "Threshold: " + (int)(_slider.ConfirmThreshold * 100f) + "%";
            page.AddView(_thresholdValue);

            SeekBar threshold = new SeekBar(_context);
            threshold.Max = 100;
            threshold.Progress = (int)(_slider.ConfirmThreshold * 100f);
            threshold.ProgressChanged += delegate(object sender, SeekBar.ProgressChangedEventArgs e)
            {
                if (e.FromUser)
                {
                    _slider.ConfirmThreshold = e.Progress / 100f;
                    _thresholdValue.Text = "Threshold: " + e.Progress + "%";
                    UpdateSample();
                }
            };
            page.AddView(threshold);

            AddText(page, "PromptText", 13, Color.Rgb(65, 75, 85));
            _promptInput.Text = _slider.PromptText;
			//_promptInput.SingleLine = true;
			_promptInput.SetHorizontallyScrolling(true);
			page.AddView(_promptInput);

            AddText(page, "ConfirmedText", 13, Color.Rgb(65, 75, 85));
            _confirmedInput.Text = _slider.ConfirmedText;
			_confirmedInput.SetHorizontallyScrolling(true);
			//_confirmedInput.SingleLine = true;
            page.AddView(_confirmedInput);

            Button apply = MakeButton("Apply text properties");
            apply.Click += delegate
            {
                _slider.PromptText = _promptInput.Text;
                _slider.ConfirmedText = _confirmedInput.Text;
                _slider.Invalidate();
                UpdateSample();
                AddEvent("PromptText and ConfirmedText updated.");
                Toast.MakeText(_context, "Properties applied", ToastLength.Short).Show();
            };
            page.AddView(apply);

            Button reset = MakeButton("Reset preview");
            reset.Click += delegate
            {
                _slider.Reset();
                _status.Text = GetStatusText();
                AddEvent("Control reset from Properties tab.");
            };
            page.AddView(reset);

            AddText(page, "Changes to the threshold are applied immediately. Text changes are applied with the button.", 12, Color.Rgb(100, 108, 116));
            return page;
        }

        private View BuildSamplePage()
        {
            LinearLayout page = CreatePage();
            AddText(page, "C# code for current settings", 16, Color.Rgb(40, 65, 85));
            AddText(page, "The snippet is regenerated when you change the properties.", 13, Color.Rgb(85, 96, 106));

            _sampleCode.Typeface = Typeface.Monospace;
            _sampleCode.TextSize = 12;
            _sampleCode.SetTextColor(Color.Rgb(35, 55, 70));
            _sampleCode.SetBackgroundColor(Color.Rgb(230, 235, 239));
            _sampleCode.SetPadding(Dp(12), Dp(12), Dp(12), Dp(12));
            _sampleCode.Text = BuildSampleText();
            page.AddView(_sampleCode);

            Button refresh = MakeButton("Refresh sample");
            refresh.Click += delegate { UpdateSample(); };
            page.AddView(refresh);
            return page;
        }

        private View BuildEventsPage()
        {
            LinearLayout page = CreatePage();
            AddText(page, "Event and state monitor", 16, Color.Rgb(40, 65, 85));
            AddText(page, "Confirmed is raised after the threshold is reached. Reset allows another test.", 13, Color.Rgb(85, 96, 106));

            TextView state = new TextView(_context);
            state.Text = GetStatusText();
            state.TextSize = 14;
            state.SetTextColor(Color.Rgb(45, 70, 85));
            page.AddView(state);

            _eventLog.TextSize = 12;
            _eventLog.Typeface = Typeface.Monospace;
            _eventLog.SetTextColor(Color.Rgb(35, 55, 70));
            _eventLog.SetBackgroundColor(Color.Rgb(230, 235, 239));
            _eventLog.SetPadding(Dp(10), Dp(10), Dp(10), Dp(10));
            _eventLog.Text = _events.Length == 0 ? "No events yet." : _events.ToString();
            page.AddView(_eventLog);

            Button clear = MakeButton("Clear event log");
            clear.Click += delegate
            {
                _events.Length = 0;
                _eventLog.Text = "No events yet.";
            };
            page.AddView(clear);
            return page;
        }

        private void OnConfirmed(object sender, EventArgs e)
        {
            _status.Text = GetStatusText();
            AddEvent("Confirmed event raised. IsConfirmed = true.");
            Toast.MakeText(_context, "Confirmed event raised", ToastLength.Short).Show();
        }

        private string GetStatusText()
        {
            return "IsConfirmed: " + _slider.IsConfirmed +
                "   Progress: " + (int)(_slider.Progress * 100f) + "%";
        }

        private void AddEvent(string message)
        {
            _events.Append(DateTime.Now.ToString("HH:mm:ss"));
            _events.Append("  ");
            _events.AppendLine(message);
            _eventLog.Text = _events.ToString();
        }

        private string BuildSampleText()
        {
            return "var slider = new SlideConfirmView(this);\n" +
                "slider.ConfirmThreshold = " + _slider.ConfirmThreshold.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "f;\n" +
                "slider.PromptText = \"" + Escape(_slider.PromptText) + "\";\n" +
                "slider.ConfirmedText = \"" + Escape(_slider.ConfirmedText) + "\";\n\n" +
                "slider.Confirmed += delegate\n" +
                "{\n    CommitOperation();\n};\n\n" +
                "parentLayout.AddView(slider);\n\n" +
                "// Reuse it later:\nslider.Reset();";
        }

        private void UpdateSample()
        {
            if (_sampleCode != null)
                _sampleCode.Text = BuildSampleText();
        }

        private static string Escape(string value)
        {
            if (value == null) return String.Empty;
            return value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }

        private LinearLayout CreatePage()
        {
            LinearLayout page = new LinearLayout(_context);
            page.Orientation = Orientation.Vertical;
            page.SetPadding(Dp(16), Dp(16), Dp(16), Dp(24));
            return page;
        }

        private void AddText(LinearLayout page, string text, float size, Color color)
        {
            TextView view = new TextView(_context);
            view.Text = text;
            view.TextSize = size;
            view.SetTextColor(color);
            view.SetPadding(0, Dp(5), 0, Dp(5));
            page.AddView(view);
        }

        private Button MakeButton(string text)
        {
            Button button = new Button(_context);
            button.Text = text;
            button.SetAllCaps(false);
            return button;
        }

        private int Dp(int value)
        {
            return (int)(value * Resources.DisplayMetrics.Density + 0.5f);
        }
    }
}
