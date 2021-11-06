using System.Collections.Generic;
using UnityEngine;

public class Localisation
{
    public enum Language
    {
        English = 1,
        German = 2
    }

    private static Language currentLanguage = Language.English;
    private static Dictionary<string, string> localisedLanguage;

    public static void SetCurrentLanguage(Language language)
    {
        currentLanguage = language;
        TextAsset localisationCSV = Resources.Load<TextAsset>("Localisation");
        localisedLanguage = Helpers.GetAttributeKeyValues(localisationCSV, (int)language);
    }
    public static Language GetCurrentLanguage()
    {
        return currentLanguage;
    }
    public static string GetLocalisedValue(string stringKey)
    {
        if (localisedLanguage == null) SetCurrentLanguage(currentLanguage);
        string value = stringKey;
        localisedLanguage.TryGetValue(stringKey, out value);
        return value;
    }
}
