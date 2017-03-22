package net.sabamiso.android.androidplugintest;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public class TestServiceController implements TestServiceListener {
    static final String TAG = "TestServiceController";

    Activity current_activity = null;
    TestService test_service = null;
    String target_obj = "";
    String target_method = "";

    protected TestServiceListener listener;

    public TestServiceController() {
    }

    Activity getActivity() {
        if (this.current_activity == null) {
            return UnityPlayer.currentActivity;
        }
        return this.current_activity;
    }

    void setActivity(Activity activity) {
        this.current_activity = activity;
    }

    public  synchronized void start() {
        Activity activity = getActivity();

        Intent bankmemory_intent = new Intent(activity, TestService.class);
        activity.bindService(bankmemory_intent, mServiceConnection, Activity.BIND_AUTO_CREATE);
    }

    public void stop() {
        Activity activity = getActivity();

        if (test_service != null) {
            activity.unbindService(mServiceConnection);
            test_service = null;
        }
    }

    public void setListener(TestServiceListener listener) {
        this.listener = listener;

        if (test_service != null) {
            test_service.setListener(listener);
        }
    }

    public void setListener(String target_obj, String target_method) {
        Log.d(TAG, "setListener() : target_obj=" + target_obj + ", target_method=" + target_method);
        this.target_obj = target_obj;
        this.target_method = target_method;

        this.listener = this;

        if (test_service != null) {
            test_service.setListener(this);
        }
    }

    @Override
    public void onMessage(String msg) {
        Log.d(TAG, "onMessage() : msg=" + msg);
        UnityPlayer.UnitySendMessage(target_obj, target_method, msg);
    }

    public void testMethod1() {
        Log.d(TAG, "call testMethod1 !");
    }

    public String testMethod2(int i, String str) {
        Log.d(TAG, "call testMethod2 !");
        String rv = str + i;
        return rv;
    }

    ServiceConnection  mServiceConnection = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName name, IBinder service) {
            Log.d(TAG, "mServiceConnection.onServiceConnected()");
            test_service = ((TestService.TestServiceBinder) service).getService();

            if (listener != null) {
                test_service.setListener(listener);
            }
        }

        @Override
        public void onServiceDisconnected(ComponentName name) {
            Log.d(TAG, "mServiceConnection.onServiceDisconnected()");
        }
    };
}
