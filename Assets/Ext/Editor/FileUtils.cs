using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.IO;
public class FileUtils : MonoBehaviour
{
	/// <summary>
	/// Reads the text.
	/// </summary>
	/// <returns>The text.</returns>
	/// <param name="fullpath">Fullpath.</param>
	public static string readText(string fullpath){


		return File.ReadAllText (fullpath);


	}

	/// <summary>
	/// Existses the file.
	/// </summary>
	/// <returns><c>true</c>, if file was existsed, <c>false</c> otherwise.</returns>
	/// <param name="fullpath">Fullpath.</param>
	public static bool existsFile(string fullpath){

		return File.Exists (fullpath);

	}

	/// <summary>
	/// Existses the file.
	/// </summary>
	/// <returns><c>true</c>, if file was existsed, <c>false</c> otherwise.</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="name">Name.</param>
	public static bool existsFile(string directory, string name){

		return File.Exists (directory+"/"+name);

	}


	/// <summary>
	/// Reads the texture.
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="fullPath">Full path.</param>


	public static Texture2D readTexture (string fullPath)
	{



		Texture2D texture = new Texture2D (1, 1);
		texture.LoadImage (File.ReadAllBytes (fullPath));
		texture.Apply ();
		return texture;

	}
	/// <summary>
	/// Reads the texture.
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="name">Name.</param>
	public static Texture2D readTexture (string directory, string name)
	{
		Texture2D texture = new Texture2D (1, 1);
		texture.LoadImage (File.ReadAllBytes (directory + "/" + name));
		texture.Apply ();
		return texture;

	}
	/// <summary>
	/// Reads the textures.
	/// </summary>
	/// <returns>The textures.</returns>
	/// <param name="directory">Directory.</param>
	public static List<Texture2D> readTextures (string directory)
	{
		List<Texture2D> lists = new List<Texture2D> ();

		try {
			string[] files = Directory.GetFiles (directory);
			foreach (string file in files) {
				Texture2D texture = new Texture2D (1, 1);
				texture.LoadImage (File.ReadAllBytes (file));
				texture.Apply ();
				if (null != texture.GetPixels () && texture.GetPixels ().Length > 128) {
					lists.Add (texture);

				}

			}
		} catch (Exception ex) {

		}

		return lists;
	}
	/// <summary>
	/// Reads the file.
	/// </summary>
	/// <returns>The file.</returns>
	/// <param name="fullPath">Full path.</param>
	public static byte[] readFile (string fullPath)
	{

		return File.ReadAllBytes (fullPath);
	}
	/// <summary>
	/// Reads the file.
	/// </summary>
	/// <returns>The file.</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="name">Name.</param>
	public static byte[] readFile (string directory, string name)
	{
		return File.ReadAllBytes (directory + "/" + name);

	}
	/// <summary>
	/// Reads the files.
	/// </summary>
	/// <returns>The files.</returns>
	/// <param name="directory">Directory.</param>
	public static List<byte[]> readFiles (string directory)
	{



		List<byte[]> lists = new List<byte[]> ();

		try {
			string[] files = Directory.GetFiles (directory);
			foreach (string file in files) {

				lists.Add (File.ReadAllBytes (file));



			}
		} catch (Exception ex) {

		}

		return lists;


	}


	public static void copyFile(string src,string dst){
	
		writeFile (dst,	readFile (src));
	
	}


	/// <summary>
	/// Writes the file.
	/// </summary>
	/// <returns>The file.</returns>
	/// <param name="fullPath">Full path.</param>
	/// <param name="bs">Bs.</param>
	public static string writeFile (string fullPath, byte[] bs)
	{
		DirectoryInfo info = Directory.GetParent (fullPath);
		if (info.Exists == false) {
			info.Create();

		}

		File.WriteAllBytes (fullPath, bs);

		return fullPath;

	}
	/// <summary>
	/// Writes the file.
	/// </summary>
	/// <returns>The file.</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="name">Name.</param>
	/// <param name="bs">Bs.</param>
	public static string writeFile (string directory, string name, byte[] bs)
	{
		if (Directory.Exists (directory) == false) {
			Directory.CreateDirectory(directory);
		}

		File.WriteAllBytes (directory + "/" + name, bs);

		return directory + "/" + name;

	}
	/// <summary>
	/// Gets the sizes.
	/// </summary>
	/// <returns>The sizes.</returns>
	/// <param name="directory">Directory.</param>
	public static long getSizes (string directory)
	{



		long temp = 0;
		try {
			string[] files = Directory.GetFiles (directory);
			foreach (string file in files) {

				temp += File.ReadAllBytes (file).Length;



			}
		} catch (Exception ex) {

		}

		return temp;
	}
	/// <summary>
	/// Gets the size.
	/// </summary>
	/// <returns>The size.</returns>
	/// <param name="fullPath">Full path.</param>
	public static int getSize (string fullPath)
	{


		return File.ReadAllBytes (fullPath).Length;

	}


}
