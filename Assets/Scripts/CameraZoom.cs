using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomedInFOV = 40f;
    public float zoomedOutFOV = 60f;
    public float zoomSmoothness = 8f;

    [HideInInspector] public bool hasZoomedIn;
    [HideInInspector] public bool hasZoomedOut;
    [HideInInspector] public bool canZoom;

    CinemachineVirtualCamera vcam1;

    bool isZoomed;
    float currentFOV;

    // Start is called before the first frame update
    void Start()
    {
        canZoom = true;

        vcam1 = GetComponent<CinemachineVirtualCamera>();

        if(vcam1 == null)
        {
            Debug.LogError("No Cinemachine Virtual Camera found!");
            return;
        }

        vcam1.m_Lens.FieldOfView = zoomedOutFOV;
        isZoomed = false;
        currentFOV = zoomedOutFOV;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Zoom"))
        {
            // Debug.Log("Zooming In");
            SetZoom(true);
        }
        else if(Input.GetButtonUp("Zoom"))
        {
            // Debug.Log("Zooming Out");
            SetZoom(false);
        }

        // If the object has to be inspected Camera should be automatically zoomed out
        if (vcam1.m_Lens.FieldOfView == zoomedOutFOV)
        {
            hasZoomedOut = true;
            hasZoomedIn = false;
        }
        else if (vcam1.m_Lens.FieldOfView == zoomedInFOV)
        {
            hasZoomedIn = true;
            hasZoomedOut = false;
        }

    }

    void LateUpdate()
    {
        if(!canZoom)
        {
            Debug.Log("Zoom isn't allowed right now!");
            return;
        }

        if(isZoomed)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    void ZoomIn()
    {
        currentFOV = Mathf.Lerp(currentFOV, zoomedInFOV, zoomSmoothness * Time.deltaTime);
        vcam1.m_Lens.FieldOfView = currentFOV;
    }

    void ZoomOut()
    {
        currentFOV = Mathf.Lerp(currentFOV, zoomedOutFOV, zoomSmoothness * Time.deltaTime);
        vcam1.m_Lens.FieldOfView = currentFOV;
    }

    // The SetZoom() function should only be called in the Update() method
    public void SetZoom(bool shouldZoom)          
    {
        isZoomed = shouldZoom;
    }
}
