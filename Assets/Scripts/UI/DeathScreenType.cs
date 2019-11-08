using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenType : MonoBehaviour
{
    TextMeshPro txt;
    string story;

    void Awake()
    {
        txt = GetComponent<TextMeshPro>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine(PlayText());
    }
    
    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.125f);
        }
    }
}
