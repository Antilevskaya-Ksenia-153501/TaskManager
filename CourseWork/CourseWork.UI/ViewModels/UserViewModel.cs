using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Application.Abstractions;
using CourseWork.Domain.Entities;
using CourseWork.UI.Pages;

namespace CourseWork.UI.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        public User SelectedUser { get; set; }
        private LoginManagerViewModel _loginManagerViewModel;
        private IUserService _userService;
        public UserViewModel(LoginManagerViewModel loginManagerViewModel, IUserService userService)
        {
            _loginManagerViewModel = loginManagerViewModel;
            SelectedUser = _loginManagerViewModel.CheckedUser;
            _userService = userService;
        }

        [RelayCommand]
        public async void ChangePhoto()
        {
            var result = await FilePicker.Default.PickAsync();

            if (result != null && (result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase)
                || result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase)))
            {
                SelectedUser.ImagePath = result.FullPath;
                await _userService.UpdateAsync(SelectedUser);
                await Shell.Current.GoToAsync(nameof(UserPage));
            }
        }

        [RelayCommand]
        public async void Delete()
        {
            await _userService.DeleteAsync(SelectedUser);
            await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");
        }
    }
}

