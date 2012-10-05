using UnityEngine;
using System.Collections;

public class NGUIEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        print("click" + gameObject.name);
    }
}
