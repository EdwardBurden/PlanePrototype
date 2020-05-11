using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : PlaneState
{
    private Plane plane;
    private bool Clicked;

    public override void Activate(Plane plane)
    {
        this.plane = plane;
    }

    public override void DeActivate()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleUpdateBehaviour()
    {
        if (!plane.Dying)
        {
            plane.Fuel -= ((plane.Speed * plane.Speed) * plane.Dampener * Time.deltaTime);
            if (plane.Fuel > 0)
            {
                plane.Rigidbody.velocity = plane.Target * plane.Speed;
                plane.Rigidbody.rotation = (Quaternion.LookRotation(plane.Target.normalized));
            }
            else
            {
                plane.Die();
            }
        }
    }

    public override void OffClick()
    {
        Clicked = false;
        plane.Selection.Stop();
    }

    public override void OffHover()
    {
        if (!Clicked)
        {
            plane.Selection.Stop();
        }
    }

    public override void OnClick()
    {
        Clicked = true; ParticleSystem.MainModule main = plane.Selection.main;
        main.startColor = new ParticleSystem.MinMaxGradient(Color.green); plane.Selection.Play();
    }

    public override void OnHover()
    {
        if (!Clicked)
        {
            ParticleSystem.MainModule main = plane.Selection.main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color.blue);
            plane.Selection.Play();
        }
    }
}
