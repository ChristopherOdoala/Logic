using System;
using System.Collections.Generic;
using System.Text;
using static Logic.Model.ViewModel.EmployeeViewModel;

namespace Logic.Model
{
    public class Status
    {
        public enum ValidationLevel
        {
            Third_Party_Validation = 1,
            SI_Validation,
            Audit_Validation,
            ES_Validation
        }

        public Guid Id { get; set; }
        public ValidationLevel CurrentStage  { get; set; }
        public int NoOfStages { get; set; }
        public string ListOfExempted { get; set; }
        public int NoOfExempted { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int FinalStage { get; set; }
        public ValidationState Validation { get; set; }
    }
}
