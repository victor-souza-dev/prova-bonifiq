using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models.DTOs;
using ProvaPub.Models.Entities;
using ProvaPub.Services;

namespace ProvaPub.Controllers;

[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		private readonly ProductService _productService;
    private readonly CustomerService _customerService;

		public Parte2Controller(ProductService productService, CustomerService customerService)
		{
			_productService = productService;
			_customerService = customerService;
		}
	
		[HttpGet("products")]
		public ProductList ListProducts(int page, int pageSize, string? query)
		{
			return _productService.GetPaginationList(page, pageSize, query);
		}

		[HttpGet("customers")]
		public CustomerList ListCustomers(int page, int pageSize, string? query)
		{
			return _customerService.GetPaginationList(page, pageSize, query);
		}
	}
