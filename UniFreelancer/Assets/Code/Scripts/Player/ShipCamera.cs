using UnityEngine;
using System.Collections;

public class ShipCamera : MonoBehaviour
{
    GameObject ship;

    float rotationXlimit = 120.0f;
    float rotationYlimit = 60.0f;

    float finalFollowDistance = 5.0f;
    float currentFollowDistance = 5.0f;
    float followDistanceMin = 0.0f;
    float followDistanceNeutral = 0.0f;
    float followDistanceMax = 0.0f;
    float followSpeed = 1.0f;

    Vector3 permaOffset = new Vector3(0f, 0.1f, 0f);

    Vector3 finalOffset = Vector3.zero;
    Vector3 currentOffset = Vector3.zero;

    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Player");

        transform.position = ship.transform.position + ship.transform.forward * -currentFollowDistance;
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

        transform.position = ship.transform.position + ship.transform.forward * -currentFollowDistance + ship.transform.TransformDirection(currentOffset);
        transform.rotation = ship.transform.rotation;

        // lerp camera in and out based on acceleration
        currentFollowDistance = Mathf.Lerp(currentFollowDistance, finalFollowDistance, Time.deltaTime * followSpeed);
    }

    public void SetAccelerating()
    {
        finalFollowDistance = followDistanceMax;
    }

    public void SetDecelerating()
    {
        finalFollowDistance = followDistanceMin;
    }

    public void SetNeutral()
    {
        finalFollowDistance = followDistanceNeutral;
    }
}