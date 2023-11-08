using CourseWork.UI.Pages;
using CourseWork.UI.ViewModels;

namespace CourseWork.UI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(TaskDetailsPage),
                          typeof(TaskDetailsPage));
        Routing.RegisterRoute(nameof(SignUpPage),
                         typeof(SignUpPage));
        Routing.RegisterRoute(nameof(AddTaskPage), 
                         typeof(AddTaskPage));
        Routing.RegisterRoute(nameof(TaskEditPage),
                         typeof(TaskEditPage));
        Routing.RegisterRoute(nameof(UserPage),
                         typeof(UserPage));
    }
}
