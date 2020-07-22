using TMPro;
using UnityEngine;

public class ActionKeyE : MonoBehaviour
{
    TMP_Text actionText;
    public static ActionKeyE instance;
    [HideInInspector] public bool showActionKey;
    // Start is called before the first frame update
    void Start()
    {
        // Important for the initialization of instance of an object in any new Scene
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        actionText = GetComponent<TMP_Text>();
        showActionKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(showActionKey)
        {
            actionText.enabled = true;
        }
        else
        {
            actionText.enabled = false;
        }
    }
}
