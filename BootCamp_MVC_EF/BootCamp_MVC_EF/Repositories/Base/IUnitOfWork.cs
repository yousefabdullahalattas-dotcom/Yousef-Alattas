namespace BootCamp_MVC_EF.Repositories.Base
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        ISupplierRepository SupplierRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        IUserRepository UserRepository { get; }

        IOrderRepository OrderRepository { get; }
    }
}
