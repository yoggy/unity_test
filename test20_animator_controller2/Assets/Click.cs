using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    public CubeController cube;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cube != null) cube.Fire();
    }
}
