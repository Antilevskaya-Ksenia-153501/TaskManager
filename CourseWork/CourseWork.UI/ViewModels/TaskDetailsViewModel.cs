using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Domain.Entities;
using CourseWork.Application.Abstractions;
using CourseWork.UI.Pages;
using CommunityToolkit.Mvvm.Input;


namespace CourseWork.UI.ViewModels
{
    public partial class TaskDetailsViewModel : IQueryAttributable
    {
        public UserTask SelectedTask { get; set; }
        private IUserTaskService _userTaskService;
        public TaskDetailsViewModel(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count != 0)
            {
                SelectedTask = (UserTask)query["TappedTask"];
            }
        }

        [RelayCommand]
        public async void EditTask() => await GoToEditPage();

        private async Task GoToEditPage()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "EditedTask", SelectedTask }
            };
            await Shell.Current.GoToAsync(nameof(TaskEditPage), parameters);
            
        }
    }
}
