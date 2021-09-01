using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsApp.WEB.Errors
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult { Content = $"Возникла ошибка: {context.Exception.Message}" };
            context.ExceptionHandled = true;
        }
    }
}