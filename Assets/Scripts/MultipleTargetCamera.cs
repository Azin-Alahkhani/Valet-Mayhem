using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public float minZoomDist;
    public float maxPossibleDistance;
    public float smoothing;
    public float maxY;
    public float minY;

    public List<Transform> targets;

    Vector3 velocity;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Zoom()
    {
        float greatestDistance = GetGreatestDistance();

        if (greatestDistance < minZoomDist)
        {
            greatestDistance = 0f;
        }

        float newY = Mathf.Lerp(minY, maxY, greatestDistance / maxPossibleDistance);

        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, newY, Time.deltaTime), transform.position.z);
    }

    float GetGreatestDistance()
    {
        Bounds bounds = EncapsulateTarget();

        return bounds.size.x > bounds.size.z ? bounds.size.x : bounds.size.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centrePoint = GetCentrePoint();
        centrePoint.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, centrePoint, ref velocity, smoothing);

    }


    Vector3 GetCentrePoint()
    {

        Bounds bounds = EncapsulateTarget();

        Vector3 centre = bounds.center;
        return centre;
    }

    Bounds EncapsulateTarget()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

        foreach (var target in targets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds;
    }

}
