using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Skoruba.IdentityServer4.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class Auth39Attribute : System.Attribute, IAsyncAuthorizationFilter
    {
        public Auth39Attribute(string name = "")
        {
            Name = name;
        }

        public string Name { get; set; }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //string controller = context.HttpContext.RouteData.Values["controller"].ToString();
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            if (!(context.ActionDescriptor is ControllerActionDescriptor))
            {
                return;
            }

            var attributeList = new List<object>();
            attributeList.AddRange((context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(true));
            attributeList.AddRange((context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.DeclaringType.GetCustomAttributes(true));
            var authorizeAttributes = attributeList.OfType<Auth39Attribute>().ToList();


            var claims = context.HttpContext.User;
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();

            string globalConfig = AttributeConfig.Get("Auth:Claims:Global");
            string controllerConfig = AttributeConfig.Get($"Auth:Claims:{controller}:Controller");
            string actionConfig = AttributeConfig.Get($"Auth:Claims:{controller}:{action}");

            if (string.IsNullOrEmpty(globalConfig) && string.IsNullOrEmpty(controller) && string.IsNullOrEmpty(actionConfig))
            {
                context.Result = new JsonResult(new { msg = "未正确配置Claims" });
                await Task.CompletedTask;
            }
            else
            {
                ClaimsPrincipal user = context.HttpContext.User;
                if (!string.IsNullOrEmpty(actionConfig))
                {
                    if (CheckClaim(user, actionConfig)) return;
                }
                else if (!string.IsNullOrEmpty(controllerConfig))
                {
                    if (CheckClaim(user, controllerConfig)) return;
                }
                else if (!string.IsNullOrEmpty(globalConfig))
                {
                    if (CheckClaim(user, globalConfig)) return;
                }

                context.Result = new ForbidResult();
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 检查client是否拥有配置要求的claim
        /// </summary>
        /// <param name="user"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool CheckClaim(ClaimsPrincipal user, string claimConfig)
        {
            if (!string.IsNullOrEmpty(claimConfig))
            {
                string[] claimArr = claimConfig.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var claim in claimArr)
                {
                    if (user.HasClaim(c => c.Value == claim))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
