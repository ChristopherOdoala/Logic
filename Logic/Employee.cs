using System;
using System.Collections.Generic;
using System.Text;
using static Logic.Model.ViewModel.EmployeeViewModel;

namespace Logic
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public ValidationState Validation { get; set; }

    }
}
