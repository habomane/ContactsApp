using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Dto;

public class ContactRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required]
    public int MobilePhoneNumber { get; set; }

    public int HomePhoneNumber { get; set; }
    public string Email { get; set; }
}

