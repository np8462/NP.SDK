using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NP.SDK.UI.PersianControls.Validation;

namespace NP.SDK.UI.PersianControls.Controls
{
    /// <summary>
    /// Persian Date TextBox
    /// YYYY / MM / DD
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("PersianDate")]
    public partial class NPDateTextBox : UserControl
    {
        #region Fields

        private readonly PersianCalendar _persianCalendar;

        private bool _isCorrectDate;

        private string _persianDate;

        private DateTime _miladiDate;

        private Color _normalColor = Color.White;

        private Color _wrongColor = Color.MistyRose;

         
        #endregion

        #region Constructor

        public NPDateTextBox()
        {
            InitializeComponent();

            _persianCalendar = new PersianCalendar();

            InitializeTextBoxes();
        }

        #endregion

        #region Initialization

        private void InitializeTextBoxes()
        {
            //------------------------------------
            // Day
            //------------------------------------

            txtDay.InputMode = InputMode.Integer;
            txtDay.MaxLength = 2;
            txtDay.MaxValue = 31;

            //------------------------------------
            // Month
            //------------------------------------

            txtMonth.InputMode = InputMode.Integer;
            txtMonth.MaxLength = 2;
            txtMonth.MaxValue = 12;

            //------------------------------------
            // Year
            //------------------------------------

            txtYear.InputMode = InputMode.Integer;
            txtYear.MaxLength = 4;
            txtYear.MaxValue = 9378;

            //------------------------------------
            // Focus

            txtDay.NextControl = txtMonth;

            txtMonth.PreviousControl = txtDay;
            txtMonth.NextControl = txtYear;

            txtYear.PreviousControl = txtMonth;

            //------------------------------------
            // Events

            txtDay.TextChanged += txt_TextChanged;
            txtMonth.TextChanged += txt_TextChanged;
            txtYear.TextChanged += txt_TextChanged;
        }

        #endregion

        #region Appearance

        [Category("NP SDK")]
        [DefaultValue(typeof(Color), "White")]
        public Color NormalColor
        {
            get
            {
                return _normalColor;
            }
            set
            {
                _normalColor = value;

                txtDay.BackColor = value;
                txtMonth.BackColor = value;
                txtYear.BackColor = value;
            }
        }

        [Category("NP SDK")]
        [DefaultValue(typeof(Color), "MistyRose")]
        public Color WrongColor
        {
            get
            {
                return _wrongColor;
            }
            set
            {
                _wrongColor = value;
            }
        }

        #endregion

        #region Keyboard

        [Category("NP SDK")]
        [DefaultValue(KeyboardMode.System)]
        public KeyboardMode KeyboardMode
        {
            get
            {
                return txtDay.KeyboardMode;
            }
            set
            {
                txtDay.KeyboardMode = value;
                txtMonth.KeyboardMode = value;
                txtYear.KeyboardMode = value;
            }
        }

        [Category("NP SDK")]
        [DefaultValue(DigitMode.System)]
        public DigitMode DigitMode
        {
            get
            {
                return txtDay.DigitMode;
            }
            set
            {
                txtDay.DigitMode = value;
                txtMonth.DigitMode = value;
                txtYear.DigitMode = value;
            }
        }

        [Category("NP SDK")]
        [DefaultValue(true)]
        public bool ConvertKeyboard
        {
            get
            {
                return txtDay.ConvertKeyboard;
            }
            set
            {
                txtDay.ConvertKeyboard = value;
                txtMonth.ConvertKeyboard = value;
                txtYear.ConvertKeyboard = value;
            }
        }

        #endregion

        #region Navigation

        [Category("NP SDK")]
        [DefaultValue(null)]
        public Control NextFocusControl
        {
            get;
            set;
        }

        [Category("NP SDK")]
        [DefaultValue(null)]
        public Control PreviousFocusControl
        {
            get;
            set;
        }

        #endregion

        #region Behavior

        [Category("NP SDK")]
        [DefaultValue(false)]
        public bool IsFillSystemDate
        {
            get;
            set;
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtDay.PreviousControl = PreviousFocusControl;
            txtYear.NextControl = NextFocusControl;

            if (IsFillSystemDate)
            {
                DateTime now = DateTime.Now;

                txtYear.Text = _persianCalendar.GetYear(now).ToString();
                txtMonth.Text = _persianCalendar.GetMonth(now).ToString();
                txtDay.Text = _persianCalendar.GetDayOfMonth(now).ToString();
            }
            else
            {
                Clear();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (!ContainsFocus)
                txtDay.Focus();
        }

        #endregion

        #region Properties

        [Browsable(false)]
        public bool IsCorrectDate
        {
            get
            {
                ValidateDate();

                return _isCorrectDate;
            }
        }

        [Browsable(false)]
        public string PersianYear
        {
            get
            {
                return txtYear.Text;
            }
        }

        [Browsable(false)]
        public string PersianMonth
        {
            get
            {
                return txtMonth.Text;
            }
        }

        [Browsable(false)]
        public string PersianDay
        {
            get
            {
                return txtDay.Text;
            }
        }

        [Browsable(false)]
        public DateTime MiladiDate
        {
            get
            {
                ValidateDate();

                if (_isCorrectDate)
                {
                    return _persianCalendar.ToDateTime(
                        Int32.Parse(txtYear.Text),
                        Int32.Parse(txtMonth.Text),
                        Int32.Parse(txtDay.Text),
                        0,
                        0,
                        0,
                        0);
                }

                return _miladiDate;
            }

            set
            {
                if (value < _persianCalendar.MinSupportedDateTime ||
                    value > _persianCalendar.MaxSupportedDateTime)
                {
                    return;
                }
                _miladiDate = value;

                txtYear.Text =
                    _persianCalendar.GetYear(value).ToString();

                txtMonth.Text =
                    _persianCalendar.GetMonth(value).ToString();

                txtDay.Text =
                    _persianCalendar.GetDayOfMonth(value).ToString();
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [Category("NP SDK")]
        public string PersianDate
        {
            get
            {
                ValidateDate();

                if (_isCorrectDate)
                {
                    return String.Format(
                        "{0:0000}/{1:00}/{2:00}",
                        Int32.Parse(txtYear.Text),
                        Int32.Parse(txtMonth.Text),
                        Int32.Parse(txtDay.Text));
                }

                return _persianDate;
            }

            set
            {
                _persianDate = value;

                if (String.IsNullOrWhiteSpace(value))
                {
                    Clear();
                    return;
                }

                string[] parts =
                    value.Split('/');

                if (parts.Length != 3)
                {
                    Clear();
                    return;
                }

                txtYear.Text = parts[0].Trim();
                txtMonth.Text = parts[1].Trim();
                txtDay.Text = parts[2].Trim();

                ValidateDate();
            }
        }

        public override string Text
        {
            get
            {
                return PersianDate;
            }

            set
            {
                PersianDate = value;
            }
        }

        #endregion

        #region Events

        private void txt_TextChanged(object sender, EventArgs e)
        {
            ValidateDate();

            OnTextChanged(EventArgs.Empty);
        }

        private void txtDay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                return;

            if (txtDay.TextLength == txtDay.MaxLength)
                txtMonth.Focus();
        }

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
                return;

            if (txtMonth.SelectionStart == 0 &&
                txtMonth.TextLength == 0)
            {
                txtDay.Focus();
            }
        }

        private void txtMonth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                return;

            if (txtMonth.TextLength == txtMonth.MaxLength)
                txtYear.Focus();
        }

        private void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
                return;

            if (txtYear.SelectionStart == 0 &&
                txtYear.TextLength == 0)
            {
                txtMonth.Focus();
            }
        }

        #endregion

        #region Private Methods

        private void ValidateDate()
        {
            _isCorrectDate = false;

            if (String.IsNullOrWhiteSpace(txtYear.Text) ||
                String.IsNullOrWhiteSpace(txtMonth.Text) ||
                String.IsNullOrWhiteSpace(txtDay.Text))
            {
                UpdateColors();
                return;
            }

            int year;
            int month;
            int day;

            if (!Int32.TryParse(txtYear.Text, out year) ||
                !Int32.TryParse(txtMonth.Text, out month) ||
                !Int32.TryParse(txtDay.Text, out day))
            {
                UpdateColors();
                return;
            }

            try
            {
                _persianCalendar.ToDateTime(
                    year,
                    month,
                    day,
                    0,
                    0,
                    0,
                    0);

                _isCorrectDate = true;

                _persianDate =
                    String.Format(
                        "{0:0000}/{1:00}/{2:00}",
                        year,
                        month,
                        day);

                _miladiDate =
                    _persianCalendar.ToDateTime(
                        year,
                        month,
                        day,
                        0,
                        0,
                        0,
                        0);
            }
            catch
            {
                _isCorrectDate = false;
            }

            UpdateColors();
        }

        private void UpdateColors()
        {
            txtYear.BackColor =
                txtYear.TextLength == 4
                    ? NormalColor
                    : WrongColor;

            txtMonth.BackColor =
                txtMonth.TextLength > 0
                    ? NormalColor
                    : WrongColor;

            txtDay.BackColor =
                txtDay.TextLength > 0
                    ? NormalColor
                    : WrongColor;

            if (!_isCorrectDate)
            {
                txtDay.BackColor = WrongColor;
            }
        }

        private void Clear()
        {
            txtYear.Clear();
            txtMonth.Clear();
            txtDay.Clear();

            _persianDate = String.Empty;
            _isCorrectDate = false;
        }

        public void FillSystemDate()
        {
            DateTime now = DateTime.Now;

            txtYear.Text =
                _persianCalendar.GetYear(now).ToString();

            txtMonth.Text =
                _persianCalendar.GetMonth(now).ToString();

            txtDay.Text =
                _persianCalendar.GetDayOfMonth(now).ToString();

            ValidateDate();
        }

        #endregion
    }
}