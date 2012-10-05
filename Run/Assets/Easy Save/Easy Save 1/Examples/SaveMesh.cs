using UnityEngine;
using System.Collections;

/*
	Attach to a GameObject which has a MeshFilter component.
	Saves a mesh and then destroys it. When clicking the appropriate button, 
	it then restores the mesh.
*/
public class SaveMesh : MonoBehaviour 
{	
	private void Start()
	{
		//Must have a MeshFilter attached to work
		if(GetComponent(typeof(MeshFilter)) == null)
			Debug.LogError("SaveMesh must be attached to a GameObject with a MeshFilter component");
	}
	
	private void OnGUI()
	{
		// Display Save & Destroy button
		if(GUI.Button(new Rect(10, 10, 180, 50),"Save & Destroy Mesh"))
		{
			MeshFilter ms = (GetComponent(typeof(MeshFilter)) as MeshFilter);
			EasySave.saveMesh(ms.mesh,"myMesh.mesh");
			Destroy(ms.mesh);
		}
		// If save file exists, display the load and delete buttons
		if(EasySave.fileExists("myMesh.mesh"))
		{
			//Load Button
			if(GUI.Button(new Rect(10, 100, 180, 50),"Load Mesh"))
			{
				(GetComponent(typeof(MeshFilter)) as MeshFilter).mesh = EasySave.loadMesh("myMesh.mesh");
			}
			//Delete Button
			if(GUI.Button(new Rect(10, 200, 180, 50),"Delete Saved Mesh File"))
			{
				EasySave.deleteFile("myMesh.mesh");
			}
		}
	}
}
