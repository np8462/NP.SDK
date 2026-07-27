using System;
using System.Collections.Generic;

namespace NP.SDK.UI.PersianControls.Internal
{
    /// <summary>
    /// Provides keyboard layout conversion utilities
    /// between English and Persian keyboards.
    /// </summary>
    internal static class PersianKeyboardMapper
    {
        #region Fields

        private static readonly Dictionary<char, char> _englishToPersian =
            new Dictionary<char, char>();

        private static readonly Dictionary<char, char> _persianToEnglish =
            new Dictionary<char, char>();

        private static readonly Dictionary<char, char> _englishShiftToPersian =
            new Dictionary<char, char>();

        #endregion

        #region Constructor

        static PersianKeyboardMapper()
        {
            InitializeMappings();
        }

        #endregion

        #region Initialization

        private static void InitializeMappings()
        {
            //-------------------------
            // Letters
            //-------------------------

            Add('q', 'ض');
            Add('w', 'ص');
            Add('e', 'ث');
            Add('r', 'ق');
            Add('t', 'ف');
            Add('y', 'غ');
            Add('u', 'ع');
            Add('i', 'ه');
            Add('o', 'خ');
            Add('p', 'ح');

            Add('[', 'ج');
            Add(']', 'چ');

            Add('a', 'ش');
            Add('s', 'س');
            Add('d', 'ی');
            Add('f', 'ب');
            Add('g', 'ل');
            Add('h', 'ا');
            Add('j', 'ت');
            Add('k', 'ن');
            Add('l', 'م');

            Add(';', 'ک');
            Add('\'', 'گ');

            Add('z', 'ظ');
            Add('x', 'ط');
            Add('c', 'ز');
            Add('v', 'ر');
            Add('b', 'ذ');
            Add('n', 'د');
            Add('m', 'پ');

            Add(',', 'و');
            Add('.', '.');
            Add('/', '/');

            //-------------------------
            // Digits
            //-------------------------

            Add('0', '۰');
            Add('1', '۱');
            Add('2', '۲');
            Add('3', '۳');
            Add('4', '۴');
            Add('5', '۵');
            Add('6', '۶');
            Add('7', '۷');
            Add('8', '۸');
            Add('9', '۹');

            //-------------------------
            // Shift Keys
            //-------------------------

            _englishShiftToPersian.Add('H', 'آ');
            _englishShiftToPersian.Add('C', 'ژ');
        }

        private static void Add(char english, char persian)
        {
            _englishToPersian[english] = persian;

            if (!_persianToEnglish.ContainsKey(persian))
                _persianToEnglish.Add(persian, english);
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts one English keyboard character to Persian.
        /// </summary>
        public static char ToPersian(
            char value,
            bool shiftPressed)
        {
            if (shiftPressed)
            {
                char shiftResult;

                if (_englishShiftToPersian.TryGetValue(
                    Char.ToUpper(value),
                    out shiftResult))
                {
                    return shiftResult;
                }
            }

            char result;

            if (_englishToPersian.TryGetValue(
                Char.ToLower(value),
                out result))
            {
                return result;
            }

            return value;
        }

        /// <summary>
        /// Converts one Persian keyboard character to English.
        /// </summary>
        public static char ToEnglish(char value)
        {
            char result;

            if (_persianToEnglish.TryGetValue(
                value,
                out result))
            {
                return result;
            }

            return value;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Determines whether the specified character
        /// is a Persian Unicode character.
        /// </summary>
        public static bool IsPersianCharacter(char value)
        {
            return value >= '\u0600' &&
                   value <= '\u06FF';
        }

        /// <summary>
        /// Converts Persian and Arabic digits
        /// to English digits.
        /// </summary>
        public static string NormalizeDigits(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                switch (chars[i])
                {
                    // Persian
                    case '۰': chars[i] = '0'; break;
                    case '۱': chars[i] = '1'; break;
                    case '۲': chars[i] = '2'; break;
                    case '۳': chars[i] = '3'; break;
                    case '۴': chars[i] = '4'; break;
                    case '۵': chars[i] = '5'; break;
                    case '۶': chars[i] = '6'; break;
                    case '۷': chars[i] = '7'; break;
                    case '۸': chars[i] = '8'; break;
                    case '۹': chars[i] = '9'; break;

                    // Arabic
                    case '٠': chars[i] = '0'; break;
                    case '١': chars[i] = '1'; break;
                    case '٢': chars[i] = '2'; break;
                    case '٣': chars[i] = '3'; break;
                    case '٤': chars[i] = '4'; break;
                    case '٥': chars[i] = '5'; break;
                    case '٦': chars[i] = '6'; break;
                    case '٧': chars[i] = '7'; break;
                    case '٨': chars[i] = '8'; break;
                    case '٩': chars[i] = '9'; break;
                }
            }

            return new string(chars);
        }

        /// <summary>
        /// Converts English digits to Persian digits.
        /// </summary>
        public static string ToPersianDigits(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            return text
                .Replace('0', '۰')
                .Replace('1', '۱')
                .Replace('2', '۲')
                .Replace('3', '۳')
                .Replace('4', '۴')
                .Replace('5', '۵')
                .Replace('6', '۶')
                .Replace('7', '۷')
                .Replace('8', '۸')
                .Replace('9', '۹');
        }

        /// <summary>
        /// Converts Persian digits to English digits.
        /// </summary>
        public static string ToEnglishDigits(string text)
        {
            return NormalizeDigits(text);
        }

        #endregion
    }
}