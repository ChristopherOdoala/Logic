using Logic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Logic.Web.Services
{
    public interface IStatusService
    {
        int[] ListOfExempted();
        int NoOfExempted();
        List<ValidationResult> CreateInitialStatus(Guid employeeId);
        Tuple<bool, bool, List<ValidationResult>> UpdateToNextStatus(Status status);
    }
}    