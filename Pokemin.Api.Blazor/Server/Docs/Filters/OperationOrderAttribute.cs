using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Api.Docs.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OperationOrderAttribute : Attribute
    {
        public int Order { get; }

        public OperationOrderAttribute(int order)
        {
            this.Order = order;
        }
    }
}
