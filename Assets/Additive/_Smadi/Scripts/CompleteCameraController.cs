using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{
    public Transform target,camTarget;
    public float rotateSpeed = 3;
    Vector3 offset;
    public FloatingJoystick rightJoystick;
    public float offsetx = 0, offsety= 2.2f, offsetz= -0.9f;

    void Start()
    {
        offset = new Vector3(target.position.x+ offsetx, target.position.y + offsety, target.position.z + offsetz);
    }

    void LateUpdate()
    {
        float horizontal = rightJoystick.Horizontal * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);
        float vertical = rightJoystick.Vertical * rotateSpeed;
        transform.Rotate(0, horizontal, 0);
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
        float desiredAngleX = target.transform.eulerAngles.x;
        Quaternion rotationX = Quaternion.Euler(0, desiredAngleX, 0);
        transform.position = target.transform.position - (rotationX * offset);


        offset = Quaternion.AngleAxis(rightJoystick.Horizontal * rotateSpeed, Vector3.up) * offset;
        transform.position = target.position + offset;
        transform.LookAt(camTarget.position);


        offset = Quaternion.AngleAxis(rightJoystick.Vertical * rotateSpeed, Vector3.left) * offset;
        transform.position = target.position + offset;

    }





}