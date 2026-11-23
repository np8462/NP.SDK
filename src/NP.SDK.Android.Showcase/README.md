# NP.SDK.Android.Showcase

A standalone Xamarin.Android demo application for the reusable controls in
`NP.SDK.Android.Controls`.

## Tabs

- **Confirm** – interactive Slide to Confirm / Swipe to Commit control.
- **Buttons** – reserved for future button controls.
- **Progress** – reserved for progress indicators.
- **Gestures** – reserved for swipe and drag controls.
- **Samples** – C# usage sample and GitHub links.

## Build

Open `NP.SDK.sln` in the existing Xamarin-enabled Visual Studio installation.
Select `NP.SDK.Android.Showcase` as the startup project and build the Debug
configuration. The project targets Android API 22 with a minimum API level of 19.

The APK is produced by the Xamarin.Android build pipeline; this repository
commit contains source and project files, not a prebuilt APK.

## Reuse the control

Reference `NP.SDK.Android.Controls.csproj` from another Xamarin.Android
application, then:

```csharp
var slider = new NP.SDK.Android.Controls.SlideConfirmView(this);
slider.Confirmed += delegate
{
    // Perform the operation only after the user confirms.
};
layout.AddView(slider);
```

Source:
https://github.com/np8462/NP.SDK/tree/main/src/NP.SDK.Android.Controls
