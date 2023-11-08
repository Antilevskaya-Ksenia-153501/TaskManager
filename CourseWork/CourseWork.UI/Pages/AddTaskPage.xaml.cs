using CourseWork.Domain.Entities;
using CourseWork.UI.ViewModels;

namespace CourseWork.UI.Pages;

public partial class AddTaskPage : ContentPage
{
	public AddTaskPage(TasksManagerViewModel tasksManagerViewModel)
	{
		InitializeComponent();
		BindingContext = tasksManagerViewModel;
	}

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(titleEntry.Text) || String.IsNullOrEmpty(descriptionEntry.Text) || String.IsNullOrEmpty(locationEntry.Text))
        {
            await Shell.Current.DisplayAlert("Error", "All fields must be filled", "OK");
        }
        else
        {
            UserTask temp = new UserTask();
            temp.Name = titleEntry.Text;
            temp.Description = descriptionEntry.Text;
            temp.Location = locationEntry.Text;
            temp.StartDateTime = ((TasksManagerViewModel)BindingContext).SelectedDate.Date + startTimePicker.Time;
            temp.EndDateTime = ((TasksManagerViewModel)BindingContext).SelectedDate.Date + endTimePicker.Time;
            temp.UserId = ((TasksManagerViewModel)BindingContext).CurrentUser.Id;
            await ((TasksManagerViewModel)BindingContext).AddTaskAsync(temp);
        }
    }
}