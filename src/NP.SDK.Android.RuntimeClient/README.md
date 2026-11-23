# NP.SDK.Android.RuntimeClient

The original Android runtime client remains the main application. Its WebSocket connection workflow is unchanged.

An **Open Custom Controls Lab** button is available directly on the main screen (and an options-menu shortcut is also present). It opens the same `SlideConfirmLabView` test page used by `NP.SDK.Android.Showcase`, allowing the control to be exercised in a second host application.

The RuntimeClient now references `NP.SDK.Android.Controls` for this optional demonstration page.
