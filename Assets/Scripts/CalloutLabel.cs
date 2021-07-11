using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalloutLabel : MonoBehaviour
{
    public LineRenderer line;
    public GameObject targetObj;
    public Animator anim;
    public Canvas labelCanvas;

    // Start is called before the first frame update
    void Start()
    {
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetInteger("NextAnim") == 1)
            ShowLabel();
        else
            HideLabel();
    }

    private void ShowLabel()
    {
        labelCanvas.enabled = true;
        line.enabled = true;
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, targetObj.transform.position);
    }

    private void HideLabel()
    {
        labelCanvas.enabled = false;
        line.enabled = false;
    }
}
