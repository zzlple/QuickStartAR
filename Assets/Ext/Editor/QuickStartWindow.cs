using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class QuickStartWindow : EditorWindow
{
	/// <summary>
	/// The x.
	/// </summary>
	public static float x, y, width, height;
	/// <summary>
	/// The pre targets URI.
	/// </summary>
	public static Dictionary<int,string> PreTargetsUri;

	public static Dictionary<int,string> AsyncUpdateTargetsUri;
	public const string ARCAMERA = "ARCamera";
	public const string IMAGETARGET = "ImageTarget";
	public const string PLEASEADDMODEL="请添加模型";
	/// <summary>
	/// The scroll position.
	/// </summary>
	public Vector2 scrollPosition;
	/// <summary>
	/// The height of the line.
	/// </summary>
	public float lineHeight = 20;
	/// <summary>
	/// The message.
	/// </summary>
	public string msg="";
	public bool showMsg;

	public float timeShowMsg;

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
				PreTargetsUri = new Dictionary<int,string> ();
				AsyncUpdateTargetsUri = new Dictionary<int, string> ();
				PreTargetsUri.Add (0, PLEASEADDMODEL);
			}

			return qsw;
		}

	}

	void OnInspectorUpdate ()
	{
		this.Repaint ();
	}




	void OnGUI ()
	{



		if (null == PreTargetsUri) {
			return;
		}


		scrollPosition =GUILayout.BeginScrollView (scrollPosition, false, true);
	
		Dictionary<int,string>.Enumerator enumerator=PreTargetsUri.GetEnumerator ();
		while (enumerator.MoveNext ()) {
			KeyValuePair<int,string> keyValue=enumerator.Current;
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("删除", new GUILayoutOption[]{ GUILayout.Height (lineHeight), GUILayout.Width (50) })) {
				if (PreTargetsUri.ContainsKey (keyValue.Key)) {
					PreTargetsUri.Remove (keyValue.Key);
					break;
				}
			}

			GUILayout.TextField (keyValue.Value, new GUILayoutOption[]{ GUILayout.Height (lineHeight) });
			if (GUILayout.Button ("导入模型", new GUILayoutOption[]{ GUILayout.Height (20), GUILayout.Width (100) })) {

				string file = Utils.OpenFileLocalPath (new string[]{ "fbx", "max", "mb", "obj" });
				if (!string.IsNullOrEmpty (file)) {
					string modelPath = "/model/" + file.Substring (file.LastIndexOf ("/") + 1);
					FileUtils.copyFile (file, Utils.GetAssetsPath () + modelPath);
					AssetDatabase.Refresh ();
					string ModelAssetsPath = "Assets" + modelPath;
					if (PreTargetsUri.ContainsKey (keyValue.Key)) {
						AsyncUpdateTargetsUri.Add (keyValue.Key, ModelAssetsPath);
					} else {
						PreTargetsUri.Add (keyValue.Key, ModelAssetsPath);
					}



				} else {
					msg = "所选模型格式错误";
					showMsg = true;
				

			

				}
			}


			GUILayout.EndHorizontal ();

		
		}



		enumerator.Dispose ();

		if (null != AsyncUpdateTargetsUri && AsyncUpdateTargetsUri.Count > 0) {
		
			var buffer = new List<int>(AsyncUpdateTargetsUri.Keys); 
			foreach(var key in buffer)
			{

				PreTargetsUri [key] = AsyncUpdateTargetsUri [key];
			}
			AsyncUpdateTargetsUri.Clear ();
		}



		GUILayout.BeginVertical ();

		if (GUILayout.Button ("新增Item", new GUILayoutOption[]{ GUILayout.Height (lineHeight) })) {

			PreTargetsUri.Add (PreTargetsUri.Count, PLEASEADDMODEL);

		}
		if (GUILayout.Button ("制作场景", new GUILayoutOption[]{ GUILayout.Height (lineHeight) })) {

			//Init ARCamera
			if (null == GameObject.Find (ARCAMERA)) {
				Object arcamera = Resources.Load (ARCAMERA);
				Instantiate (arcamera).name = ARCAMERA;
			}	

			//Add target
			Dictionary<int,string>.Enumerator enumerator1=PreTargetsUri.GetEnumerator ();
			while (enumerator1.MoveNext ()) {
				KeyValuePair<int,string> keyValue=enumerator1.Current;
				if (!keyValue.Value.Equals (PLEASEADDMODEL)) {
					Object obj=	AssetDatabase.LoadAssetAtPath (keyValue.Value, typeof(GameObject));

					AssetDatabase.CreateFolder("Assets","Prefabs");



					PrefabUtility.CreatePrefab ("Prefabs/1.prefab", (GameObject)obj);

					//GameObject.Instantiate (obj);





				} else {
				
				
					showMsg = true;

					msg = "请补全模型信息";
				
					break;
				}
			}
			enumerator1.Dispose ();


		}




		GUILayout.EndVertical ();

		GUILayout.EndScrollView ();


		timeShowMsg += Time.deltaTime;

		if (timeShowMsg > 15f) {


			showMsg = false;
			msg = "";
			timeShowMsg = 0;
		}


		if (showMsg) {
	
			GUILayout.BeginArea (new Rect(width/4+width/8,height/4+height/8,width,height));
			GUILayout.Button("("+(int)(15-timeShowMsg)+")"+msg,new GUILayoutOption[]{GUILayout.Width(width/4),GUILayout.Height(height/4)});
			GUILayout.EndArea ();
		}

	



	
	}




}
