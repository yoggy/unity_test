using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public GameObject _prefab; // ←GUIから対象とするPrefabを選択しておくこと

	void Start () {
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Vector3 p = new Vector3(Random.Range(-5, 5), 10, Random.Range(-5, 5));
			Quaternion q = Random.rotation;

			GameObject obj = (GameObject)Instantiate(_prefab, p, q); // ← prefabをインスタンス化

			// 生成したprefabには、y<-10のときにgameObjectを削除するスクリプトを付加している
		}
	}
}
