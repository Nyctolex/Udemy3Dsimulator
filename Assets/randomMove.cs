using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMove : MonoBehaviour
{
    public Camera cam;
    public Vector3 randomMovmentLimits;
    public GameObject target;
    private bool isTargetVisible;
    private Vector3 movment;
    public Vector3 initialPos;
    private bool IsVisible(Camera c, GameObject targetObj)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = targetObj.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0) return false;
        }
        return true;
    }

    public void SetUpRandomPlace()
    {
        transform.position = initialPos;
        do
        {
        movment = new Vector3(Random.Range(-randomMovmentLimits.x, randomMovmentLimits.x), Random.Range(-randomMovmentLimits.y, randomMovmentLimits.y), Random.Range(-randomMovmentLimits.z, randomMovmentLimits.z));
            transform.position += movment;
            isTargetVisible = IsVisible(cam, target);
        } while (!isTargetVisible);
    }
    // Start is called before the first frame update
    void Start()
    {
        isTargetVisible = false;
        movment = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {  
    }
}
