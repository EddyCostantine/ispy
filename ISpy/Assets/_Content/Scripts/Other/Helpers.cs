using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Helpers
{
    private static float HexToDecNormalized(string hex)
    {
        return System.Convert.ToInt32(hex, 16) / 255.0f;
    }
    public static Color ColorFromHex(string hex)
    {
        float red = HexToDecNormalized(hex.Substring(0, 2));
        float green = HexToDecNormalized(hex.Substring(2, 2));
        float blue = HexToDecNormalized(hex.Substring(4, 2));
        float alpha = 1.0f;
        if (hex.Length > 8)
            alpha = HexToDecNormalized(hex.Substring(6, 2));

        return new Color(red, green, blue, alpha);
    }

    public static void SetAllBordersWidth(VisualElement ve, float value)
    {
        ve.style.borderTopWidth = value;
        ve.style.borderBottomWidth = value;
        ve.style.borderLeftWidth = value;
        ve.style.borderRightWidth = value;
    }
    public static void SetAllBordersColor(VisualElement ve, Color value)
    {
        ve.style.borderTopColor = value;
        ve.style.borderBottomColor = value;
        ve.style.borderLeftColor = value;
        ve.style.borderRightColor = value;
    }
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

    public static IEnumerator CountdownTimer(float duration)
    {
        float timeRemaining = duration;
        bool timerIsRunning = true;
        while (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }

            yield return timeRemaining;
        }
    }
    public static string FormatDisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
public class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}

