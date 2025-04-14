

using Invoices.Api.Models;
using Invoices.Data.Models;
using Invoices.Data.Repositories;

namespace Invoices.Api.Interfaces;

public interface IPersonManager
{
    IList<PersonDto> GetAllPersons();
    PersonDto AddPerson(PersonDto personDto);
    void DeletePerson(uint personId);
    PersonDto? GetPerson(uint personId);
    // void UpdatePerson(ulong personId);
    PersonDto? EditPerson(uint personId, PersonDto personDto);
    bool? IsHidden(uint personId);

	public IList<Person> GetByIdentificationNumber(string identificationNumber);

}