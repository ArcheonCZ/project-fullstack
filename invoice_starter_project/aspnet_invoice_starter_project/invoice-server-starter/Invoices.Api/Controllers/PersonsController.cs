

using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IPersonManager personManager;


	public PersonsController(IPersonManager personManager)
	{
		this.personManager = personManager;
	}


	[HttpGet]
	public IEnumerable<PersonDto> GetPersons()
	{
		return personManager.GetAllPersons();
	}

	[HttpPost]
	public IActionResult AddPerson([FromBody] PersonDto person)
	{
		PersonDto? createdPerson = personManager.AddPerson(person);
		return StatusCode(StatusCodes.Status201Created, createdPerson);
	}

	[HttpGet("{personId}")]
	public IActionResult GetPerson(uint personId)
	{
		PersonDto? personDto = personManager.GetPerson(personId);
		//Console.WriteLine(personManager.IsHidden(personId));
		if (personDto is null || personManager.IsHidden(personId).GetValueOrDefault(true))
			return NotFound();

		return Ok(personDto);
	}
	[HttpPut("{personId}")]
	public IActionResult EditPerson(uint personId, [FromBody] PersonDto person)
	{
		PersonDto? editedPerson = personManager.EditPerson(personId, person);
		if (person is null)
			return NotFound();
		return Ok(editedPerson);
	}

	[HttpDelete("{personId}")]
	public IActionResult DeletePerson(uint personId)
	{
		personManager.DeletePerson(personId);
		return NoContent();
	}
}