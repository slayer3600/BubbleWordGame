using UnityEngine;
using UnityEngine.UI;

public class BubbleUIToggle : MonoBehaviour {

    public Color uncheckedColor;
    public Color checkedColor;

    private Image bubbleImage;
    private Animator anim;

    // Use this for initialization
    void Start () {
        bubbleImage = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }
	
	public void SetChecked()
    {
        anim.Play("BubbleScore");
        bubbleImage.color = checkedColor;
    }

    public void SetUnChecked()
    {
        bubbleImage.color = uncheckedColor;
    }
}
