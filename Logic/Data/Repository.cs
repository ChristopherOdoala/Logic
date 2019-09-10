using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Model;

namespace Logic.Data
{
    public class Repository : IRepository 
    {
        private LogicDbContext _ctx;
        public Repository(LogicDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Department> GetAllDepartments()
        {
           return _ctx.departments;
        }

        public IQueryable<Employee> GetAllEmployee()
        {
            return _ctx.employees;
        }

        public IQueryable<Status> GetAllStatus()
        {
            return _ctx.status;
        }

        public bool Insert(Employee employee)
        {
            try
            {
                _ctx.employees.Add(employee);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Insert(Status status)
        {
            try
            {
                _ctx.status.Add(status);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Insert(Department department)
        {
            try
            {
                _ctx.departments.Add(department);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Status model)
        {
            try
            {
                _ctx.Update(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveAll()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Employee employee)
        {
            try
            {
                _ctx.Update(employee);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
