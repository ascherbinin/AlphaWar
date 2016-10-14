using UnityEngine;
using System.Collections;

public interface IMoveable
{
	void OnMouseTouch();
	void CreateParticle ();
	IEnumerator MoveToTarget (GameObject letterTarget, float timeToMove);
	void Move (GameObject letter, float timeToMove);
}
