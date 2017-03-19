package net.sabamiso.android.androidplugintest;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.Handler;
import android.os.IBinder;
import android.util.Log;

public class TestService extends Service {
    static final String TAG = "TestService";

    Handler h = new Handler();
    protected TestServiceListener listener;

    public TestService() {
    }

    ///////////////////////////////////////////////////////////////////////
    //
    // binder
    //
    private final IBinder mBinder = new TestServiceBinder();

    public class TestServiceBinder extends Binder {
        TestService getService() {
            return TestService.this;
        }
    }

    @Override
    public IBinder onBind(Intent intent) {
        return mBinder;
    }

    ///////////////////////////////////////////////////////////////////////

    @Override
    public void onCreate() {
        Log.i(TAG, "onCreate");
        h.post(timer_task);
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.i(TAG, "onStartCommand() : flags=" + flags + ", startId=" + startId + ", intent=" + intent);
        return START_NOT_STICKY;
    }

    @Override
    public void onDestroy() {
        Log.i(TAG, "onDestroy");
        h.removeCallbacks(timer_task);
    }

    public synchronized TestServiceListener getListener() {
        return listener;
    }

    public synchronized void setListener(TestServiceListener listener) {
        this.listener = listener;
    }

    Runnable timer_task = new Runnable() {
        @Override
        public void run() {
            onTimer();
            h.postDelayed(timer_task, 1000);
        }
    };

    protected void onTimer() {
        long epoch =  System.currentTimeMillis() / 1000;
        String msg = "onTimer() : " + epoch;

        if (listener != null) {
            listener.onMessage(msg);
        }
    }

}
