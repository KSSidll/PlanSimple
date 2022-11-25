using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlanSimple.Controls;

public partial class HoverListBox : ListBox
{
	static readonly Brush DefaultHoverBackgroundValue = (new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush)!;

	public HoverListBox()
	{
		InitializeComponent();
	}

	public Brush HoverBackground
	{
		get => (Brush)GetValue(HoverBackgroundProperty);
		set => SetValue(HoverBackgroundProperty, value);
	}
	public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
		nameof(HoverBackground), typeof(Brush), typeof(HoverListBox), new PropertyMetadata(DefaultHoverBackgroundValue));
}