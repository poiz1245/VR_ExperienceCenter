using UnityEngine;

public class IllusionEffect : MonoBehaviour
{
    public Camera mainCamera;
    public Transform targetObject;
    public GameObject[] scatteredImages;
    public GameObject finalObject;
    public Camera anamorphicCamera;

    public float acceptableAngle;
    public float acceptableDistance;

    void Update()
    {
        Vector3 directionToTarget = targetObject.position - mainCamera.transform.position;
        float angle = Vector3.Angle(mainCamera.transform.forward, directionToTarget);
        float distance = Vector3.Distance(mainCamera.transform.position, anamorphicCamera.transform.position);

        if (angle < acceptableAngle && distance < acceptableDistance)
        {
            foreach (GameObject image in scatteredImages)
            {
                image.SetActive(false);
            }
            finalObject.SetActive(true);
        }
    }

}
