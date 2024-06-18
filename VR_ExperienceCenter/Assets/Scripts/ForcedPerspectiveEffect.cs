using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ForcedPerspectiveEffect : MonoBehaviour
{

    public Camera mainCamera;
    public CameraCenterRayCast hitObject;


    XRGrabInteractable grabInteractable;
    new Renderer renderer;
    float initialDistance;
    bool isGrab = false;
    Vector3 cubeSize;

    [SerializeField] Material unlitMaterial;
    [SerializeField] Material[]  afterGrabMaterials;
    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if (grabInteractable.isSelected && !isGrab)
        {
            isGrab = true;
            cubeSize = GetComponent<Renderer>().bounds.size;
            initialDistance = Vector3.Distance(mainCamera.transform.position, transform.position); //이거 업데이트 해야할거 같음
            renderer.materials = afterGrabMaterials;
            print(initialDistance);
        }
        else if (isGrab && !grabInteractable.isSelected)
        {
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
    }

    float CalculateScaleFactor(float distance)
    {
        float scale = distance / initialDistance;
        return Mathf.Max(scale, 0.01f); //두개의 숫자 중 더 큰 값을 반환
    }
}