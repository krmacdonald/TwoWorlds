using UnityEngine;
using UnityEngine.UI;
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
    public float timer;
    private bool plush;
    private bool transported;

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
            int layer = LayerMask.NameToLayer(layerName);
            if (layer != -1)
            {
                mask |= (1 << layer);
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
        if(Input.GetAxis("Jump") > 0)
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
