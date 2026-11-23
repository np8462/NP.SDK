using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace NP.SDK.Android.Controls
{
    /// <summary>
    /// A lightweight, dependency-free slide-to-confirm control.
    /// The Confirmed event is raised once when the thumb reaches the threshold.
    /// </summary>
    public class SlideConfirmView : View
    {
        private readonly Paint _paint = new Paint(PaintFlags.AntiAlias);
        private readonly RectF _track = new RectF();
        private float _progress;
        private bool _dragging;
        private bool _confirmed;
        private float _confirmThreshold;
        private string _promptText;
        private string _confirmedText;

        public event EventHandler Confirmed;
        public event EventHandler ProgressChanged;\n        public event EventHandler ProgressChanged;

        public float ConfirmThreshold
        {
            get { return _confirmThreshold; }
            set
            {
                _confirmThreshold = Clamp(value);
                Invalidate();
            }
        }

        public string PromptText
        {
            get { return _promptText; }
            set { _promptText = value ?? String.Empty; Invalidate(); }
        }

        public string ConfirmedText
        {
            get { return _confirmedText; }
            set { _confirmedText = value ?? String.Empty; Invalidate(); }
        }

        public SlideConfirmView(Context context) : base(context)
        {
            Initialize();
        }

        public SlideConfirmView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            Clickable = true;
            _confirmThreshold = 0.85f;
            _promptText = "Slide to commit";
            _confirmedText = "Confirmed";
            SetBackgroundColor(Color.Transparent);
        }

        public bool IsConfirmed { get { return _confirmed; } }
        public float Progress { get { return _progress; } }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int desiredHeight = (int)(56 * Resources.DisplayMetrics.Density);
            int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
            int height = ResolveSize(desiredHeight, heightMeasureSpec);
            SetMeasuredDimension(width, height);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            float width = Width;
            float height = Height;
            if (width <= 0 || height <= 0) return;

            float radius = height / 2f;
            _track.Set(0, 0, width, height);
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = Color.Rgb(225, 230, 235);
            canvas.DrawRoundRect(_track, radius, radius, _paint);

            _paint.Color = Color.Rgb(53, 125, 96);
            float fillRight = height + (_progress * Math.Max(0, width - height));
            RectF fill = new RectF(0, 0, fillRight, height);
            canvas.DrawRoundRect(fill, radius, radius, _paint);

            _paint.Color = Color.Rgb(55, 65, 75);
            _paint.TextAlign = Paint.Align.Center;
            _paint.TextSize = height * 0.27f;
            string label = _confirmed ? _confirmedText : _promptText;
            canvas.DrawText(label, width / 2f,
                height / 2f - (_paint.Ascent() + _paint.Descent()) / 2f, _paint);

            float thumbX = radius + (_progress * Math.Max(0, width - height));
            _paint.Color = Color.White;
            canvas.DrawCircle(thumbX, radius, Math.Max(1, radius - 4), _paint);
            _paint.Color = Color.Rgb(45, 60, 70);
            _paint.TextSize = height * 0.52f;
            canvas.DrawText("›", thumbX,
                radius - (_paint.Ascent() + _paint.Descent()) / 2f, _paint);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (_confirmed) return true;
            float height = Height;
            float width = Width;
            if (height <= 0 || width <= height) return false;

            float radius = height / 2f;
            float thumbX = radius + (_progress * (width - height));
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    if (Math.Abs(e.GetX() - thumbX) > radius + 12) return false;
                    _dragging = true;
                    Parent.RequestDisallowInterceptTouchEvent(true);
                    return true;
                case MotionEventActions.Move:
                    if (!_dragging) return false;
                    _progress = Clamp((e.GetX() - radius) / (width - height));
                    Invalidate();
                    RaiseProgressChanged();
                    return true;
                case MotionEventActions.Up:
                    if (!_dragging) return false;
                    _dragging = false;
                    Parent.RequestDisallowInterceptTouchEvent(false);
                    if (_progress >= _confirmThreshold)
                    {
                        _progress = 1f;
                        _confirmed = true;
                        Invalidate();
                        RaiseProgressChanged();
                        EventHandler handler = Confirmed;
                        if (handler != null) handler(this, EventArgs.Empty);
                    }
                    else
                    {
                        _progress = 0f;
                        Invalidate();
                        RaiseProgressChanged();
                    }
                    return true;
                case MotionEventActions.Cancel:
                    _dragging = false;
                    Parent.RequestDisallowInterceptTouchEvent(false);
                    _progress = 0f;
                    Invalidate();
                    RaiseProgressChanged();
                    return true;
            }
            return true;
        }

        private static float Clamp(float value)
        {
            return Math.Max(0f, Math.Min(1f, value));
        }

        private void RaiseProgressChanged()
        {
            EventHandler handler = ProgressChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void Reset()
        {
            _dragging = false;
            _confirmed = false;
            _progress = 0f;
            Invalidate();
            RaiseProgressChanged();
        }
    }
}
