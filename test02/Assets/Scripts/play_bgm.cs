using UnityEngine;
using System.Collections;

public class play_bgm : MonoBehaviour {
	
	SpriteRenderer spriteRenderer;
	AudioSource audioSource;

	public Sprite[] sprites = new Sprite[2];

	void Start ()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		audioSource = GameObject.Find("bgm").GetComponent<AudioSource>();
		Debug.Log ("Start ()");
	}
	
	void Update () 
	{
		
	}
	
	void OnMouseDown() {
		Debug.Log ("OnMouseDown ()");
		spriteRenderer.sprite = sprites[1];
		audioSource.Play ();
	}
	
	void OnMouseUp() {
		Debug.Log ("OnMouseUp ()");
		spriteRenderer.sprite = sprites[0];
	}
}
