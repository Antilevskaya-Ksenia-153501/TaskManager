using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Domain.Entities
{
    public class UserTask : Entity
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; } //GroupIdentifier
    }
}
