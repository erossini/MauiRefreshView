# Building a MAUI CustomRefreshView

Solving the .NET MAUI RefreshView Limitation on macOS: building a MAUI CustomRefreshView from scratch for all platforms

## The Problem: RefreshView on macOS

The built-in RefreshView control in .NET MAUI enables pull-to-refresh functionality for scrollable content. However, as of .NET MAUI 9, RefreshView is not supported on macOS. Attempting to use it will result in runtime errors or simply no refresh gesture support.

### Why does this happen?

- The underlying gesture and native control implementation for RefreshView is missing on macOS.
- The official documentation and GitHub issues confirm this limitation.

### Impact

- macOS users cannot trigger refresh actions via pull gestures.
- UI consistency and user experience are affected.

Here some Microsoft documentation about it:

- [Specify the UI idiom for your Mac Catalyst app - .NET MAUI | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/mac-catalyst/user-interface-idiom?view=net-maui-9.0)
- [RefreshView Class (Microsoft.Maui.Controls) | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.refreshview?view=net-maui-9.0)
- [Customize UI appearance based on the platform and device idiom - .NET MAUI | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/customize-ui-appearance?view=net-maui-9.0)

### Warning

[UIStepper](/en-us/dotnet/api/uikit.uistepper), [UIPickerView](/en-us/dotnet/api/uikit.uipickerview), and [UIRefreshControl](/en-us/dotnet/api/uikit.uirefreshcontrol) aren't supported in the Mac user interface idiom by Apple. This means that the .NET MAUI controls that consume these native controls ([Stepper](/en-us/dotnet/api/microsoft.maui.controls.stepper), [Picker](/en-us/dotnet/api/microsoft.maui.controls.picker) and [RefreshView](/en-us/dotnet/api/microsoft.maui.controls.refreshview)) can't be used in the Mac user interface idiom. Attempting to do so will throw a macOS exception.

In addition, the following constraints apply in the Mac user interface idiom:

*   [UISwitch](/en-us/dotnet/api/uikit.uiswitch) throws a macOS exception when it's title is set in a non-Mac idiom view.
*   [UIButton](/en-us/dotnet/api/uikit.uibutton) throws a macOS exception when [AddGestureRecognizer](/en-us/dotnet/api/uikit.uiview.addgesturerecognizer) is called, or when [SetTitle](/en-us/dotnet/api/uikit.uibutton.settitle) or [SetImage](/en-us/dotnet/api/uikit.uibutton.setimage) are called for any state except `UIControlStateNormal.Normal`.
*   [UISlider](/en-us/dotnet/api/uikit.uislider) throws a macOS exception when the [SetThumbImage](/en-us/dotnet/api/uikit.uislider.setthumbimage), [SetMinTrackImage](/en-us/dotnet/api/uikit.uislider.setmintrackimage), [SetMaxTrackImage](/en-us/dotnet/api/uikit.uislider.setmaxtrackimage) methods are called and when the [ThumbTintColor](/en-us/dotnet/api/uikit.uislider.thumbtintcolor#uikit-uislider-thumbtintcolor), [MinimumTrackTintColor](/en-us/dotnet/api/uikit.uislider.minimumtracktintcolor#uikit-uislider-minimumtracktintcolor), [MaximumTrackTintColor](/en-us/dotnet/api/uikit.uislider.maximumtracktintcolor#uikit-uislider-maximumtracktintcolor), [MinValueImage](/en-us/dotnet/api/uikit.uislider.minvalueimage#uikit-uislider-minvalueimage), [MaxValueImage](/en-us/dotnet/api/uikit.uislider.maxvalueimage#uikit-uislider-maxvalueimage) properties are set.

## Usage

Here an example of how to use the component

```xml
<components:CustomRefreshView
	IndicatorBackground="{StaticResource Gray100}"
	IndicatorPosition="Top"
	IndicatorText="Loading data..."
	IndicatorTextColor="{StaticResource OffBlack}"
	IsRefreshing="{Binding IsRefreshing}"
	RefreshColor="{AppThemeBinding Light={StaticResource Primary10},
									Dark={StaticResource Primary10}}"
	RefreshCommand="{Binding RefreshCommand}">
	<components:CustomRefreshView.RefreshContent>
  </components:CustomRefreshView.RefreshContent>
</components:CustomRefreshView>
```
