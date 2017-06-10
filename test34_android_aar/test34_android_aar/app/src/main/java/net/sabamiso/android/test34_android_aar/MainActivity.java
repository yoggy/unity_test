package net.sabamiso.android.test34_android_aar;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

import net.sabamiso.android.test34_android_aar_module.Accelerometer;
import net.sabamiso.android.test34_android_aar_module.AccelerometerEventListener;

public class MainActivity extends AppCompatActivity implements AccelerometerEventListener {

    TextView textViewAxisX;
    TextView textViewAxisY;
    TextView textViewAxisZ;

    Accelerometer accelerometer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        accelerometer = new Accelerometer(this);
        accelerometer.setAccelerometerEventListener(this);

        textViewAxisX = (TextView)findViewById(R.id.textViewX);
        textViewAxisY = (TextView)findViewById(R.id.textViewY);
        textViewAxisZ = (TextView)findViewById(R.id.textViewZ);
    }

    @Override
    protected void onResume() {
        super.onResume();
        accelerometer.start();
    }

    @Override
    protected void onPause() {
        accelerometer.stop();
        super.onPause();
    }

    @Override
    public void onAccelerometer(double x, double y, double z) {
        textViewAxisX.setText(String.format("%.2f", x));
        textViewAxisY.setText(String.format("%.2f", y));
        textViewAxisZ.setText(String.format("%.2f", z));
    }
}
