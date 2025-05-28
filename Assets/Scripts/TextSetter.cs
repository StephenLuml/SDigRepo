using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textToSet;
    [SerializeField]
    private string preText = "";
    [SerializeField]
    private string postText = "";

    public void SetText(string text)
    {
        textToSet.text = preText + text + postText;
    }
}
