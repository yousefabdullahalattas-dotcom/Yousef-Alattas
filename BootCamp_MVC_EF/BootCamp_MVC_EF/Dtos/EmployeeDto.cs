namespace BootCamp_MVC_EF.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }




    public class EmployeeCreateDto
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }


    public class EmployeeUpdateDto : EmployeeCreateDto
    {
        public int Id { get; set; }

    }

}