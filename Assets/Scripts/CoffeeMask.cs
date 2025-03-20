using UnityEngine;
using UnityEngine.UI;

/**
 * Created by krmacdonald
 * This script controls the transition based on the coffee mug/mask present in the scene
 * Through clicking on it, it will force you to blink and send you to the other world
 * Prerequisite: Assign the blink script from the blink manager to the blink script var
 * also have fun! :D
 */

public class CoffeeMask : MonoBehaviour {

    private RaycastHit[] hits;
    public Blink blinkScript;
    private float timer = 999f;
    private float delay = 2.5f;
    public Image crosshair;
    private void Update()
    {
        timer += Time.deltaTime;

        hits = Physics.RaycastAll(transform.position, transform.forward, 100);
        Debug.Log(hits);
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "teleport" && hit.distance < 3)
                {
                    if (timer > delay)
                        crosshair.color = Color.white;
                    else
                        crosshair.color = Color.black;
                    break;
                }
                else
                {
                    crosshair.color = Color.black;
                }
            }
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            foreach (RaycastHit hit in hits)
            {
                if (hits != null)
                {
                    if (hit.collider.gameObject.tag == "teleport" && hit.distance < 3)
                    {
                        Debug.Log("go");
                        timer = 0f;
                    }
                }
            }
        }

        if (timer < delay)
        {
            blinkScript.blinkForce = 1;
        }
        else
        {
            blinkScript.blinkForce = 0;
        }
    }

}
