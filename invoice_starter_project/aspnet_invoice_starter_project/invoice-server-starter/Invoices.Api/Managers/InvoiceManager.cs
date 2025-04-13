using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.API.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Api.Managers
{
	public class InvoiceManager : IInvoiceManager
	{
		private readonly IInvoiceRepository invoiceRepository;
		private readonly IPersonRepository personRepository;
		private readonly IMapper mapper;

		public InvoiceManager(IInvoiceRepository invoiceRepository, IPersonRepository personRepository, IMapper mapper)
		{
			this.invoiceRepository = invoiceRepository;
			this.personRepository = personRepository;
			this.mapper = mapper;
		}


		public IList<InvoiceDto> GetAllInvoices()
		{
			IList<Invoice> allInvoices = invoiceRepository.GetAll();
			return mapper.Map<IList<InvoiceDto>>(allInvoices);
		}

		public InvoiceDto GetInvoice(uint invoiceId)
		{
			Invoice? invoice = invoiceRepository.FindById(invoiceId);
			if (invoice is null)
				return null;

			return mapper.Map<InvoiceDto>(invoice);
		}

		public InvoiceDto AddInvoice(InvoiceDto invoiceDto)
		{
			Invoice invoice = mapper.Map<Invoice>(invoiceDto);
			invoice.InvoiceId = default;
			invoice.BuyerId = invoice.Buyer?.PersonId;
			invoice.SellerId = invoice.Seller?.PersonId;
			//Person? buyer = invoice.Buyer;
			//Person? seller = invoice.Seller;
			invoice.Buyer = null;
			invoice.Seller = null;
			Invoice addedInvoice = invoiceRepository.Insert(invoice);
			//addedInvoice.Buyer = buyer;
			//addedInvoice.Seller = seller;
			addedInvoice.Buyer = personRepository.FindById(invoice.BuyerId);
			addedInvoice.Seller = personRepository.FindById(invoice.SellerId);
			return mapper.Map<InvoiceDto>(addedInvoice);
		}

		public void DeleteInvoice(uint invoiceId)
		{
			invoiceRepository.Delete(invoiceId);
		}

		public InvoiceDto? EditInvoice(InvoiceDto invoiceDto)
		{
			Invoice invoice = mapper.Map<Invoice>(invoiceDto);
			invoice = invoiceRepository.Update(invoice);
			//nebo
			//Invoice invoice = invoiceRepository.Update(mapper.Map<Invoice>(invoiceDto));
			return mapper.Map<InvoiceDto?>(invoice);
		}


	}
}
