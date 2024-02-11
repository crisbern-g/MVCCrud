namespace MVCCrud.Models
{
    public class EditEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
