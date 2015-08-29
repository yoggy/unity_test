using UnityEngine;
using System.Collections;

public class LardCityParticle : MonoBehaviour {

	public float dx = -0.1f;
	public float dy = 0.0f;
	public float dz = 0.0f;

	float _size_w = 5.0f;
	public float SizeW {
		get {return _size_w;}
		set {_size_w = value; UpdateSize();}
	}

	float _grow_speed = 0.1f;
	public float GrowSpeed {
		get {return _grow_speed;}
		set {_grow_speed = value;}
	}

	float _size_h = 10.0f;
	float _now_h = 0.0f;
	public float SizeH {
		get {return _size_h;}
		set {_size_h = value; _now_h = 0.0f; UpdateSize();}
	}

	public float lifeTimeLimit = 10.0f;

	float _lifeTime = 0.0f;

	void Start () {
	}

	void UpdateSize() {
		GameObject _cube;
		_cube = transform.FindChild ("Cube").gameObject;

		_cube.transform.localScale = new Vector3 (_size_w, _now_h, _size_w);
		_cube.transform.localPosition = new Vector3 (0, _now_h / 2.0f, 0);
	}

	void Update () {
		_lifeTime += Time.deltaTime;
		
		gameObject.transform.Translate (new Vector3 (dx, dy, dz));

		if (_now_h < _size_h) {
			_now_h += _grow_speed;
		}
		UpdateSize ();

		if (_lifeTime > lifeTimeLimit) {
			Destroy(gameObject, 0.5f);
		}
	}
}
