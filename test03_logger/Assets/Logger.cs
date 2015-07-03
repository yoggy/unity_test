using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Logger : MonoBehaviour {

	public int lineNum = 10;
	public int x = 10;
	public int y = 10;
	public int width = 320;
	public int height;

	string text = "";
	List<string> msgs = new List<string> ();

	void Start () {
		height = lineNum * 16;

	}
	
	void Update () {
	}

	void OnGUI() {
		GUI.TextArea (new Rect (x, y, width, height), text);
	}

	void LogImpl(string msg) {
		string str = "" + Time.frameCount + " : " + msg;
		Debug.Log (str);
		msgs.Add (str);

		if (msgs.Count > lineNum) {
			msgs.RemoveAt (0);
		}

		text = "";
		for (int i = 0; i < msgs.Count; ++i) {
			text += msgs[i];
			text += "\n";
		}
	}

	static Logger singleton_;

	public static void Log(string msg) {
		if (singleton_ == null) {
			GameObject obj = new GameObject("logger");
			obj.AddComponent (typeof(Logger));
			singleton_ = obj.GetComponent <Logger>();
		}
		singleton_.LogImpl(msg);
	}
}
