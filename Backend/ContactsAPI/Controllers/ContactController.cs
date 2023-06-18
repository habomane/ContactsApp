using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactsAPI.Dto;
using ContactsAPI.Mapping;
using ContactsAPI.Models;
using ContactsAPI.Data;


namespace ContactsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : Controller
{
    private IContactRepository contactRepository;

    public ContactController(IContactRepository repos)
    {
        contactRepository = repos;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllContacts()
    {
        try
        {
            var result = await contactRepository.GetContacts();
            var dtoList = new List<ContactResponseDto>();

            foreach(var item in result)
            {
                var output = Mapper.ConvertDomainToResponse(item);
                dtoList.Add(output);
            }

            return Ok(dtoList);
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: Could not list resources. Reason: " + e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetContact(int id)
    {
        try
        {
            var result = await contactRepository.GetContact(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: Could not return resource at this time"); 
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddContact(ContactRequestDto dto)
    {
        try
        {
            if(dto == null) { return BadRequest(); }
            var requestDomain = Mapper.ConvertRequestToDomain(dto);
            var result = await contactRepository.AddContact(requestDomain);
            var output = Mapper.ConvertDomainToResponse(result);
            return CreatedAtAction(nameof(GetContact), new { id = result.Id }, output);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: Could not add resource at this time.");
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateContact(int id, ContactRequestDto dto)
    {
        try
        {
            var existingId = await contactRepository.GetContact(id);

            if(existingId == null) { return NotFound(); }
            if(dto == null) { ModelState.AddModelError("Error", "Information missing. Cannot process request."); return BadRequest(ModelState); }

            var domainRequest = Mapper.ConvertRequestToDomain(dto);
            var result = await contactRepository.UpdateContact(id, domainRequest);
            var output = Mapper.ConvertDomainToResponse(result);

            return Ok(output);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: Could not update resource at this time");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteContact(int id)
    {
        try
        {
            var existingResource = await contactRepository.GetContact(id);
            if(existingResource == null) { return NotFound(); }

            await contactRepository.DeleteContact(id);

            return Ok($"ID: {id} was successfully deleted");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: Could not delete resource at this time.");
        }
    }


}

