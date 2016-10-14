using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum LetterState 
{
	Active,
	Static,
	Compared
}

public class LetterObject : MonoBehaviour, ILetter
{
    public  SpriteRenderer _renderer;
	public  GameObject _alphaText;
	public  TextMesh _text;
	public  GameObject GoParticle;

	public Vector2 Position { get; set; }
	public string Value { get; set;}
	public string ID { get; set;}
	public LetterState State { get; set; }

	void Awake()
	{
		_text = _alphaText.GetComponent<TextMesh> ();
	}
	// Use this for initialization
	public virtual void Start () 
	{
		StartCoroutine (ScaleOverTime (1));
		_alphaText.GetComponent<MeshRenderer>().sortingOrder = 1;
		_alphaText.GetComponent<MeshRenderer> ().sortingLayerID = GetComponent<SpriteRenderer>().sortingLayerID;
	}
			

	public void Setup(Vector2 pos, char value, string id)
	{
//		_renderer.color = state == LetterState.Static ? _staticColor : _acviteColor;
//		_text.color = state == LetterState.Static ? _staticColor : _acviteColor;
		gameObject.tag = "Letter";
		_text.text = value.ToString();
        gameObject.transform.position = pos;
		ID = id;
//		State = state;
		Value = value.ToString ();
	}



	public IEnumerator ScaleOverTime(float time)
	{
		Vector2 originalScale = gameObject.transform.localScale;

		Vector2 destinationScale = State == LetterState.Active ? new Vector2(0.5f, 0.5f) : new Vector2(0.7f, 0.7f);

		float currentTime = 0.0f;

		do
		{
			gameObject.transform.localScale = Vector2.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
	}

	public IEnumerator UnscaleOverTime(float time)
	{
		Vector2 originalScale = gameObject.transform.localScale;

		Vector2 destinationScale = new Vector2(0f, 0f);

		float currentTime = 0.0f;

		do
		{
			gameObject.transform.localScale = Vector2.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
		Destroy (gameObject);
	}
}
