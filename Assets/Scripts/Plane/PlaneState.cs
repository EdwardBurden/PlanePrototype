using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaneState
{
    public abstract void HandleUpdateBehaviour();

    public abstract void Activate(Plane plane);
    public abstract void DeActivate();
    public abstract void OnHover();
    public abstract void OffHover();
    public abstract void OnClick();
    public abstract void OffClick();
}
