using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SpeedWalk = 8f;
    public float SpeedRun = 15f;
    public float RotateSpeed = 300f;
    public float GravityAcceleration = -2f;

    private CharacterController characterController;
    private Animator anim;

    private float speed;
    private Vector3 direction;
    private Vector3 lookAt;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = new Vector3(x, GravityAcceleration, z).normalized;
        lookAt = new Vector3(direction.x, 0, direction.z);

        anim.SetFloat("Move", lookAt.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
            speed = SpeedRun;
        else
            speed = SpeedWalk;
    }

    private void FixedUpdate()
    {
        characterController.Move(speed * Time.deltaTime * direction);

        if (lookAt != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                newRotation,
                RotateSpeed * Time.deltaTime
            );
        }
    }
}
