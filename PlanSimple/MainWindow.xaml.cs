using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PlanSimple
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	// Disable ReSharper warning, part of pre-made boiler plate
	// ReSharper disable once RedundantExtendsListEntry
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void Maximize_Toggle_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			// Disable ReSharper warning, part of pre-made boiler plate
			// ReSharper disable once AssignNullToNotNullAttribute
			((HwndSource)PresentationSource.FromVisual(this)).AddHook(HookProc);
		}
		
		public static IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == WM_GETMINMAXINFO)
			{
				// We need to tell the system what our size should be when maximized. Otherwise it will cover the whole screen,
				// including the task bar.
				
				// Disable ReSharper warning, part of pre-made boiler plate
				#pragma warning disable CS8605
				MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
				#pragma warning restore CS8605
		
				// Adjust the maximized size and position to fit the work area of the correct monitor
				IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
		
				if (monitor != IntPtr.Zero)
				{
					// Disable ReSharper warning, part of pre-made boiler plate
					// ReSharper disable once UseObjectOrCollectionInitializer
					MONITORINFO monitorInfo = new MONITORINFO();
					monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
					GetMonitorInfo(monitor, ref monitorInfo);
					RECT rcWorkArea = monitorInfo.rcWork;
					RECT rcMonitorArea = monitorInfo.rcMonitor;
					mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
					mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
					mmi.ptMaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
					mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
				}
		
				Marshal.StructureToPtr(mmi, lParam, true);
			}
		
			return IntPtr.Zero;
		}
		
		// Disable ReSharper warning, part of pre-made boiler plate
		// ReSharper disable once InconsistentNaming
		private const int WM_GETMINMAXINFO = 0x0024;
		
		// Disable ReSharper warning, part of pre-made boiler plate
		// ReSharper disable once InconsistentNaming
		private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;
		
		[DllImport("user32.dll")]
		private static extern IntPtr MonitorFromWindow(IntPtr handle, uint flags);
		
		[DllImport("user32.dll")]
		private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
		
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		// Disable ReSharper warning, part of pre-made boiler plate
		// ReSharper disable once InconsistentNaming
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		
			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}
		}
		
		[StructLayout(LayoutKind.Sequential)]
		// Disable ReSharper warning, part of pre-made boiler plate
		// ReSharper disable once InconsistentNaming
		public struct MONITORINFO
		{
			public int cbSize;
			public RECT rcMonitor;
			public RECT rcWork;
			// Disable ReSharper warning, part of pre-made boiler plate
			// ReSharper disable once FieldCanBeMadeReadOnly.Global
			public uint dwFlags;
		}
		
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		// Disable ReSharper warning, part of pre-made boiler plate
		// ReSharper disable once InconsistentNaming
		public struct POINT
		{
			public int X;
			public int Y;
		
			public POINT(int x, int y)
			{
				X = x;
				Y = y;
			}
		}
		
		[StructLayout(LayoutKind.Sequential)]
		// Disable ReSharper warning, part of pre-made boiler plate
		// ReSharper disable once InconsistentNaming
		public struct MINMAXINFO
		{
			public POINT ptReserved;
			public POINT ptMaxSize;
			public POINT ptMaxPosition;
			public POINT ptMinTrackSize;
			public POINT ptMaxTrackSize;
		}
	}
}