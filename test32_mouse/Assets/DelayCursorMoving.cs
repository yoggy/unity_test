using System;
using System.Collections.Generic;
using UnityEngine;

class Util
{
    public static long GetCurrentMilliseconds()
    {
        return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
}

class CursorPos
{
    public long t;
    public Vector3 pos;

    public CursorPos(Vector3 pos)
    {
        this.t = Util.GetCurrentMilliseconds();
        this.pos = pos;
    }
}

public class DelayCursorMoving : MonoBehaviour {

    int delay_ms = 0;

    List<CursorPos> history;

	void Start ()
    {
        history = new List<CursorPos>();
    }
	
	void Update () {
        Vector3 pos_screen = Input.mousePosition;
        Vector3 pos_world = Camera.main.ScreenToWorldPoint(pos_screen);
        pos_world.z = 100;

        history.Add(new CursorPos(pos_world));

        foreach (CursorPos p in history)
        {
            if (Util.GetCurrentMilliseconds() - p.t >= delay_ms)
            {
                gameObject.transform.position = p.pos;
                break;
            }
        }

        // cleanup old cursor position data...
        history.RemoveAll(p => p.t + delay_ms <= Util.GetCurrentMilliseconds());
    }

    public void SetDelay(float t)
    {
        delay_ms = (int)t;
    }

}
