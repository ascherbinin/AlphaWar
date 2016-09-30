using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    Play,
    Pause,
    Win,
	StageComplete,
	Restarting,
    Lose
}

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public string[] Words;
	public GameObject LevelParticle;
	private string _currentWord;
	private int _intNeedCompareLetters;
	private int _curCompareLetter = 0;
	private GameState _state = GameState.Play;

	[SerializeField]
	private float magnitude = 2F;
	[SerializeField]
	private float duration = 0.3F;

    void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance == this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	void Start () 
	{
        InitGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void InitGame()
	{
		_currentWord = Words[Random.Range(0,Words.Length)];
		_intNeedCompareLetters = _currentWord.Length;
		LetterManager.instance.RunGenerate(_currentWord);
	}

	public void Restart(bool reload)
	{
		if (_state != GameState.Restarting) 
		{
			_state = GameState.Restarting;
			if (!reload) {
				_currentWord = Words [Random.Range (0, Words.Length)];
			}
			_intNeedCompareLetters = _currentWord.Length;
			_curCompareLetter = 0;
			LetterManager.instance.ReloadLevel (_currentWord);
		}
	}

	public void AddComaredLetter()
	{
		if (++_curCompareLetter >= _intNeedCompareLetters) 
		{
			var left = LevelParticle.transform.FindChild ("LeftParticle").gameObject;
			var right = LevelParticle.transform.FindChild ("RightParticle").gameObject;
			left.GetComponent<ParticleSystem> ().Emit (50);
			right.GetComponent<ParticleSystem> ().Emit (50);
			Restart (false);
		}
		Debug.Log (_curCompareLetter);
		Debug.Log (_intNeedCompareLetters);
	}

	public IEnumerator Shake() 
	{
		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}

	public void ChangeState(GameState state)
	{
		_state = state;
	}
}
