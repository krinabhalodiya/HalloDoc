using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IAdminDashBoardActionsRepository
    {
        public bool EditCase(ViewCaseData model);
        public ViewCaseData GetRequestForViewCase(int id);
    }
}