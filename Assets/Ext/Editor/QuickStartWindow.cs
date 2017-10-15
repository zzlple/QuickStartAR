using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuickStartWindow : EditorWindow
{
	public static float x, y, width, height;


	private  QuickStartWindow ()
	{
	}
	private static QuickStartWindow qsw;
	public static QuickStartWindow Instance {
		get {
			if (null == qsw) {
				width = Screen.currentResolution.width / 2;
				height = Screen.currentResolution.height / 2;
				x = width + width / 2;
				y = height + height / 2;
				qsw = QuickStartWindow.GetWindowWithRect<QuickStartWindow> (new Rect (x, y, width, height));
			}

			return qsw;
		}

	}

	void OnInspectorUpdate()
	{
		this.Repaint();
	}


	void OnGUI(){

		GUILayout.BeginHorizontal ();

		GUILayout.Label ("请选择模型", new GUILayoutOption[]{ GUILayout.Height (20) });

		if (GUILayout.Button ("导入模型", new GUILayoutOption[]{ GUILayout.Height (20),GUILayout.Width(100) })) {
		
			string file=Utils.OpenFileLocalPath (new string[]{ "fbx", "max", "mb", "obj" });
			if (!string.IsNullOrEmpty (file)) {
				string modelPath = "/model/" + file.Substring (file.LastIndexOf ("/")+1);
				FileUtils.copyFile (file,Utils.GetAssetsPath()+modelPath);
				AssetDatabase.Refresh ();
				Object obj=	AssetDatabase.LoadAssetAtPath ("Assets"+modelPath, typeof(GameObject));
				GameObject.Instantiate (obj);
			

			}
		}

		GUILayout.EndHorizontal ();



	
	}


}
