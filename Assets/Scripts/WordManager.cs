using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordManager : MonoBehaviour 
{
    public static WordManager instance = null;

	List<Letter> word = new List<Letter>();
	public GameObject letter;


    void Awake()
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

    // Update is called once per frame
    void Update () 
	{
	
	}

	public List<Letter> GenerateWord(string wordStr)
	{
        foreach (char c in wordStr)
        {
            word.Add(new Letter(c.ToString(), LetterState.Static));
        }
        CreateWord(word);
        return word;
    }

	void CreateWord(List<Letter> _word) 
	{
        int count = _word.Count / 2;
        Vector3 center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        float posY = center.y + 2.57F * 2.5F;
        float posX = center.x - (2.57F * count) + 1.285F;
        foreach (var item in _word)
        {
            Instantiate(letter, new Vector2(posX, posY), Quaternion.identity);
            letter.GetComponent<LetterObject>().SetValue(item.value, item.State);
            posX += 2.57F;
        }
    }
}
