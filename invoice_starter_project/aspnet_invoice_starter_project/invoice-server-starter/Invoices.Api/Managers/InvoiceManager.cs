using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.API.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Api.Managers
{
	public class InvoiceManager : IInvoiceManager
	{
		private readonly IInvoiceRepository invoiceRepository;
		private readonly IPersonRepository personRepository;
		private readonly IPersonManager personManager;
		private readonly IMapper mapper;

		public InvoiceManager(IInvoiceRepository invoiceRepository, IPersonRepository personRepository, IPersonManager personManager, IMapper mapper)
		{
			this.invoiceRepository = invoiceRepository;
			this.personRepository = personRepository;
			this.personManager = personManager;
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
			return mapper.Map<InvoiceDto?>(invoice);
		}

		public List<InvoiceDto> GetPurchasesByIdentificationNumber(string identificationNumber)
		{
			IList<Person> peopleFound = personManager.GetByIdentificationNumber(identificationNumber);
			List<InvoiceDto> purchasesList = new List<InvoiceDto>();
			foreach (Person person in peopleFound)
			{
				purchasesList.AddRange(mapper.Map<List<InvoiceDto>>(person.Purchases));
			}
			return purchasesList;
		}	
		public List<InvoiceDto> GetSalesByIdentificationNumber(string identificationNumber)
		{
			IList<Person> peopleFound = personManager.GetByIdentificationNumber(identificationNumber);
			List<InvoiceDto> salesList = new List<InvoiceDto>();
			foreach (Person person in peopleFound)
			{
				salesList.AddRange(mapper.Map<List<InvoiceDto>>(person.Sales));
			}
			return salesList;
		}
		
		public StatisticsDto GetStatistics()
		{
			IEnumerable<InvoiceDto> invoices = GetAllInvoices();
			StatisticsDto statistics = new StatisticsDto();

			///dodělat naplnění statDto...
		}
	}
}
