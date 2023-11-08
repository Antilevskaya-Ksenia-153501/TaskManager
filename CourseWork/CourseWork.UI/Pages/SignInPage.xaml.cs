using CourseWork.UI.ViewModels;
namespace CourseWork.UI.Pages;

public partial class SignInPage : ContentPage
{
    public SignInPage(LoginManagerViewModel loginManagerViewModel)
    {
        InitializeComponent();
        BindingContext = loginManagerViewModel;
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