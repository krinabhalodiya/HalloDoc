using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using HallodocMVC.Repository.Admin.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HalloDoc.Models;
using System.Web.WebPages;


namespace HalloDoc.Controllers.Admin
{
    [AttributeUsage(AttributeTargets.All)]
    public class CheckProviderAccess : Attribute, IAuthorizationFilter
    {
        private readonly List<string> _role;
		private readonly string _manu;
		public CheckProviderAccess(string role="", string? manu = "")
        {
            _role = role.Split(',').ToList();
            _manu = manu;
		}
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var jwtservice = filterContext.HttpContext.RequestServices.GetService<IJwtService>();
            if (jwtservice == null)
            {
                filterContext.Result = new RedirectResult("../Home/AuthError");
                return;
            }
            var request = filterContext.HttpContext.Request;
            var toket = request.Cookies["jwt"];
            if (toket == null || !jwtservice.ValidateToken(toket, out JwtSecurityToken jwtSecurityTokenHandler))
            {
                filterContext.Result = new RedirectResult("../Home/AuthError");
                return;
            }
            var roles = jwtSecurityTokenHandler.Claims.FirstOrDefault(claiim => claiim.Type == ClaimTypes.Role);
			List<string> str = null;
			if (CV.role() != "Patient")
			{

				int RoleID = jwtSecurityTokenHandler.Claims.FirstOrDefault(claiim => claiim.Type == "RoleID").Value.AsInt();
				str = new List<string>();
				var Accessrepo = filterContext.HttpContext.RequestServices.GetService<IRoleAccessRepository>();
				str = Accessrepo.getManuByID(RoleID);
			}
			if (roles == null)
            {
                filterContext.Result = new RedirectResult("../Home/AuthError");
                return;
            }
            var flag = false;
            foreach (var role in _role)
            {
                if (string.IsNullOrWhiteSpace(role) || roles.Value != role)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                    break;
                }
            }
			if (CV.role() != "Patient")
			{
				if (flag == false || !str.Contains(_manu))
				{
					filterContext.Result = new RedirectResult("../Home/AuthError");
				}
			}
			if (!flag)
            {
                filterContext.Result = new RedirectResult("../Home/AuthError");
            }
        }
    }
}
