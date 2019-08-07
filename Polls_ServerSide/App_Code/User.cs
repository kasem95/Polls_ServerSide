using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private string email, password;
    public int ID { get; set; }
    public string UserName { get; set; }
    public string Email
    {
        get { return email; }
        set
        {
            if (IsValid(value))
            {
                email = value;
            }
            else
            {
                email = null;
            }
        }
    }
    public string Password
    {
        get { return password; }
        set
        {
            if (IsValidPassword(value))
                password = value;
            else
                password = null;
        }
    }

    public User()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private bool IsValidPassword(string input)
    {
        Match match = Regex.Match(input, @"(?=^.{8,12}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*");

        if (match.Success && match.Index == 0 && match.Length == input.Length)
            return true;
        else
            return false;

    }
}