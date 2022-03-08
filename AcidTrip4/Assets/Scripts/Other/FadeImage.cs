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
    public bool fadeOnStart = false;


    private void Start()
    {
        fadeImage = GetComponent<Image>();
        if(fadeOnStart){
            fadeImage.enabled = true;
            FadeToBlack(false);
        }
        else{
            Color finalColor = new Color();
            finalColor = fadeImage.color;
            finalColor.a = 0;
            fadeImage.color = finalColor;
            fadeImage.enabled = false;
        }
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
            fadeImage.enabled = true;
            for (float i = 0; i <= fadeMaxAlpha; i += fadeSmooth)
            {
                yield return new WaitForSeconds(fadeInterval);
                Color nextColor = new Color();
                nextColor = fadeImage.color;
                Debug.Log("i = " + i);
                nextColor.a = i;
                fadeImage.color = nextColor;
            }
            Color finalColor = new Color();
            finalColor = fadeImage.color;
            finalColor.a = fadeMaxAlpha;
            fadeImage.color = finalColor;
        }
        else
        {
            //fade in screen
            fadeImage.enabled = true;
            for (float i = fadeMaxAlpha; i >= 0; i -= fadeSmooth)
            {
                yield return new WaitForSeconds(fadeInterval);
                Color nextColor = new Color();
                nextColor = fadeImage.color;
                nextColor.a = i;
                fadeImage.color = nextColor;
            }
            Color finalColor = new Color();
            finalColor = fadeImage.color;
            finalColor.a = 0;
            fadeImage.color = finalColor;
            fadeImage.enabled = false;
        }

        isFading = false;
    }

    public void setBlack(){
        Color newColor = new Color();
        newColor = fadeImage.color;
        newColor.r = 0;
        newColor.g = 0;
        newColor.b = 0;
        fadeImage.color = newColor;
        
    }

    public bool GetIsFading()
    {
        return isFading;
    }
}
