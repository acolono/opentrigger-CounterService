using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dncCnt
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exUid = Guid.NewGuid();
            Console.Error.WriteLine($"uid={exUid}, Ex={context.Exception}");
            context.Result = new ObjectResult(new
            {
                Error = context.Exception.GetType(),
                ExceptionUid = exUid.ToString(),
            }) { StatusCode = 500 };
        }
    }
}
