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
		public IList<InvoiceDto> GetAllInvoices(InvoiceFilterDto filterDto)
		{
			IList<Invoice> invoices = invoiceRepository.GetAll(filterDto.Product, filterDto.MinPrice, filterDto.Limit, filterDto.SellerId, filterDto.BuyerId);
			return mapper.Map<IList<InvoiceDto>>(invoices);
		}

		public InvoiceDto? GetInvoice(uint invoiceId)
		{
			Invoice? invoice = invoiceRepository.FindById(invoiceId);
			if (invoice is null)
				return null;

			return mapper.Map<InvoiceDto>(invoice);
		}

		public InvoiceDto AddInvoice(InvoiceDto invoiceDto)
		{
			//Console.WriteLine("manager zacatek - buyer.id: " + invoiceDto.Buyer.PersonId + ", seller id: " + invoiceDto.Seller.PersonId);
			Invoice invoice = mapper.Map<Invoice>(invoiceDto);
			invoice.InvoiceId = default;
			invoice.BuyerId = invoiceDto.Buyer?.PersonId;
			invoice.SellerId = invoiceDto.Seller?.PersonId;

			//Console.WriteLine("seller name pred zapisem do repo: " + invoice.Seller?.Name);
			invoice.Buyer = null;
			invoice.Seller = null;
			Invoice addedInvoice = invoiceRepository.Insert(invoice);
			addedInvoice.Buyer = personRepository.FindById(invoice.BuyerId);
			addedInvoice.Seller = personRepository.FindById(invoice.SellerId);
			//Console.WriteLine(addedInvoice.InvoiceNumber);
			//Console.WriteLine("seller name po zapisu do repo: "+addedInvoice.Seller?.Name);
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
			StatisticsDto statisticsDto = new StatisticsDto();

			foreach (InvoiceDto invoice in invoices)
			{
				if (invoice.Issued.Year == DateTime.Now.Year)
					statisticsDto.CurrentYearSum = invoice.Price;
				statisticsDto.AllTimeSum += invoice.Price;
				statisticsDto.InvoicesCount++;
			}
			return statisticsDto;
		}
	
	}
}
