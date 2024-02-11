using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCrud.Data;
using MVCCrud.Models;
using MVCCrud.Models.Domain;

namespace MVCCrud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCCrudDBContext _mvcCrudContext;

        public EmployeesController(MVCCrudDBContext mvcCrudContext)
        {
            _mvcCrudContext = mvcCrudContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _mvcCrudContext.Employees.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            Employee newEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
            };

            await _mvcCrudContext.AddAsync(newEmployee);
            await _mvcCrudContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            Employee? employee = await _mvcCrudContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }

            EditEmployeeViewModel employeeViewModel = new()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                DateOfBirth= employee.DateOfBirth,
                Department = employee.Department,
                Salary = employee.Salary
            };

            return await Task.Run(() => View("View", employeeViewModel));
        }

        [HttpPost]
        public async Task<IActionResult> View(EditEmployeeViewModel model)
        {
            Employee? employee = await _mvcCrudContext.Employees.FindAsync(model.Id);

            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Salary = model.Salary;
            employee.DateOfBirth = model.DateOfBirth;
            employee.Department = model.Department;

            await _mvcCrudContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditEmployeeViewModel model)
        {
            Employee? employee = await _mvcCrudContext.Employees.FindAsync(model.Id);

            if (employee == null)
            {
                return NotFound();
            }

            _mvcCrudContext.Remove(employee);
            await _mvcCrudContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
