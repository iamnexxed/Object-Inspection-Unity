using UnityEngine;

[ExecuteInEditMode]
public class Fluid : MonoBehaviour
{
    public GameObject fluidStartObject;
    public GameObject fluidEndObject;
    public GameObject fluidLengthObject;

    float fluidWidth;
    SpriteRenderer fluidLengthRenderer;

    void Start()
    {
        fluidLengthObject.transform.position = fluidStartObject.transform.position;
        fluidEndObject.transform.position = fluidStartObject.transform.position;
        fluidLengthRenderer = fluidLengthObject.GetComponent<SpriteRenderer>();
        if(fluidLengthRenderer == null)
        {
            Debug.LogWarning("Fluid Length Sprite Renderer not found!");
        }
    }

    
    void Update()
    {
        fluidWidth = Vector2.Distance(fluidStartObject.transform.position, fluidEndObject.transform.position);
        fluidWidth /= 3.946f;
        fluidLengthRenderer.size = new Vector2(fluidWidth, fluidLengthRenderer.size.y);
    }
}
