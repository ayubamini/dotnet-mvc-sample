using Microsoft.AspNetCore.Mvc;
using CustomerManagementSystem.DATA.Entities;
using CustomerManagementSystem.Repositories.Customers;
using AutoMapper;
using CustomerManagementSystem.Models.CustomerViewModel;

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
        public async Task<IActionResult> LoadPartialList()
        {
            return PartialView("_List", _mapper.Map<List<CustomerVM>>(await _customerRepository.GetAllAsync()));
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
