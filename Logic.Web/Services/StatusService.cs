using Logic.Data;
using Logic.Model;
using Logic.Web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Logic.Model.Status;

namespace Logic.Web.Services
{
    public class StatusService : IStatusService
    {
        IRepository _repo;
        protected List<ValidationResult> results = new List<ValidationResult>();
        IConfigSettingsService _configuration;
        public StatusService(IConfigSettingsService configuration, IRepository repo)
        {
            this._configuration = configuration;
            this._repo = repo;
        }

        //list of enum indexes
        public int[] EnumList()
        {
            List<int> result = new List<int>();
            foreach (var str in Enum.GetValues(typeof(ValidationLevel)))
            {
                var id = (int)str;
                result.Add(id);
            }
            var res = result.ToArray();
            return res;
        }

        public string GetListofExempted()
        {
            return _configuration.ListOfExempted;
        }

        public int NoOfStages()
        {
            int count = EnumList().Count();
            var result = count - ListOfExempted().Count();
            return result;
        }

        public int[] ListOfExempted()
        {
            List<int> result = new List<int>();
            var exemptedStages = GetListofExempted();
            var noOfExempted = NoOfExempted();
            String[] list = exemptedStages.Split(",", noOfExempted, StringSplitOptions.RemoveEmptyEntries);
            foreach(var entry in list)
            {
                var newEntry = entry.Trim();
                int level = int.Parse(newEntry);
                result.Add(level);
            }
            var res = result.ToArray();
            return res;
        }

        public int NoOfExempted()
        {
            var noOfExempted = int.Parse(_configuration.NoOfExempted);
            return noOfExempted;
        }

        public Tuple<int, List<ValidationResult>> InitialState()
        {
            var enumList = EnumList();
            var initialState = enumList.Unique<int[]>(ListOfExempted());
            if (initialState.EqualsZero<int>())
            {
                results.Add(new ValidationResult("Error: All levels were exempted"));
                return new Tuple<int, List<ValidationResult>>(0, results);
            }
            return new Tuple<int, List<ValidationResult>>(initialState, results);
        }
        
        public List<ValidationResult> CreateInitialStatus(Guid employeeId)
        {
            ValidationLevel validLvl;
            if (InitialState().Item1 == 0)
                return InitialState().Item2;

            var value = InitialState().Item1.ToString();
            Enum.TryParse(value, out validLvl);
            var finalStage = EnumList().UniqueList<int[]>(ListOfExempted()).Last();
            var newStatus = new Status()
            {
                CurrentStage = validLvl,
                NoOfStages = NoOfStages(),
                ListOfExempted = GetListofExempted(),
                EmployeeId = employeeId,
                FinalStage = finalStage,
                NoOfExempted = NoOfExempted()
            };
            if (newStatus == null)
            {
                results.Add(new ValidationResult("Status is empty"));
            }
            try
            {
                if (_repo.Insert(newStatus) && _repo.SaveAll())
                    return results;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            results.Add(new ValidationResult("Something went wrong"));
            return results;
        }

        public int NextStatus(Status status)
        {
            if((int)status.CurrentStage == status.FinalStage)
            {
                return 0;
            }

            int count = 0;
            List<int> listExempted = new List<int> { };
            var exempted = status.ListOfExempted;
            var exemptedList = exempted.Split(",", (status.NoOfExempted), StringSplitOptions.RemoveEmptyEntries);
            foreach(var lst in exemptedList)
            {
                count += 1;
                var value = int.Parse(lst.Trim());
                listExempted.Add(value);
            }
            
            List<int> validationLvls = new List<int>();
            foreach (var str in Enum.GetValues(typeof(ValidationLevel)))
            {
                var id = (int)str;
                validationLvls.Add(id);
            }

            var currentStage = (int)status.CurrentStage;
            validationLvls.Remove(currentStage);
            var result = validationLvls.ToArray().Unique<int[]>(listExempted.ToArray());

            return result;
        }

        /* 
           First value when true indicates that status has been updated to next level
           Second value when true indicates that the current status is the final
           last value is an error validation result and is null if there is no error
        */
        public Tuple<bool, bool, List<ValidationResult>> UpdateToNextStatus(Status status)
        {
            if (NextStatus(status).EqualsZero<int>())
            {
                return new Tuple<bool, bool, List<ValidationResult>>(false, true, results);
            }

            ValidationLevel validLvl;
            var value = NextStatus(status).ToString();
            Enum.TryParse(value, out validLvl);
            status.CurrentStage = validLvl;

            try
            {
                if (_repo.Update(status) && _repo.SaveAll())
                {
                    return new Tuple<bool, bool, List<ValidationResult>>(true, false, results);
                }
            }
            catch(Exception ex)
            {
                results.Add(new ValidationResult("Could not Update Database"));
                return new Tuple<bool, bool, List<ValidationResult>>(false, false, results);
            }

            results.Add(new ValidationResult("Something went wrong"));
            return new Tuple<bool, bool, List<ValidationResult>>(false, false, results);
        }

    }
}
