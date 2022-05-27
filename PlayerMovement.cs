using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 Direction;
    public float speed;
    public float turnSmooth = 0.1f;

    public float camDistance = 6f;
    float smoothvelocity;

    public Transform cam;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Direction = new Vector3(h, 0f, v).normalized;

        if (Direction.magnitude >= 0.1f)
        {
        float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref smoothvelocity, turnSmooth);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        controller.Move(Direction * speed * Time.deltaTime);
        cam.transform.position = Vector3.Lerp(cam.transform.position,new Vector3(transform.position.x, cam.transform.position.y, transform.position.z - camDistance), Time.deltaTime * speed);
        }
    }

  
}
