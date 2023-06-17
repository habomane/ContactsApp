using Microsoft.EntityFrameworkCore;

using ContactsAPI.Models;

namespace ContactsAPI.Data;

public class ContactRepository : IContactRepository
{
    private ContactDbContext contactDbContext;

    public async Task<IEnumerable<Contacts>> GetContacts()
    {
        return await contactDbContext.Contacts.ToListAsync();
    }

    public async Task<Contacts> GetContact(int id)
    {
        return await contactDbContext.Contacts.FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<Contacts> AddContact(Contacts contact)
    {
        var result = await contactDbContext.Contacts.AddAsync(contact);
        await contactDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Contacts> UpdateContact(int id, Contacts contact)
    {
        var currentContact = await contactDbContext.Contacts.FirstOrDefaultAsync(p => p.Id == id);

        if (currentContact != null)
        {
            currentContact.FirstName = contact.FirstName;
            currentContact.LastName = contact.LastName;
            currentContact.MobilePhoneNumber = contact.MobilePhoneNumber;
            currentContact.HomePhoneNumber = contact.HomePhoneNumber;
            currentContact.Email = contact.Email;
            await contactDbContext.SaveChangesAsync();
            return currentContact;

        }

        return null;
    }

    public async Task DeleteContact(int id)
    {
        var result = await contactDbContext.Contacts.FirstOrDefaultAsync(p => p.Id == id);
        if(result != null)
        {
            contactDbContext.Remove(result);
            await contactDbContext.SaveChangesAsync();
            int pos = 1;
            foreach(var item in contactDbContext.Contacts)
            {
                item.Id = pos;
                pos++;
            }
        }

    }

}

