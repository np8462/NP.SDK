# NP.SDK.Android.Showcase

Standalone Xamarin.Android application for interactive demonstrations of reusable controls from `NP.SDK.Android.Controls`.

## Tabs

- **SlideConfirm** — a four-tab test laboratory: Preview, Properties, C# Sample and Events.
- **Buttons / Progress / Gestures** — reserved extension tabs; controls in these families are not implemented yet.
- **Samples** — basic usage and source links.

## SlideConfirm laboratory

The shared `SlideConfirmLabView` runs in both the Showcase and RuntimeClient applications. It lets you:

- Drag the live `SlideConfirmView` and observe its confirmation event.
- Change `ConfirmThreshold` from 0% to 100% with a SeekBar; the value is applied immediately.
- Edit `PromptText` and `ConfirmedText`, then apply them to the live view.
- Reset the control and inspect `IsConfirmed` / `Progress`.
- Inspect the event log and a generated C# snippet reflecting the current property values.

The lab is a diagnostic/sample page, not part of the production control API.

## C# usage

```csharp
var slider = new SlideConfirmView(this);
slider.ConfirmThreshold = 0.85f;
slider.PromptText = "Slide to commit";
slider.ConfirmedText = "Done!";
slider.Confirmed += delegate { CommitOperation(); };
parentLayout.AddView(slider);
slider.Reset();
```

## Build

1. Open the root `NP.SDK.sln` in Visual Studio with Xamarin.Android installed.
2. Set `NP.SDK.Android.Showcase` as startup project.
3. Build and deploy to an Android device or emulator.

Targets Android API 22 with minimum API 19. No prebuilt APK is committed.
