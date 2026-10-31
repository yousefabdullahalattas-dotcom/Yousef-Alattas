namespace BootCamp_MVC_EF.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int PowerWatt { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; } = string.Empty;
    }


    public class ProductCreateDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int PowerWatt { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int SupplierId { get; set; }
    }


    public class ProductUpdateDto : ProductCreateDto
    {
        public int Id { get; set; }
    }
}