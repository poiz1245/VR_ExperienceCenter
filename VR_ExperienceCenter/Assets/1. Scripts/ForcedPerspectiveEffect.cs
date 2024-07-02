using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(FollowPlayer))]
[RequireComponent(typeof(XRGrabInteractable))]
public class ForcedPerspectiveEffect : MonoBehaviour
{
    public Material[] unlitMaterial;
    public Material[] afterGrabMaterials;
    public float maxScale;
    public float minScale;

    Camera mainCamera;
    CameraCenterRayCast hitObject;
    XRGrabInteractable grabInteractable;
    XRInteractorLineVisual lineVisual;
    new Renderer renderer;
    Rigidbody rigid;

    float initialDistance;
    bool isGrab = false;
    Vector3 cubeSize;


    private void Start()
    {
        rigid  = GetComponent<Rigidbody>();
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

            renderer.materials = unlitMaterial;

            transform.position = hitObject.hitPoint;

            AdjustScale();

            Vector3 adjustment = hitObject.normal * (cubeSize.magnitude / 2);
            Vector3 newCubePosition = hitObject.hitPoint + adjustment;


            transform.position = newCubePosition;

            print(transform.position);
            gameObject.layer = 0;

            isGrab = false;
        }
    }

    void AdjustScale()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
        float scaleFactor = CalculateScaleFactor(distance);

        Vector3 previousScale = transform.localScale; //크기 조정되기 전 스케일

        transform.localScale *= scaleFactor; //크기조정

        Vector3 currentScale = transform.localScale; //크기 조정된 스케일

        currentScale = Vector3.Max(currentScale, new Vector3(minScale, minScale, minScale)); // 최소값
        currentScale = Vector3.Min(currentScale, new Vector3(maxScale, maxScale, maxScale)); //최댓값

        transform.localScale = currentScale; // 최종 스케일

        float deltaScale = currentScale.x / previousScale.x; //조정 전 스케일과 조정 후 스케일 변화량

        rigid.mass *= deltaScale; //스케일 변화량 만큼 무게 증감
    }

    float CalculateScaleFactor(float distance)
    {
        float scale = distance / initialDistance;
        return Mathf.Max(scale, 0.1f);
    }
}