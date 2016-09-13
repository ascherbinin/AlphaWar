using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LetterObject : MonoBehaviour 
{

	private static SpriteRenderer _renderer;
	private static Text _alphaText;

	private LetterState _state;
	private string _value;

	void Awake()
	{
		_renderer = gameObject.GetComponent<SpriteRenderer> ();
		_alphaText = gameObject.GetComponentInChildren<Text> ();
	}
	// Use this for initialization
	void Start () 
	{
		
	}

	public void SetValue(string val, LetterState state) 
	{
		_value = val;
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
			_renderer.color = Color.white;
		} 
		else 
		{
			_renderer.color = Color.red;
		}
		_alphaText.text = _value;
	}
}
