using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    Play,
    Pause,
    Win,
    Lose
}

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
    public string wordStr = "war";

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
        LetterManager.instance.Word = wordStr;
        LetterManager.instance.RunGenerate();
	}
}
