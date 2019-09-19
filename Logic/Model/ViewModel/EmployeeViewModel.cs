using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Model.ViewModel
{
    public class EmployeeViewModel
    {
        public enum ValidationState
        {
            Pending = 1,
            Approved,
            Rejected
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public ValidationState Validation { get; set; }
    }
}
