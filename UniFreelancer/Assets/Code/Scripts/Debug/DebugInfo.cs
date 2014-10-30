using UnityEngine;
using System.Collections;

public class DebugInfo : MonoBehaviour
{
    Transform Player;

	void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update()
    {
        string s = "";
        s += "ForwardSpeed: " + Player.rigidbody.velocity.magnitude;
        this.guiText.text = s;
	}
}
