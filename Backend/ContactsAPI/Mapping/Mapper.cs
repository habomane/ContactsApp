using System;
using ContactsAPI.Dto;
using ContactsAPI.Models;
namespace ContactsAPI.Mapping;

public static class Mapper
{

    public static Contacts ConvertRequestToDomain(ContactRequestDto request)
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

    public static ContactResponseDto ConvertDomainToResponse(Contacts request)
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

