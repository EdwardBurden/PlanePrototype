using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementInput : MonoBehaviour
{
    public struct RayCastInfo<T> where T : MonoBehaviour
    {
        public bool Focused;
        public T FocusObject;
        public RaycastHit HitInfo;
        public RayCastInfo(bool focus, RaycastHit info, T focusobject)
        {
            Focused = focus;
            HitInfo = info;
            FocusObject = focusobject;
        }
    }

    private Plane HoverPlane;
    private Plane ClickPlane;

    void Update()
    {
        CheckHoverObject();
        HandleClick();




    }

    private void HandleClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (ClickPlane)
            {
                RaycastHit hit = CheckIfObjectIsInFocus("PlaneLocation");
                if (hit.collider != null)
                {
                    ClickPlane.SetTarget(hit.point);

                }
                //ClickPlane.OffClick();
            }
            //ClickPlane = null;

        }
        if (Input.GetMouseButtonUp(0))
        {

            RayCastInfo<Plane> clickable = CheckIfObjectIsInFocus<Plane>();
            if (clickable.Focused && clickable.FocusObject != ClickPlane)
            {
                if (ClickPlane)
                {
                    ClickPlane.OffClick();
                    ClickPlane = null;
                }
                ClickPlane = clickable.FocusObject;
                ClickPlane.OnClick();
            }
            else
            {
                if (ClickPlane)
                {
                    ClickPlane.OffClick();
                    ClickPlane = null;
                }
            }

        }

    }

    private void CheckHoverObject()
    {
        RayCastInfo<Plane> clickable = CheckIfObjectIsInFocus<Plane>();
        if (clickable.Focused)
        {
            if (clickable.FocusObject != HoverPlane)
            {
                if (HoverPlane)
                    HoverPlane.OffHover();
                HoverPlane = clickable.FocusObject;
                HoverPlane.OnHover();
            }
        }
        else
        {
            if (HoverPlane)
                HoverPlane.OffHover();
            HoverPlane = null;
        }
    }

    public static RaycastHit CheckIfObjectIsInFocus(string tag)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                bool found = raycasts[i].collider.gameObject.CompareTag(tag);
                if (found)
                {
                    hit = raycasts[i];
                }

            }
        }
        return hit;
    }

    public static RayCastInfo<T> CheckIfObjectIsInFocus<T>() where T : MonoBehaviour
    {
        RaycastHit hit = new RaycastHit();
        bool inSight = false;
        T foundObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                T foundobject = raycasts[i].collider.gameObject.GetComponent<T>();
                if (foundobject)
                {
                    inSight = true;
                    hit = raycasts[i];
                    foundObject = foundobject;
                }
                else
                {
                    foundobject = raycasts[i].collider.gameObject.GetComponentInParent<T>();
                    if (foundobject)
                    {
                        inSight = true;
                        hit = raycasts[i];
                        foundObject = foundobject;
                    }
                    else
                        inSight = false;
                }

            }
        }
        return new RayCastInfo<T>(inSight, hit, foundObject);
    }
}
