using UnityEngine;
using System.Collections;

public class LoadAsyn : MonoBehaviour
{
    private AsyncOperation async;
    public UISlider progressBar;
    void Awake()
    {
        progressBar.gameObject.SetActiveRecursively(false);
        DontDestroyOnLoad(transform.gameObject);
    }

    public void LoadLevel()
    {
        progressBar.gameObject.SetActiveRecursively(true);
        StartCoroutine(LoadLevelAsyn());
    }

    IEnumerator LoadLevelAsyn()
    {
        async = Application.LoadLevelAsync("MainMenu");        
        while (!async.isDone)
        {
            progressBar.sliderValue = async.progress;
            print(async.progress);            
            yield return 0;
        }
        progressBar.sliderValue = 1;
        Debug.Log("Loading complete");
    }
}
