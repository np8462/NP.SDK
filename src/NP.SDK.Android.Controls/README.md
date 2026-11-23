# NP.SDK.Android.Controls

Reusable Xamarin.Android custom controls for NP.SDK applications.

## Implemented controls

### SlideConfirmView

Namespace: `NP.SDK.Android.Controls`

| Member | Description |
|---|---|
| `ConfirmThreshold` | Progress threshold from 0.0 to 1.0; default is 0.85 |
| `PromptText` | Text displayed before confirmation |
| `ConfirmedText` | Text displayed after confirmation |
| `Progress` | Current drag progress (read-only) |
| `IsConfirmed` | Whether the control has been confirmed (read-only) |
| `Confirmed` | Event raised once when confirmation succeeds |
| `Reset()` | Resets the control for reuse |

### Example

```csharp
var slider = new SlideConfirmView(this);
slider.ConfirmThreshold = 0.85f;
slider.PromptText = "Slide to commit";
slider.ConfirmedText = "Committed!";

slider.Confirmed += delegate
{
    // Execute the operation after the user confirms.
};

parentLayout.AddView(slider);
```

## Contribution pattern for future controls

1. Add the reusable control implementation under `Controls/`.
2. Keep Android-specific rendering and touch handling inside the control.
3. Expose configurable properties and meaningful .NET events.
4. Add a working interactive example to the matching Showcase tab.
5. Add a concise C# snippet and a direct GitHub source link to Samples.
6. Document the public API here.

The library currently contains SlideConfirmView. Other control families are
planned, not yet implemented.
