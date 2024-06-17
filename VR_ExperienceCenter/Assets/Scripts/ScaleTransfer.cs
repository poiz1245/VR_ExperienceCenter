using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleTransfer : MonoBehaviour
{

    public Camera mainCamera;
    public XRGrabInteractable grabInteractable;
    public CameraCenterRayCast hitObject;

    new Renderer renderer;
    float initialDistance;
    bool isGrab = false;
    Vector3 cubeSize;

    [SerializeField] Material firstRender;
    [SerializeField] Material unlit;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        cubeSize = GetComponent<Renderer>().bounds.size;
        initialDistance = Vector3.Distance(mainCamera.transform.position, transform.position); //�̰� ������Ʈ �ؾ��Ұ� ����

    }
    void Update()
    {
        if (grabInteractable.isSelected && !isGrab)
        {
            isGrab = true;
            renderer.material = firstRender;
        }
        else if (isGrab && !grabInteractable.isSelected)
        {
            renderer.material = unlit;
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
        return Mathf.Max(scale, 0.1f); //�ΰ��� ���� �� �� ū ���� ��ȯ
    }
}