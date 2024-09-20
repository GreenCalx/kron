using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogBank
{
    public static readonly List<string> NOSTRO_DIALOGS = new List<string>()
    {
        "I am Nostro. I exist to greet newcomers in these lands. ",
        "I'll open the storage for you. There's probably not much left in it, but take what you need and venture forth, Crusader."
    };

    public static readonly Dictionary<string,List<string>> ALL_DIALOGS = new Dictionary<string,List<string>>()
    {
        {Constants.NAME_NOSTRO, NOSTRO_DIALOGS}
    };

    public static string GetDialog(string iNPCName, int iDialogID)
    {
        if (ALL_DIALOGS.ContainsKey(iNPCName))
        { 
            return ALL_DIALOGS[iNPCName][iDialogID]; 
        }
        else
        { return string.Empty; }
    }

}