using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography; 
using System;

public class EasySave : MonoBehaviour
{
	public enum Mode{Overwrite=0,NoOverwrite,Append};
	
	// Use these to specify your own custom datapath for Windows & OSX.
	// Note: Do not include a slash at the end.
	public static string windowsDataPath = ""; // For example, "C:/Users/myusername"
	public static string osxDataPath = ""; // For example, "/Users/myusername"
	
	/* Encryption variables */
	
	public static bool encryptionOn = false;
	public static string encryptionPassword = "SET ME TO YOUR OWN SECURE PASSWORD";
	
	/* Save in memory variables */
	public static bool saveInMemory = false;
	private static Dictionary<string, byte[]> memory = new Dictionary<string, byte[]>();
	
	// phpPassword must match the password set in PHP
	public static string phpPassword = "SET ME TO YOUR OWN SECURE PASSWORD";
	// Note: If using web player, PHP file must be on same domain as the Web Player.
	public static string phpURL = "http://www.mysite.com/EasySavePHP.php";
	
	/*
		SAVE FUNCTIONS - SAVE FUNCTIONS - SAVE FUNCTIONS - SAVE FUNCTIONS
	*/
	
	public static void saveString(string param, string filename, Mode m)
	{
		save(new string[]{param},filename,m);
	}
	
	public static void saveRawString(string param, string filename, Mode m)
	{
		System.Text.Encoding encoder = System.Text.Encoding.ASCII;
		byte[] bytes = encoder.GetBytes(param);
		FileStream fs = createFileStream(filename,m);
		fs.Write(bytes,0,bytes.Length);
		fs.Close();
	}
	
	public static void saveRawString(string param, string filename)
	{
		System.Text.Encoding encoder = System.Text.Encoding.ASCII;
		byte[] bytes = encoder.GetBytes(param);
		FileStream fs = createFileStream(filename,Mode.Overwrite);
		fs.Write(bytes,0,bytes.Length);
		fs.Close();
	}
	
