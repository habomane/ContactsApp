﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
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

    public async Task UpdateContact(int id, string objectChanging, string newResult)
    {
        try
        {
            var result = Directory.FirstOrDefault(p => p.Id == id);

           if(objectChanging.ToLower() == "mobile phone number")
            {
                long newPhoneNumber;
                if(Int64.TryParse(newResult, out newPhoneNumber))
                {
                    result.MobilePhoneNumber = newPhoneNumber;
                }
            }
            if (objectChanging.ToLower() == "home phone number")
            {
                long newPhoneNumber;
                if (Int64.TryParse(newResult, out newPhoneNumber))
                {
                    result.HomePhoneNumber = newPhoneNumber;
                }
            }
            if (objectChanging.ToLower() == "email" && IsValidEmail(objectChanging))
            {
                result.Email = newResult;
            }
            if (objectChanging.ToLower() == "first name")
            {
                result.FirstName = newResult;
            }
            if (objectChanging.ToLower() == "last name")
            {
                result.LastName = newResult;
            }

            var requestDto = new ContactRequestDto()
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                MobilePhoneNumber = result.MobilePhoneNumber,
                HomePhoneNumber = result.HomePhoneNumber,
                Email = result.Email
            };

            var response = await httpClient.PutAsJsonAsync<ContactRequestDto>(httpClient.BaseAddress + $"/{id}", requestDto);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Successfully updated field.");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }

    public IEnumerable<ContactsDto> SearchDirectory(string searchItem, string searchCategory)
    {
        try
        {
            if (searchCategory.ToLower() == "mobile phone number")
            {
                long searchNumber;
                if(Int64.TryParse(searchItem, out searchNumber))
                {
                    var result = Directory.Where(p => p.MobilePhoneNumber == searchNumber);
                    return result;
                }
            }
            if (searchCategory.ToLower() == "home phone number")
            {
                long searchNumber;
                if (Int64.TryParse(searchItem, out searchNumber))
                {
                    var result = Directory.Where(p => p.HomePhoneNumber == searchNumber);
                    return result;
                }
            }
            if (searchCategory.ToLower() == "email")
            {
                var result = Directory.Where(p => p.Email.ToLower() == searchItem.ToLower());
                return result;
            }
            if (searchCategory.ToLower() == "first name")
            {
                return Directory.Where(p => p.FirstName.ToLower() == searchItem.ToLower());
            }
            if (searchCategory.ToLower() == "last name")
            {
                return Directory.Where(p => p.LastName.ToLower() == searchItem.ToLower());
            }

            return null;
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }
}


