using UnityEngine;
using System.Collections;
using RAIN.Core;

public class AIUpdate : MonoBehaviour
{
    public GameObject aiBody;

    private AIRig _aiRig = null;

    public float moveForward;
    public float moveUp;
    public float moveRight;

    Vector3 velocity = Vector3.zero;
    float rotationSpeed = 1.5f;
    float accelForce = 0.5f; // how fast the ships speed increases
    float velocityMin = 0.0f; // reverse thrust
    float velocityMax = 20.0f;
    float velocityShake = 160.0f;
    float sensitivity = 100.0f;

    void Awake()
    {
        _aiRig = GetComponent<AIRig>();
    }

	void Start()
    {
        _aiRig.AIAwake();
        _aiRig.AIStart();

        //RAIN.Memory.BasicMemory tMemory = _aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        //RAIN.Minds.BasicMind tMind = _aiRig.AI.Mind as RAIN.Minds.BasicMind;
	}

    void Update()
    {
        //if (!_aiRig.UseFixedUpdate)
        //    _aiRig.AIUpdate();

        moveForward = _aiRig.AI.WorkingMemory.GetItem<float>("forward");
        moveUp = _aiRig.AI.WorkingMemory.GetItem<float>("up");
        moveRight = _aiRig.AI.WorkingMemory.GetItem<float>("right");

        Quaternion targetRotation;
        Ray ray = new Ray(aiBody.transform.position, aiBody.transform.forward * moveForward + aiBody.transform.up * moveUp + aiBody.transform.right * moveRight);
        Plane plane;

        if (moveForward != 0)
        {
            // http://docs.unity3d.com/ScriptReference/Plane.Raycast.html
            plane = new Plane(aiBody.transform.forward, aiBody.transform.position + aiBody.transform.forward * sensitivity);

            Vector3 relativePos = (ray.origin + ray.direction * 10f) - aiBody.transform.position;
            targetRotation = Quaternion.LookRotation(relativePos, aiBody.transform.up);
            velocity.z += moveForward * accelForce;
        }
        else
        {
            // reorient to xz axis
            targetRotation = Quaternion.LookRotation(this.transform.forward, Vector3.up);
            velocity.z -= accelForce;
        }

        // interpolate rotation nicely
        aiBody.transform.rotation = Quaternion.Slerp(aiBody.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // move forward or backward

        velocity.z = Mathf.Clamp(velocity.z, velocityMin, velocityMax);

        // local to world space
        aiBody.rigidbody.velocity = aiBody.transform.TransformDirection(velocity);
    }

    void LateUpdate()
    {
        //_aiRig.AILateUpdate();
    }

    void FixedUpdate()
    {
        //if (_aiRig.UseFixedUpdate)
        //    _aiRig.AIUpdate();
    }

    void OnAnimatorMove()
    {
        //_aiRig.AIRootMotion();
    }

    void OnAnimatorIK(int aLayerIndex)
    {
        //_aiRig.AIIK(aLayerIndex);
    }
}
