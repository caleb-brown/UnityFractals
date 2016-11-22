using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
#endif
#if UNITY_STANDALONE || UNITY_WEBGL
    public float ZoomSpeed = 0.5f;
#endif

    public GameObject quad;
    // public Camera camera;

    Renderer renderer;
    Material mat;
    float zoomCounter;

    void Start()
    {
        renderer = quad.GetComponent<Renderer>();
        mat = renderer.material;
    }
    
	// Update is called once per frame
	void Update ()
    {
#if UNITY_IOS || UNITY_ANDROID

        // If there are two touches on the device
        if (Input.touchCount == 2)
        {
            // Store both touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            /// 
            /// MUST CHANGE THIS TO WORK WITH THE MATERIAL INSTEAD OF THE CAMERA!!!!!!
            /// 
            /*
            // If the camera is orthographic
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
            */

            mat.SetVector("_Zoom", new Vector4(deltaMagnitudeDiff * orthoZoomSpeed, deltaMagnitudeDiff * orthoZoomSpeed, 1, 1));


        }

#endif
#if UNITY_STANDALONE || UNITY_WEBGL

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Debug.Log("LessThan");
            mat.SetVector("_Zoom", new Vector4(mat.GetVector("_Zoom").x * -ZoomSpeed, mat.GetVector("_Zoom").x * -ZoomSpeed, 1, 1));
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("GreaterThan");
            mat.SetVector("_Zoom", new Vector4(mat.GetVector("_Zoom").x * ZoomSpeed, mat.GetVector("_Zoom").x * ZoomSpeed, 1, 1));
        }
        Debug.Log(string.Format("GetVector(\"_Zoom\") = {0}", mat.GetVector("_Zoom")));
#endif

    }
}
