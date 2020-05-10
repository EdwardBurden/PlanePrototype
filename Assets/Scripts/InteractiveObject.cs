using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Selection;
    private bool Clicked;

    public virtual void Awake()
    {
        Selection.Stop();

    }

    public virtual void OnHover()
    {
        if (!Clicked)
        {
            Debug.Log("OnHover");
        }
    }
    public virtual void OffHover()
    {
        if (!Clicked)
        {
            Debug.Log("OffHover");
        }
    }

    public virtual void OnClick() { Clicked = true; Debug.Log("OnClick"); Selection.Play(); }
    public virtual void OffClick() { Clicked = false; Debug.Log("OffClick");Selection.Stop(); }
}
