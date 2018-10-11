package net.sabamiso.android.cameracapturelib;

import android.app.Activity;
import android.graphics.Camera;
import android.opengl.GLES20;
import android.util.Log;

import org.opencv.core.Mat;

import java.nio.ByteBuffer;

public class CameraCaptureLib implements OffscreenCameraCaptureListener {
    public static final String TAG = CameraCaptureLib.class.getSimpleName();

    static {
        System.loadLibrary("opencv_java3");
    }

    Activity activity;
    int target_texture_id = 0;

    OffscreenCameraCapture camera;
    Mat img_rgba;

    int tex_w;
    int tex_h;

    public void setParams(Activity activity, int texture_width, int texture_height) {
        Log.d(TAG, "setParams() : ");

        this.activity = activity;
        this.tex_w = texture_width;
        this.tex_h = texture_height;

        camera = new OffscreenCameraCapture(activity, tex_w, tex_h);
    }

    public int createTextureId() {
        int[] textures = new int[1];
        GLES20.glGenTextures(1, textures, 0);
        target_texture_id = textures[0];

        Log.d(TAG, "createTextureId() : texture_id=" + target_texture_id);

        // texture2dオブジェクトをGL_TEXTURE0にバインドする
        GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
        GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, target_texture_id);

        // テクスチャの補間方法の指定 (ここを実行し忘れると、テクスチャが見えなくなるので要注意…)
        GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_LINEAR);
        GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MAG_FILTER, GLES20.GL_LINEAR);

        // texture2d領域の確保
        GLES20.glTexImage2D(GLES20.GL_TEXTURE_2D, 0, GLES20.GL_RGBA,
                this.tex_w,
                this.tex_h,
                0, GLES20.GL_RGBA, GLES20.GL_UNSIGNED_BYTE, null);

        return target_texture_id;
    }

    public int getTextureId() {
        return target_texture_id;
    }

    public void dispose() {
        GLES20.glDeleteTextures(1, new int[]{target_texture_id}, 0);

        this.activity = null;
        this.target_texture_id = 0;
        this.tex_w = 0;
        this.tex_h = 0;
    }

    public void update() {
        Log.d(TAG, "update() : ");


        // コンテンツ用テクスチャのアップデート
        GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
        GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, target_texture_id);

        if (img_rgba != null && !img_rgba.empty()) {
            // 生RGBA配列を用意
            byte[] buf = new byte[(int) (img_rgba.total() * img_rgba.channels())];
            img_rgba.get(0, 0, buf);
            ByteBuffer bb = ByteBuffer.wrap(buf);
            bb.position(0);

            // RGBA配列をテクスチャへ転送
            GLES20.glTexSubImage2D(GLES20.GL_TEXTURE_2D, 0, 0, 0, tex_w, tex_h, GLES20.GL_RGBA, GLES20.GL_UNSIGNED_BYTE, bb);
        }

        // テクスチャをアンバインド
        GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, 0);
    }

    public void startCamera() {
        Log.d(TAG, "start() : ");
        camera.start(this);
    }

    public void stopCamera() {
        Log.d(TAG, "stop() : ");
        camera.stop();
    }

    @Override
    public void onImageAvailable(Mat rgba, Mat mono) {
        this.img_rgba = rgba;
    }
}

