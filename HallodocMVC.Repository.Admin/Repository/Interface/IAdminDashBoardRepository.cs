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
        public PaginatedViewModel Indexdata(int ProviderId);
        PaginatedViewModel GetRequests(string Status, PaginatedViewModel data);
        public PaginatedViewModel GetRequests(string Status, PaginatedViewModel data, int ProviderId);
        public List<AdminDashboardList> Export(string status);
        public bool SendLink(String Email, int userid, string role);
        public bool SendSMS(String mobile, int userid, string role);
    }
}