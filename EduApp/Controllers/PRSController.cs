using DevExpress.Web.Mvc;
using EduApp;
using EduApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduApp.Controllers
{
   // [Authorize]
    public class PRSController : Controller
    {
        EduNationEntity db = new EduNationEntity();
        public string GetUserRolesDto(string userName)
        {
            var model = (from ur in db.UserRoles.Where(c => c.UserId.ToString() == userName)
                         from roles in db.Roles.Where(c => c.Id.ToString() == ur.RoleId)
                         from user in db.Users.Where(c => c.Id.ToString() == ur.UserId)
                         select new UserRolesDto
                         {
                             ObjId = ur.ObjId,
                             RoleName = roles.Name,
                             RoleId = ur.RoleId,
                             UserId = user.Id.ToString()
                         });
            var results = model.FirstOrDefault();
            return results.RoleName;
        }
    }
}