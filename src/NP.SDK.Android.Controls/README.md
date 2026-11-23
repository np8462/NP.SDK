# NP.SDK.Android.Controls

Reusable Xamarin.Android custom controls for NP.SDK.

## Production controls

### SlideConfirmView

| Member | Description |
|---|---|
| `ConfirmThreshold` | Confirmation threshold from 0.0 to 1.0; default 0.85 |
| `PromptText` | Text displayed before confirmation |
| `ConfirmedText` | Text displayed after confirmation |
| `Progress` | Current drag progress (read-only) |
| `IsConfirmed` | Confirmation state (read-only) |
| `Confirmed` | Event raised once when confirmation succeeds |\n| `ProgressChanged` | Event raised when drag progress changes or the control resets |
| `Reset()` | Resets the control for reuse |

Property setters invalidate the view so runtime changes are immediately redrawn. `ProgressChanged` lets a host observe live drag progress.

## Diagnostic test page

`Testing/SlideConfirmLabView.cs` contains a reusable interactive test surface. It is used by both `NP.SDK.Android.Showcase` and `NP.SDK.Android.RuntimeClient`, and provides Preview, Properties, C# Sample and Events tabs.

It is a test/demo helper, not a production UI control. It exposes the actual control's configurable properties and generates a C# snippet from the selected values.

## Example

```csharp
var slider = new SlideConfirmView(this);
slider.ConfirmThreshold = 0.85f;
slider.PromptText = "Slide to commit";
slider.ConfirmedText = "Committed!";
slider.Confirmed += delegate { CommitOperation(); };
parentLayout.AddView(slider);
```

## Contribution pattern

1. Add reusable controls under `Controls/`.
2. Expose configurable properties and .NET events.
3. Add a working sample and property editor.
4. Link the sample from Showcase and document the API.
