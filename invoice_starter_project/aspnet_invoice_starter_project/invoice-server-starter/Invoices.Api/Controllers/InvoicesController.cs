using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Api.Models;
using Invoices.API.Models;
using Invoices.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoicesController : ControllerBase
	{
		private readonly IInvoiceManager invoiceManager;
		private readonly IPersonManager personManager;
		public InvoicesController(IInvoiceManager invoiceManager, IPersonManager personManager)
		{
			this.invoiceManager = invoiceManager;
			this.personManager = personManager;
		}
		[HttpGet]
		public IEnumerable<InvoiceDto> GetInvoices()
		{
			return invoiceManager.GetAllInvoices();
		}

		[HttpPost]
		public IActionResult AddInvoice([FromBody] InvoiceDto invoiceDto)
		{
			
			if (personManager.GetPerson(invoiceDto.Buyer.PersonId) is  null || personManager.GetPerson(invoiceDto.Seller.PersonId) is  null)
				return BadRequest();
			InvoiceDto? createdInvoice = invoiceManager.AddInvoice(invoiceDto);
			return StatusCode(StatusCodes.Status201Created, createdInvoice);
		}

		[HttpGet("{invoiceId}")]
		public IActionResult GetInvoice(uint invoiceId)
		{

			InvoiceDto? invoiceDto = invoiceManager.GetInvoice(invoiceId);

			if (invoiceDto is null)
				return NotFound();

			return Ok(invoiceDto);
		}

		[HttpPut("{invoiceId}")]
		public IActionResult EditInvoice([FromBody] InvoiceDto invoiceDto, uint invoiceId)
		{
			InvoiceDto? invoiceExists = invoiceManager.GetInvoice(invoiceId);
			InvoiceDto? editedInvoice = invoiceManager.EditInvoice(invoiceDto);
				
			if (editedInvoice is null || invoiceExists is null)
				return NotFound();
			return Ok(editedInvoice);
		}


		[HttpDelete("{invoiceId}")]
		public IActionResult DeleteInvoice(uint invoiceId)
		{
			invoiceManager.DeleteInvoice(invoiceId);
			return NoContent();
		}

		[HttpGet]
		[Route("~/api/identification/{identificationNumber}/sales")]
		public IEnumerable<InvoiceDto> GetSales(string identificationNumber)
		{
			return invoiceManager.GetSalesByIdentificationNumber(identificationNumber);
			
		}

		[HttpGet]
		[Route("~/api/identification/{identificationNumber}/purchases")]
		public IEnumerable<InvoiceDto> GetPurchases(string identificationNumber)
		{
			return invoiceManager.GetPurchasesByIdentificationNumber(identificationNumber);
		}

		[HttpGet("statistics")]
		public IActionResult GetStatistics()
		{
			StatisticsDto? statistics = invoiceManager.GetStatistics();

			if (statistics is null)
				return NotFound();

			return Ok(statistics);
		}
	}
}
