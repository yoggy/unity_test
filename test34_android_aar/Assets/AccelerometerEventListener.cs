using UnityEngine;
using UnityEngine.EventSystems;

//
// AccelerometerEventListener.cs
//
// AARに含まれているJavaインタフェースをAndroidJavaProxyを使って実装したクラス
// Java側のインタフェースは次の通り
//
//      package net.sabamiso.android.test34_android_aar_module;
//
//      public interface AccelerometerEventListener
//      {
//          public void onAccelerometer(double x, double y, double z);
//      }
//

public interface IAccelerometerEvent : IEventSystemHandler
{
    void OnAccelerometer(double x, double y, double z);
}

public class AccelerometerEventListener : AndroidJavaProxy
{
    GameObject game_object;

    public AccelerometerEventListener(GameObject game_object) : base("net.sabamiso.android.test34_android_aar_module.AccelerometerEventListener")
    {
        this.game_object = game_object;
    }

    public void onAccelerometer(double x, double y, double z)
    {
        if (game_object != null)
        {
            ExecuteEvents.Execute<IAccelerometerEvent>(
                target: game_object,
                eventData: null,
                functor: (target, eventData) => target.OnAccelerometer(x, y, z)
                );
        }
    }
}
