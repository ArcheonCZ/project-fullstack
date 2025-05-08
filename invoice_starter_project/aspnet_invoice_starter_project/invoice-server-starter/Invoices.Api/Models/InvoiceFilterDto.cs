namespace Invoices.Api.Models
{
	public class InvoiceFilterDto
	{
		public string? Product { get; set; }
		public int? Limit { get; set; }
		public int? MinPrice { get; set; }
	}
}
