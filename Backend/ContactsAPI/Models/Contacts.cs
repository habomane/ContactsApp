using System;
using System.ComponentModel.DataAnnotations;
namespace ContactsAPI.Models;

public class Contacts
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }

	[Required]
	public double MobilePhoneNumber { get; set; }


    public double HomePhoneNumber { get; set; }
    public string Email { get; set; }
}


