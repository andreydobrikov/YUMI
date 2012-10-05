using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (Application.platform==RuntimePlatform.WindowsEditor)
        {
            print("This is windows platform!");
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            print("This is iPhone platform!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
