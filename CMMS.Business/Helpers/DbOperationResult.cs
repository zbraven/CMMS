using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Helpers
{
    public class DbOperationResult<T>
    {
        public DbOperationResult(bool isSucceed, string message, List<string> errors, T instance) 
        {
            IsSucceed = isSucceed;
            Message = message;
            Errors = errors;
            Instance = instance;
        }

        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Instance { get; set; }
    }
}
