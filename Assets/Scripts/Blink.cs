using UnityEngine;
using UnityEngine.UI;

/**
 * Created by krmacdonald
 * This script allows the player to blink by using the space bar or their desired "jump" button
 * Prerequisites: Please add the UI prefab into the scene, it should come attached with this script
 * from there, please assign the camera variables and adjust the other settings such
 * as delay, blinkspeed, and others accordingly
 * DELAY is the amount of time it takes to go from one world to another when blinking
 */

public class Blink : MonoBehaviour
{
    //Used to manage the resting/final positions of the blinking lids
    public float goalYOne;
    public float goalYTwo;
    public float hiddenYOne;
    public float hiddenYTwo;

    //The speed at which the eyes open and close
    public float openSpeed;
    public float closeSpeed;


    public RectTransform eyeOne;
    public RectTransform eyeTwo;

    //vars to keep track of switching between the two worlds
    public float delay;
    private float timer;
    private bool plush;
    private bool transported;
    public float blinkForce; //used in coffeemask to force the blink

    public GlobalVolumeShift volShift;

    //cam and layers for culling
    public Camera cam;
    private string plushLayer = "Plush";
    private string lowpolyLayer = "LowPoly";
    private string plushCompLayer = "PlushComputer";
    private string lowpolyCompLayer = "LowPolyComputer";
    private string[] plushLayerNames = new string[2];
    private string[] lowpolyLayerNames = new string[2];

    

    void Start()
    {
        plushLayerNames[0] = plushLayer;
        plushLayerNames[1] = plushCompLayer;

        lowpolyLayerNames[0] = lowpolyLayer;
        lowpolyLayerNames[1] = lowpolyCompLayer;

        toggleLayer(lowpolyLayerNames);
    }

    private void toggleLayer(string[] targetLayers)
    {
        int mask = 0;
        foreach (string layerName in targetLayers)
        {
            int layer = LayerMask.NameToLayer(layerName); //Gets the layers that we want to display
            if (layer != -1)
            {
                mask |= (1 << layer); //converts the layer value to bits for the mask using compound assignment
            }
            else
            {
                Debug.LogError("Layer not found: " + layerName);
            }
        }
        cam.cullingMask = mask;
    }

    void Update()
    {
        //Holding your eyes shut
        if(Input.GetAxis("Jump") + blinkForce > 0)
        {
            timer += Time.deltaTime;
            if(eyeOne.position.y < goalYOne)
            {
                eyeOne.position += new Vector3(0, closeSpeed, 0);
            }

            if (eyeTwo.position.y > goalYTwo)
            {
                eyeTwo.position -= new Vector3(0, closeSpeed, 0);
            }

            if(timer > delay && transported == false)
            {
                volShift.toggleVolume();
                transported = true;
                if (plush)
                {
                    toggleLayer(lowpolyLayerNames);
                    plush = false;
                }
                else
                {
                    toggleLayer(plushLayerNames);
                    plush = true;
                }
                timer = 0;
            }
        }
        //NOT holding your eyes shut
        else
        {
            transported = false;
            timer = 0;
            if (eyeOne.position.y > hiddenYOne)
            {
                eyeOne.position -= new Vector3(0, openSpeed, 0);
            }

            if (eyeTwo.position.y < hiddenYTwo)
            {
                eyeTwo.position += new Vector3(0, openSpeed, 0);
            }
        }
    }
}
