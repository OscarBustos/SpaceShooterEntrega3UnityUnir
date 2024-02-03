using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    private Animator animator;
    private Canvas parentCanvasComponent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeIn");
        parentCanvasComponent = gameObject.GetComponentInParent<Canvas>();
        DisableParent();
    }
    public void DisableParent()
    {
        parentCanvasComponent.sortingOrder = -1;
    }

    public void FadeOut()
    {
        parentCanvasComponent.sortingOrder = 1;
        animator.SetTrigger("FadeOut");
    }
}
