NPPersianTextBox
🇺🇸 English
Overview

NPPersianTextBox is a reusable WinForms TextBox developed for the NP.SDK.UI.PersianControls library.

It extends the standard TextBox by adding Persian keyboard conversion, input validation, Persian digit support and automatic focus navigation.

Namespace
using NP.SDK.UI.PersianControls.Controls;
Features

✔ Persian keyboard conversion

✔ English keyboard conversion

✔ Persian digit display

✔ English digit display

✔ Integer validation

✔ Decimal validation

✔ Text validation

✔ AlphaNumeric validation

✔ Maximum value validation

✔ Decimal places limitation

✔ Enter moves focus to next control

✔ Backspace moves focus to previous control

✔ WinForms Designer support

Properties
Property	Description
InputMode	Allowed input type
KeyboardMode	Keyboard conversion mode
DigitMode	Digit display mode
ConvertKeyboard	Enables keyboard conversion
MaxValue	Maximum numeric value
DecimalPlaces	Maximum decimal digits
NextControl	Focus target after Enter
PreviousControl	Focus target after Backspace
MoveToNextControlOnEnter	Enables Enter navigation
MoveToPreviousControlOnBackspace	Enables Backspace navigation
InputMode
Any

Text

Integer

Decimal

AlphaNumeric
KeyboardMode
System

Persian

English
DigitMode
System

Persian

English
Example
txtName.KeyboardMode = KeyboardMode.Persian;

txtName.InputMode = InputMode.Text;

txtPrice.InputMode = InputMode.Decimal;

txtPrice.DecimalPlaces = 2;

txtPrice.MaxValue = 999999;

txtPrice.DigitMode = DigitMode.Persian;
Notes

KeyboardMode converts keyboard input.

DigitMode changes how digits are displayed.

InputMode validates user input.

These three properties are independent.

Roadmap

Future releases may include

Paste validation
Drag & Drop validation
RTL improvements
Mask support
AutoComplete
Watermark
ErrorProvider integration
Culture aware formatting
🇮🇷 فارسی
معرفی

کنترل NPPersianTextBox یک TextBox توسعه یافته برای Windows Forms است که در کتابخانه NP.SDK.UI.PersianControls ارائه شده است.

این کنترل علاوه بر امکانات TextBox استاندارد امکانات ویژه‌ای برای زبان فارسی در اختیار برنامه‌نویس قرار می‌دهد.

امکانات

✔ تبدیل صفحه کلید انگلیسی به فارسی

✔ تبدیل فارسی به انگلیسی

✔ نمایش اعداد فارسی

✔ نمایش اعداد انگلیسی

✔ محدود کردن ورود متن

✔ محدود کردن ورود اعداد صحیح

✔ محدود کردن ورود اعداد اعشاری

✔ تعیین حداکثر مقدار عددی

✔ تعیین تعداد ارقام اعشار

✔ انتقال فوکوس با Enter

✔ انتقال فوکوس با Backspace

✔ پشتیبانی از Designer ویژوال استودیو

Property ها
InputMode

مشخص می‌کند چه نوع داده‌ای مجاز به ورود باشد.

Any

Text

Integer

Decimal

AlphaNumeric
KeyboardMode

نحوه تبدیل صفحه کلید را مشخص می‌کند.

System

Persian

English
DigitMode

نحوه نمایش اعداد را مشخص می‌کند.

System

Persian

English
ConvertKeyboard

فعال یا غیرفعال کردن تبدیل صفحه کلید.

MaxValue

بیشترین مقدار مجاز برای ورود عدد.

DecimalPlaces

تعداد ارقام اعشار مجاز.

NextControl

کنترلی که بعد از فشردن Enter فوکوس به آن منتقل می‌شود.

PreviousControl

کنترلی که در صورت Backspace روی متن خالی فوکوس به آن منتقل می‌شود.

MoveToNextControlOnEnter

فعال یا غیرفعال کردن انتقال فوکوس با Enter.

MoveToPreviousControlOnBackspace

فعال یا غیرفعال کردن انتقال فوکوس با Backspace.

نمونه استفاده
txtName.KeyboardMode = KeyboardMode.Persian;

txtName.InputMode = InputMode.Text;

txtPrice.InputMode = InputMode.Decimal;

txtPrice.DecimalPlaces = 2;

txtPrice.MaxValue = 1000000;

txtPrice.DigitMode = DigitMode.Persian;
نکات

سه ویژگی اصلی کنترل عبارت‌اند از:

InputMode

برای اعتبارسنجی داده‌ها.

KeyboardMode

برای تبدیل صفحه کلید.

DigitMode

برای نحوه نمایش اعداد.

این سه ویژگی مستقل از یکدیگر طراحی شده‌اند.

نسخه
Version : 1.0
License
MIT License
GitHub
NP.SDK.UI.PersianControls