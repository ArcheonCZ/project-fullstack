

using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Data.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
	public PersonRepository(InvoicesDbContext invoicesDbContext) : base(invoicesDbContext)
	{
	}


	public IList<Person> GetAllByHidden(bool hidden)
	{
		return dbSet
			.Where(p => p.Hidden == hidden)
			.ToList();
	}
	public IList<Person> GetAllByIdentificationNumber(string identificationNumber)
	{
		return dbSet
			.Where(p => p.IdentificationNumber == identificationNumber)
			.ToList();
	}
}