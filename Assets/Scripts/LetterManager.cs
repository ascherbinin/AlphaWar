using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterManager : MonoBehaviour
{
    public static LetterManager instance = null;

    private List<Letter> _letterStaticList = new List<Letter>();
	private List<Letter> _randomLetters = new List<Letter>();
	private List<Letter> _resList = new List<Letter>();
//	private List<GameObject> _letterObject = new List<GameObject>();
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
			_letterStaticList.Add(new Letter(item, LetterState.Static, new Vector2(posX, posY)));
            posX += 2.57F;
        }
    }

    public void GenerateRandomLetters()
    {
        Vector2 rndPosWithin;
		foreach (Letter item in _letterStaticList)
        {
            rndPosWithin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = Spawn.gameObject.transform.TransformPoint(rndPosWithin * .5f);
			_randomLetters.Add(new Letter(item.Value, LetterState.Active, rndPosWithin,item.GetID()));
        }
    }

    public void FillLetters()
    {
		_resList.AddRange(_letterStaticList);
		_resList.AddRange (_randomLetters);

		foreach (var item in _resList)
        {
            var rotation = Quaternion.identity;
            if (item.State == LetterState.Active)
            {
//                rotation = Quaternion.Euler(0, 0, Random.Range(-25, 25));
            }    
			GameObject obj = (GameObject)Instantiate(Letter, item.Position, rotation);
			obj.GetComponent<LetterObject>().Setup(item.Position, item.Value, item.State, item.GetID());
        }
    }

	public void CompareLetters(GameObject obj)
	{
		var textObj = obj.GetComponent<LetterObject>();

		foreach (var letter in _resList) 
		{
			if (letter.Value.ToString() == textObj._alphaText.text &&
				letter.State == LetterState.Static &&
				letter.GetID() == textObj.ID) 
			{
				textObj.Move (letter.Position, 1);
				letter.State = LetterState.Active;
			}
		}
	}
		
}
