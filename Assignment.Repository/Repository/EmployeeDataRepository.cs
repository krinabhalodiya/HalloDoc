using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.Entity.DataContext;
using Assignment.Entity.DataModels;
using Assignment.Entity.Model;
using Assignment.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using static Assignment.Entity.Model.EmployeeData;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Assignment.Repository.Repository
{
    public class EmployeeDataRepository : IEmployeeDataRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Getdata all employee
        public PaginatedViewModel GetData(PaginatedViewModel data)
        {
            List<EmployeeData> allData = (from emp in _context.Employees
                                          join dept in _context.Departments
                                          on emp.DeptId equals dept.Id into datagroup
                                          from dgroup in datagroup.DefaultIfEmpty()
                                          where emp.IsDelete == new BitArray(1) && (data.SearchInput == null ||
                                                         emp.FirstName.Contains(data.SearchInput) || emp.LastName.Contains(data.SearchInput) ||
                                                         emp.Email.Contains(data.SearchInput) || emp.Experience.Contains(data.SearchInput) ||
                                                         emp.Package.Contains(data.SearchInput) || emp.Education.Contains(data.SearchInput) ||
                                                         dgroup.Name.Contains(data.SearchInput) || emp.Company.Contains(data.SearchInput) ||
                                                         emp.Gender.Contains(data.SearchInput))
                                          select new EmployeeData
                                          {
                                              Id = emp.Id,
                                              FirstName = emp.FirstName,
                                              LastName = emp.LastName,
                                              DeptId = emp.DeptId,
                                              DeptName = dgroup.Name,
                                              Age = emp.Age,
                                              Email = emp.Email,
                                              Education = emp.Education,
                                              Company = emp.Company,
                                              Experience = emp.Experience,
                                              Gender = emp.Gender,
                                              Package = emp.Package,

                                          }).OrderBy(x=>x.Id).ToList();


            int totalItemCount = allData.Count();
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)data.PageSize);
            int totalentries = allData.Count();
            int currententrystart;
            int currententryend;
            if (totalentries >= data.PageSize)
            {
                 currententrystart = data.CurrentPage * data.PageSize - data.PageSize + 1;
                 currententryend = data.CurrentPage * data.PageSize;
            }
            else
            {
                 currententrystart = data.CurrentPage * data.PageSize - data.PageSize + 1;
                 currententryend = data.PageSize;
            }
            List<EmployeeData> list1 = allData.Skip((data.CurrentPage - 1) * data.PageSize).Take(data.PageSize).ToList();
            PaginatedViewModel paginatedViewModel = new PaginatedViewModel
            {
                Employees = list1,
                CurrentPage = data.CurrentPage,
                TotalPages = totalPages,
                PageSize = data.PageSize,
                SearchInput = data.SearchInput,
                totalentries = totalentries,
                curententries = currententrystart.ToString()+" of "+currententryend.ToString(),
            };
            return paginatedViewModel;
        }
        #endregion

        #region
        public PaginatedViewModel GetDatabyid(int Id)
        {
            PaginatedViewModel allData = ((PaginatedViewModel)(from emp in _context.Employees
                                          join dept in _context.Departments
                                          on emp.DeptId equals dept.Id into datagroup
                                          from dgroup in datagroup.DefaultIfEmpty()
                                          where emp.IsDelete == new BitArray(1) && emp.Id == Id
                                          select new PaginatedViewModel
                                          {
                                              Id = emp.Id,
                                              FirstName = emp.FirstName,
                                              LastName = emp.LastName,
                                              DeptId = emp.DeptId,
                                              DeptName = dgroup.Name,
                                              Age = emp.Age,
                                              Email = emp.Email,
                                              Education = emp.Education,
                                              Company = emp.Company,
                                              Experience = emp.Experience,
                                              Gender = emp.Gender,
                                              Package = emp.Package,
                                              viewname = "Edit"
                                          }).FirstOrDefault());

            return allData;
        }
        #endregion
        #region AddEmploye
        public async Task<bool> AddEmployee(PaginatedViewModel data)
        {
            try
            {
                var iscurrentrole = _context.Departments.FirstOrDefault(x => x.Name == data.DeptName);
                if(iscurrentrole == null)
                {
                    Department department = new()
                    {
                        Name = data.DeptName
                    };
                    _context.Departments.Add(department);
                    _context.SaveChanges();

                    Employee Dataforemployee = new()
                    {
                        IsDelete = new BitArray(1),
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        DeptId = department.Id,
                        Age = data.Age,
                        Email = data.Email,
                        Education = data.Education,
                        Company = data.Company,
                        Experience = data.Experience,
                        Gender = data.Gender,
                        Package = data.Package,
                        CreatedDate = DateTime.Now
                    };
                    _context.Employees.Add(Dataforemployee);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    Employee Dataforemployee = new()
                    {
                        IsDelete = new BitArray(1),
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        DeptId = iscurrentrole.Id,
                        Age = data.Age,
                        Email = data.Email,
                        Education = data.Education,
                        Company = data.Company,
                        Experience = data.Experience,
                        Gender = data.Gender,
                        Package = data.Package,
                        CreatedDate = DateTime.Now
                    };
                    _context.Employees.Add(Dataforemployee);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region DeleteEmployee
        public async Task<bool> DeleteEmployee(int employeeid)
        {
            try
            {
                var data = _context.Employees.FirstOrDefault(v => v.Id == employeeid);
                data.IsDelete = new BitArray(new[] { true });
                data.ModifiedDate = DateTime.Now;
                _context.Employees.Update(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region EditEmployee
        public async Task<bool> EditEmployee(PaginatedViewModel data)
        {
            try
            {
                var iscurrentrole = _context.Departments.FirstOrDefault(x => x.Name == data.DeptName);
                var employee = _context.Employees.Where(emp => emp.Id == data.Id).FirstOrDefault();
                if (iscurrentrole == null)
                {
                    Department department = new()
                    {
                        Name = data.DeptName
                    };
                    _context.Departments.Add(department);
                    _context.SaveChanges();

                    employee.IsDelete = new BitArray(1);
                    employee.FirstName = data.FirstName;
                    employee.LastName = data.LastName;
                    employee.DeptId = department.Id;
                    employee.Age = data.Age;
                    employee.Email = data.Email;
                    employee.Education = data.Education;
                    employee.Company = data.Company;
                    employee.Experience = data.Experience;
                    employee.Gender = data.Gender;
                    employee.Package = data.Package;
                    employee.ModifiedDate = DateTime.Now;
                   
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    employee.IsDelete = new BitArray(1);
                    employee.FirstName = data.FirstName;
                    employee.LastName = data.LastName;
                    employee.DeptId = iscurrentrole.Id;
                    employee.Age = data.Age;
                    employee.Email = data.Email;
                    employee.Education = data.Education;
                    employee.Company = data.Company;
                    employee.Experience = data.Experience;
                    employee.Gender = data.Gender;
                    employee.Package = data.Package;
                    employee.ModifiedDate = DateTime.Now;
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
