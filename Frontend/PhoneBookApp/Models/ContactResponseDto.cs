using System;
using System.ComponentModel;

namespace PhoneBookApp.Models;

public class ContactResponseDto: INotifyPropertyChanged
{
    // implementing interface
    public event PropertyChangedEventHandler PropertyChanged;

    //internal variables
    int _id;
    string first_name;
    string last_name;
    double mobile_phone_number;
    double home_phone_number;
    string email;

    //properties
    public int Id
    {
        get => _id;
        set
        {
            if(_id == value)
            {
                return;
            }
            _id = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
        }
    }

    public string FirstName
    {
        get => first_name;
        set
        {
            if(first_name == value)
            {
                return;
            }
            first_name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
        }
    }

    public string LastName
    {
        get => last_name;
        set
        {
            if (last_name == value)
            {
                return;
            }
            last_name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
        }
    }

    public double MobilePhoneNumber
    {
        get => mobile_phone_number;
        set
        {
            if (mobile_phone_number == value)
            {
                return;
            }
            mobile_phone_number = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MobilePhoneNumber)));
        }
    }

    public double HomePhoneNumber
    {
        get => home_phone_number;
        set
        {
            if (home_phone_number == value)
            {
                return;
            }
            home_phone_number = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HomePhoneNumber)));
        }
    }

    public string Email
    {
        get => email;
        set
        {
            if (email == value)
            {
                return;
            }
            email = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
        }
    }
}

