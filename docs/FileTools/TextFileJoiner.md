TextFileJoiner
Overview

TextFileJoiner is a lightweight utility included in NP.SDK that merges multiple text-based source files (such as .cs, .txt, .json, .xml, etc.) into a single output file.

The utility was originally created to simplify sharing legacy source code with AI assistants, but it can also be used for documentation, source code archiving, backups, and code reviews.

Features
Merge multiple text files into one output file.
Supports C# source files (.cs).
Supports plain text files (.txt).
Preserves file order.
Optional file headers before each merged file.
UTF-8 encoding support.
Low memory usage (stream-based processing).
Suitable for very large source collections.
Namespace
NP.SDK.Core.IO.FileTools
Classes
TextFileJoinOptions

Contains the configuration used during the merge operation.

Properties:

Property	Description
InputFiles	Collection of source files
OutputFile	Destination file
Encoding	Output encoding
AddFileHeaders	Writes file information before each merged file
TextFileJoiner

Provides the merge functionality.

Method:

TextFileJoiner.Join(options);
Example
TextFileJoinOptions options = new TextFileJoinOptions();

options.InputFiles.Add(@"C:\Project\File1.cs");
options.InputFiles.Add(@"C:\Project\File2.cs");

options.OutputFile = @"C:\Merged.cs";

options.AddFileHeaders = true;

TextFileJoiner.Join(options);
Output Format

When AddFileHeaders is enabled, the generated file contains sections similar to:

//============================================================
// File : RuntimeBridgeBootstrap.cs
// Path : C:\Projects\NP.Host.RuntimeBridge\RuntimeBridgeBootstrap.cs
//============================================================

followed by the original file contents.

Sandbox Example

The Sandbox project includes a simple WinForms demonstration.

Workflow:

Click Merge Files.
Select a source file.
Choose Yes to add another file.
Repeat until all files are selected.
Choose No.
Select the destination file.
The merged output is generated.
Typical Uses
Share large codebases with AI assistants.
Archive source code.
Create single-file source snapshots.
Prepare code for documentation.
Export legacy projects.
Combine generated code.
Performance

The implementation uses StreamReader and StreamWriter instead of loading all files into memory.

Benefits:

Low memory consumption.
Supports very large files.
Suitable for projects containing hundreds of source files.
Future Improvements

Possible future enhancements include:

Text file splitter.
File comparison.
Search within files.
Replace text.
Folder merge.
Folder comparison.
Duplicate file detection.
Drag & Drop support.
Progress reporting.
Cancellation support.
License

Part of the NP.SDK project.

# راهنمای استفاده از TextFileJoiner

## معرفی

**TextFileJoiner** یک ابزار ساده و سبک در پروژه **NP.SDK** است که امکان ادغام چندین فایل متنی را در قالب یک فایل خروجی فراهم می‌کند.

هدف اولیه از ساخت این ابزار، ادغام فایل‌های سورس پروژه‌های قدیمی (Legacy) در قالب یک فایل واحد بود تا بتوان آن را به‌راحتی برای بررسی، مستندسازی یا تحلیل در اختیار ابزارهای هوش مصنوعی مانند ChatGPT قرار داد.

این ابزار محدود به فایل‌های C# نیست و می‌تواند برای هر نوع فایل متنی مانند:

* `.cs`
* `.txt`
* `.json`
* `.xml`
* `.config`
* `.sql`

نیز مورد استفاده قرار گیرد.

---

# فضای نام (Namespace)

```csharp
NP.SDK.Core.IO.FileTools
```

---

# کلاس‌های موجود

## TextFileJoinOptions

این کلاس تنظیمات موردنیاز عملیات ادغام را نگهداری می‌کند.

خصوصیات مهم آن عبارت‌اند از:

| ویژگی          | توضیح                                |
| -------------- | ------------------------------------ |
| InputFiles     | لیست فایل‌های ورودی                  |
| OutputFile     | مسیر فایل خروجی                      |
| Encoding       | نوع Encoding فایل خروجی              |
| AddFileHeaders | درج اطلاعات هر فایل قبل از محتوای آن |

---

## TextFileJoiner

این کلاس عملیات اصلی ادغام فایل‌ها را انجام می‌دهد.

متد اصلی آن:

```csharp
TextFileJoiner.Join(options);
```

---

# نمونه اول

## انتخاب فایل‌ها به‌صورت مرحله‌ای

در این روش، کاربر ابتدا یک فایل را انتخاب می‌کند.

سپس توسط یک MessageBox سؤال می‌شود که آیا فایل دیگری نیز باید اضافه شود یا خیر.

