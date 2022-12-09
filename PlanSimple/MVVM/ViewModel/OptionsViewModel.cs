using System;
using System.Windows;
using PlanSimple.MVVM.Model;

namespace PlanSimple.MVVM.ViewModel;

public class OptionsViewModel : BaseViewModel
{
	private static Theme _theme = Theme.Dark;
	public static Theme Theme
	{
		get => _theme;
		set
		{
			_theme = value;
			RefreshTheme();
		}
	}
	
	public static void RefreshTheme()
	{
		var app = (App)Application.Current;
		switch (Theme)
		{
			case Theme.Dark:
				app.ChangeTheme(new Uri("/Theme/Dark/Colors.xaml", UriKind.Relative));
				break;
			case Theme.Light:
				app.ChangeTheme(new Uri("/Theme/Light/Colors.xaml", UriKind.Relative));
				break;
		}
	}
	
}