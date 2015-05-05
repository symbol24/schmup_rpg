using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class DialogueDataObject : ICloneable
{
    [SerializeField] private CharacterIdentifier _character;
    public CharacterIdentifier Character { get { return _character; } set { _character = value; } }
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

    [SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }

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
