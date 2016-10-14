using UnityEngine;
using System.Collections;

public class MoveLetter : LetterObject, IMoveable
{
	// Use this for initialization
	public override void  Start () 
	{
		base.Start ();
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-5f, 5f), Random.Range (-5f, 5f));
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
		
	void OnMouseDown()
	{
		OnMouseTouch ();
	}

	public void OnMouseTouch()
	{
		LetterManager.instance.CompareLetters (gameObject);
	}

	public void CreateParticle()
	{
		GameObject obj = (GameObject)Instantiate (GoParticle, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		GoParticle.GetComponent<ParticleSystem> ().emissionRate = 70;
		GoParticle.GetComponent<ParticleSystem> ().Play();
		Destroy (obj, 3);
	}

	public IEnumerator MoveToTarget(GameObject letterTarget, float timeToMove)
	{
		CreateParticle ();
		LetterManager.instance.RemoveLetter (gameObject, false);
		letterTarget.GetComponent<StaticLetter> ().SetCompared (false);
		gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
		var currentPos = transform.position;
		Vector2 originalScale = gameObject.transform.localScale;
		var t = 0f;
		while(t < 1)
		{
			t += Time.deltaTime / timeToMove;

			var strength = .3F;
			var str = Mathf.Min (strength * t, 1);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.identity, str);
			transform.position = Vector2.Lerp(currentPos, letterTarget.transform.position, t);
			transform.localScale = Vector2.Lerp(originalScale, new Vector2(0f,0f), t / 3);
			yield return null;
		}
		LetterManager.instance.RemoveLetter (gameObject, true);
		letterTarget.GetComponent<StaticLetter> ().SetCompared (true);
	}

	public void Move(GameObject letter, float timeToMove)
	{
		StartCoroutine(MoveToTarget(letter, timeToMove));
	}
}
