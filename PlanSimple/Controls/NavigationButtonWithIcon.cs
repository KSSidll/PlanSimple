using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlanSimple.Controls;

public partial class NavigationButtonWithIcon : RadioButton
{
	public NavigationButtonWithIcon()
	{
		InitializeComponent();
	}

	public object? Icon
	{
		get => (object?)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}
	public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
		nameof(Icon), typeof(object), typeof(NavigationButtonWithIcon), new PropertyMetadata(defaultValue:null));
	
	public FontFamily? IconFontFamily
	{
		get => (FontFamily?)GetValue(IconFontFamilyProperty);
		set => SetValue(IconFontFamilyProperty, value);
	}
	public static readonly DependencyProperty IconFontFamilyProperty = DependencyProperty.Register(
		nameof(IconFontFamily), typeof(FontFamily), typeof(NavigationButtonWithIcon), new PropertyMetadata(defaultValue:null));
}