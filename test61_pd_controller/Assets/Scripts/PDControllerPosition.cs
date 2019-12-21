using UnityEngine;

public class PDControllerPosition : MonoBehaviour
{
    public GameObject target_game_object;
    public float gain_p = 0.1f;
    public float gain_d = -0.8f;

    float prev_speed;

    void Start()
    {
        if (target_game_object == null) {
            Debug.LogError("PDControllerPosition : ERROR : target_game_object is null.");
            return;
        }        
    }

    void Update()
    {
        if (target_game_object == null) return;

        Vector3 v = target_game_object.transform.position - gameObject.transform.position;
        Vector3 v_normal = v.normalized;

        float distance = v.magnitude;

        float speed_p = distance * gain_p;
        float speed_d = (speed_p - prev_speed) * gain_d;

        float now_speed = speed_p + speed_d;
        gameObject.transform.position += now_speed * v_normal;

        prev_speed = now_speed;
    }
}
