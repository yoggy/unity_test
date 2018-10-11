package net.sabamiso.android.cameracapturelib;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.ImageFormat;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraDevice;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.CameraMetadata;
import android.hardware.camera2.CaptureRequest;
import android.hardware.camera2.TotalCaptureResult;
import android.media.Image;
import android.media.ImageReader;
import android.os.Handler;
import android.os.HandlerThread;
import android.renderscript.Allocation;
import android.renderscript.Element;
import android.renderscript.RenderScript;
import android.renderscript.Script;
import android.renderscript.Type;
import android.support.annotation.NonNull;
import android.util.Log;
import android.util.SparseIntArray;
import android.view.Surface;

import org.opencv.android.Utils;
import org.opencv.core.Mat;
import org.opencv.imgproc.Imgproc;

import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.List;

class OffscreenCameraCapture {
    public final String TAG = OffscreenCameraCapture.class.getSimpleName();

    CameraCharacteristics cameraCharacteristics;
    private CameraDevice cameraDevice;
    private CameraCaptureSession cameraCaptureSession;
    CaptureRequest.Builder builder;

    ImageReader imageReader;
    Bitmap captureResultBitmap;

    RenderScript renderScript;
    ScriptC_yuv420888 script;

    Activity activity;
    OffscreenCameraCaptureListener listener;

    Mat img_rgb = new Mat();
    Mat img_rgba = new Mat();
    Mat img_gray = new Mat();

    HandlerThread background_thread;

