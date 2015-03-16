using System.Collections.Generic;
using AHM.Common;

namespace AHM.BusinessLayer
{
    public class ModifyDbStateResult
    {
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }


        public ModifyDbStateResult()
        {
            Errors = new List<string>();
        }

        public ModifyDbStateResult(ValidationResult validationResult)
        {
            IsSuccessful = validationResult.IsValid;
            if (!IsSuccessful)
            {
                Errors = validationResult.Errors;
            }
        }
    }
}