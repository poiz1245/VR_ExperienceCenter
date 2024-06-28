using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(FollowPlayer))]
[RequireComponent(typeof(XRGrabInteractable))]
public class ForcedPerspectiveEffect : MonoBehaviour
{
    [SerializeField] Material unlitMaterial;
    [SerializeField] Material[] afterGrabMaterials;
    [SerializeField] float maxScale;
    [SerializeField] float minScale;
    Camera mainCamera;
    CameraCenterRayCast hitObject;
    XRGrabInteractable grabInteractable;
    XRInteractorLineVisual lineVisual;
    new Renderer renderer;
    float initialDistance;
    bool isGrab = false;
    Vector3 cubeSize;


    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        renderer = GetComponent<Renderer>();
        lineVisual = GameObject.FindWithTag("GameController").GetComponent<XRInteractorLineVisual>();
        hitObject = CameraCenterRayCast.instance.GetComponent<CameraCenterRayCast>();

        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (grabInteractable.isSelected && !isGrab)
        {
            isGrab = true;
            lineVisual.enabled = false;
            gameObject.layer = 11;

            cubeSize = GetComponent<Renderer>().bounds.size;
            initialDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
            renderer.materials = afterGrabMaterials;
        }
        else if (isGrab && !grabInteractable.isSelected)
        {
            lineVisual.enabled = true;

            renderer.materials = new Material[] { unlitMaterial };
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

        transform.localScale *= scaleFactor;
        
        /*Vector3 currentScale = transform.localScale;
        currentScale = Vector3.Max(currentScale, new Vector3(minScale, minScale, minScale));
        currentScale = Vector3.Min(currentScale, new Vector3(maxScale, maxScale, maxScale));
        transform.localScale = currentScale;*/
        
    }

    float CalculateScaleFactor(float distance)
    {
        float scale = distance / initialDistance;
        return Mathf.Max(scale, 0.1f);
    }
}