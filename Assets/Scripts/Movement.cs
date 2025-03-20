using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Transform player;

    private float horizontal;
    private float vertical;
    private Vector3 movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        player.position += player.rotation * movement * speed * Time.deltaTime;
    }
}