	public static void save(string[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)
			bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m != Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveChar(char param, string filename, Mode m)
	{
		save(new char[]{param},filename,m);
	}
	
	public static void save(char[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);

		if(m!=Mode.Append)bw.Write(noOfItems);
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveInt(int param, string filename, Mode m)
	{
		save(new int[]{param},filename,m);
	}
	
	public static void save(int[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveUInt(uint param, string filename, Mode m)
	{
		save(new uint[]{param},filename,m);
	}
	
	public static void save(uint[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveShort(short param, string filename, Mode m)
	{
		save(new short[]{param},filename,m);
	}
	
	public static void save(short[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveUShort(ushort param, string filename, Mode m)
	{
		save(new ushort[]{param},filename,m);
	}
	
	public static void save(ushort[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveFloat(float param, string filename, Mode m)
	{
		save(new float[]{param},filename,m);
	}
	
	public static void save(float[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		floatArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveLong(long param, string filename, Mode m)
	{
		save(new long[]{param},filename,m);
	}
	
	public static void save(long[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveULong(ulong param, string filename, Mode m)
	{
		save(new ulong[]{param},filename,m);
	}
	
	public static void save(ulong[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveDouble(double param, string filename, Mode m)
	{
		save(new double[]{param},filename,m);
	}
	
	public static void save(double[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveBool(bool param, string filename, Mode m)
	{
		save(new bool[]{param},filename,m);
	}
	
	public static void save(bool[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveVector2(Vector2 param, string filename, Mode m)
	{
		save(new Vector2[]{param},filename,m);
	}
	
	public static void save(Vector2[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		v2ArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveVector3(Vector3 param, string filename, Mode m)
	{
		save(new Vector3[]{param},filename,m);
	}
	
	public static void save(Vector3[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		v3ArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveVector4(Vector4 param, string filename, Mode m)
	{
		save(new Vector4[]{param},filename,m);
	}
	
	public static void save(Vector4[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		v4ArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveColor(Color param, string filename, Mode m)
	{
		save(new Color[]{param},filename,m);
	}
	
	public static void save(Color[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		colorArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveQuaternion(Quaternion param, string filename, Mode m)
	{
		save(new Quaternion[]{param},filename,m);
	}
	
	public static void save(Quaternion[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		quaternionArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}

	public static void saveMesh(Mesh param, string filename, Mode m)
	{
		save(new Mesh[]{param},filename,m);
	}
	
	public static void save(Mesh[] param, string filename, Mode m)
	{
		bool isBumpMapped = false;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Mesh mesh in param)
		{
			if(mesh == null)
			 	meshToWriter(new Mesh(),bw,isBumpMapped);
			else
				meshToWriter(mesh,bw,isBumpMapped);
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveTransform(Transform param, string filename, Mode m)
	{
		save(new Transform[]{param},filename,m);
	}
	
	public static void save(Transform[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
	if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Transform t in param)
		{
			if(t==null)
			{
				floatArrayToWriter(new float[]{0,0,0},bw);
				floatArrayToWriter(new float[]{0,0,0,0},bw);
				floatArrayToWriter(new float[]{1,1,1},bw);
				bw.Write(t.tag);
			}
			else
			{
				Vector3 pos = t.position;
				Quaternion rot = t.rotation;
				Vector3 scale = t.localScale;
				floatArrayToWriter(new float[]{pos.x,pos.y,pos.z},bw);
				floatArrayToWriter(new float[]{rot.x,rot.y,rot.z,rot.w},bw);
				floatArrayToWriter(new float[]{scale.x,scale.y,scale.z},bw);
				bw.Write(t.tag);
			}
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveTexture2D(Texture2D param, string filename, Mode m)
	{
		save(new Texture2D[]{param},filename,m);
	}
	
	public static void saveTextureAsPNG(Texture2D param, string filename)
	{
		save(param.EncodeToPNG(),filename,Mode.Overwrite);
	}
	
	public static void save(Texture2D[] param, string filename, Mode m)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Texture2D tex in param)
		{
			if(tex==null)
				bw.Write(0);
			else
			{
				byte[] encoded = tex.EncodeToPNG();
				bw.Write(encoded.Length);
				bw.Write(encoded);
			}
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveByte(byte param, string filename, Mode m)
	{
		save(new byte[]{param},filename,m);
	}
	
	public static void save(byte[] param, string filename, Mode m)
	{
		#if UNITY_WEBPLAYER
		if(m == Mode.NoOverwrite && PlayerPrefs.HasKey(filename))
		{
			Debug.LogError("Can't overwrite file as mode is set to NoOverwrite");
			return;
		}
		if(param.Length > 1024000)
		{
       		Debug.LogError("You may only save up to 1MB of data on Unity Web Player.");
       		return;
		}

		string asString = "";
		if(encryptionOn)
			asString = System.Convert.ToBase64String(encrypt(param,encryptionPassword));
		else
			asString = System.Convert.ToBase64String(param);
		PlayerPrefs.SetString(filename, asString);

		#else
		if(param==null)
		{
			Debug.LogError("Could not save: The data you are trying to save is null.");
			return;
		}
		if(!passSaveChecks(filename,m))
		{
			Debug.LogError("Did not pass save checks. You may be trying to overwrite when NoOverwrite mode is enabled.");
			return;
		}
		
		if(saveInMemory)
		{
			memory.Add(filename, param);
			return;
		}
		
		//Create FileStream
		FileStream fs = createFileStream(filename,m);

		//Write bytes to stream
		try
		{
			if(encryptionOn)
			{
				byte[] encryptedBytes = encrypt(param, encryptionPassword);
				fs.Write(encryptedBytes,0,encryptedBytes.Length);
			}
			else
			{
				fs.Write(param,0,param.Length);
			}
			fs.Close();
		}
		catch(Exception e)
        {
        	Debug.LogError("Could not save file.\nDetails: "+e.Message);
        }
        #endif
	}
	
	public static void saveString(string param, string filename)
	{
		Mode m=0;
		save(new string[]{param},filename,m);
	}
	
	
	public static void save(string[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)
			bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m != Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveChar(char param, string filename)
	{
		Mode m=0;
		save(new char[]{param},filename,m);
	}
	
	public static void save(char[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);

		if(m!=Mode.Append)bw.Write(noOfItems);
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveInt(int param, string filename)
	{
		Mode m=0;
		save(new int[]{param},filename,m);
	}
	
	public static void save(int[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveUInt(uint param, string filename)
	{
		Mode m=0;
		save(new uint[]{param},filename,m);
	}
	
	public static void save(uint[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveShort(short param, string filename)
	{
		Mode m=0;
		save(new short[]{param},filename,m);
	}
	
	public static void save(short[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveUShort(ushort param, string filename)
	{
		Mode m=0;
		save(new ushort[]{param},filename,m);
	}
	
	public static void save(ushort[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveFloat(float param, string filename)
	{
		Mode m=0;
		save(new float[]{param},filename,m);
	}
	
	public static void save(float[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		floatArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveLong(long param, string filename)
	{
		Mode m=0;
		save(new long[]{param},filename,m);
	}
	
	public static void save(long[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveULong(ulong param, string filename)
	{
		Mode m=0;
		save(new ulong[]{param},filename,m);
	}
	
	public static void save(ulong[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveDouble(double param, string filename)
	{
		Mode m=0;
		save(new double[]{param},filename,m);
	}
	
	public static void save(double[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveBool(bool param, string filename)
	{
		Mode m=0;
		save(new bool[]{param},filename,m);
	}
	
	public static void save(bool[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveVector2(Vector2 param, string filename)
	{
		Mode m=0;
		save(new Vector2[]{param},filename,m);
	}
	
	public static void save(Vector2[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		v2ArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveVector3(Vector3 param, string filename)
	{
		Mode m=0;
		save(new Vector3[]{param},filename,m);
	}
	
	public static void save(Vector3[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		v3ArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveVector4(Vector4 param, string filename)
	{
		Mode m=0;
		save(new Vector4[]{param},filename,m);
	}
	
	public static void save(Vector4[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		v4ArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveColor(Color param, string filename)
	{
		Mode m=0;
		save(new Color[]{param},filename,m);
	}
	
	public static void save(Color[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		colorArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveQuaternion(Quaternion param, string filename)
	{
		Mode m=0;
		save(new Quaternion[]{param},filename,m);
	}
	
	public static void save(Quaternion[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		// Write header
		if(m!=Mode.Append)bw.Write(noOfItems);
		quaternionArrayToWriter(param,bw);
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}

	public static void saveMesh(Mesh param, string filename)
	{
		Mode m=0;
		save(new Mesh[]{param},filename,m);
	}
	
	public static void save(Mesh[] param, string filename)
	{
		Mode m=0;
		bool isBumpMapped=false;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Mesh mesh in param)
		{
			if(mesh == null)
			 	meshToWriter(new Mesh(),bw,isBumpMapped);
			else
				meshToWriter(mesh,bw,isBumpMapped);
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveMesh(Mesh param, string filename, Mode m, bool isBumpMapped)
	{
		save(new Mesh[]{param},filename,m);
	}
	
	public static void save(Mesh[] param, string filename, Mode m, bool isBumpMapped)
	{
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Mesh mesh in param)
		{
			if(mesh == null)
			 	meshToWriter(new Mesh(),bw,isBumpMapped);
			else
				meshToWriter(mesh,bw,isBumpMapped);
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveTransform(Transform param, string filename)
	{
		Mode m=0;
		save(new Transform[]{param},filename,m);
	}
	
	public static void save(Transform[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
	if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Transform t in param)
		{
			if(t==null)
			{
				floatArrayToWriter(new float[]{0,0,0},bw);
				floatArrayToWriter(new float[]{0,0,0,0},bw);
				floatArrayToWriter(new float[]{1,1,1},bw);
				bw.Write("");
			}
			else
			{
				Vector3 pos = t.position;
				Quaternion rot = t.rotation;
				Vector3 scale = t.localScale;
				floatArrayToWriter(new float[]{pos.x,pos.y,pos.z},bw);
				floatArrayToWriter(new float[]{rot.x,rot.y,rot.z,rot.w},bw);
				floatArrayToWriter(new float[]{scale.x,scale.y,scale.z},bw);
				bw.Write(t.tag);
			}
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveTexture2D(Texture2D param, string filename)
	{
		Mode m=0;
		save(new Texture2D[]{param},filename,m);
	}
	
	public static void save(Texture2D[] param, string filename)
	{
		Mode m=0;
		int noOfItems = param.Length;
		if(noOfItems==0)
		{
			Debug.LogError("Could not save: The array you have specified is empty.");
			return;
		}
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		if(m!=Mode.Append)bw.Write(noOfItems);
		foreach(Texture2D tex in param)
		{
			if(tex==null)
				bw.Write(0);
			else
			{
				byte[] encoded = tex.EncodeToPNG();
				bw.Write(encoded.Length);
				bw.Write(encoded);
			}
		}
		if(m!=Mode.Append)
			save(stream.ToArray(),filename,m);
		else
			append(stream.ToArray(),noOfItems,filename);
		bw.Close();
	}
	
	public static void saveDictionary(Dictionary<string,string> d, string filename)
	{
		MemoryStream stream = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(stream);
		
		bw.Write(d.Count);
		
		foreach(KeyValuePair<string,string> item in d)
		{
			bw.Write(item.Key);
			bw.Write(item.Value);
		}
		save(stream.ToArray(),filename);
	}
	
	public static void saveByte(byte param, string filename)
	{
		Mode m=0;
		save(new byte[]{param},filename,m);
	}
	
	public static void save(byte[] param, string filename)
	{
		#if UNITY_WEBPLAYER

		string asString = "";
		if(encryptionOn)
			asString = System.Convert.ToBase64String(encrypt(param,encryptionPassword));
		else
			asString = System.Convert.ToBase64String(param);
		PlayerPrefs.SetString(filename, asString);

		#else
		Mode m=0;
		if(param==null)
		{
			Debug.LogError("Could not save: The data you are trying to save is null.");
			return;
		}
		if(!passSaveChecks(filename,m))
		{
			Debug.Log("Did not pass save checks");
			return;
		}
		
		if(saveInMemory)
		{
			memory.Add(filename, param);
			return;
		}
		
		//Create FileStream
		FileStream fs = createFileStream(filename,m);

		//Write bytes to stream
		try
		{
			if(encryptionOn)
			{
				byte[] encryptedBytes = encrypt(param, encryptionPassword);
				fs.Write(encryptedBytes,0,encryptedBytes.Length);
			}
			else
			{
				fs.Write(param,0,param.Length);
			}
			fs.Close();
		}
		catch(Exception e)
        {
        	Debug.LogError("Could not save file.\nDetails: "+e.Message);
        }
        #endif
	}
	
	public static IEnumerator uploadFileToWeb(string filename)
	{
		string id = "";
		// Create a POST form with our details
		WWWForm form = new WWWForm();
		form.AddField("save", getFileAsString(filename)); // Our save file as a string
		form.AddField("password", stringToMD5(phpPassword)); // Our password as an MD5 hash
		form.AddField("saveID", id); // The ID we want to use to identify save in database
		form.AddField("upload", "true"); // Lets PHP know whether we're downloading or uploading

		// Upload our form and wait for it to upload.
		WWW www = new WWW(phpURL,form);
		yield return www;
		
		// If the WWW object returns an error, report an error.
		if(www.error != null)
		{
			Debug.LogError("Could not upload file: "+www.error);
		}
		// If the PHP form returns any data, it will be an error message.
		else if(www.text != "")
		{
			Debug.LogError("Could not upload file: "+www.text);
		}
	}
	
	public static IEnumerator uploadFileToWeb(string filename, string id)
	{
		// Create a POST form with our details
		WWWForm form = new WWWForm();
		form.AddField("save", getFileAsString(filename)); // Our save file as a string
		form.AddField("password", stringToMD5(phpPassword)); // Our password as an MD5 hash
		form.AddField("saveID", id); // The ID we want to use to identify save in database
		form.AddField("upload", "true"); // Lets PHP know whether we're downloading or uploading
		// Upload our form and wait for it to upload.
		WWW www = new WWW(phpURL,form);
		yield return www;
		
		// If the WWW object returns an error, report an error.
		if(www.error != null)
		{
			Debug.LogError("Could not upload file: "+www.error);
		}
		// If the PHP form returns any data, it will be an error message.
		else if(www.text != "")
		{
			Debug.LogError("Could not upload file: "+www.text);
		}
	}
	
	public static void append(byte[] param, int paramLength, string filename)
	{
		#if UNITY_WEBPLAYER
		if(param==null)
		{
			Debug.LogError("Could not append: The data you are trying to append is null.");
			return;
		}
		if(encryptionOn)
		{
			Debug.LogError("Could not append: You can not use append while encryption is turned on.");
			return;
		}
		if(!PlayerPrefs.HasKey(filename))
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(stream);
			bw.Write(paramLength);
			bw.Write(param);
			save(stream.ToArray(),filename);
			bw.Close();
		}
		else
		{
			string path = filename;
			byte[] oldBytes = getBytesFromFile(path);
			int oldBytesLength = oldBytes.Length;
			int newBytesLength = param.Length;
			byte[] newBytes = new byte[oldBytes.Length+newBytesLength];
			oldBytes.CopyTo(newBytes, 0);
	    	param.CopyTo(newBytes, oldBytesLength);   	
	    	byte[] noOfItems = System.BitConverter.GetBytes(paramLength+System.BitConverter.ToInt32(newBytes,0));
	    	for(int i=0;i<noOfItems.Length;i++)
	    	{
	    		newBytes[i]=noOfItems[i];
	    	}

	    	save(newBytes,filename,Mode.Overwrite);
		}

		#else
		if(param==null)
		{
			Debug.LogError("Could not append: The data you are trying to append is null.");
			return;
		}
		if(encryptionOn)
		{
			Debug.LogError("Could not append: You can not use append while encryption is turned on.");
			return;
		}
		if(!fileExists(filename))
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(stream);
			bw.Write(paramLength);
			bw.Write(param);
			save(stream.ToArray(),filename);
			bw.Close();
		}
		else
		{
			string path = getPersistentDataPath()+"/"+filename;
			byte[] oldBytes = File.ReadAllBytes(path);
			int oldBytesLength = oldBytes.Length;
			int newBytesLength = param.Length;
			byte[] newBytes = new byte[oldBytes.Length+newBytesLength];
			oldBytes.CopyTo(newBytes, 0);
	    	param.CopyTo(newBytes, oldBytesLength);   	
	    	byte[] noOfItems = System.BitConverter.GetBytes(paramLength+System.BitConverter.ToInt32(newBytes,0));
	    	for(int i=0;i<noOfItems.Length;i++)
	    	{
	    		newBytes[i]=noOfItems[i];
	    	}

	    	save(newBytes,filename,Mode.Overwrite);
		}
		#endif
	}
	
	/* 
		PRIVATE FUNCTIONS
		- You shouldn't need to use any of these.
	*/
	
	public static IEnumerator downloadFileFromPHP(string saveAsFilename, string id)
	{
		// Create a POST form with our details
		WWWForm form = new WWWForm();
		form.AddField("password", stringToMD5(phpPassword)); // Our password as an MD5 hash
		form.AddField("saveID", id); // The ID we want to use to identify save in database
		form.AddField("upload", "false"); // Lets PHP know whether we're downloading or uploading
		
		// Upload our form and wait for it to upload.
		WWW www = new WWW(phpURL,form);
		yield return www;
		
		// If the WWW object returns an error, report an error.
		if(www.error != null)
		{
			Debug.LogError("Could not download file: "+www.error);
		}
		// If the PHP form returns any data, it will be an error message.
		else if(www.text.Substring(0,5) == "ERROR")
		{
			Debug.LogError("Could not download file: "+www.text);
		}
		else
		{
			bool encryption = encryptionOn;
			encryptionOn = false;
			save(System.Convert.FromBase64String(www.text), saveAsFilename);
			encryptionOn = encryption;
		}
	}
	
	public static IEnumerator downloadFileFromPHP(string saveAsFilename)
	{
		string id="";
		// Create a POST form with our details
		WWWForm form = new WWWForm();
		form.AddField("password", stringToMD5(phpPassword)); // Our password as an MD5 hash
		form.AddField("saveID", id); // The ID we want to use to identify save in database
		form.AddField("upload", "false"); // Lets PHP know whether we're downloading or uploading
		
		// Upload our form and wait for it to upload.
		WWW www = new WWW(phpURL,form);
		yield return www;
		
		// If the WWW object returns an error, report an error.
		if(www.error != null)
		{
			Debug.LogError("Could not download file: "+www.error);
		}
		// If the PHP form returns any data, it will be an error message.
		else if(www.text.Substring(0,5) == "ERROR")
		{
			Debug.LogError("Could not download file: "+www.text);
		}
		else
		{
			bool encryption = encryptionOn;
			encryptionOn = false;
			save(System.Convert.FromBase64String(www.text), saveAsFilename);
			encryptionOn = encryption;
		}
	}
	
	private static string getPersistentDataPath()
	{
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			if(windowsDataPath!="")
				return windowsDataPath;
		}
		else if(Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			if(osxDataPath!="")
				return osxDataPath;
		}
		return Application.persistentDataPath;
	}
	
	private static FileStream createFileStream(string filename, Mode m)
	{
		string savepath = getPersistentDataPath()+"/"+filename;
		
		if(!Directory.Exists(getPersistentDataPath()))
			Directory.CreateDirectory(getPersistentDataPath());
		
		if(m!=Mode.NoOverwrite)
			return new FileStream(savepath, FileMode.Create);
		else
			return new FileStream(savepath, FileMode.CreateNew);
	}
	
	private static string stringToMD5(string password)
	{
		MD5 md5 = MD5.Create();
		byte[] md5Bytes = md5.ComputeHash(ASCIIEncoding.Default.GetBytes(password));
		return BitConverter.ToString(md5Bytes).ToLower().Replace("-","");
	}
	
	private static void meshToWriter(Mesh param, BinaryWriter bw, bool isBumpMapped)
	{
		//WRITE VERTICES
		int noOfVerts = param.vertexCount;
		bw.Write(noOfVerts); // Write vertex header
		Vector3[] verts = param.vertices;
		v3ArrayToWriter(verts,bw);
		//WRITE TRIANGLES
		int[] tris = param.triangles;
		int triLength = tris.Length;
		bw.Write(triLength);
		if(noOfVerts<65535)
		{
			for(int i=0;i<triLength;i++)
			{
				bw.Write((ushort)tris[i]);
			}
		}
		else
		{
			for(int i=0;i<triLength;i++)
			{
				bw.Write((uint)tris[i]);
			}
		}
		//WRITE UVS
		Vector2[] uvs = param.uv;
		v2ArrayToWriter(uvs,bw);
		bw.Write(isBumpMapped);
		if(isBumpMapped)
		{
			Vector2[] uv2 = param.uv2;
			if(uv2.Length >0)
			{
				v2ArrayToWriter(uv2,bw);
				bw.Write(true);
			}
			else
				bw.Write(false);
			// WRITE TANGENTS
			v4ArrayToWriter(param.tangents,bw);
		}
	}
	
	private static void meshToWriter(Mesh param, BinaryWriter bw)
	{
		bool isBumpMapped=false;
		//WRITE VERTICES
		int noOfVerts = param.vertexCount;
		bw.Write(noOfVerts); // Write vertex header
		Vector3[] verts = param.vertices;
		v3ArrayToWriter(verts,bw);
		//WRITE TRIANGLES
		int[] tris = param.triangles;
		int triLength = tris.Length;
		bw.Write(triLength);
		if(noOfVerts<65535)
		{
			for(int i=0;i<triLength;i++)
			{
				bw.Write((ushort)tris[i]);
			}
		}
		else
		{
			for(int i=0;i<triLength;i++)
			{
				bw.Write((uint)tris[i]);
			}
		}
		//WRITE UVS
		Vector2[] uvs = param.uv;
		v2ArrayToWriter(uvs,bw);
		bw.Write(isBumpMapped);
		if(isBumpMapped)
		{
			Vector2[] uv2 = param.uv2;
			if(uv2.Length >0)
			{
				v2ArrayToWriter(uv2,bw);
				bw.Write(true);
			}
			else
				bw.Write(false);
			// WRITE TANGENTS
			v4ArrayToWriter(param.tangents,bw);
		}
	}
	
	private static void v2ToWriter(Vector2 v, BinaryWriter bw)
	{
		bw.Write(v.x);
		bw.Write(v.y);
	}
	
	private static void v3ToWriter(Vector3 v, BinaryWriter bw)
	{
		bw.Write(v.x);
		bw.Write(v.y);
		bw.Write(v.z);
	}
	
	private static void v4ToWriter(Vector4 v, BinaryWriter bw)
	{
		bw.Write(v.x);
		bw.Write(v.y);
		bw.Write(v.z);
		bw.Write(v.w);
	}
	
	private static void colorToWriter(Color v, BinaryWriter bw)
	{
		bw.Write(v.r);
		bw.Write(v.g);
		bw.Write(v.b);
		bw.Write(v.a);
	}
	
	private static void quaternionToWriter(Quaternion v, BinaryWriter bw)
	{
		bw.Write(v.x);
		bw.Write(v.y);
		bw.Write(v.z);
		bw.Write(v.w);
	}
	
	private static void floatArrayToWriter(float[] param, BinaryWriter bw)
	{
		int noOfItems = param.Length;
		for(int i=0;i<noOfItems;i++)
			bw.Write(param[i]);
	}
	
	private static void v2ArrayToWriter(Vector2[] array, BinaryWriter bw)
	{
	    if(array.Length == 0)
			return;
			
		foreach(Vector2 v in array)
		{
			bw.Write(v.x);
			bw.Write(v.y);
		}
	}
	
	private static void v3ArrayToWriter(Vector3[] array, BinaryWriter bw)
	{
	    if(array.Length == 0)
			return;
			
		foreach(Vector3 v in array)
		{
			bw.Write(v.x);
			bw.Write(v.y);
			bw.Write(v.z);
		}
	}
	
	private static void v4ArrayToWriter(Vector4[] array, BinaryWriter bw)
	{
	    if(array.Length == 0)
			return;
			
		foreach(Vector4 v in array)
		{
			bw.Write(v.x);
			bw.Write(v.y);
			bw.Write(v.z);
			bw.Write(v.w);
		}
	}
	
	private static void colorArrayToWriter(Color[] array, BinaryWriter bw)
	{
	    if(array.Length == 0)
			return;
			
		foreach(Color v in array)
		{
			bw.Write(v.r);
			bw.Write(v.g);
			bw.Write(v.b);
			bw.Write(v.a);
		}
	}
	
	private static void quaternionArrayToWriter(Quaternion[] array, BinaryWriter bw)
	{
	    if(array.Length == 0)
			return;
		foreach(Quaternion v in array)
		{
			bw.Write(v.x);
			bw.Write(v.y);
			bw.Write(v.z);
			bw.Write(v.w);
		}
	}
	
	private static bool passSaveChecks(string filename, Mode m)
	{
		if(!modeCheck(filename,m))
			return false;
		else
			createDirIfMissing(filename);
		return true;
	}
	
	private static bool passSaveChecks(string filename)
	{
		Mode m = 0;
		if(!modeCheck(filename,m))
			return false;
		else
			createDirIfMissing(filename);
		return true;
	}
	
	private static bool modeCheck(string filename, Mode m)
	{
		if(m==Mode.NoOverwrite)
		{
			if(File.Exists(getPersistentDataPath()+"/"+filename))
			{
				print("Could not save: Overwrite would occur, and Mode is set to NoOverwrite.");
				return false;
			}
		}
		return true;
	}
	
	private static void createDirIfMissing(string filename)
	{
		int idx = filename.LastIndexOf(@"/");
		if(idx>0)
		{
			string dir = getPersistentDataPath()+"/"+filename.Substring(0,idx);
			if(!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
		}
	}
	
	public static void writeToStream(System.Object obj, BinaryWriter bw)
	{
		System.Type type = obj.GetType();
		
	 	if(type == typeof(byte)) // Byte
			bw.Write((byte)obj);
		else if(type == typeof(string)) // String
			bw.Write((string)obj);
		else if(type == typeof(char)) // Char
			bw.Write((char)obj);
		else if(type == typeof(int)) // Int
			bw.Write((int)obj);
		else if(type == typeof(uint)) // UInt
			bw.Write((uint)obj);
		else if(type == typeof(short)) // Short
			bw.Write((short)obj);
		else if(type == typeof(ushort)) // UShort
			bw.Write((ushort)obj);
		else if(type == typeof(float)) // Float
			bw.Write((float)obj);
		else if(type == typeof(long)) // Long
			bw.Write((long)obj);
		else if(type == typeof(ulong)) // ULong
			bw.Write((ulong)obj);
		else if(type == typeof(double)) // Double
			bw.Write((double)obj);
		else if(type == typeof(bool)) // Bool
			bw.Write((bool)obj);
		else if(type == typeof(Vector2)) // Vector2
			v2ToWriter((Vector2)obj,bw);
		else if(type == typeof(Vector3)) // Vector3
			v3ToWriter((Vector3)obj,bw);
		else if(type == typeof(Vector4)) // Vector4
			v4ToWriter((Vector4)obj,bw);
		else if(type == typeof(Color)) // Color
			colorToWriter((Color)obj,bw);
		else if(type == typeof(Quaternion)) // Quaternion
			quaternionToWriter((Quaternion)obj,bw);
		else if(type == typeof(Mesh)) // Mesh
			meshToWriter((Mesh)obj,bw); // --- TODO: Save bumpmap if it is applicable!
		else if(type == typeof(Transform)) // Transform
			Debug.Log("Transform");
		else if(type == typeof(Texture2D)) // Texture2D
			Debug.Log("Texture2D");
		else
			Debug.Log("Type not supported");
	}
	
	
	/*
		LOAD FUNCTIONS - LOAD FUNCTIONS - LOAD FUNCTIONS - LOAD FUNCTIONS
	*/
	
	
	public static string loadString(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		try
        {
			BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
			br.ReadInt32();
			string result = br.ReadString();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static string[] loadStringArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
		{
			int noOfItems = br.ReadInt32();
			string[] result = new string[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadString();
			}
			br.Close();
			return result;
		}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static Dictionary<string,string> loadDictionary(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
		{
			int noOfItems = br.ReadInt32();
			Dictionary<string,string> result = new Dictionary<string,string>();
			for(int i=0;i<noOfItems;i++)
			{
				result.Add(br.ReadString(),br.ReadString());
			}
			br.Close();
			return result;
		}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static char loadChar(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return Convert.ToChar(0x0);
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
			char result = br.ReadChar();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return Convert.ToChar(0x0);
	}
	
	public static char[] loadCharArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
		{
			int noOfItems = br.ReadInt32();
			char[] result = new char[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadChar();
			}
			br.Close();
			return result;
		}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static int loadInt(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			int result = br.ReadInt32();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static int[] loadIntArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			int[] result = new int[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadInt32();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static float loadFloat(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			float result = br.ReadSingle();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static float[] loadFloatArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			float[] result = new float[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadSingle();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static uint loadUInt(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			uint result = br.ReadUInt32();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static uint[] loadUIntArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			uint[] result = new uint[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadUInt32();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static short loadShort(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			short result = br.ReadInt16();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static short[] loadShortArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			short[] result = new short[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadInt16();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static ushort loadUShort(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			ushort result = br.ReadUInt16();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static ushort[] loadUShortArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			ushort[] result = new ushort[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadUInt16();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static long loadLong(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			long result = br.ReadInt64();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static long[] loadLongArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			long[] result = new long[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadInt64();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static ulong loadULong(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			ulong result = br.ReadUInt64();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static ulong[] loadULongArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			ulong[] result = new ulong[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadUInt64();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static double loadDouble(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return 0;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			double result = br.ReadDouble();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return 0;
	}
	
	public static double[] loadDoubleArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			double[] result = new double[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadDouble();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static bool loadBool(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return false;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			bool result = br.ReadBoolean();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return false;
	}
	
	public static bool[] loadBoolArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			bool[] result = new bool[noOfItems];
			for(int i=0;i<noOfItems;i++)
			{
				result[i] = br.ReadBoolean();
			}
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static Vector2 loadVector2(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return Vector2.zero;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
			Vector2 result = new Vector2();
			result.x = br.ReadSingle();
			result.y = br.ReadSingle();
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return Vector2.zero;
	}
	
	public static Vector2[] loadVector2Array(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			Vector2[] result = v2ArrayFromReader(br,noOfItems);
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static Vector3 loadVector3(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return Vector3.zero;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
			Vector3 result = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return Vector3.zero;
	}
	
	public static Vector3[] loadVector3Array(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			Vector3[] result = v3ArrayFromReader(br,noOfItems);
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static Vector4 loadVector4(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return Vector4.zero;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
			Vector4 result = new Vector4(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return Vector4.zero;
	}
	
	public static Vector4[] loadVector4Array(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			Vector4[] result = v4ArrayFromReader(br,noOfItems);
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static Quaternion loadQuaternion(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return Quaternion.identity;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
			Quaternion result = new Quaternion(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return Quaternion.identity;
	}
	
	public static Quaternion[] loadQuaternionArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			Quaternion[] result = quatArrayFromReader(br,noOfItems);
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static Mesh loadMesh(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
  			return loadMeshFromReader(br);
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static Mesh[] loadMeshArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	int noOfItems = br.ReadInt32();
        	Mesh[] meshes = new Mesh[noOfItems];
        	for(int i=0;i<noOfItems;i++)
        	{
        		meshes[i] = loadMeshFromReader(br);
        	}
        	br.Close();
   			return meshes;
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static Transform loadTransform(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
        	Transform t = new GameObject().transform;
        	t.position = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
        	t.rotation = new Quaternion(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
        	t.localScale = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
        	if(br.PeekChar() != -1)
        			t.tag = br.ReadString();
        	br.Close();
  			return t;
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static void loadTransform(string filename, GameObject go)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
        	Transform t = go.transform;
        	t.position = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
        	t.rotation = new Quaternion(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
        	t.localScale = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
        	if(br.PeekChar() != -1)
        		t.tag = br.ReadString();
        	br.Close();
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return;
	}
	
	public static Transform[] loadTransformArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	int noOfItems = br.ReadInt32();
        	Transform[] t = new Transform[noOfItems];
        	for(int i=0;i<noOfItems;i++)
        	{
        		t[i] = new GameObject().transform;
	        	t[i].position = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	t[i].rotation = new Quaternion(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	t[i].localScale = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	if(br.PeekChar() != -1)
        			t[i].tag = br.ReadString();
        	}
        	br.Close();
  			return t;
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static void loadTransformArray(string filename, GameObject[] go)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	int noOfItems = br.ReadInt32();
        	if(noOfItems != go.Length)
        	{
        		Debug.LogError("Could not auto-assign transforms: Length of GameObject array provided is not equal to number of transforms in 									save file.");
        		return;
        	}
        	Transform[] t = new Transform[noOfItems];
        	for(int i=0;i<noOfItems;i++)
        	{
        		t[i] = go[i].GetComponent("Transform") as Transform;
	        	t[i].position = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	t[i].rotation = new Quaternion(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	t[i].localScale = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	if(br.PeekChar() != -1)
        			t[i].tag = br.ReadString();
        	}
        	br.Close();
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return;
	}
	
	public static GameObject[] loadTransformArrayAndInstantiate(string filename, GameObject go)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	int noOfItems = br.ReadInt32();

        	GameObject[] t = new GameObject[noOfItems];
        	for(int i=0;i<noOfItems;i++)
        	{
        		t[i] = Instantiate(go,new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle()),
        							new Quaternion(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle())) as GameObject;
	        	t[i].transform.localScale = new Vector3(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
	        	if(br.PeekChar() != -1)
        			t[i].tag = br.ReadString();
        	}
        	br.Close();
        	return t;
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        	return null;
        }
	}
	
	public static Texture2D loadTexture2D(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
        	Texture2D newTex = new Texture2D(0,0,TextureFormat.ARGB32,true);
        	newTex.LoadImage(br.ReadBytes(br.ReadInt32()));
        	newTex.Apply();
        	br.Close();
        	return newTex;
       	}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static Texture2D loadPNG(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		try
        {
        	Texture2D newTex = new Texture2D(0,0,TextureFormat.ARGB32,true);
        	newTex.LoadImage(getBytesFromFile(filename));
        	newTex.Apply();
        	return newTex;
       	}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static void loadPNG(GameObject go, string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return;
		}
		
		try
        {
        	((go.renderer.material.mainTexture) as Texture2D).LoadImage(getBytesFromFile(filename));
        	((go.renderer.material.mainTexture) as Texture2D).Apply();
       	}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
	}
	
	public static Texture2D[] loadTexture2DArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	int noOfItems = br.ReadInt32();
        	Texture2D[] texs = new Texture2D[noOfItems];
        	for(int i=0;i<noOfItems;i++)
        	{
        		texs[i] = new Texture2D(0,0,TextureFormat.ARGB32,true);
	        	texs[i].LoadImage(br.ReadBytes(br.ReadInt32()));
	        	texs[i].Apply();
        	}
        	br.Close();
        	return texs;
       	}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static void loadTexture2D(string filename, GameObject go)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return;
		}
		if(go.renderer == null || go.renderer.material == null)
		{
			Debug.LogError("Could not auto-assign texture: GameObject does not have a renderer or material");
			return;
		}
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	br.ReadInt32();
        	Texture2D newTex = new Texture2D(0,0,TextureFormat.ARGB32,true);
        	newTex.LoadImage(br.ReadBytes(br.ReadInt32()));
        	newTex.Apply();
        	br.Close();
        	go.renderer.material.mainTexture = newTex;
       	}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return;
	}
	
	public static void loadTexture2DArray(string filename, GameObject[] go)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
        	int noOfItems = br.ReadInt32();
        	if(noOfItems != go.Length)
        	{
        		Debug.LogError("Could not auto-assign textures: Length of GameObject array provided is not equal to number of textures in save file.");
        		return;
        	}
        	for(int i=0;i<noOfItems;i++)
        	{
        		if(go[i].renderer == null || go[i].renderer.material == null)
				{
					Debug.LogError("Could not auto-assign texture: GameObject does not have a renderer or material");
					continue;
				}
				Texture2D texs = new Texture2D(0,0);
        		texs = new Texture2D(0,0,TextureFormat.ARGB32,true);
	        	texs.LoadImage(br.ReadBytes(br.ReadInt32()));
	        	texs.Apply();
	        	go[i].renderer.material.mainTexture = texs;
        	}
        	br.Close();
       	}
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return;
	}
	
	public static IEnumerator loadImageFromWeb(string url, Texture2D tex)
	{
		if(tex ==null)
		{
			Debug.LogError("Could not load file from web: Texture supplied as parameter is null");
		}
		else
		{
			WWW download = new WWW(url);
			yield return download;
			if(download.error!=null)
			{
				Debug.LogError("Could not load file from web: "+download.error);
			}
			else
			{
				download.LoadImageIntoTexture(tex);
			}
		}
	}
	
	public static IEnumerator loadImageFromWeb(string url, GameObject go)
	{
		if(go.renderer.material.mainTexture==null)
		{
			Debug.LogError("Could not load file from web: Texture supplied as parameter is null");
		}
		else
		{
			WWW download = new WWW(url);
			yield return download;
			if(download.error!=null)
			{
				Debug.LogError("Could not load file from web: "+download.error);
			}
			else
			{
				Texture2D newTex = new Texture2D(0,0);
				go.renderer.material.mainTexture = newTex;
				download.LoadImageIntoTexture(newTex);
			}
		}
	}
	
	public static Color loadColor(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return Color.clear;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			br.ReadInt32();
			Color result = new Color(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return Color.clear;
	}
	
	public static Color[] loadColorArray(string filename)
	{
		if(!EasySave.fileExists(filename))
		{
			Debug.LogError("Could not load file: File does not exist.");
			return null;
		}
		
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		try
        {
			int noOfItems = br.ReadInt32();
			Color[] result = colorArrayFromReader(br,noOfItems);
			br.Close();
			return result;
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
		return null;
	}
	
	public static byte[] loadByteArray(string filename)
	{
		try
        {
			MemoryStream ms = new MemoryStream(getBytesFromFile(filename));
			if(ms.Length<1)
				return null;
			BinaryReader br = new BinaryReader(ms);
			return br.ReadBytes((int)ms.Length);
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	public static byte loadByte(string filename)
	{
		BinaryReader br = new BinaryReader(new MemoryStream(getBytesFromFile(filename)));
		return br.ReadByte();
	}
	
	/* 
		PRIVATE FUNCTIONS
		- You shouldn't need to use any of these.
	*/
	private static byte[] getBytesFromFile(string filename)
	{
		#if UNITY_WEBPLAYER
			if(PlayerPrefs.HasKey(filename))
			{
				if(encryptionOn)
					return decrypt(System.Convert.FromBase64String(PlayerPrefs.GetString(filename)),encryptionPassword);
				return System.Convert.FromBase64String(PlayerPrefs.GetString(filename));
			}
			Debug.LogError("File does not exists");
			return null;
		#else
		if(encryptionOn)
		{
			return decrypt(File.ReadAllBytes(getPersistentDataPath()+"/"+filename),encryptionPassword);
		}
		return File.ReadAllBytes(getPersistentDataPath()+"/"+filename);
		#endif
	}
	
	private static string getFileAsString(string filename)
	{
		if(saveInMemory)
		{
			if(!memory.ContainsKey(filename))
			{
				Debug.LogError("File does not exist.");
				return null;
			}
			else
			{
				return System.Convert.ToBase64String(memory[filename]);
			}
			
		}
		#if UNITY_WEBPLAYER
		if(!PlayerPrefs.HasKey(filename))
		{
			Debug.LogError("File does not exist.");
			return null;
		}
		else
		{
			return PlayerPrefs.GetString(filename);
		}
		#else
		return System.Convert.ToBase64String(File.ReadAllBytes(getPersistentDataPath()+"/"+filename));
		#endif
	}
	
	private static Mesh loadMeshFromReader(BinaryReader br)
	{
		try
        {
        	Mesh newMesh = new Mesh();
			int noOfVerts = br.ReadInt32();
			Vector3[] verts = v3ArrayFromReader(br,noOfVerts);
			newMesh.vertices = verts;
			int noOfTris = br.ReadInt32();
			int[] tris = new int[noOfTris];
			if(noOfVerts<65535)
			{
				for(int i=0;i<noOfTris;i++)
				{
					tris[i] = br.ReadUInt16();
				}
			}
			else
			{
				for(int i=0;i<noOfTris;i++)
				{
					tris[i] = (int)br.ReadUInt32();
				}
			}
			Vector2[] uvs = v2ArrayFromReader(br,noOfVerts);
			if(br.ReadBoolean())
			{
				if(br.ReadBoolean())
				{
					Vector2[] uvs2 = v2ArrayFromReader(br,noOfVerts);
					newMesh.uv2 = uvs2;
				}
				
				Vector4[] tans = v4ArrayFromReader(br,noOfVerts);
				newMesh.tangents = tans;
			}
			newMesh.uv = uvs;
			newMesh.triangles = tris;
			newMesh.RecalculateNormals();
			newMesh.RecalculateBounds();
			return newMesh;
        }
		catch(Exception e)
        {
        	Debug.LogError("Could not load file: File specified is not in the correct format.\nDetails: "+e.Message);
        }
        return null;
	}
	
	private static Vector2[] v2ArrayFromReader(BinaryReader br, int length)
	{
		Vector2[] newArray = new Vector2[length];
		for(int i=0; i<length; i++)
		{
			newArray[i].x = br.ReadSingle();
			newArray[i].y = br.ReadSingle();
		}
		return newArray;
	}
	
	private static Vector3[] v3ArrayFromReader(BinaryReader br, int length)
	{
		Vector3[] newArray = new Vector3[length];
		for(int i=0; i<length; i++)
		{
			newArray[i].x = br.ReadSingle();
			newArray[i].y = br.ReadSingle();
			newArray[i].z = br.ReadSingle();
		}
		return newArray;
	}
	
	private static Vector4[] v4ArrayFromReader(BinaryReader br, int length)
	{
		Vector4[] newArray = new Vector4[length];
		for(int i=0; i<length; i++)
		{
			newArray[i].x = br.ReadSingle();
			newArray[i].y = br.ReadSingle();
			newArray[i].z = br.ReadSingle();
			newArray[i].w = br.ReadSingle();
		}
		return newArray;
	}
	
	private static Quaternion[] quatArrayFromReader(BinaryReader br, int length)
	{
		Quaternion[] newArray = new Quaternion[length];
		for(int i=0; i<length; i++)
		{
			newArray[i].x = br.ReadSingle();
			newArray[i].y = br.ReadSingle();
			newArray[i].z = br.ReadSingle();
			newArray[i].w = br.ReadSingle();
		}
		return newArray;
	}
	
	private static Color[] colorArrayFromReader(BinaryReader br, int length)
	{
		Color[] newArray = new Color[length];
		for(int i=0; i<length; i++)
		{
			newArray[i].r = br.ReadSingle();
			newArray[i].g = br.ReadSingle();
			newArray[i].b = br.ReadSingle();
			newArray[i].a = br.ReadSingle();
		}
		return newArray;
	}
	
	/*
		FILE TOOLS - FILE TOOLS - FILE TOOLS - FILE TOOLS - FILE TOOLS
	*/
	public static void deleteFile(string filename)
	{
		#if UNITY_WEBPLAYER
			PlayerPrefs.DeleteKey(filename);
		#else
		if(fileExists(filename))
		{
			File.Delete(getPersistentDataPath()+"/"+filename);
			return;
		}
		Debug.LogError("Could not delete file: File does not exist");
		#endif
	}
	
	public static bool fileExists(string filename)
	{
		#if UNITY_WEBPLAYER
			if(PlayerPrefs.HasKey(filename))
				return true;
			return false;
		#else
		FileInfo fi = new FileInfo(getPersistentDataPath()+"/"+filename);
		if(fi.Exists)
			return true;
		return false;
		#endif
	}
	
	public static bool fileExistsAtPath(string path)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return false;
		#else
		FileInfo fi = new FileInfo(path);
		if(fi.Exists)
			return true;
		return false;
		#endif
	}
	
	public static string[] getFiles()
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetFiles(getPersistentDataPath());
		#endif
	}
	
	public static string[] getFiles(string fileExtension)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetFiles(getPersistentDataPath(),"*"+fileExtension);
		#endif
	}
	
	public static string[] getFilesInFolder(string folder)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetFiles(getPersistentDataPath()+"/"+folder);
		#endif
	}
	
	public static string[] getFilesInFolder(string folder, string fileExtension)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetFiles(getPersistentDataPath()+"/"+folder,"*"+fileExtension);
		#endif
	}
	
	public static string[] getAllFiles()
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetFiles(getPersistentDataPath(), "",SearchOption.AllDirectories);
		#endif
	}
	
	public static string[] getAllFiles(string fileExtension)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetFiles(getPersistentDataPath(),"*"+fileExtension,SearchOption.AllDirectories);
		#endif
	}
	
	/* 
	- Folder Tools 
	*/
	
	public static void deleteFolder(string folder)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
		#else
		if(folderExists(folder))
		{
			Directory.Delete(getPersistentDataPath()+"/"+folder,true);
			return;
		}
		Debug.LogError("Could not delete folder: Folder does not exist");
		#endif
	}
	
	public static bool folderExists(string folder)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return false;
		#else
		string fullPath = getPersistentDataPath()+"/"+folder;
		if(Directory.Exists(fullPath))
			return true;
		return false;
		#endif
	}
	
	public static string[] getFolders()
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetDirectories(getPersistentDataPath());
		#endif
	}
	
	public static string[] getFoldersInFolder(string folder)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetDirectories(getPersistentDataPath()+"/"+folder);
		#endif
	}
	
	public static string[] getAllFolders()
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return Directory.GetDirectories(getPersistentDataPath(),"",SearchOption.AllDirectories);
		#endif
	}
	
	public static string getDefaultFolderPath()
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return null;
		#else
		return getPersistentDataPath();
		#endif
	}
	
	public static bool folderExistsAtPath(string path)
	{
		#if UNITY_WEBPLAYER
			Debug.LogError("Unity Web Player does not support this function.");
			return false;
		#else
		if(Directory.Exists(path))
			return true;
		return false;
		#endif
	}
	
	/*
		ENCRYPTION FUNCTIONS - ENCRYPTION FUNCTIONS - ENCRYPTION FUNCTIONS - ENCRYPTION FUNCTIONS
	*/
	private static byte[] encrypt(byte[] clearData, byte[] key, byte[] iv) 
    {
    	MemoryStream ms = new MemoryStream();
    	Rijndael alg = Rijndael.Create();
    	alg.Key = key; 
        alg.IV = iv; 
        CryptoStream cs = new CryptoStream(ms, 
        alg.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(clearData, 0, clearData.Length);
        cs.Close();
        return ms.ToArray();
    }
    
    private static byte[] encrypt(byte[] param, string password) 
    {
    	Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, 
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
        return encrypt(param, pdb.GetBytes(32), pdb.GetBytes(16));
    }
    
    private static byte[] decrypt(byte[] param, byte[] key, byte[] iv) 
    {
    	MemoryStream ms = new MemoryStream();
    	Rijndael alg = Rijndael.Create();
    	alg.Key = key; 
        alg.IV = iv;
        CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
        try
        {
	        cs.Write(param, 0, param.Length);
	        cs.Close();
	        return ms.ToArray();
        }
        catch(Exception e)
        {
        	Debug.LogError("Could not decrypt file: Password may be incorrect, or file is non-encrypted/corrupt.\nDetails: "+e.Message);
        	return null;
        }
    }
    
    private static byte[] decrypt(byte[] param, string password) 
    {
    	Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, 
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
        return decrypt(param, pdb.GetBytes(32), pdb.GetBytes(16));
    }
    
    public static void clearMemory()
    {
    	memory.Clear();
    }
}