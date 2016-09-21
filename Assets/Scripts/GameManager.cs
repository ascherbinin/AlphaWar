using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    Play,
    Pause,
    Win,
	StageComplete,
    Lose
}

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public string[] Words;
	private string _currentWord;
	private int _intNeedCompareLetters;
	private int _curCompareLetter = 0;

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
		if (!reload) 
		{
			_currentWord = Words[Random.Range(0,Words.Length)];
		}
		_intNeedCompareLetters = _currentWord.Length;
		_curCompareLetter = 0;
		LetterManager.instance.ReloadLevel (_currentWord);
	}

	public void AddComaredLetter()
	{
		if (++_curCompareLetter >= _intNeedCompareLetters) 
		{
			Restart (false);
		}
		Debug.Log (_curCompareLetter);
		Debug.Log (_intNeedCompareLetters);
	}
}
