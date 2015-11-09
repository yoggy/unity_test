using UnityEngine;
using System.Collections;

public class ButtonClickCube : MonoBehaviour {

    public CubeController cube;

	public void OnClick () {
        if (cube != null) cube.Fire();
    }
	
}
