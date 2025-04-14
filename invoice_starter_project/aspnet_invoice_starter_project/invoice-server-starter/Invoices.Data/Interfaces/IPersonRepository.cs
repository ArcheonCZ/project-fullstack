using Invoices.Data.Models;

namespace Invoices.Data.Interfaces;

public interface IPersonRepository : IBaseRepository<Person>
{
    IList<Person> GetAllByHidden(bool hidden);
	public IList<Person> GetAllByIdentificationNumber(string identificationNumber);
}