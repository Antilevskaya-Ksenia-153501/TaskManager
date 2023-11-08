using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Application.Abstractions;
using CourseWork.Domain.Entities;
using CourseWork.UI.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentValidation.Validators;

namespace CourseWork.UI.ViewModels
{
    public partial class LoginManagerViewModel : ObservableObject
    {
        private IUserService _userService;
        public User CheckedUser { get; set; } = null;

        public LoginManagerViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string name;

        [RelayCommand]
        public async void Login() => await GotoMain();

        public async Task GotoMain()
        {
            if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Error", "All fields must be filled", "OK");
            }
            else if (!(new EmailFluentValidator().Validate(Email).IsValid))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid email", "OK");
            }
            else
            {
                CheckedUser = _userService.GetByEmailAsync(Email).Result;
                if (CheckedUser != null && CheckedUser.Password == Password)
                {
                    IDictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "CheckedUser", CheckedUser}
                    };
                    Email = String.Empty;
                    Password = String.Empty;
                    await Shell.Current.GoToAsync($"//{nameof(TasksManager)}", parameters);
                }
                else if (CheckedUser != null && CheckedUser.Password != Password)
                {
                    await Shell.Current.DisplayAlert("Error", "Wrong password!!!", "OK");
                }
                else if (CheckedUser == null)
                {
                    await Shell.Current.DisplayAlert("Error", "There is no such user. You should sign up first", "OK");
                }
            }
        }

        [RelayCommand]
        public async void SignUp() => await AddUser();

        [RelayCommand]
        public async void GoToSignUpPage()
        {
            Email = String.Empty;
            Password = String.Empty;
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        }
        public async Task AddUser()
        {
            if (String.IsNullOrEmpty(Name) ||  String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Error", "All fields must be filled", "OK");
            }
            else if (!(new EmailFluentValidator().Validate(Email).IsValid))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid email", "OK");
            }
            else if (_userService.GetByEmailAsync(Email).Result != null) 
            {
                await Shell.Current.DisplayAlert("Error", "User with such email already exists. Try to sign in", "OK");
            }
            else
            {
                
                CheckedUser = new User();
                CheckedUser.Email = Email;
                CheckedUser.Password = Password;
                CheckedUser.Name = Name;
                await _userService.AddAsync(CheckedUser);
                CheckedUser = null;
                Name = String.Empty;
                Email = String.Empty;
                Password = String.Empty;
                await Shell.Current.GoToAsync("..");

            }
        }
        public async void BackToSignInPage()
        {
            Name = String.Empty;
            Email = String.Empty;
            Password = String.Empty;
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async void SignOut()
        {
            CheckedUser = null;
            await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");
        }

        public class EmailFluentValidator : AbstractValidator<string>
        {
            public EmailFluentValidator()
            {
                RuleFor(email => email)
                    .EmailAddress(EmailValidationMode.Net4xRegex);
            }
        }
    }
}
