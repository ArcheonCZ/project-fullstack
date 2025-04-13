using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Models
{
	public class Invoice
	{
		[Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public uint InvoiceId { get; set; }
		[Required]
		public int InvoiceNumber { get; set; }
		[Required]
		public DateTime Issued { get; set; }
		[Required]
		public DateTime DueTime { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Product { get; set; } = "";
		[Required]
		public int Vat { get; set; }
		public string Note { get; set; } = "";
		public uint? SellerId { get; set; }
		[ForeignKey(nameof(SellerId))]
		public virtual Person? Seller { get; set; }
		public uint? BuyerId { get; set; }
		[ForeignKey(nameof(BuyerId))]
		public virtual Person? Buyer { get; set; }
	}
}
