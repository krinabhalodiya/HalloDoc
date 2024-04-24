using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity.Model;

namespace Assignment.Repository.Repository.Interface
{
    public interface IEmployeeDataRepository
    {
        public PaginatedViewModel GetDatabyid(int Id);
        public PaginatedViewModel GetData(PaginatedViewModel data);
        public Task<bool> AddEmployee(PaginatedViewModel data);
        public Task<bool> DeleteEmployee(int employeeid);
        public Task<bool> EditEmployee(PaginatedViewModel data);
    }
}
