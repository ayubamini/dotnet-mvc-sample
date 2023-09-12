using Microsoft.AspNetCore.Mvc;
using CustomerManagementSystem.DATA.Entities;
using CustomerManagementSystem.Repositories.Customers;
using CustomerManagementSystem.Models.CustomerViewModel;
using CustomerManagementSystem.Models;
using AutoMapper;
using CustomerManagementSystem.Extensions;
using System.Linq.Dynamic.Core;

namespace CustomerManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadPartialList(DataTablesRequest request)
        {
            if (request == null)
            {
                return BadRequest("error");
            }

            int pageNumber = request.Start / request.Length + 1;
            int pageSize = request.Length;

            var query = _customerRepository.GetAll(pageSize, pageNumber).Result.AsQueryable();

            // Apply sorting
            if (request.Order != null)
            {
                foreach (var order in request.Order)
                {
                    var column = request.Columns[order.Column];
                    var columnName = column.Data;

                    if (order.Dir == "asc")
                        query = query.OrderByDynamic(columnName);
                    else
                        query = query.OrderByDynamicDescending(columnName);
                }
            }

            // Apply filtering
            if (request.Columns != null)
            {
                foreach (var column in request.Columns)
                {
                    var searchValue = column.Search.Value;
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        query = query.Where(c =>
                            c.FirstName!.Contains(searchValue) ||
                            c.LastName!.Contains(searchValue) ||
                            c.Email!.Contains(searchValue));
                    }
                }
            }            

            var totalRecords = await _customerRepository.GetNumberOfCustomers();

            //query = query.Skip(request.Start).Take(request.Length);
            query = query.Take(request.Length);

            var customers = query.ToList();

            var response = new
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = customers
            };

            return Json(response);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _mapper.Map<CustomerVM>(await _customerRepository.GetByIdAsync(id));

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVM customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddAsync(_mapper.Map<Customer>(customer));

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _mapper.Map<CustomerVM>(await _customerRepository.GetByIdAsync(id));

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerVM customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerRepository.UpdateAsync(_mapper.Map<Customer>(customer));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string selectedId)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                return BadRequest("Entity set 'Customers'  is null.");
            }

            var customer = await _customerRepository.GetByIdAsync(int.Parse(selectedId));

            if (customer != null)
            {
                await _customerRepository.RemoveAsync(customer);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CustomerExists(int id)
        {
            if (await _customerRepository.ExistsAsync(id))
            {
                return true;
            }

            return false;
        }
    }
}
