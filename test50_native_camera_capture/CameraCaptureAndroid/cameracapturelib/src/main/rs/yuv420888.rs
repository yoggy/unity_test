// see also...
//   https://stackoverflow.com/questions/36212904/yuv-420-888-interpretation-on-samsung-galaxy-s7-camera2
//   http://werner-dittmann.blogspot.com/2016/03/using-android-renderscript-to-convert.html
#pragma version(1)
#pragma rs java_package_name(net.sabamiso.android.cameracapturelib);
#pragma rs_fp_relaxed

int32_t width;
int32_t height;

uint picWidth, uvPixelStride, uvRowStride ;
rs_allocation ypsIn, uIn, vIn;

uchar4 __attribute__((kernel)) doConvert(uint32_t x, uint32_t y) {
    uint uvIndex=  uvPixelStride * (x/2) + uvRowStride*(y/2);

    uchar yps = rsGetElementAt_uchar(ypsIn, x, y);
    uchar u = rsGetElementAt_uchar(uIn, uvIndex);
    uchar v = rsGetElementAt_uchar(vIn, uvIndex);

    uchar4 out = rsYuvToRGBA_uchar4(yps, u, v);

    return out;
}