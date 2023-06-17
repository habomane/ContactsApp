using System;
using ContactsAPI.Dto;
using ContactsAPI.Models;
namespace ContactsAPI.Mapping;

public class Mapper
{

    public Contacts ConvertRequestToDomain(ContactRequestDto request)
    {
        var response = new Contacts()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobilePhoneNumber = request.MobilePhoneNumber,
            HomePhoneNumber = request.HomePhoneNumber,
            Email = request.Email
        };

        return response;
    }

    public ContactResponseDto ConvertDomainToResponse(Contacts request)
    {
        var response = new ContactResponseDto()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobilePhoneNumber = request.MobilePhoneNumber,
            HomePhoneNumber = request.HomePhoneNumber,
            Email = request.Email
        };

        return response;
    }
}

