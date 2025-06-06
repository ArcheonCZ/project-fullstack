﻿
using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Api.Managers;

public class PersonManager : IPersonManager
{
	private readonly IPersonRepository personRepository;
	private readonly IMapper mapper;


	public PersonManager(IPersonRepository personRepository, IMapper mapper)
	{
		this.personRepository = personRepository;
		this.mapper = mapper;
	}


	public IList<PersonDto> GetAllPersons()
	{
		IList<Person> persons = personRepository.GetAllByHidden(false);
		return mapper.Map<IList<PersonDto>>(persons);
	}

	public PersonDto GetPerson(uint personId)
	{
		Person? person = personRepository.FindById(personId);
		if (person is null)
			return null!;
		return mapper.Map<PersonDto>(person);
	}
	public PersonDto AddPerson(PersonDto personDto)
	{
		Person person = mapper.Map<Person>(personDto);
		person.PersonId = default;
		Person addedPerson = personRepository.Insert(person);

		return mapper.Map<PersonDto>(addedPerson);
	}

	public PersonDto? EditPerson(uint personId, PersonDto personDto)
	{
		HidePerson(personId);
		return AddPerson(personDto);
	}

	public void DeletePerson(uint personId)
	{
		HidePerson(personId);
	}


	private Person? HidePerson(uint personId)
	{
		Person? person = personRepository.FindById(personId);

		if (person is null)
			return null;

		person.Hidden = true;
		return personRepository.Update(person);
	}
	public bool? IsHidden(uint personId)
	{
		//  Person person=mapper.Map<Person>(GetPerson(personId));
		Person? person = personRepository.FindById(personId);
		return person?.Hidden;
	}

	public IList<Person> GetByIdentificationNumber(string identificationNumber)
	{

		IList<Person> peopleFound = personRepository.GetAllByIdentificationNumber(identificationNumber);
		if (peopleFound is null)
			return null!;
		return peopleFound;


	}


}