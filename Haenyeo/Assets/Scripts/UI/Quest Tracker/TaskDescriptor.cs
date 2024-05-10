using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskDescriptor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Color normalColor;

    [SerializeField]
    Color taskCompletionColor;

    [SerializeField]
    Color taskSuccessCountColor;

    [SerializeField]
    Color strikeThrounghColor;

    public void UpdateText(string text)
    {
        this.text.fontStyle = FontStyles.Normal;
        this.text.text = text;
    }

    public void UpdateText(Task task)
    {
        text.fontStyle = FontStyles.Normal;

        if (task.IsComplete)
        {
            var colorCode = ColorUtility.ToHtmlStringRGB(taskCompletionColor);
            text.text = BuildText(task, colorCode, colorCode);
        }
        else
            text.text = BuildText(task, ColorUtility.ToHtmlStringRGB(normalColor), ColorUtility.ToHtmlStringRGB(taskSuccessCountColor));
    }

    public void UpdateTextUsingStrikeThrough(Task task)
    {
        var colorCode = ColorUtility.ToHtmlStringRGB(strikeThrounghColor);
        text.fontStyle = FontStyles.Strikethrough;
        text.text = BuildText(task, colorCode, colorCode);

    }

    string BuildText(Task task, string textColorCode, string successCountColorCode)
    {
        Debug.Log(task.Category);
        //return $"<color=#{textColorCode}>¡Ü{task.Description} <color=#{successCountColorCode}>{task.CurrentSuccess}</color>/{task.NeedSuccessToComplete}</color>";
        if (task.Category != "DIALOGUE")
            return $"<color=#{textColorCode}>¡Ü{task.Description} <color=#{successCountColorCode}>{task.CurrentSuccess}</color>/{task.NeedSuccessToComplete}</color>";
        else
            return $"<color=#{textColorCode}>¡Ü{task.Description}</color>";
        //return $"<color=#{textColorCode}>¡Ü{task.Description}</color>" + task.Category != "DIALOGUE" ? $"<color=#{successCountColorCode}>{task.CurrentSuccess}</color>/{task.NeedSuccessToComplete}" : "";
    }
}