در صورت انتخاب گزینه **Yes** مجدداً پنجره انتخاب فایل نمایش داده می‌شود.

این روند تا زمانی ادامه پیدا می‌کند که کاربر گزینه **No** را انتخاب نماید.

در انتها مسیر فایل خروجی از کاربر دریافت شده و عملیات ادغام انجام می‌شود.

نمونه کد:

```csharp
TextFileJoinOptions options = new TextFileJoinOptions();

while (true)
{
    using (OpenFileDialog dialog = new OpenFileDialog())
    {
        dialog.Filter =
            "Source Files (*.cs;*.txt)|*.cs;*.txt|All Files (*.*)|*.*";

        if (dialog.ShowDialog() != DialogResult.OK)
            break;

        options.InputFiles.Add(dialog.FileName);
    }

    if (MessageBox.Show(
            "آیا فایل دیگری نیز انتخاب می‌کنید؟",
            "ادامه",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)
        == DialogResult.No)
    {
        break;
    }
}

if (options.InputFiles.Count > 0)
{
    SaveFileDialog save = new SaveFileDialog();

    save.Filter = "C# File (*.cs)|*.cs";

    save.FileName = "Merged.cs";

    if (save.ShowDialog() == DialogResult.OK)
    {
        options.OutputFile = save.FileName;

        TextFileJoiner.Join(options);
    }
}
```

---

# نمونه دوم

## انتخاب همزمان چند فایل

اگر نیاز باشد کاربر تنها یک مرتبه پنجره انتخاب فایل را مشاهده کند، می‌توان از ویژگی **Multiselect** استفاده نمود.

نمونه:

```csharp
OpenFileDialog dialog = new OpenFileDialog();

dialog.Multiselect = true;

dialog.Filter =
    "Source Files (*.cs;*.txt)|*.cs;*.txt|All Files (*.*)|*.*";

if (dialog.ShowDialog() == DialogResult.OK)
{
    TextFileJoinOptions options = new TextFileJoinOptions();

    foreach (string file in dialog.FileNames)
    {
        options.InputFiles.Add(file);
    }

    SaveFileDialog save = new SaveFileDialog();

    save.Filter = "C# File (*.cs)|*.cs";

    save.FileName = "Merged.cs";

    if (save.ShowDialog() == DialogResult.OK)
    {
        options.OutputFile = save.FileName;

        TextFileJoiner.Join(options);
    }
}
```

---

# ساختار فایل خروجی

در صورتی که گزینه **AddFileHeaders** فعال باشد، قبل از محتوای هر فایل اطلاعاتی مشابه نمونه زیر درج می‌شود:

```csharp
//============================================================
// File : RuntimeBridgeBootstrap.cs
// Path : D:\Projects\NP.Host.RuntimeBridge\RuntimeBridgeBootstrap.cs
//============================================================
```

پس از آن محتوای اصلی فایل نوشته خواهد شد.

این ویژگی باعث می‌شود هنگام مطالعه فایل خروجی، محل شروع هر فایل به‌راحتی قابل تشخیص باشد.

---

# مزایا

* سرعت مناسب
* مصرف بسیار کم حافظه
* امکان ادغام صدها فایل
* مناسب برای پروژه‌های بزرگ
* مناسب جهت تهیه نسخه آرشیوی از سورس
* مناسب جهت اشتراک‌گذاری کد با ابزارهای هوش مصنوعی
* مناسب برای مستندسازی پروژه‌های قدیمی (Legacy)

---

# نکات

* ترتیب فایل‌ها در خروجی دقیقاً مطابق ترتیب اضافه شدن آن‌ها خواهد بود.
* از StreamReader و StreamWriter استفاده شده است؛ بنابراین فایل‌های بسیار بزرگ نیز بدون مصرف زیاد حافظه قابل پردازش هستند.
* در صورت فعال بودن AddFileHeaders، تشخیص محل شروع هر فایل در خروجی بسیار ساده خواهد بود.

---

# کاربردهای پیشنهادی

این ابزار در موارد زیر بسیار کاربردی است:

* ادغام سورس پروژه‌های قدیمی
* ارسال کد برای بررسی توسط ChatGPT
* تهیه نسخه مستند از سورس پروژه
* آرشیو کردن کدها
* تحلیل ساختار پروژه
* تهیه نسخه قابل اشتراک از فایل‌های متنی

---

**پروژه:** NP.SDK

**بخش:** NP.SDK.Core.IO.FileTools

**کلاس اصلی:** TextFileJoiner
