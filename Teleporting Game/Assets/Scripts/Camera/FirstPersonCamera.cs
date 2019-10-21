using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Rotation Values")]

    public float RotationSpeed = 2.5f;
    public float RotationSmoothing = 0.1f;

    public Vector2 XClampMinMax;

    [Space(10)]
    [Header("Dampening")]
    public float LookSmoothDamp = 2;

    [Space(10)]
    [Header("Axis Names")]
    public string XAxisName;
    public string YAxisName;

    public Camera cam;

    private void Awake()
    {
        LockCursor();
        cam = GetComponent<Camera>();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

  

    public void UpdateCameraRotation()
    {
        m_XRotation += Input.GetAxis(YAxisName) * RotationSpeed * Time.deltaTime;
        m_YRotation += Input.GetAxis(XAxisName) * RotationSpeed * Time.deltaTime;

        m_XRotation = Mathf.Clamp(m_XRotation, XClampMinMax.x, XClampMinMax.y);
       

        CurrentYRotation = Mathf.SmoothDamp(CurrentYRotation, m_YRotation, ref m_YRotationV, LookSmoothDamp);
        CurrentXRotation = Mathf.SmoothDamp(CurrentXRotation, m_XRotation, ref m_XRotationV, LookSmoothDamp);

        ApplyRotationChange(new Vector3(-CurrentXRotation, CurrentYRotation, 0));

    }

    public void ApplyRotationChange(Vector3 eulerChanges)
    {
        if (eulerChanges == new Vector3(0, 0, -1))
        {
            Debug.Log(eulerChanges);
        }
        transform.rotation = Quaternion.Euler(eulerChanges.x, eulerChanges.y, eulerChanges.z);
    }

    public void SetYRotation(float y)
    {
        m_YRotation = y;
    }



    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
   


    public Ray GetCenterCameraRay()
    {
        return cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    }


    public float CurrentXRotation { get; private set; }
    public float CurrentYRotation { get; private set; }

    private float m_XRotation = 0;
    private float m_YRotation = 0;

    private float m_XRotationV = 0;
    private float m_YRotationV = 0;


}
