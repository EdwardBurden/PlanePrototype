using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;

    public float ZoomSpeed;
    public List<Vector3> ActivePositions;

    private Vector3 TargetPosition;

    private void Awake()
    {
        Instance = this;
        TargetPosition = this.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * ZoomSpeed);
    }


    public void MoveCamera()
    {
        Vector3[] points = new Vector3[] { Vector3.zero, Vector3.zero };
        ActivePositions = GameManger.Instance.Wave.ActiveAirports.Select(x => x.transform.position).ToList();
        //todo make faster
        Vector3[] testpoints = new Vector3[] { ActivePositions[0], ActivePositions[0] };
        foreach (var item in ActivePositions)
        {
            testpoints[0] = item;
            foreach (var nextitem in ActivePositions)
            {
                if (item != nextitem && Vector3.Distance(testpoints[0], nextitem) > Vector3.Distance(testpoints[0], testpoints[1]))
                {
                    testpoints[1] = nextitem;
                }
            }
            if (Vector3.Distance(testpoints[0], testpoints[1]) > Vector3.Distance(points[0], points[1]))
            {
                points = testpoints;
            }
        }
        float distanceMax = Vector3.Distance(points[0], points[1]) * 2;
        TargetPosition = new Vector3((points[0].x + points[1].x) / 2, distanceMax, (points[0].z + points[1].z) / 2);
    }
}
