using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float sensitivity;
    [SerializeField] float zoomedSensitivity;

    private float cameraSpeed;
    [SerializeField] private float cameraDistance;
    [SerializeField] Camera cam;
    private float mouseX;
    private float mouseY;
    private bool zoomInput;
    private GameObject pivotPoint;
    private GameObject aimPoint;
    private RaycastHit hit;
    private Vector3 aimPointOffset;


   // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameManager>();
        aimPoint = GameObject.FindGameObjectWithTag("AimPoint");
        pivotPoint = GameObject.Find("Pivotpoint");
    }

    private void Update()
    {
        if (GameManager.isGamePaused == false)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }
        transform.LookAt(pivotPoint.transform);
        transform.Translate(Vector3.up * -mouseY * cameraSpeed);
        transform.Translate(Vector3.right * -mouseX * cameraSpeed);
        if ((pivotPoint.transform.position - transform.position).magnitude > cameraDistance)
        {
            transform.Translate(new Vector3(0,0,1));
        }
        if(zoomInput)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 50, 10*Time.deltaTime);
            cameraSpeed = zoomedSensitivity;
        }
        else if (!zoomInput)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90, 10*Time.deltaTime);
            cameraSpeed = sensitivity;
        }
        Physics.Raycast(this.transform.position, pivotPoint.transform.position - this.transform.position);
        var ray = new Ray(this.transform.position, pivotPoint.transform.position - this.transform.position);
        Debug.DrawRay(this.transform.position, pivotPoint.transform.position - this.transform.position);

       if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                aimPointOffset = new Vector3(0, -0.1f, 0);
                aimPoint.transform.position = hit.point - aimPointOffset;
            }
        }
    }
}
