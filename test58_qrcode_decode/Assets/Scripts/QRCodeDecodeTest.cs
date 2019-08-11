using UnityEngine;
using UnityEngine.UI;
using OpenCVForUnity.CoreModule; // using OpenCV for Unity-2.3.6.unitypackage
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;

public class QRCodeDecodeTest : MonoBehaviour
{
    public Message message;
    public RawImage webcam_rawimage;
    public RawImage qrcode_rawimage;

    int capture_w = 1280;
    int capture_h = 720;

    WebCamTexture webcam_texture;
    Texture2D result_texuture;
    Texture2D qrcode_texuture;

    Mat capture_mat;
    Mat display_mat;
    Mat qcode_mat;

    void Start()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            message.SetMessage("WebCamTexture.devices.Length == 0");
            return;
        }

        WebCamDevice dev = WebCamTexture.devices[0];
        webcam_texture = new WebCamTexture(dev.name, capture_w, capture_h, 60);
        Debug.Log("open dev.name=" + dev.name);

        webcam_texture.Play();
    }

    void Update()
    {
        // webcam_textureがアップデートされるまでは処理しない
        if (webcam_texture == null || webcam_texture.didUpdateThisFrame == false)
        {
            return;
        }

        CheckMat();
        Utils.webCamTextureToMat(webcam_texture, capture_mat);
        ProcessImage(capture_mat, display_mat);
        Utils.matToTexture2D(display_mat, result_texuture);
    }

    void CheckMat()
    {
        if (capture_mat == null)
        {
            int w = webcam_texture.width;
            int h = webcam_texture.height;
            capture_mat = new Mat(h, w, CvType.CV_8UC3);
            display_mat = new Mat(h, w, CvType.CV_8UC3);

            result_texuture = new Texture2D(w, h, TextureFormat.ARGB32, false);
            webcam_rawimage.texture = result_texuture;

            qcode_mat = new Mat(256, 256, CvType.CV_8UC3);

            qrcode_texuture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
            qrcode_rawimage.texture = qrcode_texuture;

            message.SetMessage("w=" + w + ", h=" + h);
        }
    }

    void ProcessImage(Mat src, Mat dst)
    {
        src.copyTo(dst);

        Mat gray = new Mat();
        Imgproc.cvtColor(src, gray, Imgproc.COLOR_RGBA2GRAY);

        Mat points = new Mat();
        Mat straight_qrcode = new Mat();

        QRCodeDetector detector = new QRCodeDetector();
        bool result = detector.detect(gray, points);

        if (result)
        {
            string decode_str = detector.decode(gray, points, straight_qrcode);

            float[] p = new float[8];
            points.get(0, 0, p);
            Imgproc.line(dst, new Point(p[0], p[1]), new Point(p[2], p[3]), new Scalar(255, 0, 255), 2);
            Imgproc.line(dst, new Point(p[2], p[3]), new Point(p[4], p[5]), new Scalar(255, 255, 0), 2);
            Imgproc.line(dst, new Point(p[4], p[5]), new Point(p[6], p[7]), new Scalar(0, 255, 0), 2);
            Imgproc.line(dst, new Point(p[6], p[7]), new Point(p[0], p[1]), new Scalar(255, 0, 0), 2);

            Mat src_rect = new Mat(4, 1, CvType.CV_32FC2);
            Mat dst_rect = new Mat(4, 1, CvType.CV_32FC2);

            src_rect.put(0, 0, p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7]);
            dst_rect.put(0, 0, 0, 0, 255, 0, 255, 255, 0, 255);

            Mat t = Imgproc.getPerspectiveTransform(src_rect, dst_rect);
            Imgproc.warpPerspective(src, qcode_mat, t, qcode_mat.size());
            Utils.matToTexture2D(qcode_mat, qrcode_texuture);

            message.SetMessage("decode_str: " + decode_str);
        }
        else
        {
            qcode_mat.setTo(new Scalar(0, 0, 0));
            Utils.matToTexture2D(qcode_mat, qrcode_texuture);
        }

    }
}
