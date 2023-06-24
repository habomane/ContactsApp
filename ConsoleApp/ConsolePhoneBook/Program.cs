using System;
using ConsolePhoneBook.Dto;
using Spectre.Console;

namespace ConsolePhoneBook;

class Program
{
    static void Main(string[] args)
    {
        PhoneBook phoneBook = new PhoneBook();
        Console.WriteLine("Welcome to your phone book!");
        Uri api = new Uri("https://localhost:7080/api/Contact");
        phoneBook.SetClientUri(api).GetAwaiter().GetResult();

            while (true)
            {
            Console.Clear();
             var options = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
            .Title("Phone Book")
            .PageSize(10)
            .MoreChoicesText("[blue](Move up and down to reveal more fruits)[/]")
            .AddChoices(new[] {
                        "Search phonebook", "List all contacts", "Edit phonebook",
                        "Exit"
            }));
            if(options == "Edit phonebook")
            {
                var actionItem = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                .Title("How would you like to edit the phone book?")
                .PageSize(10)
                .MoreChoicesText("[green](Move up and down to reveal more fruits)[/]")
                .AddChoices(new[] {
            "Add new contact", "Edit individual contact",
            "Exit" }));

                if(actionItem == "Exit") { break; }
                if (actionItem == "Add new contact")
                {
                    var firstName = AnsiConsole.Ask<string>("What is the first name? ");
                    var lastName = AnsiConsole.Ask<string>("What is the last name? ");
                    var mobilPhoneNumber = AnsiConsole.Ask<long>("What is the mobile number? (Please include areacode) ");
                    var homePhoneNumber = AnsiConsole.Ask<long>("What is the phone number? (Please include areacode) ");
                    var email = AnsiConsole.Ask<string>("What is the email? ");

                    var dtoRequest = new ContactRequestDto()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        HomePhoneNumber = homePhoneNumber,
                        MobilePhoneNumber = mobilPhoneNumber,
                        Email = email

                    };

                    phoneBook.AddContact(dtoRequest).GetAwaiter().GetResult();
                    Console.WriteLine("Please refresh the console app to see the updated changes");

                }
                else
                {
                    Console.WriteLine("Current phone book: ");
                    Table contactTable = new Table();
                    contactTable.AddColumn("Id");
                    contactTable.AddColumn("Name");
                    contactTable.AddColumn("Mobile Number");
                    contactTable.AddColumn("Phone Number");
                    contactTable.AddColumn("Email");

                    foreach (var item in phoneBook.Directory)
                    {
                        contactTable.AddRow($"{item.Id}", $"{item.FirstName} {item.LastName}", $"{item.MobilePhoneNumber}", $"{item.HomePhoneNumber}", $"{item.Email}");
                    }

                    var idItem = AnsiConsole.Ask<int>($"Enter the id of the contact you would like to edit");


                    var idItemAction = AnsiConsole.Prompt(
                     new SelectionPrompt<string>()
                    .Title("How would you like to edit the phone book?")
                    .PageSize(10)
                    .MoreChoicesText("[red](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] {
                    "Delete contact", "Update contact",
                    "Exit"
                    }));

                    if (idItemAction == "Delete contact")
                    {
                        phoneBook.DeleteContact(idItem).GetAwaiter().GetResult();
                        Console.WriteLine("Item deleted");
                        if (!AnsiConsole.Confirm("Go back?"))
                        {
                            AnsiConsole.MarkupLine("Exiting now...");
                        }
                    }
                    if (idItemAction == "Update contact")
                    {
                        var editAction = AnsiConsole.Prompt(
                     new SelectionPrompt<string>()
                    .Title("How would you like to update the phone book?")
                    .PageSize(10)
                    .MoreChoicesText("[green](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] {
                    "First name", "Last name", "Email", "Mobile phone number", "Home phone number",
                    "Exit"
                    }));
                        var changingObject = AnsiConsole.Ask<string>($"What would you like to change the {editAction.ToLower()} to?");
                        phoneBook.UpdateContact(idItem, editAction, changingObject).GetAwaiter().GetResult();
                        Console.WriteLine("Successfully updated action.");
                        if (!AnsiConsole.Confirm("Go back?"))
                        {
                            AnsiConsole.MarkupLine("Exiting now...");
                            break;
                        }
                    }
                    else { break; }
                }
            }

            if (options == "Search phonebook")
            {
                var searchCategory = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                .Title("What do you want to search?")
                .PageSize(10)
                .MoreChoicesText("[blue](Move up and down to reveal more fruits)[/]")
                .AddChoices(new[] {
            "First name", "Last name", "Mobile phone number",
            "Home phone number", "email"
                }));

                var searchItem = AnsiConsole.Ask<string>($"Enter the new {searchCategory.ToLower()}: ");

                var results = phoneBook.SearchDirectory(searchItem, searchCategory);

                foreach (var item in results)
                {
                    Console.WriteLine($"Name: {item.FirstName} {item.LastName} \nMobile phone number: {item.MobilePhoneNumber} \nHome phone number: {item.HomePhoneNumber} \nEmail: {item.Email} ");
                }
                if (!AnsiConsole.Confirm("Go back?"))
                {
                    AnsiConsole.MarkupLine("Exiting now...");
                }
            }
            if(options == "List all contacts")
            {
                Table contactTable = new Table();
                contactTable.AddColumn("Id");
                contactTable.AddColumn("Name");
                contactTable.AddColumn("Mobile Number");
                contactTable.AddColumn("Phone Number");
                contactTable.AddColumn("Email");

                foreach(var item in phoneBook.Directory)
                {
                    contactTable.AddRow($"{item.Id}", $"{item.FirstName} {item.LastName}", $"{item.MobilePhoneNumber}", $"{item.HomePhoneNumber}", $"{item.Email}");
                }

                AnsiConsole.Write(contactTable);
                if (!AnsiConsole.Confirm("Go back?"))
                {
                    AnsiConsole.MarkupLine("Exiting now...");
                    break;
                }

            }
            if(options == "Exit")
            {
                return;
            }
        }

        
    }
}

