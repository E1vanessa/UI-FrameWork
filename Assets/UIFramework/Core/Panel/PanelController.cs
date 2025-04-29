using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Device;

public class PanelController : ScreenController, IPanelController
{
    [SerializeField]
    private PanelPriority priority;

    public PanelPriority Priority 
    { 
        get {return priority;}
    }
}
