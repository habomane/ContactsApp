using System;

namespace ConsolePhoneBook;

class Program
{
    static void Main(string[] args)
    {
        PhoneBook phoneBook = new PhoneBook();
        Console.WriteLine("Welcome to your phone book!");
        Uri api = new Uri("https://localhost:7080/api/Contact");
        phoneBook.SetClientUri(api).GetAwaiter().GetResult();

        foreach(var item in phoneBook.Directory)
        {
            Console.WriteLine("Id: " + item.Id);
            Console.WriteLine("First Name: " + item.FirstName);
            Console.WriteLine("Last Name: " + item.LastName);
            Console.WriteLine("Mobile Number: " + item.MobilePhoneNumber);
            Console.WriteLine("Home Phone Number: " + item.HomePhoneNumber);
            Console.WriteLine("Email: " + item.Email);

        }

        Console.ReadKey();
    }
}

