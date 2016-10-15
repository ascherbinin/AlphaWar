using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterManager : MonoBehaviour
{
    public static LetterManager instance = null;

	private List<Letter> _lettersList = new List<Letter>();
	private List<GameObject> _lettersObject = new List<GameObject>();
    public GameObject Spawn;
    public GameObject MoveLetter;
	public GameObject StaticLetter;
    public GameObject HunterObject;

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
		StartCoroutine(FillLetters());
    }

    public void GenerateLettersForWord()
    {
        int count = Word.Length / 2;
        Vector3 center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        float posY = center.y + 2.57F * 3F;
        float posX = center.x - (1.79F * count);
        foreach (char item in Word)
        {
            _lettersList.Add(new Letter(item, LetterState.Static, new Vector2(posX, posY)));
            posX += 1.74F;
        }
    }

    public void GenerateFlyLetters()
    {
        var tempList = new List<Letter>(_lettersList);
        foreach (Letter item in tempList)
        {
			var temp = new Letter(item.Value, LetterState.Active, GetRandomPositionFromSpawnObject(), item.GetID());
			while (DoLetterIntersect (temp)) 
			{
				temp = new Letter(item.Value, LetterState.Active, GetRandomPositionFromSpawnObject(), item.GetID());
			}
            _lettersList.Add (temp);
        }
    }

	public void GenerateRandomLetters()
	{
		for (int i = 0; i < 10; i++) 
		{
			char ch = GetRandomLetter ();
			while (_lettersList.Exists (item => item.Value == ch)) 
			{
				ch = GetRandomLetter ();
			}
			var temp = new Letter(ch, LetterState.Active, GetRandomPositionFromSpawnObject());
			while (DoLetterIntersect (temp)) 
			{
				temp = new Letter (ch, LetterState.Active, GetRandomPositionFromSpawnObject ());
			}
            _lettersList.Add (temp);
		}
	}

	public IEnumerator FillLetters()
    {
		foreach (var item in _lettersList)
        {
            var rotation = Quaternion.identity;
			GameObject letter;
			if (item.State == LetterState.Active) 
			{
				rotation = Quaternion.Euler (0, 0, Random.Range (-25, 25));
				letter = MoveLetter;
			} 
			else 
			{
				letter = StaticLetter;
			}
			GameObject obj = (GameObject)Instantiate(letter, item.Position, rotation);
			obj.GetComponent<LetterObject>().Setup(item.Position, item.Value, item.GetID());
			_lettersObject.Add (obj);
        }
        GameObject enemy = (GameObject)Instantiate(HunterObject, GetRandomPositionFromSpawnObject(), Quaternion.identity);
        enemy.GetComponent<HunterObject>().Setup(enemy.transform.position, GetRandomLetter());
        yield return new WaitForSeconds(2.5F);
		GameManager.instance.ChangeState (GameState.Play);
    }

	public void CompareLetters(GameObject obj)
	{
		var textObj = obj.GetComponent<MoveLetter>();
		var item = GetFirstStaticLetter();
		if (item.GetComponent<StaticLetter> ().ID == textObj.ID || item.GetComponent<StaticLetter> ().Value == textObj.Value)
		{
			textObj.Move (item, 1);
		} 
		else 
		{
			GameManager.instance.StartCoroutine (GameManager.instance.Shake());
		}
	}
		
	public void ReloadLevel(string newWord)
	{
		DeleteAll ();
        _lettersList.Clear();
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

		foreach (var item in _lettersList) 
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
		return _lettersObject.Find (state => state.GetComponent<StaticLetter> ().IsCompared == false);
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
