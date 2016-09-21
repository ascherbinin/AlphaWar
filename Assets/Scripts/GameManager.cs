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
		string wordStr = Words[Random.Range(0,Words.Length)];
        LetterManager.instance.Word = wordStr;
        LetterManager.instance.RunGenerate(wordStr);
	}

	public void Restart()
	{
		string wordStr = Words[Random.Range(0,Words.Length)];
		LetterManager.instance.ReloadLevel (wordStr);
	}
}
