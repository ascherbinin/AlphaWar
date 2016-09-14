using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterManager : MonoBehaviour
{
    public static LetterManager instance = null;

    private List<Letter> _letterList = new List<Letter>();
	private List<GameObject> _letterObject = new List<GameObject>();
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

    public void RunGenerate()
    {
        GenerateLettersForWord();
        GenerateRandomLetters();
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
            _letterList.Add(new Letter(item, LetterState.Static, new Vector2(posX, posY)));
            posX += 2.57F;
        }
    }

    public void GenerateRandomLetters()
    {
        Vector2 rndPosWithin;
        foreach (char item in Word)
        {
            rndPosWithin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = Spawn.gameObject.transform.TransformPoint(rndPosWithin * .5f);
            _letterList.Add(new Letter(item, LetterState.Active, rndPosWithin));
        }
    }

    public void FillLetters()
    {
        foreach (var item in _letterList)
        {
            var rotation = Quaternion.identity;
            if (item.State == LetterState.Active)
            {
                rotation = Quaternion.Euler(0, 0, Random.Range(-75, 75));
            }    
            Instantiate(Letter, item.Position, rotation);
            Letter.GetComponent<LetterObject>().Setup(item.Position, item.Value, item.State);
        }
    }

	public void CompareLetters(GameObject obj)
	{
		var letObj = obj.GetComponent<LetterObject> ();
	}
}
