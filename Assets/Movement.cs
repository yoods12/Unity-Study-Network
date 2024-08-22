using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController controller;
    Transform _transform;
    Animator animator;
    Camera _camera;

    Plane plane;
    Ray ray;
    Vector3 hitPoint;

    public float moveSpped = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        _camera = Camera.main; // �±װ� MainCamera�� ī�޶�

        plane = new Plane(transform.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
    }

    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    void Move()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 moveDir = (cameraForward * v) + (cameraRight * h);
        moveDir.Set(moveDir.x, 0f, moveDir.z);

        controller.SimpleMove(moveDir * moveSpped);
        float forward = Vector3.Dot(moveDir, transform.forward);
        float strafe = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Strafe", strafe);
    }
    void Turn()
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);
        float enter = 0f;

        plane.Raycast(ray, out enter);
        hitPoint = ray.GetPoint(enter);
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0f;

        transform.localRotation = Quaternion.LookRotation(lookDir);
    }

}
