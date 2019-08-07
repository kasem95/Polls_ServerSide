using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    string StrCon = ConfigurationManager.ConnectionStrings["LIVEDNS"].ConnectionString;
    SqlConnection con = null;
    DataTable dt = null;
    SqlDataAdapter adapter = null;
    SqlCommand com = null;
    SqlDataReader reader = null;
    public DAL()
    {
        //
        // TODO: Add constructor logic here
        //
        con = new SqlConnection(StrCon);
        com = new SqlCommand();
        com.Connection = con;

    }

    public User checkUser(string email, string password)
    {
        User user = null;
        try
        {
            con.Open();
            com.Connection = con;
            com.CommandText = $"SELECT User_ID,User_Name FROM [site09].[Betya_UsersTB] Where User_Email = '{email}' AND User_Password = '{password}'";
            reader = com.ExecuteReader();
            if (reader.Read())
            {
                user = new User
                {
                    ID = int.Parse(reader["User_ID"].ToString()),
                    UserName = reader["User_Name"].ToString()
                };
            }
            reader.Close();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            if (reader != null && !reader.IsClosed)
                reader.Close();
        }
        return user;
    }

    public string registerUser(string email, string username, string password)
    {
        User user = null;
        try
        {
            user = new User();
            con.Open();
            com.Connection = con;
            com.CommandText = $"SELECT * FROM [site09].[Betya_UsersTB] WHERE User_Email = '{email}' OR User_Name = '{username}'";
            reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader["User_Email"].ToString() == email)
                    return "Email is already exists!";
                else if (reader["User_Name"].ToString() == username)
                    return "username is already exists!";
            }
            else
            {
                user.Email = email;
                user.UserName = username;
                user.Password = password;
                if (user.Email == null)
                    return "wrong email/empty field!";
                if (user.UserName == null)
                    return "empty field!";
                if (user.Password == null)
                    return "wrong password/empty field!";
            }
            reader.Close();

            com.CommandText = $"INSERT INTO [site09].[Betya_UsersTB](User_Name, User_Email, User_Password) VALUES (@param1, @param2, @param3)";
            com.Parameters.AddWithValue("@param1", username);
            com.Parameters.AddWithValue("@param2", email);
            com.Parameters.AddWithValue("@param3", password);
            int rowsAffected = com.ExecuteNonQuery();
            if (rowsAffected < 1)
                return "Something wrong happened";
            return "All is okay!";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "Something wrong happened";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            if (reader != null && !reader.IsClosed)
                reader.Close();
        }
    }

    public string addAPoll(string[] choices, string pollName, int userID,string pollDate)
    {
        Poll poll = null;
        try
        {
            poll = new Poll(pollName, choices.Length);
            poll.PollDate = DateTime.ParseExact(pollDate, "d/m/yyyy", CultureInfo.InvariantCulture);
            foreach(var choice in choices)
            {
                poll.addChoices(choice);
            }
            string commandSTR = "INSERT INTO Betya_PollsTB(Poll_Title,User_ID,Poll_Date";
            int index = 1;
            foreach(var choice in choices)
            {
                commandSTR += ",Choice_" + index++ ;
            }
            commandSTR += ") VALUES (@param1, @param2, @param3";
            index = 4;
            foreach (var choice in choices)
            {
                commandSTR += ", @param" + index++;
            }
            commandSTR += ")";

            com.CommandText = commandSTR;
            con.Open();
            com.Parameters.AddWithValue("@param1", pollName);
            com.Parameters.AddWithValue("@param2", userID);
            com.Parameters.AddWithValue("@param3", pollDate);
            index = 4;
            foreach (var choice in choices)
            {
                com.Parameters.AddWithValue("@param" + index++, choice);
            }
            int rowsAffected = com.ExecuteNonQuery();
            if(rowsAffected < 1)
            {
                return "Something wrong happened!";
            }

            return "Poll added successfully!!";

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "Something wrong happened!";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    public DataTable getPolls()
    {
        con.Open();
        dt = new DataTable();
        adapter = new SqlDataAdapter("SELECT * FROM Betya_PollsTB", con);
        adapter.Fill(dt);
        return dt;
    }
}