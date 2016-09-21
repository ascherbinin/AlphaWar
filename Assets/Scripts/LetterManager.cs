﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterManager : MonoBehaviour
{
    public static LetterManager instance = null;

	private List<Letter> _wordLetters = new List<Letter>();
	private List<Letter> _randomLetters = new List<Letter>();
	private List<Letter> _resList = new List<Letter>();
	private List<GameObject> _lettersObject = new List<GameObject>();
    public GameObject Spawn;
    public GameObject Letter;

    public string Word { get; set; }

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void RunGenerate(string pWord)
    {
		Word = pWord;
        GenerateLettersForWord();
        GenerateFlyLetters();
		GenerateRandomLetters ();
        FillLetters();
    }

    public void GenerateLettersForWord()
    {
        int count = Word.Length / 2;
        Vector3 center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        float posY = center.y + 2.57F * 2.5F;
        float posX = center.x - (2.57F * count) + 1.285F;
        foreach (char item in Word)
        {
			_wordLetters.Add(new Letter(item, LetterState.Static, new Vector2(posX, posY)));
            posX += 2.57F;
        }
    }

    public void GenerateFlyLetters()
    {
		foreach (Letter item in _wordLetters)
        {
			var temp = new Letter(item.Value, LetterState.Active, GetRandomPositionFromSpawnObject(), item.GetID());
			while (DoLetterIntersect (temp)) 
			{
				temp = new Letter(item.Value, LetterState.Active, GetRandomPositionFromSpawnObject(), item.GetID());
			}
			_randomLetters.Add (temp);
        }
    }

	public void GenerateRandomLetters()
	{
		for (int i = 0; i < 10; i++) 
		{
			char ch = GetRandomLetter ();
			while (_randomLetters.Exists (item => item.Value == ch)) 
			{
				ch = GetRandomLetter ();
			}
			var temp = new Letter(ch, LetterState.Active, GetRandomPositionFromSpawnObject());
			while (DoLetterIntersect (temp)) 
			{
				temp = new Letter (ch, LetterState.Active, GetRandomPositionFromSpawnObject ());
			}
			_randomLetters.Add (temp);
		}
	}

    public void FillLetters()
    {
		_resList.AddRange(_wordLetters);
		_resList.AddRange (_randomLetters);

		foreach (var item in _resList)
        {
            var rotation = Quaternion.identity;
            if (item.State == LetterState.Active)
            {
                rotation = Quaternion.Euler(0, 0, Random.Range(-25, 25));
            }    
			GameObject obj = (GameObject)Instantiate(Letter, item.Position, rotation);
			obj.GetComponent<LetterObject>().Setup(item.Position, item.Value, item.State, item.GetID());
			_lettersObject.Add (obj);
        }
    }

	public void CompareLetters(GameObject obj)
	{
		var textObj = obj.GetComponent<LetterObject>();
		var item = GetFirstStaticLetter();
		if (item.GetComponent<LetterObject> ().ID == textObj.ID || item.GetComponent<LetterObject>().Value == textObj.Value) 
		{
			textObj.Move (item, 1);
		}
	}
		
	public void ReloadLevel(string newWord)
	{
		DeleteAll ();
		_resList.Clear ();
		_wordLetters.Clear ();
		_randomLetters.Clear ();
		_lettersObject.Clear ();
		RunGenerate (newWord);
	}

	public void DeleteAll()
	{
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Letter"))
		{
			o.GetComponent<LetterObject> ().StartCoroutine (o.GetComponent<LetterObject> ().UnscaleOverTime (1f));
		}
	}

	public void RemoveLetter(GameObject letter, bool isDestroy)
	{
		if (!isDestroy) 
		{
			_lettersObject.Remove (letter);
		} 
		else 
		{
			GameManager.instance.AddComaredLetter ();
			Destroy (letter);
		}
	}

    bool DoLetterIntersect(Letter a)
    {
		bool intersect = false;

		foreach (var item in _randomLetters) 
		{
			if ((Mathf.Abs(a.Position.x - item.Position.x) * 2 < (a.Width + item.Width)) &&
				(Mathf.Abs(a.Position.y - item.Position.y) * 2 < (a.Height + item.Height)))
			{
				intersect = true;
			}
		}
		return intersect;
    }

	GameObject GetFirstStaticLetter()
	{
		return _lettersObject.Find (state => state.GetComponent<LetterObject> ().State == LetterState.Static);
	}

	Vector2 GetRandomPositionFromSpawnObject()
	{
		var rndPosWithin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		rndPosWithin = Spawn.gameObject.transform.TransformPoint(rndPosWithin * .5f);
		return rndPosWithin;
	}

	static char GetRandomLetter()
	{
		// This method returns a random lowercase letter.
		// ... Between 'a' and 'z' inclusize.
		int num = Random.Range(0, 26); // Zero to 25
		char let = (char)('A' + num);
		return let;
	}
}
