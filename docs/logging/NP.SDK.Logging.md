# NP.SDK Logging

## راهنمای سیستم لاگ‌گیری NP.SDK

### English / فارسی

---

# English Documentation

## 1. Overview

`NP.SDK.Logging` provides a small and reusable logging system for .NET Framework applications.

The current implementation supports:

* Debug messages
* Information messages
* Warning messages
* Error messages
* Exception logging
* Visual Studio Debug Output
* JSON file logging

The main goal is to keep the logging API simple while leaving room for future expansion.

---

## 2. Project Structure

The logging components are currently organized as:

```text
NP.SDK.Contracts
└── Logging
    └── ILogger.cs

NP.SDK.Core
└── Logging
    └── Logger.cs
```

`ILogger` defines the logging contract.

`Logger` provides the current implementation.

---

## 3. Referencing NP.SDK

A consuming application should reference:

```text
NP.SDK.Contracts.dll
NP.SDK.Core.dll
```

The application can then use the `ILogger` interface instead of depending directly on the implementation wherever appropriate.

---

## 4. Creating a Logger

Basic usage:

```csharp
using NP.SDK.Contracts.Logging;
using NP.SDK.Core.Logging;

ILogger logger = new Logger();
```

The logger can then be used throughout the application.

---

## 5. Debug Logging

Use `Debug` for information intended primarily for development and debugging.

```csharp
logger.Debug("Debug message.");
```

Example:

```csharp
logger.Debug("Loading configuration...");
```

The message is written to the Visual Studio Debug Output.

---

## 6. Information Logging

Use `Info` for normal application events.

```csharp
logger.Info("Application started.");
```

Example:

```csharp
logger.Info("User successfully logged in.");
```

---

## 7. Warning Logging

Use `Warning` when something unexpected happens but the application can continue working.

```csharp
logger.Warning("Configuration file was not found.");
```

Warnings should not normally indicate a fatal application error.

---

## 8. Error Logging

Use `Error` when an operation fails or an error occurs.

```csharp
logger.Error("Unable to load configuration.");
```

---

## 9. Logging Exceptions

When an exception is caught, pass the exception to `Error`.

```csharp
try
{
    int result = 10 / 0;
}
catch (Exception ex)
{
    logger.Error("An error occurred.", ex);
}
```

This preserves the exception information and stack trace in the log.

This is preferred over:

```csharp
catch (Exception ex)
{
    logger.Error(ex.Message);
}
```

because passing the complete `Exception` provides more diagnostic information.

---

## 10. Typical try/catch Usage

A typical application method can use:

```csharp
public void LoadData()
{
    try
    {
        // Application operation

        logger.Info("Data loading started.");

        // ...

        logger.Info("Data loading completed.");
    }
    catch (Exception ex)
    {
        logger.Error("Data loading failed.", ex);
    }
}
```

The logger therefore becomes a reusable component for handling diagnostic information without duplicating logging code throughout the application.

---

## 11. Visual Studio Output

During development, log messages are written to:

```csharp
System.Diagnostics.Debug.WriteLine(...)
```

Therefore, when running the application under Visual Studio, messages can be viewed in:

```text
Visual Studio
    → Debug
        → Windows
            → Output
```

Example:

```text
[2026-07-28 13:37:40] [Debug] Debug message
[2026-07-28 13:37:40] [Info] Application started.
[2026-07-28 13:37:40] [Warning] This is a warning.
[2026-07-28 13:37:40] [Error] An error occurred.
```

---

## 12. JSON Log File

The current implementation also stores logs in JSON format.

The log directory is created relative to the application's executable directory:

```text
Logs/
└── NP.SDK.Log.json
```

For example:

```text
bin/
└── Debug/
    └── Logs/
        └── NP.SDK.Log.json
```

The file contains log entries in JSON array format.

Example:

```json
[
  {
    "Time": "2026-07-28 13:37:40",
    "Message": "[Info] Application started."
  },
  {
    "Time": "2026-07-28 13:37:41",
    "Message": "[Error] An error occurred."
  }
]
```

The JSON file can therefore be inspected later without Visual Studio.

---

## 13. Recommended Log Levels

Use the levels according to their purpose:

