# NPMoneyTextBox

`NPMoneyTextBox` یک کنترل Windows Forms برای ورود و نمایش مقادیر مالی است که بر پایه `NPPersianTextBox` ساخته شده است.

این کنترل برای پروژه‌های ایرانی و همچنین مقادیر ارزی عمومی طراحی شده و امکاناتی مانند:

- نمایش نام واحد پول
- پشتیبانی از ریال و تومان
- پشتیبانی از ارزهای متداول
- تعریف واحد پول سفارشی
- جداکننده هزارگان
- تعداد اعشار قابل تنظیم
- نمایش ارقام فارسی یا انگلیسی
- محدود کردن حداکثر مقدار
- تبدیل خودکار صفحه‌کلید
- حرکت به کنترل بعدی و قبلی

را در اختیار برنامه‌نویس قرار می‌دهد.

---

## Namespace

```csharp
NP.SDK.UI.PersianControls.Controls
افزودن کنترل
پس از Build کردن پروژه، کنترل NPMoneyTextBox را می‌توان از Toolbox به فرم Windows Forms اضافه کرد.
کنترل شامل یک TextBox داخلی و Label مربوط به واحد پول است.
واحد پول به صورت پیش‌فرض در سمت چپ کنترل نمایش داده می‌شود.
نمونه ظاهری:
┌────────────────────────────────────┐
│ تومان    1,250,000                 │
└────────────────────────────────────┘
Properties
Currency
نوع واحد پول را مشخص می‌کند.
نوع:
NPMoneyTextBox.NPCurrency
مقادیر موجود:
Rial
Toman
Dollar
Euro
Pound
Dirham
Custom
مثال:
moneyTextBox.Currency =
    NPMoneyTextBox.NPCurrency.Toman;
CustomCurrencyName
نام واحد پول سفارشی را مشخص می‌کند.
این Property زمانی استفاده می‌شود که:
Currency = NPCurrency.Custom
باشد.
مثال:
moneyTextBox.Currency =
    NPMoneyTextBox.NPCurrency.Custom;

moneyTextBox.CustomCurrencyName =
    "دلار کانادا";
در این حالت متن دلار کانادا در سمت چپ کنترل نمایش داده می‌شود.
اگر ShowCurrency برابر false باشد، نام واحد پول نمایش داده نمی‌شود.
ShowCurrency
مشخص می‌کند که نام واحد پول نمایش داده شود یا خیر.
مقدار پیش‌فرض:
true
مثال:
moneyTextBox.ShowCurrency = false;
در این حالت فقط مقدار پول نمایش داده می‌شود.
Value
مقدار عددی کنترل را دریافت یا تنظیم می‌کند.
نوع:
decimal
دریافت مقدار:
decimal value =
    moneyTextBox.Value;
تنظیم مقدار:
moneyTextBox.Value =
    1250000m;
با توجه به تنظیمات کنترل، مقدار می‌تواند با جداکننده هزارگان و تعداد اعشار مشخص‌شده نمایش داده شود.
UseThousandsSeparator
مشخص می‌کند که جداکننده سه‌رقمی برای بخش صحیح مقدار استفاده شود یا خیر.
مقدار پیش‌فرض:
true
مثال:
moneyTextBox.UseThousandsSeparator =
    true;
نمایش:
1,250,000
در صورت غیرفعال کردن:
moneyTextBox.UseThousandsSeparator =
    false;
نمایش:
1250000
DecimalPlaces
تعداد ارقام بعد از ممیز را مشخص می‌کند.
مقدار پیش‌فرض:
2
مثال:
moneyTextBox.DecimalPlaces =
    2;
در صورت نیاز به مقدار بدون اعشار:
moneyTextBox.DecimalPlaces =
    0;
یا برای سه رقم اعشار:
moneyTextBox.DecimalPlaces =
    3;
DigitMode
نحوه نمایش ارقام را مشخص می‌کند.
این Property از NPPersianTextBox به ارث رسیده است.
مقادیر:
DigitMode.System
DigitMode.Persian
DigitMode.English
نمایش ارقام فارسی:
moneyTextBox.DigitMode =
    DigitMode.Persian;
نمایش ارقام انگلیسی:
moneyTextBox.DigitMode =
    DigitMode.English;
استفاده از حالت سیستم:
moneyTextBox.DigitMode =
    DigitMode.System;
MaxValue
حداکثر مقدار قابل قبول را مشخص می‌کند.
نوع:
decimal
مثال:
moneyTextBox.MaxValue =
    1000000000m;
در این حالت مقدار بیشتر از یک میلیارد پذیرفته نمی‌شود.
مقدار پیش‌فرض کنترل:
Decimal.MaxValue
ConvertKeyboard
مشخص می‌کند که تبدیل صفحه‌کلید فعال باشد یا خیر.
مثال:
moneyTextBox.ConvertKeyboard =
    true;
این قابلیت از NPPersianTextBox استفاده می‌کند.
KeyboardMode
حالت صفحه‌کلید داخلی را مشخص می‌کند.
این Property در PropertyGrid به صورت عمومی نمایش داده نمی‌شود و از نظر طراحی کنترل یک Property داخلی محسوب می‌شود.
حالت‌های اصلی:
KeyboardMode.System
KeyboardMode.Persian
KeyboardMode.English
NextControl
کنترلی را مشخص می‌کند که پس از فشردن Enter فوکوس به آن منتقل شود.
مثال:
moneyTextBox.NextControl =
    txtDescription;
PreviousControl
کنترلی را مشخص می‌کند که هنگام استفاده از Backspace در شرایط مناسب فوکوس به آن منتقل شود.
مثال:
moneyTextBox.PreviousControl =
    txtPrevious;
نمونه استفاده
نمونه ساده برای مبلغ تومان:
NPMoneyTextBox moneyTextBox =
    new NPMoneyTextBox();

moneyTextBox.Currency =
    NPMoneyTextBox.NPCurrency.Toman;

moneyTextBox.ShowCurrency =
    true;

moneyTextBox.UseThousandsSeparator =
    true;

moneyTextBox.DecimalPlaces =
    0;

moneyTextBox.DigitMode =
    DigitMode.Persian;
نمونه مقداردهی
moneyTextBox.Value =
    1250000m;
با تنظیمات بالا مقدار می‌تواند به صورت زیر نمایش داده شود:
تومان    ۱,۲۵۰,۰۰۰
استفاده با اعشار
برای مقادیر دارای اعشار:
moneyTextBox.DecimalPlaces =
    2;

moneyTextBox.Value =
    1250000.50m;
کاربر می‌تواند هنگام ورود مقدار، ممیز را نیز وارد کند.
مثال:
1,250,000.50
واحد پول سفارشی
برای واحد پولی که در فهرست NPCurrency وجود ندارد، از Custom استفاده کنید.
moneyTextBox.Currency =
    NPMoneyTextBox.NPCurrency.Custom;

moneyTextBox.CustomCurrencyName =
    "لیر ترکیه";
یا:
moneyTextBox.CustomCurrencyName =
    "دلار کانادا";
این قابلیت باعث می‌شود برای اضافه کردن یک واحد پول جدید، نیازی به تغییر Enum نباشد.
مخفی کردن واحد پول
در صورت عدم نیاز به نمایش واحد پول:
moneyTextBox.ShowCurrency =
    false;
در این حالت کنترل فقط مقدار عددی را نمایش می‌دهد.
رفتار جداکننده هزارگان
در حالت فعال بودن:
moneyTextBox.UseThousandsSeparator =
    true;
هنگام ورود عدد، جداکننده هزارگان به صورت خودکار اعمال می‌شود.
برای مثال:
1
12
123
1,234
12,345
123,456
1,234,567
جداکننده هنگام تایپ نیز به‌روزرسانی می‌شود.
رفتار ممیز
کنترل اجازه ورود مقدار اعشاری را بر اساس DecimalPlaces فراهم می‌کند.
برای مثال:
moneyTextBox.DecimalPlaces =
    2;
مقادیر زیر قابل استفاده هستند:
100
100.5
100.50
تعداد ارقام بعد از ممیز بر اساس مقدار DecimalPlaces کنترل می‌شود.
معماری کنترل
NPMoneyTextBox از NPPersianTextBox به عنوان TextBox داخلی استفاده می‌کند.
به همین دلیل امکاناتی مانند:
اعتبارسنجی عددی
DigitMode
KeyboardMode
ConvertKeyboard
MaxValue
NextControl
PreviousControl
بر پایه قابلیت‌های کنترل پایه NPPersianTextBox عمل می‌کنند.
مثال کامل
moneyTextBox.Currency =
    NPMoneyTextBox.NPCurrency.Toman;

moneyTextBox.CustomCurrencyName =
    "";

moneyTextBox.ShowCurrency =
    true;

moneyTextBox.UseThousandsSeparator =
    true;

moneyTextBox.DecimalPlaces =
    0;

moneyTextBox.DigitMode =
    DigitMode.Persian;

moneyTextBox.MaxValue =
    1000000000m;

moneyTextBox.ConvertKeyboard =
    true;

moneyTextBox.Value =
    1250000m;
نکات طراحی
NPMoneyTextBox برای ورود مقادیر مالی در برنامه‌های Windows Forms طراحی شده است.
نام واحد پول در سمت چپ کنترل قرار می‌گیرد تا با رابط‌های کاربری فارسی و راست‌به‌چپ سازگاری بیشتری داشته باشد.
برای پروژه‌های ایرانی، ترکیب زیر معمولاً مناسب است:
moneyTextBox.Currency =
    NPMoneyTextBox.NPCurrency.Toman;

moneyTextBox.DigitMode =
    DigitMode.Persian;

moneyTextBox.DecimalPlaces =
    0;

moneyTextBox.UseThousandsSeparator =
    true;
برای مقادیر ارزی دارای اعشار می‌توان DecimalPlaces را روی مقدار مناسب تنظیم کرد.
Version
این مستند مربوط به نسخه فعلی NPMoneyTextBox در NP.SDK است.

این نسخه را من با API فعلی کلاسی که با هم تست کردیم هماهنگ کردم؛ یعنی **`CustomCurrencyName`، `NPCurrency.Custom`، `DigitMode`، `DecimalPlaces`، `UseThousandsSeparator`، `Value` و `MaxValue`** همگی در Manual پوشش داده شده‌اند.

بعد از اینکه فایل را در `docs/controls/NPMoneyTextBox.md` گذاشتی، پیشنهاد می‌کنم قبل از Commit فقط یک بار در `FrmNPMoneyTextBoxTest` این سناریوها را چک کنیم:

1. تومان + ارقام فارسی + بدون اعشار
2. تومان + ارقام انگلیسی + اعشار
3. `UseThousandsSeparator = false`
4. `ShowCurrency = false`
5. `Custom` + `CustomCurrencyName`
6. `MaxValue`
7. تایپ طولانی مثل `123456789012345`
8. تایپ `1234.56` و تغییر `DecimalPlaces`
