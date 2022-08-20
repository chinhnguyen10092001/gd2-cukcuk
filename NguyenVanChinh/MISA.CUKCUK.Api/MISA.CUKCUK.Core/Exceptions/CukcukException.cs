using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Exceptions
{
    public class CukcukException:Exception
    {
        /// <summary>
        /// overide lại exception 
        /// CratedBy:NVCHINH (08/08/2022)
        /// </summary>
        public string? ValidateError { get; set; }
        public CukcukException(string error)
        {
            this.ValidateError = error;
        }
        public override string Message => this.ValidateError;
    }
}
