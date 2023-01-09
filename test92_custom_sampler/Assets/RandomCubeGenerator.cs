using UnityEngine;

public class RandomCubeGenerator : MonoBehaviour
{
    public GameObject prefabCube;
    public int generateCount = 20000;

    bool _enableRandomCube;
    public bool EnableRandomCube
    {
        get => _enableRandomCube;
        set
        {
            _enableRandomCube = value;

            if (_enableRandomCube == false)
            {
                foreach (Transform t in transform)
                {
                    Destroy(t.gameObject);
                }
            }
            else
            {
                for (int i = 0; i < generateCount; ++i)
                {
                    GameObject obj = Instantiate(prefabCube);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                }
            }
        }
    }
}
