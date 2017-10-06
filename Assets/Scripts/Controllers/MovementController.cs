using UnityEngine;
using UnityEngine.Networking;

public class MovementController : MonoBehaviour{
    [SerializeField]
    private float speed = 10.0f;

    public float jumpForce = 999f;

    void Update()
    {
        // Handle movement 
        //if (!isLocalPlayer) return;
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);     // limits diagonal movement to the same speed as movement along an axis
        movement *= Time.deltaTime;
        transform.Translate(movement);                          // TODO: Change to force or velocity changing, or move to FixedUpdate to prevent going into walls.

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.Translate(0, 10f * Time.deltaTime, 0);
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
        }

    }
}
