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

	public Color _startColor = Color.white;
    public Color _staticColor = Color.gray;
    public Color _blinkColor = Color.yellow;

	public Vector2 Position { get; set; }
	public string Value { get; set;}
	public string ID { get; set;}

	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (ScaleOverTime (1));
	}
			
	// Update is called once per frame
	void Update () {
	
	}

	public void Setup(Vector2 pos, char value, LetterState state, string id)
	{
		_renderer.color = state == LetterState.Static ? _staticColor : _startColor;
		_alphaText.color = state == LetterState.Static ? _staticColor : _startColor;
		gameObject.tag = state == LetterState.Static ? "LetterStatic" : "LetterActive";
		_alphaText.text = value.ToString();
        gameObject.transform.position = pos;
		ID = id;
		Debug.Log ("Setup ID: " + ID);
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
		LetterManager.instance.CompareLetters (gameObject);
	}

	void OnMouseEnter()
	{
		Debug.Log ("ID: " + ID);
	}

	IEnumerator MoveToTarget(Vector2 pos, float timeToMove)
	{
		var currentPos = transform.position;
		var t = 0f;
		while(t < 1)
		{
			t += Time.deltaTime / timeToMove;
			transform.position = Vector2.Lerp(currentPos, pos, t);
			yield return null;
		}
	}

	public void Move(Vector2 pos, float timeToMove)
	{
		StartCoroutine(MoveToTarget(pos, timeToMove));
	}
}
