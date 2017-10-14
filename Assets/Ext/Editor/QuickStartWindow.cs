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

		GUI.Box (new Rect (0, 0, width - 100, 50), "请选择模型");

		if (GUI.Button (new Rect(width- 100,0,100,50),"导入模型")) {


			Debug.Log (Utils.getProjectPath ());
		}
	
	}


}
