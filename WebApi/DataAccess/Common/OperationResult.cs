using System;
using System.Collections.Generic;
using System.Text;
using Remotion.Linq.Clauses;

namespace DataAccess.Common
{
    public class OperationResult<T>
    {
        public eOperationStatus Status { get; set; }
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }
    }
}
