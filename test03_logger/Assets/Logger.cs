using UnityEngine;
using System.Collections;

public class Logger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Logger.singleton_ = (Logger)gameObject;	
	}
	
	void Update () {
	
	}

	void LogImpl(string msg) {
		Debug.Log (msg);
	}

	static Logger singleton_;

	public static void Log(string msg) {
		if (singleton_ != null) {
			singleton_.LogImpl(msg);
		}
	}
}
