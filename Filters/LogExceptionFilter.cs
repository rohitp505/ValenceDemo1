using HandleLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.UI.WebControls;
using System.Web.Routing;

namespace ValenceDemo.Filters
{
    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        private ILogException _ILogException;
        public LogExceptionFilter()
        {
            this._ILogException = logGenerator.CreateInstance;
        }
        //Exception overriding
        public override void OnException(HttpActionExecutedContext actioncontext)
        {
            string errorMessage = string.Empty;
            string errorMessageStack = string.Empty;
            string errorMessageSource = string.Empty;
            if (actioncontext.Exception.InnerException == null)
            {
                errorMessage = actioncontext.Exception.Message;
                errorMessageStack = actioncontext.Exception.StackTrace;
                errorMessageSource = actioncontext.Exception.Source;
            }
            else
            {


                errorMessage = actioncontext.Exception.InnerException.Message;
            }
            //We can log this exception message to the file or database.  
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service."),
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };
            //Log file 
            _ILogException.LogException(errorMessage,errorMessageStack,errorMessageSource);
            actioncontext.Response = response;
            Controllers.HomeController controller = new Controllers.HomeController();
            controller.Error();


        }

    }
}