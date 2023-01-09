# test92_custom_sampler

```
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

[DefaultExecutionOrder(-99999)]
public class CustomSamplerTest : MonoBehaviour
{
    public Text textEnable;
    public Text textTimeLateUpdate;
    public Text textTimeEndOfFrame;
    public Text textFPS;

    CustomSampler _customSamplerLateUpdate = CustomSampler.Create("Update_to_LateUpdate");
    CustomSampler _customSamplerEndOfFrame = CustomSampler.Create("Update_to_WaitForEndOfFrame");

    int _samplingFrameCount = 60;
    List<double> _elapsedTimesLateUpdate = new List<double>();
    List<double> _elapsedTimesEndOfFrame = new List<double>();
    float _elapsedTime = 0.0f;

    public bool EnableHeavyFunction { get; set; }

    void Start()
    {
        textEnable.text = "";
        textTimeLateUpdate.text = "";
        textTimeEndOfFrame.text = "";
        textFPS.text = "";
    }

    void Update()
    {
        _customSamplerLateUpdate.Begin(); // Update→LastUpdate
        _customSamplerEndOfFrame.Begin(); // Update→EndOfFrame

        _elapsedTime += Time.deltaTime;

        if (EnableHeavyFunction) DummyHeavyFunction();

        StartCoroutine("WaitForEndOfFrame");
    }

    void LateUpdate()
    {
        _customSamplerLateUpdate.End();
        Recorder r = _customSamplerLateUpdate.GetRecorder();
        double elapsedTime = r.elapsedNanoseconds / 1000.0f / 1000.0f / 1000.0f;

        _elapsedTimesLateUpdate.Add(elapsedTime);
    }

    IEnumerator WaitForEndOfFrame()
    {
        // see also... https://docs.unity3d.com/ja/2021.3/Manual/ExecutionOrder.html
        yield return new WaitForEndOfFrame();
        _customSamplerEndOfFrame.End();

        UpdateStatus();
    }

    void UpdateStatus()
    {
        Recorder r = _customSamplerEndOfFrame.GetRecorder();

        double elapsedTime = r.elapsedNanoseconds / 1000.0f / 1000.0f / 1000.0f;
        _elapsedTimesEndOfFrame.Add(elapsedTime);
        if (_elapsedTimesEndOfFrame.Count >= _samplingFrameCount)
        {
            double averageElapsedLateUpdate = _elapsedTimesLateUpdate.Sum() / _samplingFrameCount;
            double averageElapsedTimeEndOfFrame = _elapsedTimesEndOfFrame.Sum() / _samplingFrameCount;

            textEnable.text = r.enabled ? "true" : "false";
            textTimeLateUpdate.text = $"{averageElapsedLateUpdate:F7}";
            textTimeEndOfFrame.text = $"{averageElapsedTimeEndOfFrame:F7}";
            Debug.Log(_elapsedTime);
            textFPS.text = $"{1.0f / (_elapsedTime / _samplingFrameCount):F2}";

            _elapsedTimesLateUpdate.Clear();
            _elapsedTimesEndOfFrame.Clear();
            _elapsedTime = 0.0f;
        }
    }

    void DummyHeavyFunction()
    {
        Thread.Sleep(20);
    }
}
```