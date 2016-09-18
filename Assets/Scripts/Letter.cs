using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; 



public class Letter
{

    private LetterState _state;
   
    public Vector2 Position { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public LetterState State
    {
        get { return _state; }
		set { _state = value; }
    }

	public string ID { get; set; }

    public char Value
    {
        get;
        set;
    }

	public Letter(char alphaValue, LetterState state, Vector2 pos)
    {
        Value = alphaValue;
        _state = state;
        Position = pos;
		ID = System.Guid.NewGuid().ToString();
        Height = 2.7f;
        Width = 2.7F;
    }

	public Letter(char alphaValue, LetterState state, Vector2 pos, string id)
	{
		Value = alphaValue;
		_state = state;
		Position = pos;
		ID = id;
        Height = 2.7f;
        Width = 2.7F;
    }

	public string GetID()
	{
		return ID;
	}
}
