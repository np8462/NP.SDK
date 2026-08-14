# Identity

## معرفی

بخش Identity در NP.SDK مسئول تعریف کاربران، نقش‌ها و مجوزهای آنها است.

ساختار کلی:

User
└── Role
└── Permission

---

## User

کلاس `User` نماینده یک کاربر است.

### ایجاد کاربر

```csharp
var user = new User(
    "u001",
    "ali",
    "علی"
);

user.Status = UserStatus.Active;

پارامترها به ترتیب:

1. شناسه کاربر


2. نام کاربری


3. نام نمایشی



دسترسی به اطلاعات کاربر

string id = user.Id;
string name = user.UserName;
string displayName = user.DisplayName;

UserStatus status = user.Status;

افزودن نقش

user.AddRole(role);

حذف نقش

user.RemoveRole(role);

مشاهده نقش‌ها

foreach (var role in user.Roles)
{
    Console.WriteLine(role.Name);
}


---

Role

کلاس Role یک نقش کاربری را مشخص می‌کند.

ایجاد نقش

var role = new Role(
    "role001",
    "مدیر فنی",
    "مدیریت بخش فنی"
);

افزودن مجوز

role.AddPermission(permission);

حذف مجوز

role.RemovePermission(permission);

مشاهده مجوزها

foreach (var permission in role.Permissions)
{
    Console.WriteLine(permission.Name);
}


---

Permission

کلاس Permission یک مجوز مشخص را تعریف می‌کند.

ایجاد مجوز

var permission = new Permission(
    "server.create",
    "ایجاد سرور",
    "اجازه ایجاد سرور جدید"
);

پارامترها:

1. شناسه مجوز


2. نام مجوز


3. توضیح مجوز




---

ساخت یک ساختار کامل

ابتدا مجوزها را ایجاد می‌کنیم:

var createServer = new Permission(
    "server.create",
    "ایجاد سرور",
    "اجازه ایجاد سرور"
);

var connectServer = new Permission(
    "server.connect",
    "اتصال به سرور",
    "اجازه اتصال کلاینت به سرور"
);

سپس یک نقش ایجاد می‌کنیم:

var technicalManager = new Role(
    "role.technical",
    "مدیر فنی",
    "مدیریت بخش فنی"
);

مجوزها را به نقش اضافه می‌کنیم:

technicalManager.AddPermission(createServer);
technicalManager.AddPermission(connectServer);

سپس کاربر را ایجاد می‌کنیم:

var user = new User(
    "u001",
    "ali",
    "علی"
);

user.Status = UserStatus.Active;

و نقش را به کاربر می‌دهیم:

user.AddRole(technicalManager);

در این مرحله ساختار زیر ایجاد شده است:

علی
 │
 └── مدیر فنی
      │
      ├── ایجاد سرور
      └── اتصال به سرور


---

بررسی نقش‌های کاربر

برای مشاهده نقش‌های کاربر:

foreach (var role in user.Roles)
{
    Console.WriteLine(role.Name);
}


---

بررسی مجوزهای یک نقش

برای مشاهده مجوزهای یک نقش:

foreach (var permission in role.Permissions)
{
    Console.WriteLine(permission.Name);
}


---

بررسی مستقیم مجوز

در صورت نیاز می‌توان مجوزهای نقش‌های کاربر را بررسی کرد:

bool allowed = false;

foreach (var role in user.Roles)
{
    foreach (var permission in role.Permissions)
    {
        if (permission.Id == "server.create")
        {
            allowed = true;
            break;
        }
    }

    if (allowed)
        break;
}

در معماری نهایی، بهتر است این بررسی مستقیم در UI انجام نشود و از AuthorizationService استفاده شود.


---

AuthorizationService

AuthorizationService مسئول بررسی اجازه انجام عملیات است.

UI نباید مستقیماً ساختار Role و Permission را برای تصمیم‌گیری امنیتی مدیریت کند.

نمونه:

var authorization = new AuthorizationService(
    securityAdapter
);

bool allowed = authorization.Can(
    user,
    "server.create",
    "server"
);

نتیجه:

true  → اجازه انجام عملیات وجود دارد
false → اجازه وجود ندارد


---

استفاده در رابط کاربری

مثلاً برای دکمه ایجاد سرور:

btnCreateServer.Enabled =
    authorization.Can(
        user,
        "server.create",
        "server"
    );

اما این فقط کنترل رابط کاربری است.

در معماری واقعی، Server نیز باید دوباره مجوز را بررسی کند.


---

سه مرحله امنیت

Identity:

این کاربر چه کسی است؟

Authorization:

آیا این کاربر اجازه انجام این عملیات را دارد؟

Transport Security:

آیا ارتباط Client و Server امن است؟

این سه بخش باید از یکدیگر جدا باقی بمانند.


---

ارتباط با Security

Identity در NP.SDK.Core قرار دارد و قراردادهای آن در NP.SDK.Contracts تعریف شده‌اند.

ارتباط با مکانیزم امنیتی از طریق قراردادهای Security و ISecurityAdapter انجام می‌شود.

بنابراین NP.SDK.Core نباید مستقیماً به یک Provider امنیتی خاص وابسته شود.

ساختار کلی:

NP.SDK.Contracts
       ↑
       │
NP.SDK.Core
       │
       ↓
ISecurityAdapter
       │
       ↓
Security Provider

Provider را می‌توان در آینده با پیاده‌سازی دیگری جایگزین کرد.


---

نکته امنیتی

وجود Permission در User یا Role به تنهایی امنیت ایجاد نمی‌کند.

تصمیم نهایی برای عملیات حساس مانند:

ایجاد سرور

تغییر تنظیمات سرور

اتصال کلاینت

مدیریت کاربران


باید در لایه سرویس و به‌خصوص سمت Server نیز بررسی شود.

کنترل UI فقط برای تجربه کاربری است و نباید مرز امنیتی اصلی محسوب شود.


---

حالت آزمایشی

در Sandbox می‌توان از Provider یا Adapter ساده استفاده کرد تا توسعه و آزمایش بدون پیاده‌سازی کامل سیستم امنیتی امکان‌پذیر باشد.

این حالت برای Test مناسب است و نباید به عنوان مکانیزم امنیت واقعی Production در نظر گرفته شود.

به نظرم این نسخه برای `identity.md` مناسب‌تر است، چون هم **معرفی Entityها** را دارد، هم نمونه استفاده از `User`، `Role` و `Permission` را، و هم جایگاه `AuthorizationService` را مشخص می‌کند.

نکته مهم معماری هم این است که **Identity خودش نباید تبدیل به Authorization شود**؛ یعنی `User/Role/Permission` داده و مدل دسترسی را نگه می‌دارند و تصمیم «اجازه دارد یا ندارد» را `AuthorizationService` انجام می‌دهد.