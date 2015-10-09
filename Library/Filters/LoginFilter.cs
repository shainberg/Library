using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!"LoginController".Equals(filterContext.Controller.GetType().Name))
            {
                if (HttpContext.Current.Session["Connected"] == null || HttpContext.Current.Session["Connected"] == string.Empty)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Login" },
                                            { "action", "Index" }

                                            });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}