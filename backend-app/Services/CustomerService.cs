using MyApp.Models;
using MyApp.Utils;

namespace MyApp.Services;

public class CustomerService {
    private readonly MyDbContext _context;

    public CustomerService(MyDbContext context) {
        _context = context;
    }

    /**
    * Reaturn a list of customers
    */
    public IEnumerable<Customer> GetCustomerList(PaginationParams options) {
        return _context.Customers
            .OrderBy( c => c.Id )
            .Skip( options.Offset )
            .Take( options.Size )
            .ToList();
    }

    /**
    * Return a a paginated list of customers
    */
    public PaginatedResponse PaginatedCustomerList(PaginationParams options) {
        var count = _context.Customers.Count();
        return new PaginatedResponse {
            Data = GetCustomerList(options),
            Page = options.Page,
            Size = options.Size,
            TotalPages = (int) Math.Ceiling(count / (double) options.Size),
            Total = count
        };
    }

    /**
    * Return a customer by id
    */
    public Customer? GetCustomer(int id) {
        return _context.Customers.Find(id);
    }

    /**
    * Create a new customer and return it
    */
    public Customer AddCustomer(Customer customer) {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    /**
    * Update a customer and return it
    */
    public Customer UpdateCustomer(Customer customer) {
        _context.Customers.Update(customer);
        _context.SaveChanges();
        return customer;
    }

    /**
    * Delete a customer by id
    */
    public Boolean DeleteCustomer(int id) {
        var customer = _context.Customers.Find(id);
        if (customer != null) {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}

public class PaginationParams {
    public int Page { get; set; } = 1;
    public int Offset { get { return (Page - 1) * Size; } }
    public int Size { get; set; } = 20;
}

public class PaginatedResponse {
    public int Page { get; set; }
    public int Size { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<Customer> Data { get; set; }
}