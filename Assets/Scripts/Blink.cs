using UnityEngine;
using UnityEngine.UI;
public class Blink : MonoBehaviour
{
    public float goalYOne;
    public float goalYTwo;
    public float hiddenYOne;
    public float hiddenYTwo;

    public float openSpeed;
    public float closeSpeed;

    public float eyeHeightGoal;
    public float eyeHeightHidden;


    public RectTransform eyeOne;
    public RectTransform eyeTwo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Jump") > 0)
        {
            if(eyeOne.position.y < goalYOne)
            {
                eyeOne.position += new Vector3(0, closeSpeed, 0);
            }

            if (eyeTwo.position.y > goalYTwo)
            {
                eyeTwo.position -= new Vector3(0, closeSpeed, 0);
            }
        }
        else
        {
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
