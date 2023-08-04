// https://github.com/yoggy/unity_test/blob/master/test94_permission_request_test/Assets/Scripts/PermissionRequester.cs

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class PermissionRequester : MonoBehaviour
{
    public enum PermissionType { CAMERA, LOCATION };
    public PermissionType[] permissions = new PermissionType[] { PermissionType.CAMERA, PermissionType.LOCATION };

    public UnityEvent OnSuccess;
    public UnityEvent OnError;

#if UNITY_ANDROID && !UNITY_EDITOR
    PermissionRequesterBase req = new PermissionRequesterAndroid();
#else
    PermissionRequesterBase req = new PermissionRequesterDummy();
#endif

    public void StartRequestPermissions()
    {
        StartCoroutine(StartRequestPermissionsImpl());
    }

    IEnumerator StartRequestPermissionsImpl()
    {
        if (permissions == null || permissions.Length == 0)
        {
            Debug.LogError("Please set permissions member variable!!");
            yield break;
        }

        yield return req.Requests(permissions);

        // Androidの場合、ダイアログを閉じてから少し待たないと、なぜかパーミッションの設定が反映されない？
        for (var i = 0; i < 5; i++)
        {
            yield return null;
        }

        foreach (var p in permissions)
        {
            if (req.HasPermission(p) == false)
            {
                Debug.LogError("Please allow camera permissions...");
                OnError.Invoke();
                yield break;
            }
        }

        OnSuccess.Invoke();
    }

    IEnumerator OnApplicationFocus(bool hasFocus)
    {
        req.OnApplicationFocus(hasFocus);
        yield return null;
    }
}

public abstract class PermissionRequesterBase
{
    protected bool _showPermissionDialog = false;

    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == true)
        {
            _showPermissionDialog = false;
        }
    }

    public abstract bool HasPermission(PermissionRequester.PermissionType type);
    public abstract IEnumerator Requests(PermissionRequester.PermissionType[] type);
}

#if UNITY_ANDROID && !UNITY_EDITOR
public class PermissionRequesterAndroid : PermissionRequesterBase
{
    Dictionary<PermissionRequester.PermissionType, string> dict = new Dictionary<PermissionRequester.PermissionType, string>();

    public PermissionRequesterAndroid()
    {
        dict[PermissionRequester.PermissionType.CAMERA] = Permission.Camera;
        dict[PermissionRequester.PermissionType.LOCATION] = Permission.FineLocation;
    }

    public string P(PermissionRequester.PermissionType type)
    {
        return dict[type];
    }

    public override bool HasPermission(PermissionRequester.PermissionType type)
    {
        return Permission.HasUserAuthorizedPermission(P(type));
    }

    public override IEnumerator Requests(PermissionRequester.PermissionType[] types)
    {
        yield return null;

        _showPermissionDialog = true;

        string[] permission_str = new string[types.Length];

        for (int i = 0; i < types.Length; ++i)
        {
            permission_str[i] = P(types[i]);
        }

        // Permission.RequestUserPermission()を連続して複数回callすると、2回目以降のダイアログが表示されない…？
        Permission.RequestUserPermissions(permission_str);
    }
}
#else
public class PermissionRequesterDummy : PermissionRequesterBase
{
    public override bool HasPermission(PermissionRequester.PermissionType type)
    {
        return true;
    }

    public override IEnumerator Requests(PermissionRequester.PermissionType[] type)
    {
        yield return null;
    }
}
#endif
