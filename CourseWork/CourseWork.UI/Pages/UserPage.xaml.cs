using CourseWork.UI.ViewModels;

namespace CourseWork.UI.Pages;

public partial class UserPage : ContentPage
{
	public UserPage(UserViewModel userViewModel)
	{
		InitializeComponent();
		BindingContext = userViewModel;
	}

    protected override bool OnBackButtonPressed()
    {
        IDictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "CheckedUser", ((UserViewModel)BindingContext).SelectedUser}
        };
        Shell.Current.GoToAsync($"//{nameof(TasksManager)}", parameters);
        return true;
    }
    private void onLoaded(object sender, EventArgs e)
    {
        image.Source = ((UserViewModel)BindingContext).SelectedUser.ImagePath;
        nameLabel.Text = "Name: " + ((UserViewModel)BindingContext).SelectedUser.Name;
        emailLabel.Text = "Email: " + ((UserViewModel)BindingContext).SelectedUser.Email;
    }
}