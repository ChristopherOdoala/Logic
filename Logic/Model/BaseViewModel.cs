
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Logic.Model
{
    public abstract class BaseViewModel<T>
    {
        public BaseViewModel()
        {
            ErrorList = new List<string>();
        }

        public T Id { get; set; }
        public virtual Boolean HasError
        {
            get
            {
                if (this.ErrorList.Any())
                    return true;

                return false;
            }
        }

        public virtual List<string> ErrorList { get; set; }

        public Guid Created_Id { get; set; }
    }

    public abstract class BaseViewModel : BaseViewModel<string>, IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Created_Id == Guid.Empty)
            {
                yield return new ValidationResult("User editting record couldn't be determined");
            }
        }
    }
}
