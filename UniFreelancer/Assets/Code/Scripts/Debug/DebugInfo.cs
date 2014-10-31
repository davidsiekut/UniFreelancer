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
        s += "Magnitude: " + Player.rigidbody.velocity.magnitude + "\n";
        s += "Coords: " + Player.position + "\n";
        this.guiText.text = s;
	}
}
