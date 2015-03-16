using System.Collections.Generic;

namespace AHM.Common
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public List<string> Errors { get; set; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }
    }
}