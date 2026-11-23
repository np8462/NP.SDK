# NP.SDK.Android.Showcase

A standalone Xamarin.Android application for interactive demonstrations of
reusable controls from `NP.SDK.Android.Controls`.

## Showcase tabs

| Tab | Purpose | Current state |
|---|---|---|
| Confirm | Slide-to-confirm / swipe-to-commit | Implemented |
| Buttons | Custom button family | Planned |
| Progress | Progress and loading indicators | Planned |
| Gestures | Swipe, drag and gesture controls | Planned |
| Samples | C# usage sample and repository links | Implemented |

The Buttons, Progress and Gestures tabs are intentionally prepared as extension
points. They do not claim to contain working controls yet. As each new control
is implemented in the Controls library, its interactive example, C# sample and
direct source link should be added to the corresponding Showcase tab.

## Current control: SlideConfirmView

Features:

- Native Android `View` drawn with `Canvas`.
- Drag the thumb to confirm; the threshold is configurable.
- `Confirmed` event fires once for each confirmation.
- `Reset()` restores the control for another operation.
- No third-party UI dependency.

### C# usage

Add a project reference to `NP.SDK.Android.Controls.csproj`:

```csharp
using NP.SDK.Android.Controls;

var slider = new SlideConfirmView(this);
slider.ConfirmThreshold = 0.85f;
slider.PromptText = "Slide to commit";
slider.ConfirmedText = "Done!";

slider.Confirmed += delegate
{
    CommitOperation();
};

layout.AddView(slider);

// Reset it when it needs to be used again.
slider.Reset();
```

Implementation:
[SlideConfirmView.cs](../NP.SDK.Android.Controls/Controls/SlideConfirmView.cs)

## Build the APK

1. Open the root `NP.SDK.sln` in Visual Studio with Xamarin.Android installed.
2. Set `NP.SDK.Android.Showcase` as the startup project.
3. Select Debug or Release and build.
4. Install the generated APK on an Android device or emulator.

The project targets Android API 22 and declares minimum API level 19.
The repository contains source code and project files, not a prebuilt APK.
