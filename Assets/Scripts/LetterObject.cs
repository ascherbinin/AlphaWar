using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum LetterState 
{
	Active,
	Static
}

public class LetterObject : MonoBehaviour 
{

	public  SpriteRenderer _renderer;
	public  Text _alphaText;

	private LetterState _state;

	private Color _startColor = Color.white;
	private Color _staticColor = Color.gray;
	private Color _blinkColor = Color.yellow;

	public static Vector2 Position { get; set; }
	public static string Value { get; set;}

	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (ScaleOverTime (1));
	}

	public void SetValue(string value, LetterState state) 
	{
		Value = value;
		_state = state;
		UpdateValues ();
	}
			
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateValues()
	{
		if (_state == LetterState.Static) 
		{
			gameObject.tag = "LetterStatic";
			_renderer.color = _staticColor;
		} 
		else 
		{
			gameObject.tag = "LetterActive";
			_renderer.color = _startColor;
		}
		_alphaText.text = Value;
	}

	IEnumerator ScaleOverTime(float time)
	{
		Vector2 originalScale = gameObject.transform.localScale;
		Vector2 destinationScale = new Vector2(1.0f, 1.0f);

		float currentTime = 0.0f;

		do
		{
			gameObject.transform.localScale = Vector2.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
	}

	void OnMouseOver() 
	{
		if (gameObject.tag == "LetterActive") 
		{
			_renderer.color = Color.Lerp(_startColor, _blinkColor, Mathf.PingPong(Time.time, 1));
		}
	}

	void OnMouseExit()
	{
		if (gameObject.tag == "LetterActive") 
		{
			_renderer.color = Color.Lerp(_renderer.color, _startColor, 1);
		}
	}

	void OnMouseDown()
	{
		LetterManager.instance.CompareLetters(gameObject);
	}


	public string GetValue ()
	{
		return Value;
	}
}
