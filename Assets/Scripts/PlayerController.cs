using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public float MoveSpeed;
    public float MaxMoveSpeed;
    public float MoveDrag;

    public float LookSpeed;
    public float LookLimit;
    public float MaxLookSpeed;
    public float LookDrag;

    private CharacterController character;

    Vector3 moveSpeed;
    Vector2 lookSpeed;

    void Awake()
    {
        character = GetComponent<CharacterController>();
    }

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    // move
        moveSpeed += transform.right * Input.GetAxis("Move Horizontal");
        moveSpeed += transform.forward * Input.GetAxis("Move Vertical");

        // limit move
        if (moveSpeed.magnitude > MaxMoveSpeed)
        {
            moveSpeed = moveSpeed.normalized * MaxMoveSpeed;
        }

        character.SimpleMove(moveSpeed);

        // damp move
        moveSpeed = Vector3.Lerp(moveSpeed, Vector3.zero, MoveDrag * Time.deltaTime);

        // look
        lookSpeed -= Vector2.right * Input.GetAxis("Look Horizontal");
        lookSpeed -= Vector2.up * Input.GetAxis("Look Vertical");

        // limit look speed
        if (lookSpeed.magnitude > MaxLookSpeed)
            lookSpeed = lookSpeed.normalized * MaxLookSpeed;

        transform.Rotate(Vector3.up, lookSpeed.x, Space.World);
        transform.Rotate(Vector3.right, lookSpeed.y, Space.Self);

        // limit look
        Vector3 euler = transform.eulerAngles;
        euler.x %= 360f;

        if (euler.x > 90f - LookLimit)
            euler.x = 90f - LookLimit;

        if (euler.x < 270f + LookLimit)
            euler.x = 270f + LookLimit;

        //transform.eulerAngles = euler;

        // damp look
        lookSpeed = Vector2.Lerp(lookSpeed, Vector2.zero, LookDrag * Time.deltaTime);

	}
}
