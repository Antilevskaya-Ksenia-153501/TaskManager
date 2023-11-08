using CourseWork.Application.Abstractions;
using CourseWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.UI.ViewModels
{
    public partial class TaskEditViewModel : IQueryAttributable
    {
        public UserTask SelectedTask { get; set; }
        private IUserTaskService _userTaskService;
        public TaskEditViewModel(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count != 0)
            {
                SelectedTask = (UserTask)query["EditedTask"];
            }
        }
    }
}
