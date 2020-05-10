using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolloObjectUiComponent : MonoBehaviour
{
    public Transform FollowObject;
    public void Init(Transform follow)
    {
        FollowObject = follow;
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowObject)
        {
            Vector3 screenpos = Camera.main.WorldToScreenPoint(FollowObject.position);
            this.transform.position = new Vector3(screenpos.x, screenpos.y, 0);
        }
    }
}
