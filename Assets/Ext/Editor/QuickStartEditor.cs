using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuickStartEditor : Editor
{
	public const string ARQUICKSTART="ARQuickStart";

	[MenuItem (ARQUICKSTART+"/QuickStartEditor")]
	static void  CreateAR ()
	{
		QuickStartWindow qsw = QuickStartWindow.Instance;
		qsw.titleContent = new GUIContent ("QuickStart");
		qsw.Show ();
		qsw.Focus ();
	}
}
