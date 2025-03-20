using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

/**
 * ALSO MANAGES THE LIGHTS IN THE SCENE***********
 * Created by krmacdonald
 * To implement, just attach the script to the global volume, and insert the volume into the inspector slot. 
 * Output: Switches between the two smh profiles to alter the lighting within the two scenes.
 */

public class GlobalVolumeShift : MonoBehaviour
{
    public Volume volume;
    public bool plush = true;
    public bool devDebug;
    public ShadowsMidtonesHighlights goal;

    //plush world vectors
    private Vector4Parameter plushShadows = new Vector4Parameter(new Vector4(1, 0.53f, 0.71f, 0f));
    private Vector4Parameter plushMidtones = new Vector4Parameter(new Vector4(1.59f, 1.52f, 1.57f, 0f));
    private Vector4Parameter plushHighlights = new Vector4Parameter(new Vector4(1, 0.78f, 0f, 0f));

    //low-poly vectors
    private Vector4Parameter lowShadows = new Vector4Parameter(new Vector4(0.65f, 1f, 0.73f, 0f));
    private Vector4Parameter lowMidtones = new Vector4Parameter(new Vector4(0.9f, 1f, 0.84f, .21f)); //mysterious fourth coordinate might be inaccurate
    private Vector4Parameter lowHighlights = new Vector4Parameter(new Vector4(1, 1f, 0.98f, 0f));

    

    public Collider[] lowpolyColliders;
    public Collider[] plushColliders;
    public Light[] plushLights;
    public Light[] lowpolyLights;
    public GameObject plushSearchParent;
    public GameObject lowpolySearchParent;



    private void getAllColliders()
    {
        lowpolyColliders = lowpolySearchParent.GetComponentsInChildren<Collider>();
        plushColliders = plushSearchParent.GetComponentsInChildren<Collider>();
        plushLights = plushSearchParent.GetComponentsInChildren<Light>();
        lowpolyLights = lowpolySearchParent.GetComponentsInChildren<Light>();
    }

    private void swapColliders(string world)
    {
        if(world == "plush")
        {
            foreach (Collider collider in lowpolyColliders)
            {
                collider.enabled = false;
            }
            foreach (Collider collider in plushColliders)
            {
                collider.enabled = true;
            }
        }
        else
        {
            foreach (Collider collider in lowpolyColliders)
            {
                collider.enabled = true;
            }
            foreach (Collider collider in plushColliders)
            {
                collider.enabled = false;
            }
        }
        
    }

    private void Start()
    {
        getAllColliders();
        //Tries to get the shadowsmidtoneshighlights with catch
        if(volume.profile.TryGet(out goal))
        {
            Debug.Log("SMH Acquired");
            goal.midtones.SetValue(lowMidtones);
            goal.shadows.SetValue(lowShadows);
            goal.highlights.SetValue(lowHighlights);
            foreach (Light go in plushLights)
            {
                go.enabled = false;
            }
            foreach (Light go in lowpolyLights)
            {
                go.enabled = true;
            }
        }
        else
        {
            Debug.Log("ERROR CANNOOT FIND SMH");
        }

    }

    public void toggleVolume()
    {
        if (goal)
        {
            if (plush)
            {
                goal.midtones.SetValue(lowMidtones);
                goal.shadows.SetValue(lowShadows);
                goal.highlights.SetValue(lowHighlights);
                foreach (Light go in plushLights)
                {
                    go.enabled = false;
                }
                foreach (Light go in lowpolyLights)
                {
                    go.enabled = true;
                }
                swapColliders("poly");
            }
            else
            {
                goal.midtones.SetValue(plushMidtones);
                goal.shadows.SetValue(plushShadows);
                goal.highlights.SetValue(plushHighlights);
                foreach (Light go in plushLights)
                {
                    go.enabled = true;
                }
                foreach (Light go in lowpolyLights)
                {
                    go.enabled = false;
                }
                swapColliders("plush");
            }
            plush = !plush;
        }
    }

    private void Update()
    {
        //TODO DISABLE FOR RELEASE
        if (Input.GetKeyDown(KeyCode.Q) && devDebug)
        {
            toggleVolume();
        }
    }
}
