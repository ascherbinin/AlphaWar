using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterManager : MonoBehaviour
{
    public static LetterManager instance = null;

	private List<char> _charsList = new List<char>();
	private List<GameObject> _letterList = new List<GameObject>();
    public Spawn spawn;
    public GameObject letter;

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

    public void SetChars(List<char> chars)
    {
        foreach (var item in chars)
        {
            _charsList.Add(item);
        }
    }

    public void GenerateRandomLetters(float width, float height)
    {
        foreach (var item in _charsList)
        {
          spawn.SpawnObject(letter);
		  letter.GetComponent<LetterObject>().SetValue(item.ToString(), LetterState.Active);
			_letterList.Add (letter);
        }
    }

	public void CompareLetters(GameObject obj)
	{
		var letObj = obj.GetComponent<LetterObject> ();
		Debug.Log (letObj.GetValue());
	}
}
