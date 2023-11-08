using CourseWork.UI.ViewModels;
using System.Globalization;

namespace CourseWork.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App()
	{
		CultureInfo ci = new CultureInfo("en-US");
		Thread.CurrentThread.CurrentCulture = ci;
		InitializeComponent();
		MainPage = new AppShell();
	}
}