    public OffscreenCameraCapture(Activity activity, int width, int height) {
        this.activity = activity;

        renderScript = RenderScript.create(activity);
        script = new ScriptC_yuv420888(renderScript);

        imageReader = ImageReader.newInstance(width, height , ImageFormat.YUV_420_888, 2);
        imageReader.setOnImageAvailableListener(imageReaderOnImageAvailableListener, new Handler(activity.getMainLooper()));

        captureResultBitmap = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);
    }

    public void setListener(OffscreenCameraCaptureListener listener) {
        this.listener = listener;
    }

    @SuppressLint("MissingPermission")
    public void start(OffscreenCameraCaptureListener listener) {
        this.listener = listener;

        CameraManager manager = (CameraManager) activity.getSystemService(Context.CAMERA_SERVICE);

        try {
            String cameraId = getBackfaceCameraId();
            manager.openCamera(cameraId, stateCallback, null);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void stop() {
        if (cameraCaptureSession != null) {
            try {
                cameraCaptureSession.stopRepeating();
            } catch (CameraAccessException e) {
                e.printStackTrace();
            }
            cameraCaptureSession.close();
            cameraCaptureSession = null;
        }
        if(cameraDevice != null) {
            cameraDevice.close();
            cameraDevice = null;
        }
    }

    public void dispose() {
    }

    String getBackfaceCameraId() {
        CameraManager manager = (CameraManager)activity.getSystemService(Context.CAMERA_SERVICE);

        try {
            for (String cameraId : manager.getCameraIdList()) {
                cameraCharacteristics = manager.getCameraCharacteristics(cameraId);
                Integer facing = cameraCharacteristics.get(CameraCharacteristics.LENS_FACING);
                if (facing != null && facing == CameraCharacteristics.LENS_FACING_BACK) {
                    return cameraId;
                }
            }
        } catch (CameraAccessException e) {
            e.printStackTrace();
        }

        return null;
    }

    int getOrientation() {
        // see also...https://github.com/googlesamples/android-Camera2Basic/blob/d1a4f53338b76c7aaa2579adbc16ef5a553a5462/Application/src/main/java/com/example/android/camera2basic/Camera2BasicFragment.java#L857

        int rotation = activity.getWindowManager().getDefaultDisplay().getRotation();

        final SparseIntArray ORIENTATIONS = new SparseIntArray();
        ORIENTATIONS.append(Surface.ROTATION_0, 90);
        ORIENTATIONS.append(Surface.ROTATION_90, 0);
        ORIENTATIONS.append(Surface.ROTATION_180, 270);
        ORIENTATIONS.append(Surface.ROTATION_270, 180);

        int displayRotation = activity.getWindowManager().getDefaultDisplay().getRotation();
        int cameraOrientation = cameraCharacteristics.get(CameraCharacteristics.SENSOR_ORIENTATION);

        int angle = (ORIENTATIONS.get(rotation) + cameraOrientation + 270) % 360;
        return angle;
    }

    final CameraDevice.StateCallback stateCallback = new CameraDevice.StateCallback() {

        @Override
        public void onOpened(@NonNull CameraDevice cameraDevice) {
            Log.d(TAG, " CameraDevice.StateCallback.onOpend()");

            OffscreenCameraCapture.this.cameraDevice = cameraDevice;

            List<Surface> surfacesList = new ArrayList<Surface>();

            Surface surface_image_reader = imageReader.getSurface();
            surfacesList.add(surface_image_reader);

            try {
                builder = cameraDevice.createCaptureRequest(CameraDevice.TEMPLATE_RECORD);
                builder.addTarget(surface_image_reader);
                builder.set(CaptureRequest.JPEG_ORIENTATION, getOrientation());
                cameraDevice.createCaptureSession(surfacesList, captureSessionCallback, null);

                // addTarget(), createCaptureSession()に使用するsurfaceは複数指定可能
                // SurfaceView, TextureView, ImageReaderなどが使用可能
            } catch (CameraAccessException e) {
                e.printStackTrace();
            }
        }

        @Override
        public void onDisconnected(@NonNull CameraDevice cameraDevice) {
            Log.d(TAG, " CameraDevice.StateCallback.onDisconnected()");
            cameraDevice.close();
        }

        @Override
        public void onError(@NonNull CameraDevice cameraDevice, int i) {
            Log.d(TAG, " CameraDevice.StateCallback.onError()");
            cameraDevice.close();
        }
    };

    final CameraCaptureSession.StateCallback captureSessionCallback = new CameraCaptureSession.StateCallback() {
        @Override
        public void onConfigured(CameraCaptureSession session) {
            Log.d(TAG, "CameraCaptureSession.StateCallback.onConfigured()");

            OffscreenCameraCapture.this.cameraCaptureSession = session;

            try {
                builder.set(CaptureRequest.CONTROL_AF_MODE, CaptureRequest.CONTROL_AF_MODE_AUTO);
                builder.set(CaptureRequest.CONTROL_AF_TRIGGER, CameraMetadata.CONTROL_AF_TRIGGER_START); // オートフォーカスのトリガー

                session.setRepeatingRequest(builder.build(), captureCallback, null);

            } catch (CameraAccessException e) {
                Log.e(TAG, e.toString());
            }
        }

        @Override
        public void onConfigureFailed(CameraCaptureSession session) {
            Log.e(TAG, "CameraCaptureSession.StateCallback.onConfigureFailed()");
        }
    };

    final CameraCaptureSession.CaptureCallback captureCallback = new CameraCaptureSession.CaptureCallback() {
        @Override
        public void onCaptureCompleted(CameraCaptureSession session,
                                       CaptureRequest request,
                                       TotalCaptureResult result) {
        }
    };

    final ImageReader.OnImageAvailableListener imageReaderOnImageAvailableListener = new ImageReader.OnImageAvailableListener() {

        @Override
        public void onImageAvailable(ImageReader imageReader) {
            Image img = imageReader.acquireLatestImage();
            if (img == null) {
                return;
            }

            int w = img.getWidth();
            int h = img.getHeight();

            // see also...https://stackoverflow.com/questions/36212904/yuv-420-888-interpretation-on-samsung-galaxy-s7-camera2
            Image.Plane[] planes = img.getPlanes();
            ByteBuffer buffer = planes[0].getBuffer();
            byte[] y = new byte[buffer.remaining()];
            buffer.get(y);

            buffer = planes[1].getBuffer();
            byte[] u = new byte[buffer.remaining()];
            buffer.get(u);

            buffer = planes[2].getBuffer();
            byte[] v = new byte[buffer.remaining()];
            buffer.get(v);

            int yRowStride    = planes[0].getRowStride();
            int uvRowStride   = planes[1].getRowStride();
            int uvPixelStride = planes[1].getPixelStride();  // same for u and v.

            Type.Builder typeUcharY = new Type.Builder(renderScript, Element.U8(renderScript));
            typeUcharY.setX(yRowStride).setY(h);
            Allocation yAlloc = Allocation.createTyped(renderScript, typeUcharY.create());
            yAlloc.copyFrom(y);
            script.set_ypsIn(yAlloc);

            Type.Builder typeUcharUV = new Type.Builder(renderScript, Element.U8(renderScript));
            typeUcharUV.setX(u.length);
            Allocation uAlloc = Allocation.createTyped(renderScript, typeUcharUV.create());
            uAlloc.copyFrom(u);
            script.set_uIn(uAlloc);

            Allocation vAlloc = Allocation.createTyped(renderScript, typeUcharUV.create());
            vAlloc.copyFrom(v);
            script.set_vIn(vAlloc);

            // handover parameters
            script.set_picWidth(w);
            script.set_uvRowStride (uvRowStride);
            script.set_uvPixelStride (uvPixelStride);

            Allocation outAlloc = Allocation.createFromBitmap(renderScript, captureResultBitmap,
                    Allocation.MipmapControl.MIPMAP_NONE, Allocation.USAGE_SCRIPT);

            Script.LaunchOptions lo = new Script.LaunchOptions();
            lo.setX(0, w);
            lo.setY(0, h);

            script.forEach_doConvert(outAlloc,lo);
            outAlloc.copyTo(captureResultBitmap);
            img.close();

            Utils.bitmapToMat(captureResultBitmap, img_rgb);
            Imgproc.cvtColor(img_rgb, img_rgba, Imgproc.COLOR_RGB2RGBA);
            Imgproc.cvtColor(img_rgb, img_gray, Imgproc.COLOR_RGB2GRAY);

            if (listener != null) {
                listener.onImageAvailable(img_rgba, img_gray);
            }
        }
    };
}
