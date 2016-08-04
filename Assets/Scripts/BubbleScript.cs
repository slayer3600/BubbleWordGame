using UnityEngine;
using System.Collections;


public class BubbleScript : MonoBehaviour {

    public AudioClip[] bubblePops;
    public AudioClip bubbleSpawn;
    public AudioClip bubbleSelect;
    public AudioClip bubbleDeselect;
    public Color isSelectedColor;
    public float timeToLive = 10f;
    public bool isSelected = false;
    public Color vowelColor;

    private GameManagerScript gm;
    private AudioSource source;
    private Rigidbody2D rb;
    private Renderer rend;
    private CircleCollider2D circleCollider;
    private TextMesh letter;
    private bool blinkingSlow = false;
    private bool blinkingFast = false;
    private bool popped = false;
    


    // Use this for initialization
    void Start () {

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        source = GetComponentInChildren<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<Renderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        letter = GetComponentInChildren<TextMesh>();

        char[] alphabet = "AABCDEEEFGHIIJKLMNOOPQRSTUVWXYZ".ToCharArray();
        int randomLetter = Random.Range(0, alphabet.Length);
        letter.text = alphabet[randomLetter].ToString();

        if ("AEIOU".Contains(letter.text))
        {
            letter.color = vowelColor;
        }
        
        timeToLive = Time.time + timeToLive;

        rb.drag = 0;

        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);
        int speed = Random.Range(50, 150);

        source.PlayOneShot(bubbleSpawn, .5f);
        rb.AddForce(new Vector2(x, y) * speed);
    }
	
	// Update is called once per frame
	void Update () {

        float timeRemaining = timeToLive - Time.time;

        if (timeRemaining <= 0  && !popped)
        {
            Pop();
        }
        else if (timeRemaining < 2 && !blinkingFast)
        {
            CancelInvoke();
            InvokeRepeating("Blink", 0f, 0.2f);
            blinkingFast = true;
        }
        else if (timeRemaining < 5 && !blinkingSlow)
        {
            InvokeRepeating("Blink", 0f, 1f);
            blinkingSlow = true;
        }
	
	}

    void OnTouchDown(Vector2 point)
    {

        //Vibration.Vibrate(25);
        BuildWord();

    }

    //void OnMouseDown()
    //{
    //    OnTouchDown(Vector2.zero);
    //}

    void BuildWord()
    {
        if (!isSelected)
        {
            
            source.PlayOneShot(bubbleSelect);
            rend.material.color = isSelectedColor;
            isSelected = true;
            gm.BuildWord(letter.text);
        }
        else
        {
            //long[] vibPattern = new long[2] {25,25};
            //Vibration.Vibrate(vibPattern, 2);
            source.PlayOneShot(bubbleDeselect);
            gm.UnbuildWord(letter.text);
            Reset();
        }
    }

    public void Reset()
    {
        isSelected = false;
        rend.material.color = Color.white;
    }

    public void Pop()
    {
        popped = true;
        circleCollider.enabled = false;
        rend.enabled = false;
        rb.gravityScale = 1f;
        source.PlayOneShot(bubblePops[Random.Range(0, bubblePops.Length-1)]);
        Destroy(rb.gameObject, 2f);

        if (isSelected)
        {
            gm.UnbuildWord(letter.text);
        }
    }

    public void ToggleLetterVisibility(bool visible)
    {

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (Renderer myRenderer in renderers)
        {
            myRenderer.enabled = visible;
        }

    }

    void Blink()
    {

        if (isSelected)
        {
            if (rend.material.color == Color.red)
            {
                rend.material.color = isSelectedColor;
            }
            else
            {
                rend.material.color = Color.red;
            }
        }
        else
        {
            if (rend.material.color == Color.red)
            {
                rend.material.color = Color.white;
            }
            else
            {
                rend.material.color = Color.red;
            }
        }


    }
}