| Level     | Purpose                                     |
| --------- | ------------------------------------------- |
| `Debug`   | Development and troubleshooting information |
| `Info`    | Normal application events                   |
| `Warning` | Unexpected but recoverable situations       |
| `Error`   | Failed operations and exceptions            |

---

## 14. Recommended Pattern

For normal operations:

```csharp
logger.Info("Operation started.");
```

For development diagnostics:

```csharp
logger.Debug("Current state: " + state);
```

For recoverable problems:

```csharp
logger.Warning("Optional configuration was not found.");
```

For exceptions:

```csharp
try
{
    DoSomething();
}
catch (Exception ex)
{
    logger.Error("Operation failed.", ex);
}
```

---

## 15. Current Design

The current logging design intentionally remains small:

```text
Application
     │
     ▼
   ILogger
     │
     ▼
   Logger
     │
     ├──────────────► Visual Studio Output
     │
     └──────────────► JSON Log File
```

No database, network service, repository, or additional logging project is currently required.

Future versions may extend the logging system when an actual requirement appears.

---

# مستندات فارسی

## ۱. معرفی

`NP.SDK.Logging` یک سیستم ساده و قابل استفاده مجدد برای ثبت رویدادهای برنامه‌های مبتنی بر .NET Framework است.

نسخه فعلی از موارد زیر پشتیبانی می‌کند:

* پیام‌های Debug
* پیام‌های Information
* پیام‌های Warning
* پیام‌های Error
* ثبت Exception
* نمایش لاگ در Output ویژوال استودیو
* ذخیره لاگ در فایل JSON

هدف اصلی این طراحی، **سادگی استفاده و جلوگیری از ایجاد لایه‌ها و کلاس‌های غیرضروری** است.

---

## ۲. ساختار پروژه

اجزای Logging در حال حاضر به این صورت هستند:

```text
NP.SDK.Contracts
└── Logging
    └── ILogger.cs

NP.SDK.Core
└── Logging
    └── Logger.cs
```

`ILogger` قرارداد سیستم لاگ‌گیری را مشخص می‌کند.

`Logger` پیاده‌سازی فعلی این قرارداد است.

---

## ۳. Reference کردن NP.SDK

پروژه‌ای که می‌خواهد از Logging استفاده کند باید Referenceهای زیر را داشته باشد:

```text
NP.SDK.Contracts.dll
NP.SDK.Core.dll
```

در صورت امکان، بهتر است کد مصرف‌کننده با `ILogger` کار کند و مستقیماً به پیاده‌سازی وابسته نشود.

---

## ۴. ساخت Logger

استفاده ساده:

```csharp
using NP.SDK.Contracts.Logging;
using NP.SDK.Core.Logging;

ILogger logger = new Logger();
```

از این Logger می‌توان برای ثبت رویدادهای مختلف برنامه استفاده کرد.

---

## ۵. ثبت Debug

برای اطلاعاتی که بیشتر در زمان توسعه و Debug برنامه مورد استفاده هستند:

```csharp
logger.Debug("Debug message.");
```

مثلاً:

```csharp
logger.Debug("Loading configuration...");
```

این پیام در Output ویژوال استودیو نمایش داده می‌شود.

---

## ۶. ثبت Information

برای رویدادهای معمول برنامه از `Info` استفاده کنید:

```csharp
logger.Info("Application started.");
```

مثلاً:

```csharp
logger.Info("User successfully logged in.");
```

---

## ۷. ثبت Warning

زمانی که اتفاق غیرمنتظره‌ای رخ داده ولی برنامه همچنان می‌تواند به کار خود ادامه دهد:

```csharp
logger.Warning("Configuration file was not found.");
```

Warning معمولاً به معنای توقف یا خطای جدی برنامه نیست.

---

## ۸. ثبت Error

برای عملیات ناموفق یا خطاهای برنامه:

```csharp
logger.Error("Unable to load configuration.");
```

---

## ۹. ثبت Exception

وقتی یک Exception در `try/catch` رخ می‌دهد، بهتر است خود Exception را به Logger بدهیم:

```csharp
try
{
    int result = 10 / 0;
}
catch (Exception ex)
{
    logger.Error("An error occurred.", ex);
}
```

