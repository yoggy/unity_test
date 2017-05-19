using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseCursor : MonoBehaviour {

    public void showCursor()
    {
        Cursor.visible = true;
    }

    public void hideCursor()
    {
        Cursor.visible = false;
    }

    public void toggleHideCursor()
    {
        Cursor.visible = !Cursor.visible;
    }
}
