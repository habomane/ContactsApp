using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ConsolePhoneBook.Dto;

namespace ConsolePhoneBook;

public class PhoneBook
{
    private HttpClient httpClient;
    private CancellationToken token;
    public List<ContactsDto> Directory { get; set; }

    public PhoneBook() { Directory = new List<ContactsDto>(); httpClient = new HttpClient(); token = new CancellationToken(); }

    public async Task SetClientUri(Uri uri)
    {
        httpClient.BaseAddress = uri;
        try
        {
            var response = await httpClient.GetAsync(httpClient.BaseAddress);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ContactsDto>>();

                foreach(var item in result)
                {
                    Directory.Add(item);
                }
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public async Task AddContact(ContactRequestDto contacts)
    {
        try
        {
            if(contacts == null) { Console.WriteLine("Error: missing information."); }

            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, contacts, token);

            if(response.IsSuccessStatusCode)
            {
                var output = new ContactsDto()
                {
                    Id = Directory.Count,
                    FirstName = contacts.FirstName,
                    LastName = contacts.LastName,
                    MobilePhoneNumber = contacts.MobilePhoneNumber,
                    HomePhoneNumber = contacts.HomePhoneNumber,
                    Email = contacts.Email
                };

                Directory.Add(output);
                Console.WriteLine("Successfully added new contact");
            }


        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

    }

    public async Task DeleteContact(int id)
    {
        try
        {

            bool existingId = false;

            foreach (var item in Directory)
            {
                if (item.Id == id)
                {
                    existingId = true;
                    break;
                }
            }

            if (existingId)
            {
                var response = await httpClient.DeleteAsync(httpClient.BaseAddress + $"/{id}");
                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Id: {id} successfully deleted");
                }
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}

