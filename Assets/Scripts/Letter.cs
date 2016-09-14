using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; 



public class Letter
{

    private LetterState _state;
   
    public Vector2 Position { get; set; }
   
    public LetterState State
    {
        get { return _state; }
    }

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
    }

}
