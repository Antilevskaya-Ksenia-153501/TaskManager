using CourseWork.UI.ViewModels;

namespace CourseWork.UI.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(LoginManagerViewModel loginManagerViewModel)
	{
		InitializeComponent();
		BindingContext= loginManagerViewModel;
	}
    protected override bool OnBackButtonPressed()
    {
        ((LoginManagerViewModel)BindingContext).BackToSignInPage();
        return true;
    }
    private void PasswordWatch_Tapped(object sender, TappedEventArgs e)
    {
        if (passwordEntry.IsPassword == true)
        {
            passwordEntry.IsPassword = false;
        }
        else if (passwordEntry.IsPassword == false)
        {
            passwordEntry.IsPassword = true;
        }
    }
}