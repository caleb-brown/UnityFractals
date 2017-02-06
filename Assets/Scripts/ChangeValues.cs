using UnityEngine;
using System.Collections;

public class ChangeValues : MonoBehaviour
{
    public GameObject Quad;
    public float iterations, zoomLevel, zoomSpeed, moveSpeed;
    public bool autoplay;

    Material FractalMat;
    Vector2 center, viewSize, startViewSize = new Vector2(3, 2);

    void Init()
    {
        center = new Vector2(-0.5f, 0.0f);
        viewSize = startViewSize;
        autoplay = false;
        zoomLevel = 0.0f;
    }

    void Start()
    {
        Renderer renderer = Quad.GetComponent<Renderer>();
        FractalMat = renderer.material;
        Init();
    }

    void Update()
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

        float zoomRatio = 1.0f / Mathf.Pow(2, zoomLevel);

        if (zoomed)
        {
            viewSize = startViewSize * zoomRatio;   // higher zoom = smaller view size
        }

        center.x += Input.GetAxis("Horizontal") * moveSpeed * zoomRatio;
        center.y -= Input.GetAxis("Vertical") * moveSpeed * zoomRatio;

        FractalMat.SetVector("_ZoomLevel", new Vector4(zoomLevel, zoomLevel, 1, 1));
        FractalMat.SetFloat("_Iterations", iterations);
        // FractalMat.SetInt("_Exponent", exponent);
        // FractalMat.SetVector("_ScreenRes", new Vector2(Screen.width, Screen.height));
        FractalMat.SetVector("_Pan", center);
        FractalMat.SetVector("_Aspect", viewSize);
    }

    /*void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        FractalMat.SetVector("_ZoomLevel", new Vector4(zoomLevel, zoomLevel, 1, 1));
        FractalMat.SetFloat("_Iterations", iterations);
        // FractalMat.SetInt("_Exponent", exponent);
        // FractalMat.SetVector("_ScreenRes", new Vector2(Screen.width, Screen.height));
        FractalMat.SetVector("_Pan", center);
        FractalMat.SetVector("_Aspect", viewSize);
    }*/

    /* public void IterationsValue(float newValue)
     {
         newIterationValue = newValue;

         Renderer renderer = Quad.GetComponent<Renderer>();
         Material mat = renderer.material;
         mat.SetFloat("_Iterations", newValue);
     }

     public void ZoomValues(float newValue)
     {
         newZoomValue = newValue;

         Renderer renderer = Quad.GetComponent<Renderer>();
         Material mat = renderer.material;
         mat.SetVector("_Zoom", new Vector4(newValue, newValue, 1, 1));
     }

     public void PanValues(float newValue)
     {
         newPanValue = newValue;

         Renderer renderer = Quad.GetComponent<Renderer>();
         Material mat = renderer.material;
         mat.SetVector("_Pan", new Vector4(newValue, 0, 1, 1));
     }*/
}
