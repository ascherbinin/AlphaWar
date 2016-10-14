using UnityEngine;
using System.Collections;

public class StaticLetter : LetterObject, IStatic 
{
		public Color _activeColor = Color.white;
	    public Color _staticColor = Color.gray;
	    public Color _blinkColor = Color.yellow;
		public Color _compareColor = Color.cyan;
		
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCompared(bool finish)
	{
		if (finish) {
			_renderer.color = _compareColor;
			_text.color = _compareColor;
		}
	}
}
