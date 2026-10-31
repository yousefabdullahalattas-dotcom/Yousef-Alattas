namespace BootCamp_MVC_EF.Dtos
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string MaterialType { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }




    public class SupplierCreateDto
    {
        public string SupplierName { get; set; } = string.Empty;
        public string MaterialType { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }


    public class SupplierUpdateDto : SupplierCreateDto
    {
        public int Id { get; set; }

    }

}