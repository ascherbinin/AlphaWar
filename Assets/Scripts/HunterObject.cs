using UnityEngine;
using System.Collections;
using System;


public class HunterObject : MonoBehaviour, IEnemy
{
    public SpriteRenderer _renderer;
    public GameObject _alphaText;
    private TextMesh _text;

    public GameObject target;

    public Vector2 Position { get; set; }
    public string Value { get; set; }

    private float moveSpeed = 10f;

    public int hp
    {
        get;
        set;
    }

    public EnemyType type
    {
        get { return EnemyType.Hunter; }
    }

    public void Activate()
    {
        throw new NotImplementedException();
    }

    public void Deactivate()
    {
        throw new NotImplementedException();
    }

    public void Interact(GameObject go)
    {
        if (go == target)
        {
            Destroy(target);
            target = GetTarget();
        }
    }

    public void OnMouseTouch()
    {
        throw new NotImplementedException();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Interact(collision.gameObject);
    }


    IEnumerator ScaleOverTime(float time)
    {
        Vector2 originalScale = gameObject.transform.localScale;

        Vector2 destinationScale = new Vector2(0.5f, 0.5f);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.localScale = Vector2.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }

    void Awake()
    {
        _text = _alphaText.GetComponent<TextMesh>();
    }

    void Start()
    {
        StartCoroutine(ScaleOverTime(1));
        _alphaText.GetComponent<MeshRenderer>().sortingOrder = 1;
        _alphaText.GetComponent<MeshRenderer>().sortingLayerID = GetComponent<SpriteRenderer>().sortingLayerID;
        _renderer.color = Color.red;
		target = GetTarget();
    }

    public void Setup(Vector2 pos, char value)
    {
        gameObject.tag = "EnemyLetter";
        _text.text = value.ToString();
        gameObject.transform.position = pos;
        Value = value.ToString();
	
    }


    // Update is called once per frame
    void Update ()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);

        //move towards the player
        if (target != null)
        { 
            Vector3 vectorToTarget = target.transform.position - transform.position;
            transform.position += vectorToTarget.normalized * moveSpeed * Time.deltaTime;
        }
    }


    GameObject GetTarget()
    {
        var array = GameObject.FindGameObjectsWithTag("Letter");
        target = array[UnityEngine.Random.Range(0, array.Length)];
        target.GetComponent<SpriteRenderer>().color = Color.gray;
        return target;
    }
}