با این کار اطلاعات Exception و StackTrace نیز حفظ می‌شود.

این روش بهتر از این است:

```csharp
catch (Exception ex)
{
    logger.Error(ex.Message);
}
```

زیرا ارسال خود `Exception` اطلاعات کامل‌تری برای خطایابی فراهم می‌کند.

---

## ۱۰. الگوی پیشنهادی try/catch

یک متد معمولی می‌تواند به این صورت باشد:

```csharp
public void LoadData()
{
    try
    {
        logger.Info("Data loading started.");

        // عملیات برنامه

        logger.Info("Data loading completed.");
    }
    catch (Exception ex)
    {
        logger.Error("Data loading failed.", ex);
    }
}
```

به این ترتیب Logging به صورت یک Component قابل استفاده مجدد در قسمت‌های مختلف برنامه در اختیار Developer قرار می‌گیرد.

---

## ۱۱. خروجی Visual Studio

در زمان توسعه، Logger پیام‌ها را از طریق:

```csharp
System.Diagnostics.Debug.WriteLine(...)
```

در Output ویژوال استودیو نمایش می‌دهد.

مسیر مشاهده:

```text
Visual Studio
    → Debug
        → Windows
            → Output
```

نمونه:

```text
[2026-07-28 13:37:40] [Debug] Debug message
[2026-07-28 13:37:40] [Info] Application started.
[2026-07-28 13:37:40] [Warning] This is a warning.
[2026-07-28 13:37:40] [Error] An error occurred.
```

---

## ۱۲. فایل JSON

علاوه بر Output، Logger فعلی لاگ‌ها را در فایل JSON نیز ذخیره می‌کند.

پوشه Log در کنار مسیر اجرای برنامه ایجاد می‌شود:

```text
Logs/
└── NP.SDK.Log.json
```

برای مثال:

```text
bin/
└── Debug/
    └── Logs/
        └── NP.SDK.Log.json
```

نمونه محتوای فایل:

```json
[
  {
    "Time": "2026-07-28 13:37:40",
    "Message": "[Info] Application started."
  },
  {
    "Time": "2026-07-28 13:37:41",
    "Message": "[Error] An error occurred."
  }
]
```

بنابراین لاگ‌ها حتی بعد از بسته شدن Visual Studio نیز قابل بررسی هستند.

---

## ۱۳. انتخاب سطح مناسب Log

| Level     | کاربرد                             |
| --------- | ---------------------------------- |
| `Debug`   | اطلاعات توسعه و خطایابی            |
| `Info`    | رویدادهای عادی برنامه              |
| `Warning` | وضعیت‌های غیرمنتظره ولی قابل ادامه |
| `Error`   | عملیات ناموفق و Exceptionها        |

---

## ۱۴. الگوی پیشنهادی استفاده

عملیات معمول:

```csharp
logger.Info("Operation started.");
```

اطلاعات Debug:

```csharp
logger.Debug("Current state: " + state);
```

مشکل قابل بازیابی:

```csharp
logger.Warning("Optional configuration was not found.");
```

Exception:

```csharp
try
{
    DoSomething();
}
catch (Exception ex)
{
    logger.Error("Operation failed.", ex);
}
```

---

## ۱۵. معماری فعلی

ساختار فعلی عمداً ساده نگه داشته شده است:

```text
Application
     │
     ▼
   ILogger
     │
     ▼
   Logger
     │
     ├──────────────► Visual Studio Output
     │
     └──────────────► JSON Log File
```

در نسخه فعلی نیازی به Database، Network Service، Repository یا پروژه جداگانه برای Logging وجود ندارد.

در صورت ایجاد نیاز واقعی در آینده، قابلیت‌های جدید می‌توانند به صورت مرحله‌ای به سیستم اضافه شوند.

---

## خلاصه

برای استفاده از Logging کافی است:

```csharp
ILogger logger = new Logger();

logger.Debug("Debug information.");
logger.Info("Application started.");
logger.Warning("Something unexpected happened.");

try
{
    // operation
}
catch (Exception ex)
{
    logger.Error("Operation failed.", ex);
}
```

همزمان خروجی در Visual Studio قابل مشاهده است و در فایل JSON نیز ذخیره می‌شود.
