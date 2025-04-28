using Invoices.Api.Models;
using Invoices.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Invoices.API.Models
{
	public class InvoiceDto
	{
		[JsonPropertyName("_id")]
		public uint PersonId { get; set; }
		public DateTime Issued { get; set; }
		public DateTime DueTime { get; set; }
		public decimal Price { get; set; }
		public string Product { get; set; } = "";
		public int Vat { get; set; }
		public string Note { get; set; } = "";
		public PersonDto? Seller { get; set; }
		public PersonDto? Buyer { get; set; }

	}
}
