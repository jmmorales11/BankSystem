using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Service.Filters
{
    public class ExcludeJwtAuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Si la acción es de login, no aplicar el filtro de autenticación
            if (actionContext.ActionDescriptor.ActionName == "Login")
            {
                // Llamar al siguiente filtro sin aplicar el JWT Authentication
                base.OnActionExecuting(actionContext);
            }

        }
    }

}