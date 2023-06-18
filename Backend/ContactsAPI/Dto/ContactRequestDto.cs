using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Dto;

public class ContactRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required]
    public double MobilePhoneNumber { get; set; }

    public double HomePhoneNumber { get; set; }
    public string Email { get; set; }
}

