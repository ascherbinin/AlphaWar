using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    public void SpawnObject(GameObject obj)
    {
        Vector3 rndPosWithin;
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(-75, 75));
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
		Instantiate(obj, rndPosWithin, randomRotation);
    }
}
