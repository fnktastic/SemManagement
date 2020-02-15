using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class BoolResult
    {
        public bool Success { get; set; }

        public BoolResult()
        {

        }
        public BoolResult(bool success)
        {
            Success = success;
        }
    }
}
