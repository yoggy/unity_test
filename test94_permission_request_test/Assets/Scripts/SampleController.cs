using UnityEngine;
using UnityEngine.UI;

public class SampleController : MonoBehaviour
{
    public PermissionRequester requester;
    public Text textMessage;

    void Start()
    {
        requester.permissions = new PermissionRequester.PermissionType[] { PermissionRequester.PermissionType.CAMERA, PermissionRequester.PermissionType.LOCATION };
        requester.OnSuccess.AddListener(OnSuccess);
        requester.OnError.AddListener(OnError);

        requester.StartRequestPermissions();
    }

    public void OnSuccess()
    {
        Log("OnSuccess()");
    }

    public void OnError()
    {
        Log("OnError()");
    }

    public void Log(string msg)
    {
        Debug.Log("OnSuccess()");
        textMessage.text = msg;
    }

}
