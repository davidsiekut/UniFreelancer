using UnityEngine;
using System.Collections;

public class ShipCamera : MonoBehaviour
{
    public GameObject ship;

    float rotationXlimit = 120.0f;
    float rotationYlimit = 60.0f;
    float followDistance = 5.0f;
    float followSpeed = 1.0f;
    Vector3 permaOffset = new Vector3(0f, 1.0f, 0f);

    Vector3 finalOffset = Vector3.zero;
    Vector3 currentOffset = Vector3.zero;

    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Player");

        transform.position = ship.transform.position + ship.transform.forward * -followDistance;
        transform.rotation = ship.transform.rotation;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseOffsetX = Input.mousePosition.x - (Screen.width / 2);
            float mouseOffsetY = Input.mousePosition.y - (Screen.height / 2);
            finalOffset.x = Mathf.Clamp(mouseOffsetX * 0.35f, -rotationXlimit, rotationXlimit) / 110;
            finalOffset.y = Mathf.Clamp(mouseOffsetY * 0.35f, -rotationYlimit, rotationYlimit) / 130;
        }
        else
        {
            finalOffset.x = 0;
            finalOffset.y = 0;
        }

        finalOffset = finalOffset + permaOffset;
        currentOffset = Vector3.Lerp(currentOffset, finalOffset, Time.deltaTime * followSpeed);

        transform.position = ship.transform.position + ship.transform.forward * -followDistance + ship.transform.TransformDirection(currentOffset);
        transform.rotation = ship.transform.rotation;
    }
}