using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Domain.Entities;
using CourseWork.UI.Pages;
using CourseWork.UI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CourseWork.Application.Abstractions;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace CourseWork.UI.ViewModels
{
    public partial class TasksManagerViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private User currentUser;

        private IUserTaskService _userTaskService;
        private IUserService _userService;
        private List<UserTask> allUserTasks = new List<UserTask>();
        private LoginManagerViewModel _loginManagerViewModel;

        public TasksManagerViewModel(IUserTaskService userTaskService, IUserService userService, LoginManagerViewModel loginManagerViewModel)
        {
            _userTaskService = userTaskService;
            _userService = userService;
            _loginManagerViewModel = loginManagerViewModel;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count != 0)
            {
                CurrentUser = null;
                CurrentUser = (User)query["CheckedUser"];
                await FilterBySelectedDate();
            }
        }

        public ObservableCollection<DaysModel> WeekDays { get; set; } = new();
        public ObservableCollection<UserTask> SelectedDateTasks { get; set; } = new();

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        [RelayCommand]
        public async void UpdateMembersList() => await FilterBySelectedDate();

        public async Task GetTasks()
        {
            var tasks = await _userService.GetAllTasksByUserAsync(CurrentUser.Id);
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                allUserTasks.Clear();
                foreach (var task in tasks)
                {
                    allUserTasks.Add(task);
                }
            });
        }

        public async Task FilterBySelectedDate()
        {
            await GetTasks();
            var filtredList = allUserTasks.Where(task => task.StartDateTime.Date == SelectedDate.Date).ToList();
            filtredList = (filtredList.OrderBy(x => x.StartDateTime)).ToList();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                SelectedDateTasks.Clear();
                foreach (var task in filtredList)
                {
                    SelectedDateTasks.Add(task);
                }
            });
            GetWeekDaysInfo();
        }

        private void GetWeekDaysInfo()
        {
            DateTime startDayOfWeek = SelectedDate.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                (int)SelectedDate.DayOfWeek);
            WeekDays.Clear();
            for (int i = 0; i < 7; i++)
            {
                var recordToAdd = new DaysModel
                {
                    DayName = DayOfWeekChar((int)startDayOfWeek.DayOfWeek),
                    Date = startDayOfWeek.Date,
                    IsSelected = startDayOfWeek.Date == SelectedDate.Date,
                };
                WeekDays.Add(recordToAdd);
                startDayOfWeek = startDayOfWeek.AddDays(1);
            }
        }

        private char DayOfWeekChar(int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0:
                    return 'S';
                case 1:
                    return 'M';
                case 2:
                    return 'T';
                case 3:
                    return 'W';
                case 4:
                    return 'T';
                case 5:
                    return 'F';
                case 6:
                    return 'S';
            }
            return ' ';
        }

        [RelayCommand]
        public async void WeekDaysSelected(DaysModel item)
        {
            WeekDays.ToList().ForEach(f => f.IsSelected = false);
            item.IsSelected = true;
            SelectedDate = item.Date;
            await FilterBySelectedDate();
        }

        [RelayCommand]
        public async void AddTask() => await GoToAddTaskPage();

        private async Task GoToAddTaskPage()
        {
            await Shell.Current.GoToAsync(nameof(AddTaskPage));
        }

        public async Task AddTaskAsync(UserTask task)
        {
            await _userTaskService.AddAsync(task);
            UpdateMembersList();
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async void ShowDetails(UserTask task) => await GotoDetailsPage(task);

        private async Task GotoDetailsPage(UserTask task)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "TappedTask", task}
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailsPage), parameters);
        }

        public async Task UpdateTaskAsync(UserTask task)
        {
            await _userTaskService.UpdateAsync(task);
            UpdateMembersList();
            //IDictionary<string, object> parameters = new Dictionary<string, object>()
            //{
            //{ "TappedTask", task}
            //};
            //await Shell.Current.GoToAsync(nameof(TaskDetailsPage), parameters);
            await Shell.Current.GoToAsync($"//{nameof(TasksManager)}");
        }

        public async Task UpdateCompletedAsync(UserTask task)
        {
            await _userTaskService.UpdateAsync(task);
            UpdateMembersList();
        }

        public async Task DeleteTaskAsync(UserTask task)
        {
            await _userTaskService.DeleteAsync(task);
            UpdateMembersList();
            await Shell.Current.GoToAsync($"//{nameof(TasksManager)}");
        }

        [RelayCommand]
        public async Task GoToUserPage()
        {
            await Shell.Current.GoToAsync(nameof(UserPage));
        }
    }
}
