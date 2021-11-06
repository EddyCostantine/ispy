using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Helpers
{
    public static Dictionary<string, string> GetAttributeKeyValues(TextAsset fileToParse, int attributeIndex = 1, char lineSeparator = '\n', char surround = '"')
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string[] lines = fileToParse.text.Split(lineSeparator, System.StringSplitOptions.None);

        Regex regexCSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        for (int i = 0; i < lines.Length; i++)
        {
            string[] fields = regexCSVParser.Split(lines[i]);
            for (int j = 0; j < fields.Length; j++)
            {
                fields[j] = fields[j].Replace("\n", "").Replace("\r", "");
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd(surround);
            }
            if (fields.Length > attributeIndex)
            {
                var key = fields[0];
                if (dictionary.ContainsKey(key)) { continue; }
                var value = fields[attributeIndex];
                dictionary.Add(key, value);
            }
        }
        return dictionary;
    }
}
