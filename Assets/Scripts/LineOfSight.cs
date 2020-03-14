

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class LineOfSight : MonoBehaviour
{
    public TMP_Text objectName;                              // Text Mesh Pro- Text Component on UI

    public PostProcessVolume postProcessVolume;              // PostProcessVolume Component
    public Image aimImage;                                   // Image Script for Aim Object in UI
    public float raycastLength = 7.0f;                       // Length of player arm
    public float distanceOfObjectFromCamera = 3f;            // Object Inspection distance from Camera

    public int foregroundLayerNumber = 9;
    public int backgroundLayerNumber = 10;

    // To be changed for different versions
    public ForwardRendererData forwardRenderer;              // ForwardRenderer Data Asset   

    public MouseLook mouse;                                  // To access the parameters of MouseLook Script
    RotateObject rotateObject;

    public Sprite unFocussedAim;                             // Aim Sprite when Raycast does not hit an interactable object
    public Sprite focussedAim;                               // Aim Sprite when Raycast hits an interactable object
    public LayerMask mask;                                   // What should the Raycast hit?

    RaycastHit vision;
    bool isGrabbed;
    Rigidbody grabbedObject;
    Transform positionInEnvironment;                         // Previous position of object in the Environment
    MeshRenderer grabbedObjectRenderer;

    // To be changed for different versions
    RenderObjects objectToInspect;

    // Start is called before the first frame update
    void Start()
    {
        objectToInspect = (RenderObjects)forwardRenderer.rendererFeatures[0];
        objectName.text = "";

        postProcessVolume.enabled = false;

        if (objectToInspect.settings == null)
        {
            Debug.LogWarning("Foreground Object Renderer not found!");
            return;
        }
        else
        {
            objectToInspect.settings.depthCompareFunction = CompareFunction.LessEqual;
            objectToInspect.Create();
            Debug.Log("Renderer connected! : " + objectToInspect.name);

            // Set Mask for blur background shader to Nothing and create the Object
        }

        Camera.main.transform.Rotate(Quaternion.identity.eulerAngles);
        isGrabbed = false;
        aimImage.sprite = unFocussedAim;
    }

    // Update is called once per frame
    void Update()
    {
        

        Debug.DrawRay(transform.position, transform.forward * raycastLength, Color.red, 0.5f);

        if(Physics.Raycast(transform.position, transform.forward, out vision, raycastLength, mask))
        {
            if(vision.collider.CompareTag("Interactable"))
            {
                objectName.text = vision.collider.name;
                aimImage.sprite = focussedAim;

                // Debug.Log("Raycast Hit : " + vision.collider.name);

                if(Input.GetButtonDown("PickUp") && !isGrabbed)       // Rename the Input in Project Settings for LMB to PickUp
                {
                    objectName.text = "";
                    
                    aimImage.color = new Vector4(aimImage.color.r, aimImage.color.g, aimImage.color.b, 0);                    

                    grabbedObject = vision.rigidbody;
                    rotateObject = grabbedObject.GetComponent<RotateObject>();

                    // Disable Cast and Receive shadows
                    grabbedObjectRenderer = grabbedObject.GetComponent<MeshRenderer>();
                    grabbedObjectRenderer.receiveShadows = false;
                    grabbedObjectRenderer.shadowCastingMode = ShadowCastingMode.Off;


                    if (grabbedObject.transform.parent == null)
                    {
                        Debug.LogError("The Object Doesn't have a parent");
                        return;
                    }

                    positionInEnvironment = rotateObject.objectParent;
                    grabbedObject.isKinematic = true;
                    
                    grabbedObject.transform.SetParent(gameObject.transform);
                   
                    grabbedObject.transform.localPosition = new Vector3(0, 0, distanceOfObjectFromCamera);
                    grabbedObject.transform.localRotation = Quaternion.identity;

                  
                    // Debug.Log("Position of Cube after assignment: " + grabbedObject.transform.position);

                    isGrabbed = true;

                    InspectObject();
      
                }

                else if(isGrabbed && Input.GetButtonDown("PutDown"))              // Rename the Input in Project Settings for RMB to PutDown
                {
                    objectName.text = "";
                    
                    aimImage.color = new Vector4(aimImage.color.r, aimImage.color.g, aimImage.color.b, 1);
                    grabbedObject.transform.SetParent(positionInEnvironment.transform);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity;
                    grabbedObject.isKinematic = false;

                    // Enable Cast and Receive shadows
                    grabbedObjectRenderer.receiveShadows = true;
                    grabbedObjectRenderer.shadowCastingMode = ShadowCastingMode.On;



                    isGrabbed = false;

                    StopInspectingObject();

                }
            }

            else if(!vision.collider.CompareTag("Interactable"))
            {
                objectName.text = "";
                aimImage.sprite = unFocussedAim;

                // Debug.Log("Raycast has hit : " + vision.collider.name);

                if (isGrabbed && Input.GetButtonDown("PutDown"))              // Rename the Input in Project Settings for RMB to PutDown
                {
                    objectName.text = "";
                    aimImage.color = new Vector4(aimImage.color.r, aimImage.color.g, aimImage.color.b, 1);
                    grabbedObject.transform.SetParent(positionInEnvironment.transform);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity;
                    grabbedObject.isKinematic = false;

                    // Enable Cast and Receive shadows
                    grabbedObjectRenderer.receiveShadows = true;
                    grabbedObjectRenderer.shadowCastingMode = ShadowCastingMode.On;



                    isGrabbed = false;

                    StopInspectingObject();

                }
            }
        }
        else
        {
            objectName.text = "";
            aimImage.sprite = unFocussedAim;
        }




    }

    void InspectObject()
    {
        
        postProcessVolume.enabled = !postProcessVolume.enabled;

        grabbedObject.gameObject.layer = foregroundLayerNumber;      // Set layer Foreground
        objectToInspect.settings.depthCompareFunction = CompareFunction.Disabled;
        objectToInspect.Create();
        // Debug.Log("Renderer setting changed! : " + objectToInspect.settings.depthCompareFunction.ToString());

        // Set Mask for blur background shader to Background and create the Object

        mouse.DisableLook();
        PlayerMovement.instance.canMove = false;
        rotateObject.canRotate = true;
        Cursor.visible = false;
        objectName.enabled = false;
    }

    void StopInspectingObject()
    {
        postProcessVolume.enabled = !postProcessVolume.enabled;

        grabbedObject.gameObject.layer = backgroundLayerNumber;  // Set layer Background
        objectToInspect.settings.depthCompareFunction = CompareFunction.LessEqual;
        objectToInspect.Create();
        // Debug.Log("Renderer setting changed! : " + objectToInspect.settings.depthCompareFunction.ToString());

        // Set Mask for blur background shader to Nothing and create the Object

        PlayerMovement.instance.canMove = true;
        rotateObject.canRotate = false;
        Cursor.visible = false;
        mouse.EnableLook();
        objectName.enabled = true;
    }

   
}
