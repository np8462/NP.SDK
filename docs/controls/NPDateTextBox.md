# NPDateTextBox

## NP.SDK.UI.PersianControls

---

## English

### Overview

`NPDateTextBox` is a lightweight WinForms UserControl for entering and validating Persian (Jalali) dates.

The control is composed of three `NPPersianTextBox` controls:

- Year
- Month
- Day

It automatically validates the entered date using the .NET `PersianCalendar` class.

---

## Features

- Persian date input
- Automatic validation
- Leap year validation
- PersianCalendar conversion
- Persian to Gregorian conversion
- Focus navigation between day, month and year
- Uses NPPersianTextBox internally
- Supports PropertyGrid at design-time
- WinForms (.NET Framework 4.x)

---

## Properties

| Property | Description |
|----------|-------------|
| Text | Gets or sets Persian date (yyyy/MM/dd) |
| PersianDate | Persian date string |
| MiladiDate | Gregorian DateTime |
| IsCorrectDate | Indicates whether the entered date is valid |
| PersianYear | Year part |
| PersianMonth | Month part |
| PersianDay | Day part |
| IsFillSystemDate | Loads today's Persian date on startup |
| NormalColor | Background color for valid values |
| WrongColor | Background color for invalid values |
| NextFocusControl | Control to focus after Enter |
| PreviousFocusControl | Control to focus after Backspace |

---

## Validation Rules

- Month must be between 1 and 12.
- Day must be valid for the selected month.
- Months 7-12 cannot contain day 31.
- Esfand 30 is only allowed in leap years.
- Year must be a valid Persian calendar year.

---

## Example

```csharp
npDateTextBox1.Text = "1404/05/07";

if (npDateTextBox1.IsCorrectDate)
{
    MessageBox.Show(npDateTextBox1.MiladiDate.ToString());
}
```

---

## Sandbox

A complete test form is available in

```
NP.SDK.Sandbox
```

The sandbox allows testing

- Properties
- Validation
- PropertyGrid integration
- Runtime behavior

---

## Future Roadmap

Future versions may include:

- Persian DatePicker
- Popup Calendar
- Nullable dates
- Min/Max date
- Formatting options
- Localization improvements

---

# فارسی

## معرفی

کنترل **NPDateTextBox** یک کنترل سبک WinForms برای ورود تاریخ شمسی است.

این کنترل از سه کنترل داخلی **NPPersianTextBox** تشکیل شده است:

- سال
- ماه
- روز

اعتبارسنجی تاریخ به صورت خودکار توسط کلاس `PersianCalendar` انجام می‌شود.

---

## امکانات

- ورود تاریخ شمسی
- اعتبارسنجی خودکار تاریخ
- بررسی سال کبیسه
- تبدیل تاریخ شمسی به میلادی
- حرکت خودکار فوکوس بین روز، ماه و سال
- استفاده از NPPersianTextBox
- پشتیبانی از PropertyGrid
- مناسب پروژه‌های WinForms

---

## ویژگی‌ها

کنترل دارای قابلیت‌های زیر است:

- بررسی صحت تاریخ
- تعیین رنگ خطا
- تعیین رنگ عادی
- دریافت تاریخ میلادی
- دریافت رشته تاریخ شمسی
- دریافت سال، ماه و روز
- مقداردهی خودکار تاریخ روز سیستم
- تعیین کنترل قبلی و بعدی برای حرکت فوکوس

---

## قالب تاریخ

```
yyyy/MM/dd
```

نمونه:

```
1404/05/07
```

---

## قوانین اعتبارسنجی

- ماه بین 1 تا 12
- روز معتبر نسبت به ماه
- ماه‌های 7 تا 12 دارای حداکثر 30 روز
- اسفند 30 فقط در سال کبیسه
- بررسی کامل توسط PersianCalendar

---

## نمونه استفاده

```csharp
npDateTextBox1.Text = "1404/05/07";

if(npDateTextBox1.IsCorrectDate)
{
    DateTime date = npDateTextBox1.MiladiDate;
}
```

---

## تست

فرم تست این کنترل در پروژه

```
NP.SDK.Sandbox
```

قرار دارد.

در Sandbox می‌توان موارد زیر را بررسی کرد:

- تمامی Property ها
- اعتبارسنجی
- تغییر Runtime
- عملکرد PropertyGrid

---

## نسخه

Current Version

```
1.0.0
```

---

## License

NP.SDK

Copyright © NP Software Development Kit
