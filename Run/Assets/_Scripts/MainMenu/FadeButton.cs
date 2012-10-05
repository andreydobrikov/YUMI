using UnityEngine;
using System.Collections;

public class FadeButton : MonoBehaviour
{
    public Fade fadeScript;
    // Use this for initialization    

    void OnClick()
    {
        fadeScript.FadeButton();
    }
}
