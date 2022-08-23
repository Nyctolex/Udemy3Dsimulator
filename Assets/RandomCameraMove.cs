using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RandomCameraMove : MonoBehaviour
{
    public Camera cam;
    public int fileCounter;
    public GameObject target;
    private bool isTargetVisible;
    private Vector3 movement;
    private Vector3 rotation;
    public string CapturedirectoryName;
    public Vector3 randomMovmentLimits;
    public Vector3 randomRotationLimits;
    public Vector3 randomMovmentNoiseLimits;
    public Vector3 randomRotationNoiseLimits;
    private Vector3 finalMovment;
    private Vector3 finalRotation;
    Vector3 viewPos;
    int resWidth;
    int resHeight;
  
    private bool IsVisible(Camera c, GameObject targetObj)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = targetObj.transform.position;

        foreach(var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0) return false;
        }
        return true;
    }
    
    private Vector3 randomVector(Vector3 limits, bool positive)
    {
        if (!positive){
            return new Vector3(Random.Range(-limits.x, limits.x), Random.Range(-limits.y, limits.y), Random.Range(-limits.z, limits.z));
        }
        else
        {
            return new Vector3(Random.Range(0, limits.x), Random.Range(0, limits.y), Random.Range(0, limits.z));
        }
    }
    public void generateFinalPosition()
    {
        Vector3 initialPos = transform.position;
        Vector3 initialAngle = transform.eulerAngles;
        do
        {
            movement = randomVector(randomMovmentLimits, false);
            rotation = randomVector(randomRotationLimits, false);
            transform.position += movement;
            transform.eulerAngles += rotation;
            isTargetVisible = IsVisible(cam, target);
        } while (!isTargetVisible);
        finalMovment = movement;
        finalRotation = rotation;
        transform.position= initialPos;
        transform.eulerAngles = initialAngle;
    }

    public void moveCamera(int maxItirations, Vector3 maxCartesianAcceleration, Vector3 maxAngularAcceleration)
    {
        Debug.Log("Move Cam");
        Vector3 cartesianAccelerationFactor = randomVector(maxCartesianAcceleration, true);
        Vector3 angularAccelerationFactor = randomVector(maxAngularAcceleration, true);
        Vector3 cartesianNoise = randomVector(randomMovmentNoiseLimits, true);
        Vector3 angularNoise = randomVector(randomRotationNoiseLimits, true);
        transform.position += Vector3.Scale(finalMovment,angularAccelerationFactor)/(maxItirations-1)+cartesianNoise;
        transform.eulerAngles += Vector3.Scale(finalRotation, angularAccelerationFactor)/(maxItirations-1) + angularNoise;
    }
    public void resetCamera()
    {
        Debug.Log("Reset Cam");
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }

    public void capture(string name) {
        {
            var fileName = name + ".png";
            this.cam.depthTextureMode = DepthTextureMode.DepthNormals;
            DirectoryInfo screenshotDirectory = Directory.CreateDirectory(CapturedirectoryName);
            string fullPath = Path.Combine(screenshotDirectory.FullName, fileName);

            ScreenCapture.CaptureScreenshot(fullPath);

        } }
    // Start is called before the first frame update
    void Start()
    {
        isTargetVisible = false;
        movement = Vector3.zero;
        rotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
       
        

    }
}
