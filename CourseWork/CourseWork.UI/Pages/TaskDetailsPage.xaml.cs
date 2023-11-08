using CourseWork.UI.ViewModels;

namespace CourseWork.UI.Pages;

public partial class TaskDetailsPage : ContentPage
{
    private TasksManagerViewModel _tasksManagerViewModel;
	public TaskDetailsPage(TasksManagerViewModel tasksManagerViewModel,TaskDetailsViewModel taskDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = taskDetailsViewModel;
        _tasksManagerViewModel = tasksManagerViewModel;
	}

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync($"//{nameof(TasksManager)}");
        ((TaskDetailsViewModel)BindingContext).SelectedTask.IsCompleted = checkBox.IsChecked;
        _tasksManagerViewModel.UpdateCompletedAsync(((TaskDetailsViewModel)BindingContext).SelectedTask);
        return true;
    }
    private void onLoaded(object sender, EventArgs e)
    {
        nameLabel.Text = "Name: " + ((TaskDetailsViewModel)BindingContext).SelectedTask.Name;
        descriptionLabel.Text = "Description: " + ((TaskDetailsViewModel)BindingContext).SelectedTask.Description;
        locationLabel.Text = "Location: " + ((TaskDetailsViewModel)BindingContext).SelectedTask.Location;
        checkBox.IsChecked = ((TaskDetailsViewModel)BindingContext).SelectedTask.IsCompleted;
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        await _tasksManagerViewModel.DeleteTaskAsync(((TaskDetailsViewModel)BindingContext).SelectedTask);
    }
}