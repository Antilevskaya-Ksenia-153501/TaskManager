using CourseWork.UI.ViewModels;

namespace CourseWork.UI.Pages;

public partial class TasksManager : ContentPage
{
	private LoginManagerViewModel _loginManagerViewModel;
	private bool isPanelView = false;
	public TasksManager(LoginManagerViewModel loginManagerViewModel, TasksManagerViewModel tasksManagerViewModel)
	{
		InitializeComponent();
		BindingContext = tasksManagerViewModel;
		_loginManagerViewModel= loginManagerViewModel;
		panelView.TranslateTo(-80, 0, 150);
	}

    private void Exit_Tapped(object sender, TappedEventArgs e)
    {
		_loginManagerViewModel.SignOut();
    }

    private void PanelView_Tapped(object sender, TappedEventArgs e)
    {
		if (isPanelView)
		{
            panelView.TranslateTo(-80, 0, 150);
        }
		else if (!isPanelView)
		{
            panelView.TranslateTo(0, 0, 150);
		}
		isPanelView = !isPanelView;
    }
}