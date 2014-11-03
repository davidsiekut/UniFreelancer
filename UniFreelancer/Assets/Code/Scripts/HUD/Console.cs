using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour
{
    public GameObject Status;
    public GameObject Heat;
    public GameObject Speed;
    public GameObject Crosshair;
    public GameObject CrosshairBounds;
    public GameObject Target;
    public GameObject Hardpoints;

    string[] buffer;
    float fadeTimer;
    float _fadeTimer = 3.0f;

	void Start()
    {
        fadeTimer = _fadeTimer;

        buffer = new string[10];

        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = "";
        }
	}
	
	void Update()
    {
        if (fadeTimer < 0)
        {
            this.guiText.enabled = false;
        }
        else
        {
            fadeTimer -= Time.deltaTime;
        }
	}

    public void SystemCheck()
    {
        Status.SetActive(false);
        Heat.SetActive(false);
        Speed.SetActive(false);
        Crosshair.SetActive(false);
        CrosshairBounds.SetActive(false);
        Target.SetActive(false);
        Hardpoints.SetActive(false);

        StartCoroutine(CoSystemCheck());
    }

    IEnumerator CoSystemCheck()
    {
        List<string> l = new List<string>();
        l.Add("swordfish iii status check initiated");
        l.Add("vital system... online");
        l.Add("mono system... online");
        l.Add("weapon system... online");
        l.Add("good luck, space cowboy!");

        while (l.Count > 0)
        {
            this.Add(l[0]);
            l.RemoveAt(0);

            if (l.Count == 3)
            {
                Status.SetActive(true);
            }
            if (l.Count == 2)
            {
                Speed.SetActive(true);
                Heat.SetActive(true);
            }
            if (l.Count == 1)
            {
                Crosshair.SetActive(true);
                CrosshairBounds.SetActive(true);
                Target.SetActive(true);
                Hardpoints.SetActive(true);
                GameController.Player.GetComponent<ShipInput>().CanControl = true;
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public void Add(string s)
    {
        for (int i = 1; i < buffer.Length; i++)
        {
            buffer[i - 1] = buffer[i];
        }

        buffer[buffer.Length - 1] = s;
        refresh();
    }

    void refresh()
    {
        fadeTimer = _fadeTimer;
        this.guiText.enabled = true;

        string s = "";

        for (int i = 0; i < buffer.Length; i++)
        {
            s += buffer[i] + "\n";
        }

        this.guiText.text = s;
    }
}
