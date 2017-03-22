package net.sabamiso.android.androidplugintest;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;

public class MainActivity extends AppCompatActivity implements TestServiceListener {
    final String TAG = getClass().getSimpleName();

    TestServiceController controller;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        controller = new TestServiceController();
        controller.setActivity(this);
        controller.setListener(this);
    }

    @Override
    protected void onResume() {
        super.onResume();
        controller.start();
    }

    @Override
    protected void onPause() {
        super.onPause();
        controller.stop();
    }

    @Override
    public void onMessage(String msg) {
        Log.d(TAG, "msg=" + msg);
    }
}
