using UnityEngine;
using System.Collections;

public class ChangeValues : MonoBehaviour
{
    public GameObject Quad;

    Renderer renderer; 
    Material mat;

    void Start()
    {
        renderer = Quad.GetComponent<Renderer>();
        mat = renderer.material;
    }

    public void IterationsValue(float newValue)
    {
        mat.SetFloat("_Iterations", newValue);
    }

    public void ZoomValues(float newValue)
    {
        mat.SetVector("_Zoom", new Vector4(newValue, newValue, 1, 1));
    }

    public void PanValues(float newValue)
    {
        mat.SetVector("_Pan", new Vector4(newValue, 0, 1, 1));
    }
}
