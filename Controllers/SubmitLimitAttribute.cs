using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WeddingProject.Controllers
{
    public class SubmitLimitAttribute : ActionFilterAttribute
    {
        private readonly int _limitSeconds;

        public SubmitLimitAttribute(int limitSeconds)
        {
            _limitSeconds = limitSeconds;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var lastSubmitTime = filterContext.HttpContext.Session["lastSubmitTime"];

            if (lastSubmitTime != null && (DateTime.Now - (DateTime)lastSubmitTime).TotalSeconds < _limitSeconds)
            {
     
              //0.5秒后停顿
                Task.Delay(TimeSpan.FromSeconds(0.5)).Wait();
                filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.UrlReferrer.ToString());
            }
            else
            {
                filterContext.HttpContext.Session["lastSubmitTime"] = DateTime.Now;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}