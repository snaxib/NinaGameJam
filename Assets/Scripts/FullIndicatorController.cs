using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullIndicatorController : MonoBehaviour
{
    public Image borderImg, fillImg, fillBorderImg;
    public List<Color> fillColorValues;
    public List<Vector3> fillScaleValues;

    public float fadeInSpeed;

    private int canCount;

    // Start is called before the first frame update
    void Start()
    {
        canCount = 0;

        borderImg.color = new Color(borderImg.color.r, borderImg.color.g, borderImg.color.b, 0);
        fillImg.color = new Color(fillImg.color.r, fillImg.color.g, fillImg.color.b, 0);
        fillBorderImg.color = new Color(fillBorderImg.color.r, fillBorderImg.color.g, fillBorderImg.color.b, 0);
    }

    public void SetCanCount(int count)
    {
        canCount = count;
        StartCoroutine("FadeImage", true);
    }

    private IEnumerator FadeImage(bool setVisible)
    {
        float timer = 0f;
        float imgAlpha = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime * fadeInSpeed;
            imgAlpha = setVisible ? timer : 1f - timer;

            Debug.Log(imgAlpha);

            borderImg.color = new Color(borderImg.color.r, borderImg.color.g, borderImg.color.b, imgAlpha);
            fillImg.color = new Color(fillImg.color.r, fillImg.color.g, fillImg.color.b, imgAlpha);
            fillBorderImg.color = new Color(fillBorderImg.color.r, fillBorderImg.color.g, fillBorderImg.color.b, imgAlpha);

            yield return false;
        }

        if (setVisible)
            StartCoroutine("AdjustFill");
    }

    private IEnumerator AdjustFill()
    {
        float timer = 0f;
        float transitionSpeed = 1f;

        while (timer < 1f)
        {
            timer += Time.deltaTime / transitionSpeed;

            fillImg.color = Color.Lerp(fillColorValues[canCount - 1], fillColorValues[canCount], Mathf.SmoothStep(0f, 1f, timer));
            fillImg.transform.localScale = Vector3.Lerp(fillScaleValues[canCount - 1], fillScaleValues[canCount], Mathf.SmoothStep(0f, 1f, timer));

            yield return false;
        }

        StartCoroutine("FadeImage", false);
    }
}