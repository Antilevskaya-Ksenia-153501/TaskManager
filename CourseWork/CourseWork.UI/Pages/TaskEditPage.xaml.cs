using CourseWork.UI.ViewModels;
namespace CourseWork.UI.Pages;

public partial class TaskEditPage : ContentPage
{
	private TasksManagerViewModel _taskManagerViewModel;
	public TaskEditPage(TaskEditViewModel taskEditViewModel, TasksManagerViewModel tasksManagerViewModel)
	{
		InitializeComponent();
		BindingContext = taskEditViewModel;
		_taskManagerViewModel = tasksManagerViewModel;
	}
    private void onLoadedPage(object sender, EventArgs e)
    {
        titleEntry.Text = ((TaskEditViewModel)BindingContext).SelectedTask.Name;
        descriptionEntry.Text = ((TaskEditViewModel)BindingContext).SelectedTask.Description;
        locationEntry.Text = ((TaskEditViewModel)BindingContext).SelectedTask.Location;
        startTimePicker.Time = ((TaskEditViewModel)BindingContext).SelectedTask.StartDateTime - ((TaskEditViewModel)BindingContext).SelectedTask.StartDateTime.Date;
        endTimePicker.Time = ((TaskEditViewModel)BindingContext).SelectedTask.EndDateTime - ((TaskEditViewModel)BindingContext).SelectedTask.EndDateTime.Date;
    }

    private async void OnOkClicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(titleEntry.Text) || String.IsNullOrEmpty(descriptionEntry.Text) || String.IsNullOrEmpty(locationEntry.Text))
        {
            await Shell.Current.DisplayAlert("Error", "All fields must be filled", "OK");
        }
        else
        {
            ((TaskEditViewModel)BindingContext).SelectedTask.Name = titleEntry.Text;
            ((TaskEditViewModel)BindingContext).SelectedTask.Description = descriptionEntry.Text;
            ((TaskEditViewModel)BindingContext).SelectedTask.Location = locationEntry.Text;
            ((TaskEditViewModel)BindingContext).SelectedTask.StartDateTime = ((TaskEditViewModel)BindingContext).SelectedTask.StartDateTime.Date + startTimePicker.Time;
            ((TaskEditViewModel)BindingContext).SelectedTask.EndDateTime = ((TaskEditViewModel)BindingContext).SelectedTask.StartDateTime.Date + endTimePicker.Time;
            await _taskManagerViewModel.UpdateTaskAsync(((TaskEditViewModel)BindingContext).SelectedTask);
        }
    }
}