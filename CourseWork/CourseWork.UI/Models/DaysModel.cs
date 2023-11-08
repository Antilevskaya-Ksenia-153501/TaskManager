using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CourseWork.UI.Models
{
    public partial class DaysModel : ObservableObject
    {
        public char DayName { get; set; }
        public DateTime Date { get; set; }

        [ObservableProperty]
        public bool isSelected;
    }
}
