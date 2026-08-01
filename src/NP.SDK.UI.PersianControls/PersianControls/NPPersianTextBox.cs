using System;
using System.ComponentModel;
using System.Windows.Forms;
using NP.SDK.UI.PersianControls.Internal;
using NP.SDK.UI.PersianControls.Validation;

namespace NP.SDK.UI.PersianControls.Controls
{
    /// <summary>
    /// Persian TextBox with keyboard conversion,
    /// numeric validation and focus navigation.
    /// </summary>
    [ToolboxItem(true)]
    public class NPPersianTextBox : TextBox
    {
        private bool _shiftPressed;

        public NPPersianTextBox()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            InputMode = InputMode.Any;
            KeyboardMode = KeyboardMode.System;
            DigitMode = DigitMode.System;

            ConvertKeyboard = true;

            DecimalPlaces = 2;
            MaxValue = Decimal.MaxValue;

            MoveToNextControlOnEnter = true;
            MoveToPreviousControlOnBackspace = true;
        }

        #region Properties

        [Category("NP SDK")]
        public InputMode InputMode { get; set; }

        [Category("NP SDK")]
        public KeyboardMode KeyboardMode { get; set; }

        [Category("NP SDK")]
        public DigitMode DigitMode { get; set; }

        [Category("NP SDK")]
        public bool ConvertKeyboard { get; set; }

        [Category("NP SDK")]
        public decimal MaxValue { get; set; }

        [Category("NP SDK")]
        public int DecimalPlaces { get; set; }

        [Category("NP SDK")]
        public bool MoveToNextControlOnEnter { get; set; }

        [Category("NP SDK")]
        public bool MoveToPreviousControlOnBackspace { get; set; }

        [Category("NP SDK")]
        public Control NextControl { get; set; }

        [Category("NP SDK")]
        public Control PreviousControl { get; set; }

        #endregion

        #region Keyboard

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _shiftPressed = e.Shift;

            if (MoveToNextControlOnEnter &&
                e.KeyCode == Keys.Enter &&
                NextControl != null &&
                NextControl.CanFocus)
            {
                NextControl.Focus();
                e.SuppressKeyPress = true;
                return;
            }

            if (MoveToPreviousControlOnBackspace &&
                e.KeyCode == Keys.Back &&
                TextLength == 0 &&
                PreviousControl != null &&
                PreviousControl.CanFocus)
            {
                PreviousControl.Focus();
                e.SuppressKeyPress = true;
                return;
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar))
            {
                //-------------------------------------------------
                // Keyboard conversion
                //-------------------------------------------------

                if (ConvertKeyboard)
                {
                    switch (KeyboardMode)
                    {
                        case KeyboardMode.Persian:

                            e.KeyChar =
                                PersianKeyboardMapper.ToPersian(
                                    e.KeyChar,
                                    _shiftPressed);

                            break;

                        case KeyboardMode.English:

                            e.KeyChar =
                                PersianKeyboardMapper.ToEnglish(
                                    e.KeyChar);

                            break;
                    }
                }

                //-------------------------------------------------
                // Build final text
                //-------------------------------------------------

                string finalText =
                    Text.Remove(
                        SelectionStart,
                        SelectionLength);

                finalText =
                    finalText.Insert(
                        SelectionStart,
                        e.KeyChar.ToString());

                //-------------------------------------------------
                // Validation
                //-------------------------------------------------

                bool valid =
                    TextInputValidator.IsInputAllowed(
                        finalText,
                        InputMode,
                        DecimalPlaces,
                        MaxValue);

                if (!valid)
                {
                    e.Handled = true;
                    return;
                }
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (DigitMode == DigitMode.System)
                return;

            int caret = SelectionStart;

            switch (DigitMode)
            {
                case DigitMode.Persian:

                    Text =
                        PersianKeyboardMapper.ToPersianDigits(Text);

                    break;

                case DigitMode.English:

                    Text =
                        PersianKeyboardMapper.ToEnglishDigits(Text);

                    break;
            }

            SelectionStart = Math.Min(caret, Text.Length);
        }

        #endregion
    }
}