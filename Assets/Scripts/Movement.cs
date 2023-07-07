using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float RunSpeed = 7f;
    public float RotateSpeed = 2f;

    [SerializeField] private float speed;

    private Rigidbody rb;
    private Animator anim;

    public Vector3 direction;

    private void Start()
    {
        speed = WalkSpeed;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        direction = new Vector3(x, 0f, z);
        direction = direction.normalized;

        anim.SetFloat("Move", direction.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
            speed = RunSpeed;
        else
            speed = WalkSpeed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * direction);
        //rb.velocity = direction * speed;

        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                newRotation,
                RotateSpeed * Time.deltaTime
            );
        }
    }
}
