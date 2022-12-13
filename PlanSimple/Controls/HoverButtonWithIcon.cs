using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlanSimple.Controls;

public partial class HoverButtonWithIcon : Button
{
	static readonly Brush DefaultHoverBackgroundValue = (new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush)!;
	static readonly Brush DefaultHoverForegroundValue = (new BrushConverter().ConvertFromString("#FF000000") as Brush)!;

	public HoverButtonWithIcon()
	{
		InitializeComponent();
	}

	public Brush HoverBackground
	{
		get => (Brush)GetValue(HoverBackgroundProperty);
		set => SetValue(HoverBackgroundProperty, value);
	}
	public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
		nameof(HoverBackground), typeof(Brush), typeof(HoverButtonWithIcon), new PropertyMetadata(DefaultHoverBackgroundValue));
	
	public Brush HoverForeground
	{
		get => (Brush)GetValue(HoverForegroundProperty);
		set => SetValue(HoverForegroundProperty, value);
	}
	public static readonly DependencyProperty HoverForegroundProperty = DependencyProperty.Register(
		nameof(HoverForeground), typeof(Brush), typeof(HoverButtonWithIcon), new PropertyMetadata(DefaultHoverForegroundValue));
	
	public int CornerRadius
	{
		get => (int)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}
	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
		nameof(CornerRadius), typeof(int), typeof(HoverButtonWithIcon), new PropertyMetadata(defaultValue:0));
	
	public object? Icon
	{
		get => (object?)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}
	public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
		nameof(Icon), typeof(object), typeof(HoverButtonWithIcon), new PropertyMetadata(defaultValue:null));
	
	public FontFamily? IconFontFamily
	{
		get => (FontFamily?)GetValue(IconFontFamilyProperty);
		set => SetValue(IconFontFamilyProperty, value);
	}
	public static readonly DependencyProperty IconFontFamilyProperty = DependencyProperty.Register(
		nameof(IconFontFamily), typeof(FontFamily), typeof(HoverButtonWithIcon), new PropertyMetadata(defaultValue:null));
}