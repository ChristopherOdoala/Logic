using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Division { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
