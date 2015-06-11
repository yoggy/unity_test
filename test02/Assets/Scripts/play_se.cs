using UnityEngine;
using System.Collections;

public class play_se : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	public Sprite[] sprites = new Sprite[2];

	private AudioSource audioSource;

	void Start ()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		audioSource = gameObject.GetComponent<AudioSource> ();

		Debug.Log ("Start ()");
	}

	void Update () 
	{

	}

	void OnMouseDown() {
		Debug.Log ("OnMouseDown ()");
		spriteRenderer.sprite = sprites[1];
		audioSource.PlayOneShot (audioSource.clip);
	}

	void OnMouseUp() {
		Debug.Log ("OnMouseUp ()");
		spriteRenderer.sprite = sprites[0];
	}
}
