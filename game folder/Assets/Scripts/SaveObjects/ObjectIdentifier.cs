using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

[Serializable]
public abstract class SavableObject : MonoBehaviour
{
    private const string DATABASEFOLDER = "DATABASE";
    private static string _databasePath;
    private static string DatabasePath
    {
        get
        {
            if (string.IsNullOrEmpty(_databasePath))
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                var applicationPath = Path.GetDirectoryName(path);
                _databasePath = Path.Combine(applicationPath, DATABASEFOLDER);
            }
            return _databasePath;
        }
    }

    public void Save(string fileName)
    {
        var currentType = GetType();
        var writer = new XmlSerializer(currentType);
        var textWriter = new StreamWriter(Path.Combine(DatabasePath, currentType.Name + ".xml"));
    }
}
