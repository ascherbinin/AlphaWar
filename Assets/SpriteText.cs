using UnityEngine;
using System.Collections;

public class SpriteText : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		var parent = transform.parent;

		var parentRenderer = parent.GetComponent<Renderer>();
		var renderer = GetComponent<Renderer>();
		renderer.sortingLayerID = parentRenderer.sortingLayerID;
		renderer.sortingOrder = parentRenderer.sortingOrder+1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText (char ch) 
	{
		var text = GetComponent<TextMesh>();
		text.text = ch.ToString();
	}
}
