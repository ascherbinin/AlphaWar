using UnityEngine;
using System.Collections;

public interface ILetter 
{
	Vector2 Position { get; set; }
	string Value { get; set;}
	string ID { get; set;}

	void Setup (Vector2 pos, char value, string id);
	IEnumerator ScaleOverTime (float time);
	IEnumerator UnscaleOverTime (float time);
}
