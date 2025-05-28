using Invoices.Data.Models;

namespace Invoices.Data.Interfaces
{
	public interface IInvoiceRepository : IBaseRepository<Invoice>
	{
		IList<Invoice> GetAll(string? product = null, int? limit = null, int? minPrice = null, int? sellerId = null, int? buyerId=null);
	}
}
