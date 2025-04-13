using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Api.Models;
using Invoices.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoicesController : ControllerBase
	{
		private readonly IInvoiceManager invoiceManager;
		public InvoicesController(IInvoiceManager invoiceManager)
		{
			this.invoiceManager = invoiceManager;
		}
		[HttpGet]
		public IEnumerable<InvoiceDto> GetInvoices()
		{
			return invoiceManager.GetAllInvoices();
		}

		[HttpPost]
		public IActionResult AddInvoice([FromBody] InvoiceDto invoiceDto)
		{
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
		public IActionResult EditInvoice([FromBody] InvoiceDto invoiceDto)
		{
			InvoiceDto? editedInvoice = invoiceManager.EditInvoice(invoiceDto);
			if (editedInvoice is null)
				return NotFound();
			return Ok(editedInvoice);
		}


		[HttpDelete("{invoiceId}")]
		public IActionResult DeleteInvoice(uint invoiceId)
		{
			invoiceManager.DeleteInvoice(invoiceId);
			return NoContent();
		}
	}
}
