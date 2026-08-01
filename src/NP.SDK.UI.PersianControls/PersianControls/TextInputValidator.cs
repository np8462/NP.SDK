using System;
using System.Globalization;
using NP.SDK.UI.PersianControls.Internal;

namespace NP.SDK.UI.PersianControls.Validation
{
    /// <summary>
    /// Specifies allowed input mode.
    /// </summary>
    public enum InputMode
    {
        Any,

        Text,

        Integer,

        Decimal,

        AlphaNumeric
    }

    /// <summary>
    /// Specifies keyboard conversion mode.
    /// </summary>
    public enum KeyboardMode
    {
        System,

        Persian,

        English
    }

    /// <summary>
    /// Specifies how digits are displayed.
    /// </summary>
    public enum DigitMode
    {
        System,

        Persian,

        English
    }

    /// <summary>
    /// Performs validation for Persian controls.
    /// </summary>
    internal static class TextInputValidator
    {
        /// <summary>
        /// Validates the final text after inserting a character.
        /// </summary>
        public static bool IsInputAllowed(
            string finalText,
            InputMode inputMode,
            int decimalPlaces,
            decimal maxValue)
        {
            if (String.IsNullOrEmpty(finalText))
                return true;

            finalText =
                PersianKeyboardMapper.NormalizeDigits(finalText);

            switch (inputMode)
            {
                case InputMode.Any:
                    return true;

                case InputMode.Text:
                    return ValidateText(finalText);

                case InputMode.AlphaNumeric:
                    return ValidateAlphaNumeric(finalText);

                case InputMode.Integer:
                    return ValidateInteger(
                        finalText,
                        maxValue);

                case InputMode.Decimal:
                    return ValidateDecimal(
                        finalText,
                        decimalPlaces,
                        maxValue);

                default:
                    return true;
            }
        }

        #region Validation

        private static bool ValidateText(string text)
        {
            foreach (char c in text)
            {
                if (Char.IsLetter(c))
                    continue;

                if (Char.IsWhiteSpace(c))
                    continue;

                if (Char.IsPunctuation(c))
                    continue;

                return false;
            }

            return true;
        }

        private static bool ValidateAlphaNumeric(string text)
        {
            foreach (char c in text)
            {
                if (Char.IsLetterOrDigit(c))
                    continue;

                if (Char.IsWhiteSpace(c))
                    continue;

                if (Char.IsPunctuation(c))
                    continue;

                return false;
            }

            return true;
        }

        private static bool ValidateInteger(
            string text,
            decimal maxValue)
        {
            decimal value;

            if (!Decimal.TryParse(
                    text,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out value))
            {
                return false;
            }

            return value <= maxValue;
        }

        private static bool ValidateDecimal(
            string text,
            int decimalPlaces,
            decimal maxValue)
        {
            decimal value;

            if (!Decimal.TryParse(
                    text,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out value))
            {
                return false;
            }

            if (value > maxValue)
                return false;

            int pointIndex =
                text.IndexOf('.');

            if (pointIndex >= 0)
            {
                int digits =
                    text.Length - pointIndex - 1;

                if (digits > decimalPlaces)
                    return false;
            }

            return true;
        }

        #endregion
    }
}