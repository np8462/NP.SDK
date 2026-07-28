# NP.SDK Identity

## سیستم User / Role / Permission

**NP.SDK Identity** یک مدل ساده و قابل توسعه برای تعریف کاربران، نقش‌ها و دسترسی‌ها در NP.SDK است.

طراحی فعلی بر اساس رابطه زیر است:

```text
User
 └── Role
      └── Permission
```

این ساختار برای استفاده در برنامه‌های Desktop، Local Network، Server/Client و در مراحل بعدی ارتباط با Chrome Extension طراحی شده است.

---

## 1. ساختار پروژه

### NP.SDK.Contracts

قراردادهای عمومی Identity در این پروژه قرار دارند:

```text
NP.SDK.Contracts
└── Identity
    ├── Enums
    │   └── UserStatus.cs
    ├── IUser.cs
    ├── IRole.cs
    └── IPermission.cs
```

### NP.SDK.Core

پیاده‌سازی قراردادها در این پروژه قرار دارد:

```text
NP.SDK.Core
└── Identity
    ├── User.cs
    ├── Role.cs
    └── Permission.cs
```

---

# 2. UserStatus

وضعیت User توسط `UserStatus` مشخص می‌شود.

```csharp
public enum UserStatus
{
    Unknown = 0,
    Active = 1,
    Disabled = 2,
    Locked = 3,
    Pending = 4
}
```

مقادیر فعلی به صورت صریح شماره‌گذاری شده‌اند.

این موضوع مهم است، زیرا مقدار عددی Enum بخشی از قرارداد محسوب می‌شود.

در آینده اگر وضعیت جدیدی لازم باشد، باید مقدار جدید اضافه شود:

```csharp
Suspended = 5
```

اما مقادیر قبلی نباید Rename یا جابه‌جا شوند.

---

# 3. Permission

هر Permission یک دسترسی مشخص را تعریف می‌کند.

Contract:

```csharp
public interface IPermission
{
    string Id { get; }

    string Name { get; }

    string Description { get; }
}
```

پیاده‌سازی:

```csharp
Permission permission =
    new Permission(
        "user.read",
        "Read Users",
        "Allows reading user information.");
```

نمونه Permissionها:

```text
user.read
user.write
data.read
data.write
remote.execute
chrome.connect
```

Permission عمداً Enum نیست.

در نتیجه برنامه مصرف‌کننده می‌تواند Permissionهای جدید خود را بدون تغییر در NP.SDK تعریف کند.

---

# 4. Role

Role مجموعه‌ای از Permissionها است.

Contract:

```csharp
public interface IRole
{
    string Id { get; }

    string Name { get; }

    string Description { get; }

    IReadOnlyList<IPermission> Permissions { get; }
}
```

ساخت Role:

```csharp
Role administrator =
    new Role(
        "administrator",
        "Administrator",
        "Full application administrator role.");
```

اضافه کردن Permission:

```csharp
administrator.AddPermission(userRead);
administrator.AddPermission(userWrite);
administrator.AddPermission(dataRead);
```

دسترسی‌ها از طریق:

```csharp
administrator.Permissions
```

قابل مشاهده هستند.

افزودن Permission تکراری نیز نادیده گرفته می‌شود.

---

# 5. User

User نماینده کاربر سیستم است.

Contract:

```csharp
public interface IUser
{
    string Id { get; }

    string UserName { get; }

    string DisplayName { get; }

    UserStatus Status { get; }

    IReadOnlyList<IRole> Roles { get; }
}
```

ساخت User:

```csharp
User user =
    new User(
        "user-001",
        "navid",
        "Navid Piri");

user.Status = UserStatus.Active;
```

---

# 6. اختصاص Role به User

برای اختصاص Role:

```csharp
user.AddRole(administrator);
```

بعد از آن:

```csharp
user.Roles
```

لیست Roleهای User را در اختیار قرار می‌دهد.

Role تکراری نیز دوباره اضافه نمی‌شود.

---

# 7. نمونه کامل

نمونه ساده ایجاد یک User، Role و Permission:

```csharp
Permission userRead =
    new Permission(
        "user.read",
        "Read Users",
        "Allows reading user information.");

Permission userWrite =
    new Permission(
        "user.write",
        "Write Users",
        "Allows creating and modifying users.");

Role administrator =
    new Role(
        "administrator",
        "Administrator",
        "Full application administrator role.");

administrator.AddPermission(userRead);
administrator.AddPermission(userWrite);

User user =
    new User(
        "user-001",
        "navid",
        "Navid Piri");

user.Status = UserStatus.Active;

user.AddRole(administrator);
```

