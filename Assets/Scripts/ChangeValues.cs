using UnityEngine;
using System.Collections;

public class ChangeValues : MonoBehaviour
{
    public GameObject Quad;

    public void IterationsValue(float newValue)
    {
        Renderer renderer = Quad.GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetFloat("_Iterations", newValue);
    }

    public void ZoomValues(float newValue)
    {
        Renderer renderer = Quad.GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetVector("_Zoom", new Vector4(newValue, newValue, 1, 1));
    }

    public void PanValues(float newValue)
    {
        Renderer renderer = Quad.GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetVector("_Pan", new Vector4(newValue, 0, 1, 1));
    }
}
