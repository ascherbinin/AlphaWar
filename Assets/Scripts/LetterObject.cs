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

	public Color _acviteColor = Color.white;
    public Color _staticColor = Color.gray;
    public Color _blinkColor = Color.yellow;

	public Vector2 Position { get; set; }
	public string Value { get; set;}
	public string ID { get; set;}
	public LetterState State { get; set; }

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
		_renderer.color = state == LetterState.Static ? _staticColor : _acviteColor;
		_alphaText.color = state == LetterState.Static ? _staticColor : _acviteColor;
		gameObject.tag = state == LetterState.Static ? "LetterStatic" : "LetterActive";
		_alphaText.text = value.ToString();
        gameObject.transform.position = pos;
		ID = id;
		State = state;
	}

	public void SetActive()
	{
		_renderer.color = _acviteColor;
		_alphaText.color = _acviteColor;
		State = LetterState.Active;
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
			_renderer.color = Color.Lerp(_acviteColor, _blinkColor, Mathf.PingPong(Time.time, 1));
		}

	}

	void OnMouseExit()
	{
		if (gameObject.tag == "LetterActive") 
		{
			_renderer.color = Color.Lerp(_renderer.color, _acviteColor, 1);
		}
	}

	void OnMouseDown()
	{
		LetterManager.instance.CompareLetters (gameObject);
	}

	void OnMouseEnter()
	{

	}

	IEnumerator MoveToTarget(GameObject letterTarget, float timeToMove)
	{
		var currentPos = transform.position;
		Vector2 originalScale = gameObject.transform.localScale;
		var t = 0f;
		while(t < 1)
		{
			t += Time.deltaTime / timeToMove;

			var strength = .3F;
			var str = Mathf.Min (strength * t, 1);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.identity, str);
			transform.position = Vector2.Lerp(currentPos, letterTarget.transform.position, t);
			transform.localScale = Vector2.Lerp(originalScale, new Vector2(0f,0f), t / 3);
			yield return null;
		}
		LetterManager.instance.RemoveLetter (gameObject);
		letterTarget.GetComponent<LetterObject> ().SetActive ();
	}

	public void Move(GameObject letter, float timeToMove)
	{
		StartCoroutine(MoveToTarget(letter, timeToMove));
	}
}
