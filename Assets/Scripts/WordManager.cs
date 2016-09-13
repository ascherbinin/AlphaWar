using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordManager : MonoBehaviour 
{
	public string wordStr = "war";
	List<Letter> word = new List<Letter>();
	public GameObject letter;
	// Use this for initialization
	private Screen _screen;
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
		int count = _word.Count / 2;
		Vector3 center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );
		float posY = center.y + 2.75F * 2.5F;
		float posX = center.x - 2.75F * count;
		foreach (var item in _word) {
			Instantiate (letter, new Vector2 (posX, posY), Quaternion.identity);
			letter.GetComponent<LetterObject> ().SetValue (item.value, item.State);
			posX += 2.57F;
		}
	}

	void CreateWord() 
	{
		
	}
}
