package net.sabamiso.android.test34_android_aar_module;

import android.content.Context;
import android.hardware.*;

import java.util.List;

public class Accelerometer implements SensorEventListener {
    Context context;
    SensorManager sensor_manager;
    AccelerometerEventListener listener;

    public Accelerometer(Context context) {
        this.context = context;
        sensor_manager = (SensorManager)context.getSystemService(Context.SENSOR_SERVICE);
    }

    public void setAccelerometerEventListener(AccelerometerEventListener listener) {
        this.listener = listener;
    }

    public void start() {
        List<Sensor> sensors = sensor_manager.getSensorList(Sensor.TYPE_ACCELEROMETER);
        if (sensors.size() > 0) {
            Sensor sensor = sensors.get(0);
            sensor_manager.registerListener(this, sensor, SensorManager.SENSOR_DELAY_GAME);
        }
    }

    public void stop() {
        sensor_manager.unregisterListener(this);
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        if(event.sensor.getType() == Sensor.TYPE_ACCELEROMETER && listener != null) {
            listener.onAccelerometer(event.values[0], event.values[1], event.values[2]);
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {
    }
}
