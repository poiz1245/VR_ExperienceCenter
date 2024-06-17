using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleTransfer : MonoBehaviour
{

    public Camera mainCamera;
    public XRGrabInteractable grabInteractable;
    public CameraCenterRayCast hitObject;

    float initialDistance;
    bool isGrab = false;
    Vector3 cubeSize;

    private void Start()
    {
        cubeSize = GetComponent<Renderer>().bounds.size;
        initialDistance = Vector3.Distance(mainCamera.transform.position, transform.position);

    }
    void Update()
    {
        if (grabInteractable.isSelected && !isGrab)
        {
            isGrab = true;
        }
        else if (isGrab && !grabInteractable.isSelected)
        {
            Vector3 adjustment = hitObject.normal * (cubeSize.magnitude / 2);
            Vector3 newCubePosition = hitObject.hitPoint + adjustment;

            transform.position = newCubePosition;

            AdjustScale();
            isGrab = false;
        }
    }

    void AdjustScale()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
        float scaleFactor = CalculateScaleFactor(distance);

        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    float CalculateScaleFactor(float distance)
    {
        float scale = distance / /*3.0f*/initialDistance;
        return Mathf.Max(scale, 0.1f); //두개의 숫자 중 더 큰 값을 반환
    }
}