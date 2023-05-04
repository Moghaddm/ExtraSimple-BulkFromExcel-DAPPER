using Extensions;
using Application.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Application.Domain;
using System.Diagnostics;

namespace Application.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerRepository _customers;

    public CustomerController(ILogger<CustomerController> logger, CustomerRepository customers) =>
        (_logger, _customers) = (logger, customers);

    public async Task<IActionResult> AddCustomers(string path = "../Customers.xlsx")
    {
        var dataTable = ExcelHandler.Read(path);
        _logger.LogInformation("Excel Reader Ok!");
        var customers = DataTableToListConvertor<Customer>.Convert(dataTable: dataTable);
        await _customers.BulkInsert(customers: customers);
        return Ok("Customers are Added!");
    }
}
