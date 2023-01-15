using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PlanSimple.Controls;

public partial class HoverToggleButton : ToggleButton
{
	static readonly Brush DefaultHoverBackgroundValue = (new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush)!;
	static readonly Brush DefaultHoverForegroundValue = (new BrushConverter().ConvertFromString("#FF000000") as Brush)!;

	public HoverToggleButton()
	{
		InitializeComponent();
	}

	public Brush HoverBackground
	{
		get => (Brush)GetValue(HoverBackgroundProperty);
		set => SetValue(HoverBackgroundProperty, value);
	}
	public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
		nameof(HoverBackground), typeof(Brush), typeof(HoverToggleButton), new PropertyMetadata(DefaultHoverBackgroundValue));
	
	public Brush HoverForeground
	{
		get => (Brush)GetValue(HoverForegroundProperty);
		set => SetValue(HoverForegroundProperty, value);
	}
	public static readonly DependencyProperty HoverForegroundProperty = DependencyProperty.Register(
		nameof(HoverForeground), typeof(Brush), typeof(HoverToggleButton), new PropertyMetadata(DefaultHoverForegroundValue));
	
	public int CornerRadius
	{
		get => (int)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}
	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
		nameof(CornerRadius), typeof(int), typeof(HoverToggleButton), new PropertyMetadata(defaultValue:0));
}