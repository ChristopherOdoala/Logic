using Logic.Model;
using System.Linq;

namespace Logic.Data
{
    public interface IRepository
    {
        bool SaveAll();
        bool Insert(Employee employee);
        bool Insert(Status status);
        bool Insert(Department department);
        bool Update(Status status);
        bool Update(Employee employee);

        IQueryable<Employee> GetAllEmployee();

        IQueryable<Department> GetAllDepartments();

        IQueryable<Status> GetAllStatus();
    }
}