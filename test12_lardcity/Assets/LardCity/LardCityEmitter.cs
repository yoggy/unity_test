using UnityEngine;
using System.Collections;

public class LardCityEmitter : MonoBehaviour {

	public GameObject _prefab;

	[SerializeField]
	bool _enableEmit = false;
	public bool EnableEmit {
		get { return _enableEmit; }
		set { _enableEmit = value; }
	}

	[Range(0.01f, 1.0f)]
	public float _slideSpeed = 0.1f;

	[Range(0, 1.0f)]
	public float _emitRate = 0.5f;

	[Range(1,30)]
	public float _lifeTimeLimit = 10;

	public float _growSpeed = 0.1f;

	public float _minHeight = 7;
	public float _maxHeight = 20;

	public float _minWidth = 3;
	public float _maxWidth = 7;

	public float _minPosZ = 0;
	public float _maxPosZ = 100;
	
	public int _quantizeUnit = 12;

	void Update() {
		if (_enableEmit) {
			emitLardCityCube();
		}
	}
		
	void emitLardCityCube() {
		if (Random.Range (0.0f, 1.0f) > _emitRate)
			return;

		// 親と同じ位置にプレハブ生成
		Vector3 parent_pos = gameObject.transform.position;
		Quaternion parent_q = gameObject.transform.rotation;
		GameObject obj = (GameObject)Instantiate(_prefab, parent_pos, parent_q);
		
		// 親を設定。このときobjの座標は親オブジェクトから差し引きされる。つまり0に戻される
		obj.transform.parent = gameObject.transform;
		
		// 子独自の位置を設定
		int z = (int)(Random.Range(_minPosZ, _maxPosZ) / _quantizeUnit) * _quantizeUnit;
		obj.transform.Translate(0, 0, z);
		
		// パラメータ設定
		LardCityParticle p = obj.GetComponent<LardCityParticle>();
		p.lifeTimeLimit = _lifeTimeLimit;
		p.SizeW = Random.Range (_minWidth, _maxWidth);
		p.SizeH = Random.Range (_minHeight, _maxHeight);
		p.dx = -_slideSpeed;
		p.GrowSpeed = _growSpeed;
	}
}