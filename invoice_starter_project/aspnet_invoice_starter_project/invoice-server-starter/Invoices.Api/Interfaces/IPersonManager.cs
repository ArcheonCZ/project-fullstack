

using Invoices.Api.Models;

namespace Invoices.Api.Interfaces;

public interface IPersonManager
{
    IList<PersonDto> GetAllPersons();
    PersonDto AddPerson(PersonDto personDto);
    void DeletePerson(uint personId);
    PersonDto? GetPerson(uint personId);
    // void UpdatePerson(ulong personId);
    PersonDto? EditPerson(uint personId, PersonDto personDto);
    bool IsHidden (PersonDto personDto);
}