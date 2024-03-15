using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HalloDoc.Entity.Models;
using static HalloDoc.Entity.Models.PaginatedViewModel;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IAdminDashBoardRepository
    {
        PaginatedViewModel Indexdata();
        PaginatedViewModel GetRequests(string Status, PaginatedViewModel data);
    }
}