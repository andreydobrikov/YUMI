using UnityEngine;
using System.Collections;

/*
	This example instantiates GameObjects stored in the Resources folder.
	It then saves the transforms and the names of these GameObjects,
	and then destroys them.
	Finally, it re-instantiates the GameObjects using the names that we
	saved and Unity's Resources.LoadAll() function.
*/
public class ResourcesFolderExample : MonoBehaviour 
{
	public string pathInResources = "";
	private GameObject[] instantiatedObjs = new GameObject[0];
	private bool objectsInstantiated = false;
	private bool objectsDestroyed = false;
	
	private void OnGUI()
	{
		// Display 'Instantiate Objects' Button
		if(!objectsInstantiated)
		{
			if(GUI.Button(new Rect(10, 10, 180, 50),"Instantiate Objects"))
			{
				loadObjectsFromResources();
			}
		}
		
		// Display 'Save and Destroy Objects' Button
		if(!objectsDestroyed && objectsInstantiated)
		{
			if(GUI.Button(new Rect(10, 10, 180, 50),"Save and Destroy Objects"))
			{
				saveAndDestroyObjects();
			}
		}
		
		// Display 'Save and Destroy Objects' Button
		if(objectsDestroyed)
		{
			if(GUI.Button(new Rect(10, 10, 180, 50),"Re-Instantiate Objects"))
			{
				reinstantiateObjects();
			}
		}
	}
	
	private void loadObjectsFromResources()
	{
		//Load all of the GameObjects in Resources into an array
		Object[] resources = Resources.LoadAll(pathInResources,typeof(GameObject));
		instantiatedObjs = new GameObject[resources.Length];
		//Instantiate these objects
		for(int i=0;i<resources.Length;i++)
		{
			instantiatedObjs[i] = Instantiate(resources[i]) as GameObject;
			// Unity changes the name of instantiated objects automatically,
			// so we must undo this by applying the original name back to it.
			instantiatedObjs[i].name = resources[i].name;
		}
		objectsInstantiated = true;
	}
	
	private void saveAndDestroyObjects()
	{
		// Create an array containing the transform of each object
		// and another with the name of each object
		Transform[] ts = new Transform[instantiatedObjs.Length];
		string[] names = new string[instantiatedObjs.Length];
		for(int i=0;i<instantiatedObjs.Length; i++)
		{
			ts[i] = instantiatedObjs[i].transform;
			names[i] = instantiatedObjs[i].name;
			Destroy(instantiatedObjs[i]);
		}
		// Save our arrays using EasySave into two seperate files
		EasySave.save(ts,"transforms.txt");
		EasySave.save(names,"names.txt");
		
		objectsDestroyed = true;
	}
	
	private void reinstantiateObjects()
	{
		//Load all of the GameObjects in Resources into an array
		Object[] resources = Resources.LoadAll(pathInResources,typeof(GameObject));
		
		//Instantiate the appropriate GameObjects by comparing the names
		//in the save file to the names of the objects in Resources
		string[] names = EasySave.loadStringArray("names.txt");
		instantiatedObjs = new GameObject[names.Length];
		
		for(int i=0;i<names.Length;i++)
		{
			foreach(Object go in resources)
			{
				if((go as GameObject).name == names[i])
				{
					instantiatedObjs[i] = Instantiate(resources[i]) as GameObject;
					instantiatedObjs[i].name = resources[i].name;
				}
			}
		}
		
		//Now apply the transforms using the version of 'loadTransformArray'
		//which automatically applys the transforms to an array of game objects.
		EasySave.loadTransformArray("transforms.txt",instantiatedObjs);
		objectsDestroyed = false;
	}
}
