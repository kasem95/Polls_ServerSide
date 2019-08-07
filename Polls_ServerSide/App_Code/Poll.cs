using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Poll
/// </summary>
public class Poll
{
    List<string> choices = null; 
    public int ID { get; set; }
    public string Name { get; set; }
    public int NumOfChoices { get; set; }
    public DateTime PollDate { get; set; }
    public Poll(string name, int numOfChoices)
    {
        Name = name;
        NumOfChoices = numOfChoices;
        choices = new List<string>(NumOfChoices);
    }

    public void addChoices(string choice)
    {
        choices.Add(choice);
    }

    public string getChoiceByIndex(int index)
    {
        return choices[index];
    }

    public List<string> getChoicesList()
    {
        return choices;
    }
}