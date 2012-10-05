using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private tk2dAnimatedSprite _aniSp;
    // Use this for initialization
    void Start()
    {
        _aniSp = GetComponent<tk2dAnimatedSprite>();
        if (_aniSp == null) return;
        _aniSp.Play(0);
        StartCoroutine(accelerate());
    }
    IEnumerator accelerate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _aniSp.anim.clips[0].fps = Mathf.Clamp(++_aniSp.anim.clips[0].fps, 0, 20);
            print(_aniSp.anim.clips[0].fps);
            if (_aniSp.anim.clips[0].fps >= 20)
            {
                StopAllCoroutines();                                         
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //print(_aniSp.spriteId);
    }
}
