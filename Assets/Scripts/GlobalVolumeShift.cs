using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

/**
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
    private Vector4Parameter lowShadows = new Vector4Parameter(new Vector4(1, 0.53f, 0.71f, 0f));
    private Vector4Parameter lowMidtones = new Vector4Parameter(new Vector4(1, 0.53f, 0.71f, .21f)); //mysterious fourth coordinate might be inaccurate
    private Vector4Parameter lowHighlights = new Vector4Parameter(new Vector4(1, 0.53f, 0.71f, 0f));

    private void Start()
    {
        //Tries to get the shadowsmidtoneshighlights with catch
        if(volume.profile.TryGet(out goal))
        {
            Debug.Log("SMH Acquired");
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
            }
            else
            {
                goal.midtones.SetValue(plushMidtones);
                goal.shadows.SetValue(plushShadows);
                goal.highlights.SetValue(plushHighlights);
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
