using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    BAL getBAL = null;
    public WebService()
    {
        getBAL = new BAL();
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string getUserAfterCheck(string email, string password)
    {
        return getBAL.getUserAfterCheck(email, password);
    }

    [WebMethod]
    public string getMessageAfterRegisterAttempt(string email, string username, string password)
    {
        return getBAL.getMessageAfterRegisterAttempt(email, username, password);
    }

    [WebMethod]
    public string getMessageAfterAddingPollAttempt(string[] choices, string pollName, int userID, string pollDate)
    {
        return getBAL.getMessageAfterAddingPollAttempt(choices, pollName, userID, pollDate);
    }

    [WebMethod]
    public string pollsTable()
    {
        return getBAL.pollsTable();
    }

}
