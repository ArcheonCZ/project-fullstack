﻿using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Repositories
{
	public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
	{
		public InvoiceRepository(InvoicesDbContext invoicesDbContext) : base(invoicesDbContext)
		{

		}

		public IList<Invoice> GetAll(string? product = null,  int? minPrice = null, int? limit = null, int? sellerId=null, int? buyerId=null)
		{
			var query = dbSet.AsQueryable();

			if (product is not null)
			{
				query = query.Where(i => i.Product == product);
			}

			if (minPrice is not null)
			{
				Console.WriteLine("minPrice: " + minPrice);
				query = query.Where(i => i.Price >= minPrice);
			}

			if (limit is not null)
			{
				Console.WriteLine("limit: " + limit);
				Console.WriteLine("limit.Value: " + limit.Value);
				query = query.Take(limit.Value);
			}

			if (sellerId is not null)
			{
				query = query.Where(i => i.SellerId == sellerId);
			}

			if (buyerId is not null)
			{
				query = query.Where(i => i.BuyerId == buyerId);
			}
			return query.ToList();
		}

	}
}
