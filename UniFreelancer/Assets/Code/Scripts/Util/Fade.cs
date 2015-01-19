using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
    }

    public void FadeInTex()
    {
        StartCoroutine(coFadeInTex());
    }

    IEnumerator coFadeInTex()
    {
        while (this.guiTexture.color.a > 0)
        {
            Color c = new Color(this.guiTexture.color.r, this.guiTexture.color.b, this.guiTexture.color.g, this.guiTexture.color.a - Time.deltaTime * 0.1f);
            this.guiTexture.color = c;

            yield return null;
        }
    }

    public void FadeOutTex()
    {
        StartCoroutine(coFadeOutTex());
    }

    IEnumerator coFadeOutTex()
    {
        while (this.guiTexture.color.a < 1)
        {
            Color c = new Color(this.guiTexture.color.r, this.guiTexture.color.b, this.guiTexture.color.g, this.guiTexture.color.a + Time.deltaTime * 0.1f);
            this.guiTexture.color = c;

            yield return null;
        }
    }

    public void FadeInText()
    {
        StartCoroutine(coFadeInText());
    }

    IEnumerator coFadeInText()
    {
        while (this.guiText.color.a < 1)
        {
            Color c = new Color(this.guiText.color.r, this.guiText.color.b, this.guiText.color.g, this.guiText.color.a + 0.5f * Time.deltaTime);
            this.guiText.color = c;

            yield return null;
        }
    }
}
