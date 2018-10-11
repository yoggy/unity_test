package net.sabamiso.android.cameracapturelib;

import org.opencv.core.Mat;

public interface OffscreenCameraCaptureListener {
    void onImageAvailable(Mat rgba, Mat mono);
}
