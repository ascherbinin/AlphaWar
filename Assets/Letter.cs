using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; 

public enum LetterState 
{
	Active,
	Static
}

public class Letter : IComparable<Letter> 
{
	public string value;
	private LetterState _state;

	public LetterState State
	{
		get {return _state;}
	}
		
	public Letter(string alphaValue, LetterState state)
	{
		value = alphaValue;
		_state = state;
	}

	//This method is required by the IComparable
	//interface. 
	public int CompareTo(Letter other)
	{
		if (other.value == this.value) 
		{
			return 1;
		} 
		else 
		{
			return 0;
		}

	}
}
