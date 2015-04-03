using System;
using UnityEngine;
using System.Collections;

public class DialogueDataObject : ICloneable
{
    public CharacterIdentifier Character { get; set; }
    private string _title;
    public string Title
    {
        get
        {
            if (string.IsNullOrEmpty(_title))
            {
                _title = Enum.GetName(typeof (CharacterIdentifier), Character);
            }
            return _title;
        }
        set { _title = value; }
    }
    public string Text { get; set; }
    public object Clone()
    {
        var ret = new DialogueDataObject()
        {
            Character = Character,
            Title = Title,
            Text = Text,
        };
        return ret;
    }
}
