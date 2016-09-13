using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordManager : MonoBehaviour 
{
	public string wordStr = "war";
	List<Letter> word = new List<Letter>();
	public GameObject letter;
	// Use this for initialization
	void Start () 
	{
		foreach(char c in wordStr){
			word.Add(new Letter(c.ToString(), LetterState.Static));
		}
		GenerateWord (word);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void GenerateWord(List<Letter> _word)
	{
		float posX = -2.75F;
		foreach (var item in _word) {
			Instantiate (letter, new Vector2 (posX, 0), Quaternion.identity);
			letter.GetComponent<LetterObject> ().SetValue (item.value, item.State);
			posX += 2.57F;
		}
	}

	void CreateWord() 
	{
		
	}
}
