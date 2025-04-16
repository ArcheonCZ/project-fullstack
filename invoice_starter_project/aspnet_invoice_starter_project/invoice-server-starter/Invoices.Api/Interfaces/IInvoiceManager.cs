using Invoices.Api.Models;
using Invoices.API.Models;
using Invoices.Data.Models;

namespace Invoices.Api.Interfaces
{
	public interface IInvoiceManager
	{
		IList<InvoiceDto> GetAllInvoices();
		InvoiceDto AddInvoice (InvoiceDto invoice);
		void DeleteInvoice (uint invoiceId);
		InvoiceDto? GetInvoice (uint id);
		InvoiceDto? EditInvoice (InvoiceDto invoiceDto);
		public List<InvoiceDto> GetPurchasesByIdentificationNumber(string identificationNumber);
		public List<InvoiceDto> GetSalesByIdentificationNumber(string identificationNumber);
		public StatisticsDto GetStatistics();
	}
}
