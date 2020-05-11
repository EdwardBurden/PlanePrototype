using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffState : PlaneState
{
    private Plane Plane;
    public override void Activate(Plane plane)
    {
        Plane = plane;
        plane.StartCoroutine(WaitAndFly());
    }

    private IEnumerator WaitAndFly()
    {
        yield return new WaitForSeconds(3);
        Plane.ChangeState(typeof(FlyingState));

    }
    public override void DeActivate()
    {
      //  throw new System.NotImplementedException();
    }

    public override void HandleUpdateBehaviour()
    {
        //throw new System.NotImplementedException();
    }

    public override void OffClick()
    {
        throw new System.NotImplementedException();
    }

    public override void OffHover()
    {
        throw new System.NotImplementedException();
    }

    public override void OnClick()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHover()
    {
        throw new System.NotImplementedException();
    }
}
