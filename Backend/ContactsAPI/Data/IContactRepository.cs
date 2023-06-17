using System;
using ContactsAPI.Models;
namespace ContactsAPI.Data;

public interface IContactRepository
{
    public Task<IEnumerable<Contacts>> GetContacts();

    public Task<Contacts> GetContact(int id);

    public Task<Contacts> AddContact(Contacts contact);

    public Task<Contacts> UpdateContact(int id, Contacts contact);

    public Task DeleteContact(int id);
}

