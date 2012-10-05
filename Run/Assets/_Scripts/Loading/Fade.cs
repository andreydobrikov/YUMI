using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public UISprite spriteFade;
    public float fadeBackPauseTime = 0f;
    public float fadeTime = 2.0f;
    public LoadAsyn loadAsyn;
    public bool fadeFromStart = true;
    public bool fadeBack = true;
    public float waitTime = 0f;
    public float fadeButtonTime = .1f;
    public UISlider progressBar;
   
    void Awake()
    {
        spriteFade = GetComponent<UISprite>();
        if (spriteFade == null) return;        
    }
    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(waitTime);
        if(progressBar!=null)
        {
            progressBar.gameObject.SetActiveRecursively(false);
        }
        if(fadeFromStart)
            fadeTheSprite();
    }

    public void FadeButton()
    {
        spriteFade.color = new Color(1, 1, 1, 1);
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", fadeButtonTime, "onUpdate", "UpdateScoreDisplay"));
    }
   

    void fadeTheSprite()
    {
        //spriteFade.color = new Color(spriteFade.color.r, spriteFade.color.g, spriteFade.color.b, 0.3f);
        if (spriteFade.color.a==1)
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", fadeTime, "onUpdate", "UpdateScoreDisplay","onComplete","FadeBack"));
        }
        else
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", fadeTime, "onUpdate", "UpdateScoreDisplay", "onComplete", "loadAsynLevel"));
        }
    }

    void UpdateScoreDisplay(float newScore)
    {
        spriteFade.color = new Color(spriteFade.color.r, spriteFade.color.g, spriteFade.color.b, newScore);
    }

    IEnumerator FadeBack()
    {
        if (fadeBack)
        {
            yield return new WaitForSeconds(fadeBackPauseTime);
            fadeTheSprite();
        }
    }

    void loadAsynLevel()
    {        
        loadAsyn.LoadLevel();
    }

    void OnClick()
    {
        print("click the button !");
    }
}
