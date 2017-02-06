using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MandelbrotEffect : MonoBehaviour
{
    public Material FractalMat;
    public float iterations, zoomLevel, zoomSpeed, moveSpeed;
    public Text textZoomLevel, textControls;
    public bool autoPlay;

    Vector2 center, viewSize, startViewSize = new Vector2(3, 2);
    int exponent;

    void Init()
    {
        center = new Vector2(-0.5f, 0.0f);
        viewSize = startViewSize;
        autoPlay = false;
        exponent = 1;
        zoomLevel = 0;
    }

	// Use this for initialization
	void Start ()
    {
        Init();

        StartCoroutine(ChangeZoom());
        StartCoroutine(ChangePan());
        // StartCoroutine(ChangeIterations());
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool zoomed = false;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKey(KeyCode.Q))
        {
            zoomLevel += zoomSpeed;
            zoomed = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.E))
        {
            zoomLevel -= zoomSpeed;
            zoomed = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Init();
        }

        /*if (Input.GetKeyDown(KeyCode.H))
        {
            textControls.gameObject.SetActive(!textControls.gameObject.activeSelf);
        }*/

        // Change view size depending on zoom level (smoothly)
        float zoomRatio = 1.0f / Mathf.Pow(2, zoomLevel);

        if (zoomed)
        {
            viewSize = startViewSize * zoomRatio;   // higher zoom = smaller view size
        }

        center.x += Input.GetAxis("Horizontal") * moveSpeed * zoomRatio;
        center.y -= Input.GetAxis("Vertical") * moveSpeed * zoomRatio;

        // textZoomLevel.text = "Zoomlevel: " + zoomLevel;
    }

    IEnumerator ChangeZoom()
    {
        while (true)
        {
            FractalMat.SetVector("_Zoom", new Vector4(zoomLevel, zoomLevel, 1, 1));

            yield return null;
        }
    }

    IEnumerator ChangePan()
    {
        while (true)
        {
            FractalMat.SetVector("_Pan", center);

            yield return null;
        }
    }

    IEnumerator ChangeIterations()
    {
        while (true)
        {
            FractalMat.SetFloat("_Interations", iterations);

            yield return null;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // FractalMat.SetVector("_Zoom", new Vector4(zoomLevel, zoomLevel, 1, 1));
        // FractalMat.SetFloat("_Interations", iterations);
        // FractalMat.SetInt("_Exponent", exponent);
        // FractalMat.SetVector("_ScreenRes", new Vector2(Screen.width, Screen.height));
        //FractalMat.SetVector("_Pan", center);
        FractalMat.SetVector("_Aspect", viewSize);

        // Graphics.Blit(source, destination, FractalMat);
    }

    // GUI callbacks
    public void SetIterations(float n)
    {
        iterations = n;
    }

    public void SetAutoplay(bool ap)
    {
        autoPlay = ap;
    }

    public void SetExponent(float e)
    {
        exponent = (int)e - 1;
    }
}