ساختار ایجادشده:

```text
User
│
├── Id: user-001
├── UserName: navid
├── DisplayName: Navid Piri
├── Status: Active
│
└── Roles
     │
     └── Administrator
          │
          ├── user.read
          └── user.write
```

---

# 8. حذف Role

برای حذف Role:

```csharp
bool removed =
    user.RemoveRole(administrator);
```

در صورت حذف موفق مقدار `true` برگردانده می‌شود.

---

# 9. حذف Permission

برای حذف Permission از Role:

```csharp
bool removed =
    administrator.RemovePermission(userRead);
```

در صورت حذف موفق مقدار `true` برگردانده می‌شود.

---

# 10. Logging و Exception Handling

Entityهای Identity شامل:

```text
User
Role
Permission
```

مسئول Logging نیستند.

این کلاس‌ها فقط وظیفه نگهداری Data و Relationship را دارند.

در عملیات‌هایی که ممکن است Exception ایجاد کنند، مانند:

```text
Storage
Database
File
Network
Authentication
Authorization
Remote Command
```

باید Exception در لایه عملیاتی با `ILogger` ثبت شود.

الگوی کلی:

```csharp
try
{
    // Operation
}
catch (Exception ex)
{
    logger.Error(
        "Operation failed.",
        ex);

    throw;
}
```

به این ترتیب Logging در تمام Entityها پراکنده نمی‌شود.

---

# 11. معماری فعلی

در نسخه فعلی رابطه به شکل زیر است:

```text
NP.SDK.Contracts
       │
       │ Contracts
       ▼
NP.SDK.Core
       │
       ├── User
       │
       ├── Role
       │
       └── Permission
```

و ارتباط Entityها:

```text
User
 │
 └── Roles
       │
       └── Permissions
```

---

# 12. چرا Permission و Role از Enum استفاده نمی‌کنند؟

Permissionها و Roleها ممکن است با توجه به برنامه تغییر کنند.

برای مثال یک برنامه ممکن است داشته باشد:

```text
user.read
user.write
data.read
```

و برنامه دیگری:

```text
invoice.read
invoice.create
invoice.delete
warehouse.read
warehouse.write
```

اگر Permissionها Enum بودند، هر برنامه برای اضافه کردن Permission جدید مجبور به تغییر SDK می‌شد.

بنابراین Permission به صورت Object/Contract طراحی شده است.

---

# 13. طراحی قابل توسعه

هدف NP.SDK این است که اضافه شدن قابلیت‌های آینده باعث شکستن Contractهای فعلی نشود.

ساختار فعلی می‌تواند در آینده به مواردی مانند:

```text
User
 │
 ├── Roles
 │    └── Permissions
 │
 └── Authentication
        │
        └── Authorization
                │
                └── Permission Check
```

توسعه پیدا کند.

در مراحل بعدی می‌توان از همین مدل برای مواردی مانند:

```text
Local Server
LAN Client
Desktop Client
Chrome Extension
Remote Commands
Data Sharing
```

استفاده کرد.

---

# 14. تست Sandbox

برای تست Identity در:

```text
NP.SDK.Sandbox
└── Tests
    └── IdentityTest.cs
```

تست زیر انجام شده است:

```text
Create Permissions
        ↓
Create Role
        ↓
Add Permissions
        ↓
Create User
        ↓
Assign Role
        ↓
Test duplicate Role
        ↓
Test duplicate Permission
        ↓
Remove Permission
        ↓
Remove Role
```

نمونه خروجی موفق:

```text
[Info] === Identity Test Started ===
[Debug] Permissions created.
[Debug] Role created: Administrator
[Debug] Permission count after duplicate test: 3
[Debug] User created: navid
[Debug] Role assigned to user: Administrator
[Debug] Role count after duplicate test: 1
[Info] User Status: Active
[Info] Role: Administrator
[Info]   Permission: user.read
[Info]   Permission: user.write
[Info]   Permission: data.read
[Debug] Permission removed: True
[Debug] Role removed: True
[Info] Identity Test completed successfully.
[Info] === Identity Test Finished ===
```

---

## نتیجه

Identity فعلی NP.SDK یک هسته کوچک برای مدیریت:

```text
User
Role
Permission
UserStatus
```

فراهم می‌کند.

این طراحی عمداً ساده نگه داشته شده تا در مراحل بعد بتوان قابلیت‌های Authentication، Authorization، Local Server و Client را بدون ایجاد Layerها و پروژه‌های غیرضروری روی آن توسعه داد.
