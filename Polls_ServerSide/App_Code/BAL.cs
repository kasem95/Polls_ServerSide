using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for BAL
/// </summary>
public class BAL
{
    DAL getDAL = null;
    public BAL()
    {
        //
        // TODO: Add constructor logic here
        //
        getDAL = new DAL();
    }

    public string getUserAfterCheck(string email, string password)
    {
        User user = getDAL.checkUser(email, password);
        return new JavaScriptSerializer().Serialize(user);
    }

    public string getMessageAfterRegisterAttempt(string email, string username, string password)
    {
        return new JavaScriptSerializer().Serialize(getDAL.registerUser(email, username, password));
    }

    public string getMessageAfterAddingPollAttempt(string[] choices, string pollName, int userID, string pollDate)
    {
        return new JavaScriptSerializer().Serialize(getDAL.addAPoll(choices, pollName, userID, pollDate));
    }

    public string pollsTable()
    {
        DataTable polls = getDAL.getPolls();
        return DataTableToJsonObj(polls);
    }

    string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
}