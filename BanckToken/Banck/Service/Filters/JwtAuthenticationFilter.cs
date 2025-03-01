using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Service.Filters
{
    public class JwtAuthenticationFilter : AuthorizationFilterAttribute
    {
        // Clave y parámetros de validación que usaste al generar el token:
        private const string SecretKey = "f5aN7jVh9F2LwB3zD6rM8qS1pW0tX4kY";
        private const string Issuer = "tu_issuer";
        private const string Audience = "tu_audience";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // 1) Verificar si la acción o el controlador tienen [AllowAnonymous]
            if (SkipAuthorization(actionContext))
            {
                // No hacemos nada, permitimos la ejecución sin token
                return;
            }

            // 2) Leer el encabezado Authorization
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                // No hay token o el esquema no es Bearer
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { Message = "No se proporcionó un token válido en la cabecera 'Authorization'." }
                );
                return;
            }

            // 3) Validar el token
            var token = authHeader.Parameter;
            if (!ValidateToken(token, out IPrincipal principal))
            {
                // Token inválido o expirado
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { Message = "Token inválido o expirado." }
                );
                return;
            }

            // 4) Si el token es válido, establecer el usuario en el contexto
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            // Continuar con la ejecución
            base.OnAuthorization(actionContext);
        }

        private bool ValidateToken(string token, out IPrincipal principal)
        {
            principal = null;

            try
            {
                // Preparamos los parámetros de validación
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,  // Se valida la expiración
                    ClockSkew = TimeSpan.Zero // Sin tolerancia en la expiración
                };

                // Validamos el token
                var principalTemp = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Si la validación es correcta, asignamos el principal
                principal = principalTemp;

                // Devolvemos true si todo salió bien
                return true;
            }
            catch
            {
                // Cualquier excepción significa que el token es inválido
                return false;
            }
        }

        private bool SkipAuthorization(HttpActionContext actionContext)
        {
            // Si el action o el controller tienen [AllowAnonymous], se salta la autenticación
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}
