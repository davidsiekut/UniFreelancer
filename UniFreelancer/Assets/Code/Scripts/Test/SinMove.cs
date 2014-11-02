using UnityEngine;
using System.Collections;

public class SinMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.x = Mathf.PingPong(Time.time * 10f, 500000f);
        this.transform.position = pos;

        /*
        float x = 0;
        float y = 0;

        Vector2 direction = Vector2.zero;

        x = radius * Mathf.Cos(angle);
        y = radius * Mathf.Sin(angle);

        transform.position = new Vector2(x, y);

        angle += 15 * Mathf.Rad2Deg * Time.deltaTime * 0.0001f;
         * */
	}
}
