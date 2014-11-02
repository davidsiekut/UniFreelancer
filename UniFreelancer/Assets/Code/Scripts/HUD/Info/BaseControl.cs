using UnityEngine;
using System.Collections;

public abstract class BaseControl : MonoBehaviour
{
    public GUIText Text;
    public GUITexture[] Bars = new GUITexture[10];
    protected GameObject ship;
    protected Color c1;
    protected Color c2;

    void Start()
    {
        Init();
    }

    protected void Init()
    {
        ship = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < Bars.Length; i++)
        {
            Bars[i].color = c2;
        }
    }

    void Update()
    {
    }

    protected void Refresh(float f, float max, string s)
    {
        Text.text = (int) f + s;

        if (f <= 0)
        {
            Bars[0].color = c2;
        }
        else if (f > 0 & f <= max * 0.10f)
        {
            Bars[0].color = c1;
            Bars[1].color = c2;
        }
        else if (f > max * 0.1f & f <= max * 0.2f)
        {
            Bars[1].color = c1;
            Bars[2].color = c2;
        }
        else if (f > max * 0.2f & f <= max * 0.3f)
        {
            Bars[2].color = c1;
            Bars[3].color = c2;
        }
        else if (f > max * 0.3f & f <= max * 0.4f)
        {
            Bars[3].color = c1;
            Bars[4].color = c2;
        }
        else if (f > max * 0.4f & f <= max * 0.5f)
        {
            Bars[4].color = c1;
            Bars[5].color = c2;
        }
        else if (f > max * 0.5f & f <= max * 0.6f)
        {
            Bars[5].color = c1;
            Bars[6].color = c2;
        }
        else if (f > max * 0.6f & f <= max * 0.7f)
        {
            Bars[6].color = c1;
            Bars[7].color = c2;
        }
        else if (f > max * 0.7f & f <= max * 0.8f)
        {
            Bars[7].color = c1;
            Bars[8].color = c2;
        }
        else if (f > max * 0.8f & f <= max * 0.9f)
        {
            Bars[8].color = c1;
            Bars[9].color = c2;
        }
        else if (f > max * 0.9f & f <= max)
        {
            Bars[9].color = c1;
        }
    }
}
