using UnityEngine;

public class IllusionEffect : MonoBehaviour
{
    public Transform targetObject;
    public GameObject finalObject;
    public Camera anamorphicCamera;
    public float acceptableAngle;
    public float acceptableDistance;
    public GameObject[] scatteredImages;
    public Material[] originalImage;
    
    Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 directionToTarget = targetObject.position - mainCamera.transform.position;
        float angle = Vector3.Angle(mainCamera.transform.forward, directionToTarget);
        float distance = Vector3.Distance(mainCamera.transform.position, anamorphicCamera.transform.position);

        if (angle < acceptableAngle && distance < acceptableDistance)
        {
            for (int i = 0; i < scatteredImages.Length; i++)
            {
                MeshRenderer scatteredObject = scatteredImages[i].GetComponent<MeshRenderer>();
                scatteredObject.material = originalImage[i];
            }
            
            finalObject.SetActive(true);
        }
        /*else
        {
            finalObject.SetActive(false);
        }*/
    }

}
