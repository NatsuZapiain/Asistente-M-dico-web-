using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PacienteAsistente.Model.Enum;
using PacienteAsistente.Web.Service.Interface;
using PacienteAsistente.Web.Service.Service;

namespace PacienteAsistente.Utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class AuthorizedRoleAttribute : AuthorizeAttribute
    {
        private bool _authorized;
        private readonly RoleEnum _roleEnum;
        private IAuthorizeService _authorizeService;

        public AuthorizedRoleAttribute(RoleEnum rolEnum)
        {
            _roleEnum = rolEnum;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            _authorizeService = new AuthorizeService();
            _authorized = httpContext.User.Identity.IsAuthenticated && _authorizeService.GetIfHaveAothorization(httpContext.User.Identity.Name, _roleEnum);
            return _authorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "InicioSesion" }));
            else
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "InicioSesion" }));
        }
    }
}