using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[/*RequireComponent(typeof(Outline)), */HideMonoScript]
public class Target : MonoBehaviour
{
    public float stopDistance = 1;

    [ShowInInspector, ReadOnly]
    public static Target Selected { get; private set; }

    //private Outline outline;

    private void Awake()
    {
        //outline = GetComponent<Outline>();
        //outline.enabled = false;
    }

    public void Select()
    {
        if (Selected)
            Selected.Unselect();
        Selected = this;
        //outline.enabled = true;
    }

    public void Unselect()
    {
        Selected = null;
        //outline.enabled = false;
    }
}
