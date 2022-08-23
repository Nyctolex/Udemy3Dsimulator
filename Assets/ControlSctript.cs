using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSctript : MonoBehaviour
{
    public RandomCameraMove cam;
    public randomMove target;
    public Vector3 cameraRandomMovmentLimits;
    public Vector3 cameraRandomRotationLimits;
    public Vector3 cameraRandomMovmentNoiseLimits;
    public Vector3 cameraRandomRotationNoiseLimits;
    public Vector3 cameraMaxAcceleration;
    public Vector3 cameraMaxAngularAcceleration;
    public Vector3 targetRandomMovmentLimits;
    public Vector3 targetInitPos;
    public string ouputDirectory = "C:/Users/USER/Desktop/Backgrounds/"; 
    public int amoutOfData = 5; // the amount of samples we want to take
    public int fps; // frame per second
    public int deltaTime; // the length og the video in seconds
    private int maxItirations;
    private int itteration; //The index of the image sice the camera have started moving
    private int sampleCount; //the index of the sample we are taking

    
    // Start is called before the first frame update
    void Start()
    {
        maxItirations = fps*deltaTime;
        itteration = 0;
        sampleCount = 0;
        target.randomMovmentLimits = targetRandomMovmentLimits;
        cam.randomMovmentLimits = cameraRandomMovmentLimits;
        cam.randomRotationLimits = cameraRandomRotationLimits;
        cam.randomMovmentNoiseLimits = cameraRandomMovmentNoiseLimits;
        cam.randomRotationNoiseLimits = cameraRandomRotationNoiseLimits;
        cam.CapturedirectoryName = ouputDirectory;
        target.initialPos = targetInitPos;

    }

    // Update is called once per frame
    void Update()
    {
        //var nextSample = 0; //adding to the sample counter after the name of the picture had been saved.
        if (itteration % maxItirations !=  0)
        {
            //move and rotate
            cam.moveCamera(maxItirations, cameraMaxAcceleration, cameraMaxAngularAcceleration);
            }
        else
        {
            //reset
            //nextSample = 1;
            sampleCount += 1;
            cam.resetCamera();
            cam.generateFinalPosition();
            target.SetUpRandomPlace();
        }
        if (sampleCount <= amoutOfData)
        {
            var capture_name = sampleCount + "." + itteration;
            cam.capture(capture_name);
            itteration= (itteration+1)%maxItirations;
        }
        else
        {
                QuitGame();
        }
    }
        

    //}

    public void QuitGame()
    {
        // save any game data here
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
         Application.Quit();

    }
}
