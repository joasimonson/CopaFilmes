using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CopaFilmes.Api.Middlewares.Exceptions
{
    public class ProduceResponseTypeModelProvider : IApplicationModelProvider
    {
        public int Order => 3;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                foreach (ActionModel action in controller.Actions)
                {
                    action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError));
                }
            }
        }
    }
}
