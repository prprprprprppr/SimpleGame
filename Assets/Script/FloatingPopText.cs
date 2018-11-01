using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPopText : MonoBehaviour {

    public Animator anim;
    private Text showText;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        showText = anim.GetComponent<Text>();
    }

    public void SetText(string text)
    {
        showText.text = text;
        showText.fontSize = Random.Range(18, 25);
    }
}
