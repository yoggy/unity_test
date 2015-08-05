using UnityEngine;
using System.Collections;

public class ImageList : MonoBehaviour {

	public string _targetDirectory = "C:\\test\\";
	public string _targetExt = ".jpg";

	GameObject [] _images;

	void Start () {
		string [] files = GetFiles (_targetDirectory, ".jpg");	
		_images = CreateImagePlanes(files);
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	string [] GetFiles(string targetDirectory, string targetExt) {
		string[] files = System.IO.Directory.GetFiles(targetDirectory);
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		
		foreach (string f in files)
		{
			string ext = System.IO.Path.GetExtension(f);
			if (ext == targetExt)
			{
				list.Add(f);
			}
		}
		
		string [] result = list.ToArray ();

		return result;
	}

	GameObject [] CreateImagePlanes(string [] imageFiles) {
		if (imageFiles == null)
			return null;

		System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>();

		for (int i = 0; i < imageFiles.Length; ++i) {
			float x = (i % 7) * 3.5f;
			float y = (i / 7) * 2.5f;
			GameObject obj = CreateImagePlane (imageFiles[i], x, y, 0.0f, 0.3f, 0.2f);
			if (obj != null) {
				list.Add (obj);
			}
		}

		return list.ToArray ();
	}

	GameObject CreateImagePlane(string filename, float x, float y, float z, float w, float h) {
		if (filename == null)
			return null;

		byte [] buf = System.IO.File.ReadAllBytes (filename);
		if (buf == null) {
			Debug.LogError("cannot load image file...filename=" + filename);
			return null;
		}

		Texture2D tex = new Texture2D(2, 2);
		if (tex.LoadImage(buf) == false) {
			Debug.LogError("tex.LoadImage() failed...filename=" + filename);
			return null;
		}

		GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
		obj.name = "image-" + filename;

		Renderer rend = obj.GetComponent <Renderer>();
		rend.material = new Material(Shader.Find("UI/Default"));
		rend.material.mainTexture = tex;

		obj.transform.position = new Vector3(x, y, z);
		obj.transform.localRotation = Quaternion.Euler(new Vector3(90, 180, 0));
		obj.transform.localScale = new Vector3(w, 1.0f, h);

		return obj;
	}
}
