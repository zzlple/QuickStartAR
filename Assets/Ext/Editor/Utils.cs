using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Utils {


	public static string OpenFileLocalPath (string[] format)
	{
		string tempPath = "";
		tempPath = EditorUtility.OpenFilePanel ("Please Select File", "", "");
		if (string.IsNullOrEmpty (tempPath)) {

			return "";
		}
		int count = 0;
		for (int i = 0; i < format.Length; i++) {


			if (tempPath.EndsWith (format [i])) {

				count++;

			}

		}

		if (count == 0) {
			return "";

		}

		return tempPath;

	}

	public static string getProjectPath ()
	{

		return Application.dataPath.Substring (0, Application.dataPath.LastIndexOf ('/'));

	}


	public static string GetAssetsPath(){

		return getProjectPath()+"/Assets";
	}


	public static void Copy(string src,string dstDir){




	}




	public static Texture2D getLocalImage (string path)
	{



		if (string.IsNullOrEmpty (path)) {

			return null;
		}
		Texture2D tempTexture = new Texture2D (100, 100);
		byte[] bs =	System.IO.File.ReadAllBytes (path);
		tempTexture.LoadImage (bs);
		tempTexture.Apply ();


		AssetDatabase.Refresh ();

		return tempTexture;

	}



}