namespace BootCamp_MVC_EF.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
    }




    public class CustomerCreateDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
    }


    public class CustomerUpdateDto : CustomerCreateDto
    {
        public int Id { get; set; }

    }

}