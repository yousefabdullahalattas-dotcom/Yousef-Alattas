using BootCamp_MVC_EF.Data;

namespace BootCamp_MVC_EF.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork

    {

        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            ProductRepository = new ProductRepository(_db);
            SupplierRepository = new SupplierRepository(_db);
            CustomerRepository = new CustomerRepository(_db);
            EmployeeRepository = new EmployeeRepository(_db);
            UserRepository = new UserRepository(_db);
            OrderRepository = new OrderRepository(_db);
        }





        public IProductRepository ProductRepository { get; }

        public ISupplierRepository SupplierRepository { get; }

        public ICustomerRepository CustomerRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public IUserRepository UserRepository { get; }

        public IOrderRepository OrderRepository { get; }
    }
}
