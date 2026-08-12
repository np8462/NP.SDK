using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using NP.SDK.UI.PersianControls.Internal;
using NP.SDK.UI.PersianControls.Validation;

namespace NP.SDK.UI.PersianControls.Controls
{
    /// <summary>
    /// Money input control designed for Iranian and
    /// international currency values.
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    public partial class NPMoneyTextBox : UserControl
    {
        private readonly NPPersianTextBox _textBox;
        private readonly Label _currencyLeft;
        private readonly Label _currencyRight;

        private NPCurrency _currency = NPCurrency.Toman;
        private string _customCurrencyName = String.Empty;

        private bool _showCurrency = true;
        private bool _useThousandsSeparator = true;

        private bool _formatting;

        public NPMoneyTextBox()
        {
            _textBox = new NPPersianTextBox();
            _currencyLeft = new Label();
            _currencyRight = new Label();

            // -----------------------------------------
            // Default Money Settings
            // -----------------------------------------

            _textBox.InputMode = InputMode.Decimal;
            _textBox.DecimalPlaces = 2;
            _textBox.DigitMode = DigitMode.System;
            _textBox.MaxValue = Decimal.MaxValue;

            InitializeControl();
        }

        #region Initialize

        private void InitializeControl()
        {
            SuspendLayout();

            // -----------------------------------------
            // Main Control
            // -----------------------------------------

            Height = 26;

            BorderStyle =
                BorderStyle.FixedSingle;

            // -----------------------------------------
            // Left Currency Label
            // -----------------------------------------

            _currencyLeft.AutoSize = false;

            _currencyLeft.Width = 55;

            _currencyLeft.Dock =
                DockStyle.Left;

            _currencyLeft.TextAlign =
                ContentAlignment.MiddleLeft;

            _currencyLeft.Padding =
                new Padding(4, 0, 4, 0);

            // -----------------------------------------
            // Right Currency Label
            // -----------------------------------------

            _currencyRight.AutoSize = false;

            _currencyRight.Width = 0;

            _currencyRight.Text =
                String.Empty;

            _currencyRight.Visible =
                false;

            // -----------------------------------------
            // Main TextBox
            // -----------------------------------------

            _textBox.BorderStyle =
                BorderStyle.None;

            _textBox.TextAlign =
                HorizontalAlignment.Right;

            _textBox.InputMode =
                InputMode.Decimal;

            _textBox.DecimalPlaces =
                2;

            _textBox.DigitMode =
                DigitMode.System;

            _textBox.MaxValue =
                Decimal.MaxValue;

            // -----------------------------------------
            // Position
            // -----------------------------------------

            _textBox.Top = 3;

            _textBox.Left =
                _currencyLeft.Width + 2;

            _textBox.Width =
                Math.Max(
                    10,
                    Width -
                    _currencyLeft.Width -
                    4);

            _textBox.Height =
                Math.Max(
                    10,
                    Height - 6);

            _textBox.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            // -----------------------------------------
            // Events
            // -----------------------------------------

            _textBox.TextChanged +=
                TextBox_TextChanged;

            _textBox.KeyPress += _textBox_KeyPress;
            // -----------------------------------------
            // Add Controls
            // -----------------------------------------

            Controls.Add(_textBox);
            Controls.Add(_currencyRight);
            Controls.Add(_currencyLeft);

            UpdateCurrencyLabels();

            ResumeLayout(false);
            PerformLayout();
        }

        void _textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '.' && (DecimalPlaces == 0 || DecimalPlaces < 1 || DecimalPlaces == null))
                    e.Handled = true;           
                }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Currency

        [Category("Money")]
        [Description(
            "Currency used by the money control.")]
        [DefaultValue(NPCurrency.Toman)]
        public NPCurrency Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;

                UpdateCurrencyLabels();
            }
        }

        [Category("Money")]
        [Description(
            "Custom currency name used when Currency is set to Custom.")]
        [DefaultValue("")]
        public string CustomCurrencyName
        {
            get
            {
                return _customCurrencyName;
            }
            set
            {
                _customCurrencyName =
                    value ?? String.Empty;

                UpdateCurrencyLabels();
            }
        }

        [Category("Money")]
        [Description(
            "Determines whether the currency name is displayed.")]
        [DefaultValue(true)]
        public bool ShowCurrency
        {
            get
            {
                return _showCurrency;
            }
            set
            {
                _showCurrency = value;

                UpdateCurrencyLabels();
            }
        }

        private void UpdateCurrencyLabels()
        {
            string currencyText =
                GetCurrencyText();

            // Currency is intentionally displayed
            // on the LEFT side.

            _currencyLeft.Text =
                _showCurrency
                    ? currencyText
                    : String.Empty;

            _currencyLeft.Visible =
                _showCurrency &&
                !String.IsNullOrEmpty(currencyText);

            // Right side is currently unused.

            _currencyRight.Text =
                String.Empty;

            _currencyRight.Visible =
                false;
        }

        private string GetCurrencyText()
        {
            switch (_currency)
            {
                case NPCurrency.Rial:
                    return "ریال";

                case NPCurrency.Toman:
                    return "تومان";

                case NPCurrency.Dollar:
                    return "دلار";

                case NPCurrency.Euro:
                    return "یورو";

                case NPCurrency.Pound:
                    return "پوند";

                case NPCurrency.Dirham:
                    return "درهم";

                case NPCurrency.Custom:

                    return _customCurrencyName;

                default:
                    return String.Empty;
            }
        }

        #endregion

        #region Value

        [Category("Money")]
        [Description(
            "Gets or sets the numeric money value.")]
        public decimal Value
        {
            get
            {
                decimal value;

                string text =
                    PersianKeyboardMapper.NormalizeDigits(
                        _textBox.Text);

                // Remove thousands separators
                // before parsing.

                text =
                    text.Replace(",", "");

                if (Decimal.TryParse(
                    text,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out value))
                {
                    return value;
                }

                return 0m;
            }

            set
            {
                if (value < 0)
                    value = 0;

                if (value > MaxValue)
                    value = MaxValue;

                string format =
                    _useThousandsSeparator
                        ? "N" + DecimalPlaces
                        : "F" + DecimalPlaces;

                _textBox.Text =
                    value.ToString(
                        format,
                        CultureInfo.InvariantCulture);

                _textBox.SelectionStart =
                    _textBox.Text.Length;
            }
        }

        #endregion

        #region Formatting

        [Category("Money")]
        [Description(
            "Determines whether thousands separators are displayed.")]
        [DefaultValue(true)]
        public bool UseThousandsSeparator
        {
            get
            {
                return _useThousandsSeparator;
            }

            set
            {
                if (_useThousandsSeparator == value)
                    return;

                _useThousandsSeparator = value;

                FormatCurrentValue();
            }
        }

        [Category("Money")]
        [Description(
            "Number of digits allowed after the decimal point.")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get
            {
                return _textBox.DecimalPlaces;
            }

            set
            {
                if (value < 0)
                    value = 0;

                if (value > 28)
                    value = 28;

                _textBox.DecimalPlaces =
                    value;

                FormatCurrentValue();
            }
        }

        private void TextBox_TextChanged(
            object sender,
            EventArgs e)
        {
            if (_formatting)
                return;

            if (!_useThousandsSeparator)
                return;

            FormatCurrentValue();
        }

        /// <summary>
        /// Formats only the integer part of the
        /// currently entered value.
        ///
        /// Important:
        /// This method does NOT convert the complete
        /// text to Decimal. Therefore values such as
        /// "1234." remain valid while typing.
        /// </summary>
        private void FormatCurrentValue()
        {
            if (_formatting)
                return;

            string originalText =
                PersianKeyboardMapper.NormalizeDigits(
                    _textBox.Text);

            if (String.IsNullOrEmpty(originalText))
                return;

            // -----------------------------------------
            // Preserve caret logically
            // -----------------------------------------

            int oldCaret =
                _textBox.SelectionStart;

            if (oldCaret < 0)
                oldCaret = 0;

            if (oldCaret > originalText.Length)
                oldCaret = originalText.Length;

            // Count actual numeric digits before caret.
            int digitsBeforeCaret =
                CountDigits(
                    originalText,
                    oldCaret);

            // -----------------------------------------
            // Remove existing separators
            // -----------------------------------------

            string cleanText =
                originalText.Replace(",", "");

            // -----------------------------------------
            // Find decimal point
            // -----------------------------------------

            int decimalIndex =
                cleanText.IndexOf('.');

            string integerPart;
            string decimalPart = String.Empty;

            bool hasDecimalPoint =
                decimalIndex >= 0;

            if (hasDecimalPoint)
            {
                integerPart =
                    cleanText.Substring(
                        0,
                        decimalIndex);

                if (decimalIndex + 1 <
                    cleanText.Length)
                {
                    decimalPart =
                        cleanText.Substring(
                            decimalIndex + 1);
                }
            }
            else
            {
                integerPart =
                    cleanText;
            }

            // -----------------------------------------
            // Empty integer part
            // -----------------------------------------

            if (String.IsNullOrEmpty(integerPart))
                integerPart = "0";

            // -----------------------------------------
            // Validate integer part
            // -----------------------------------------

            decimal integerValue;

            if (!Decimal.TryParse(
                integerPart,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out integerValue))
            {
                return;
            }

            // -----------------------------------------
            // Format integer part
            // -----------------------------------------

            string formattedInteger;

            if (_useThousandsSeparator)
            {
                formattedInteger =
                    integerValue.ToString(
                        "N0",
                        CultureInfo.InvariantCulture);
            }
            else
            {
                formattedInteger =
                    integerValue.ToString(
                        "0",
                        CultureInfo.InvariantCulture);
            }

            // -----------------------------------------
            // Preserve decimal point
            // -----------------------------------------

            string result =
                formattedInteger;

            if (hasDecimalPoint)
            {
                result += ".";

                // Do NOT automatically add zeros here.
                //
                // This is important:
                //
                // 1234.
                //
                // must remain:
                //
                // 1,234.
                //
                // while the user is typing.

                result += decimalPart;
            }

            // -----------------------------------------
            // Nothing changed
            // -----------------------------------------

            if (result == originalText)
                return;

            // -----------------------------------------
            // Apply formatting
            // -----------------------------------------

            _formatting = true;

            try
            {
                _textBox.Text =
                    result;

                // -------------------------------------
                // Restore caret based on digit position
                // -------------------------------------

                int newCaret =
                    FindCaretPosition(
                        result,
                        digitsBeforeCaret,
                        hasDecimalPoint,
                        oldCaret,
                        originalText);

                _textBox.SelectionStart =
                    Math.Max(
                        0,
                        Math.Min(
                            newCaret,
                            result.Length));
            }
            finally
            {
                _formatting = false;
            }
        }

        /// <summary>
        /// Counts numeric digits before the specified
        /// caret position.
        /// </summary>
        private int CountDigits(
            string text,
            int caret)
        {
            int count = 0;

            for (int i = 0;
                 i < Math.Min(caret, text.Length);
                 i++)
            {
                if (Char.IsDigit(text[i]))
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Finds the new caret position after
        /// thousands separators have been inserted.
        /// </summary>
        private int FindCaretPosition(
            string formattedText,
            int digitsBeforeCaret,
            bool hasDecimalPoint,
            int oldCaret,
            string oldText)
        {
            if (digitsBeforeCaret == 0)
                return 0;

            int digitCount = 0;

            for (int i = 0;
                 i < formattedText.Length;
                 i++)
            {
                if (Char.IsDigit(formattedText[i]))
                {
                    digitCount++;

                    if (digitCount ==
                        digitsBeforeCaret)
                    {
                        return i + 1;
                    }
                }
            }

            return formattedText.Length;
        }

        /// <summary>
        /// Applies the selected DigitMode to the
        /// existing text.
        /// </summary>
        private void ApplyDigitMode()
        {
            if (String.IsNullOrEmpty(
                _textBox.Text))
            {
                return;
            }

            int caret =
                _textBox.SelectionStart;

            string text =
                _textBox.Text;

            switch (_textBox.DigitMode)
            {
                case DigitMode.Persian:

                    text =
                        PersianKeyboardMapper.ToPersianDigits(
                            PersianKeyboardMapper.NormalizeDigits(
                                text));

                    break;

                case DigitMode.English:

                    text =
                        PersianKeyboardMapper.ToEnglishDigits(
                            text);

                    break;

                case DigitMode.System:

                    break;
            }

            _formatting = true;

            try
            {
                _textBox.Text = text;

                _textBox.SelectionStart =
                    Math.Min(
                        caret,
                        text.Length);
            }
            finally
            {
                _formatting = false;
            }

            // Re-apply formatting after digit conversion.

            if (_useThousandsSeparator)
                FormatCurrentValue();
        }

        private void RefreshValue()
        {
            if (String.IsNullOrEmpty(
                _textBox.Text))
            {
                return;
            }

            decimal value =
                Value;

            if (value > MaxValue)
                value = MaxValue;

            string format =
                _useThousandsSeparator
                    ? "N" + DecimalPlaces
                    : "F" + DecimalPlaces;

            _formatting = true;

            try
            {
                _textBox.Text =
                    value.ToString(
                        format,
                        CultureInfo.InvariantCulture);

                _textBox.SelectionStart =
                    _textBox.Text.Length;
            }
            finally
            {
                _formatting = false;
            }
        }

        #endregion

        #region Persian TextBox Properties

        [Browsable(false)]
        [EditorBrowsable(
            EditorBrowsableState.Never)]
        public new InputMode InputMode
        {
            get
            {
                return InputMode.Decimal;
            }
        }

        [Category("Money")]
        [Description(
            "Specifies how digits are displayed.")]
        [DefaultValue(DigitMode.System)]
        public new DigitMode DigitMode
        {
            get
            {
                return _textBox.DigitMode;
            }

            set
            {
                _textBox.DigitMode =
                    value;

                ApplyDigitMode();
            }
        }

        [Category("Money")]
        [Description(
            "Maximum allowed money value.")]
        [DefaultValue(typeof(decimal), "79228162514264337593543950335")]
        public decimal MaxValue
        {
            get
            {
                return _textBox.MaxValue;
            }

            set
            {
                if (value < 0)
                    value = 0;

                _textBox.MaxValue =
                    value;

                if (Value > value)
                    Value = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(
            EditorBrowsableState.Never)]
        public KeyboardMode KeyboardMode
        {
            get
            {
                return _textBox.KeyboardMode;
            }

            set
            {
                _textBox.KeyboardMode =
                    value;
            }
        }

        [Category("Money")]
        [Description(
            "Determines whether keyboard layout conversion is enabled.")]
        [DefaultValue(true)]
        public bool ConvertKeyboard
        {
            get
            {
                return _textBox.ConvertKeyboard;
            }

            set
            {
                _textBox.ConvertKeyboard =
                    value;
            }
        }

        [Category("Money")]
        public Control NextControl
        {
            get
            {
                return _textBox.NextControl;
            }

            set
            {
                _textBox.NextControl =
                    value;
            }
        }

        [Category("Money")]
        public Control PreviousControl
        {
            get
            {
                return _textBox.PreviousControl;
            }

            set
            {
                _textBox.PreviousControl =
                    value;
            }
        }

        #endregion

        #region NPCurrency

        /// <summary>
        /// Available currency types.
        /// </summary>
        public enum NPCurrency
        {
            Rial,
            Toman,
            Dollar,
            Euro,
            Pound,
            Dirham,
            Custom
        }

        #endregion
    }
}