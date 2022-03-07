using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
	private Image fadeImage;   //The image that will fade

	[Range(0, 1)]
	public float fadeMaxAlpha; //The max alpha the image will fade to
	public float fadeSmooth;   //Interpolation value for image alpha lerp
	public float fadeInterval; //Interval between each fadeSmooth interpolation

    private bool isFading = false; //is in the process of fading?


    private void Start()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeToBlack(bool b)
    {
        if (!isFading)
        {
            isFading = true;
            StartCoroutine("Fade", b);
        }
        else
        {
            Debug.LogWarning("Wait until fade is completed to call again!");
        }
    }

    private IEnumerator Fade(bool b)
    {

        if (b)
        {
            //fade screen to black
            for (float i = 0; i <= fadeMaxAlpha; i += fadeSmooth)
            {
                yield return new WaitForSeconds(fadeInterval);
                Color nextColor = new Color();
                nextColor = fadeImage.color;
                Debug.Log("i = " + i);
                nextColor.a = i;
                fadeImage.color = nextColor;
            }
        }
        else
        {
            //fade in screen
            for (float i = fadeMaxAlpha; i >= 0; i -= fadeSmooth)
            {
                yield return new WaitForSeconds(fadeInterval);
                Color nextColor = new Color();
                nextColor = fadeImage.color;
                nextColor.a = i;
                fadeImage.color = nextColor;
            }
        }

        isFading = false;
    }

    public bool GetIsFading()
    {
        return isFading;
    }
}
