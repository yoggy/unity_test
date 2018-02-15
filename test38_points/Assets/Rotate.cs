using UnityEngine;

public class Rotate : MonoBehaviour {
	void Update () {
        gameObject.transform.Rotate(0.1f, 0.2f, 0.3f);
	}
}
