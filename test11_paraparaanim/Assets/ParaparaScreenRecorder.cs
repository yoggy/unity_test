﻿//
//	see also... https://keijiro.github.io/posts/unity_screen_recording
//
using UnityEngine;
using System.Collections;

public class ParaparaScreenRecorder : MonoBehaviour
{
	public int framerate = 30;
	public int superSize = 1;
	public bool autoStartRecording = false;
	public bool autoStopRecording = false;
	public int autoStopFrame = 0;

	int _frameCount;
	bool _recording;
	
	void Start ()
	{
		if (autoStartRecording) StartRecording ();
	}
	
	void StartRecording ()
	{
		System.IO.Directory.CreateDirectory ("Capture");
		Time.captureFramerate = framerate;
		_frameCount = -1;
		_recording = true;
	}

	void StopRecording() {
		_recording = false;
		enabled = false;
	}

	void Update ()
	{
		if (_recording)
		{
			if (Input.GetMouseButtonDown (0))
			{
				StopRecording();
			}
			else
			{
				if (_frameCount > 0)
				{
					var name = "Capture/frame" + _frameCount.ToString ("0000") + ".png";
					Application.CaptureScreenshot (name, superSize);
				}
				
				_frameCount++;

				if (autoStopRecording == true && autoStopFrame == _frameCount) {
					StopRecording();
				}

				if (_frameCount > 0 && _frameCount % 60 == 0)
				{
					Debug.Log ((_frameCount / 60).ToString() + " seconds elapsed.");
				}
			}
		}
	}
	
	void OnGUI ()
	{
		if (!_recording && GUI.Button (new Rect (0, 0, 200, 50), "Start Recording"))
		{
			StartRecording ();
			Debug.Log ("Click Game View to stop recording.");
		}
	}
}