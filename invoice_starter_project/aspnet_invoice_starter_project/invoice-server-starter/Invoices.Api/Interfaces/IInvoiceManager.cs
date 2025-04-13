using Invoices.API.Models;

namespace Invoices.Api.Interfaces
{
	public interface IInvoiceManager
	{
		IList<InvoiceDto> GetAllInvoices();
		InvoiceDto AddInvoice (InvoiceDto invoice);
		void DeleteInvoice (uint invoiceId);
		InvoiceDto? GetInvoice (uint id);
		InvoiceDto? EditInvoice (InvoiceDto invoiceDto);
	}
}
