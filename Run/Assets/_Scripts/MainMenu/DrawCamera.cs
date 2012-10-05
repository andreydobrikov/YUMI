using UnityEngine;
using System.Collections;

public class DrawCamera : MonoBehaviour {

    public int width = 1;
    public int height = 1;
    public Color color;
	void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(new Vector3(transform.position.x,transform.position.y,40), new Vector3(width, height, 1));
    }
}
