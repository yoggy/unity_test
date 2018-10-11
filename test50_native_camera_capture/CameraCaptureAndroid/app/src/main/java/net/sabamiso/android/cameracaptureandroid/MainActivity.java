package net.sabamiso.android.cameracaptureandroid;

import android.Manifest;
import android.content.pm.PackageManager;
import android.opengl.GLSurfaceView;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import net.sabamiso.android.cameracapturelib.CameraCaptureLib;

public class MainActivity extends AppCompatActivity {
    public static final String TAG = MainActivity.class.getSimpleName();

    GLSurfaceView glSurfaceView;
    TestGLRenderer myRenderer;
    CameraCaptureLib cameraCaptureLib;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        glSurfaceView = new GLSurfaceView(this);
        glSurfaceView.setEGLContextClientVersion(2);

        cameraCaptureLib = new CameraCaptureLib();
        cameraCaptureLib.setParams(this, 1280, 720);

        myRenderer = new TestGLRenderer(this, glSurfaceView, cameraCaptureLib);
        glSurfaceView.setRenderer(myRenderer);
        glSurfaceView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
        setContentView(glSurfaceView);
    }

    @Override
    protected void onResume() {
        super.onResume();

        if (isIgnorePermission() == true) {
            Toast.makeText(this, "カメラ利用のパーミッションを許可してください", Toast.LENGTH_LONG).show();
        }
        else if (hasPermission() == false) {
            requestPermission();
        }
        else {
            myRenderer.startCamera();
        }
    }

    @Override
    protected void onPause() {
        super.onPause();
        myRenderer.stopCamera();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    boolean hasPermission() {
        int permission = ContextCompat.checkSelfPermission(this, Manifest.permission.CAMERA);
        if (permission != PackageManager.PERMISSION_GRANTED) {
            return false;
        }
        return true;
    }

    boolean isIgnorePermission() {
        return ActivityCompat.shouldShowRequestPermissionRationale(this, Manifest.permission.CAMERA);
    }

    protected final int MY_PERMISSIONS_REQUEST_CAMERA = 1234;

    void requestPermission() {
        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.CAMERA},
                MY_PERMISSIONS_REQUEST_CAMERA);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           String permissions[], int[] grantResults) {
        Log.d(TAG, "onRequestPermissionsResult()");
        switch (requestCode) {
            case MY_PERMISSIONS_REQUEST_CAMERA: {
                if (grantResults.length > 0  && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    Log.d(TAG, "GRANTED Camera permission");
                    // パーミッションダイアログからActivityが返ってくるときのonResume()の呼び出しで
                    // startCamera()するので、ここではなにもしない。
                } else {
                    Log.d(TAG, "IGNORE Camera permission");
                }

                return;
            }
        }
    }
}
