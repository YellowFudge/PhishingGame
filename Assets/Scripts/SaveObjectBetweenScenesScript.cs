using UnityEngine;

public class SaveObjectBetweenScenesScript : MonoBehaviour
{
    public static GameObject objectToSaveInstance;
    
    void Awake()
    {
        if(SaveObjectBetweenScenesScript.objectToSaveInstance != null && !SaveObjectBetweenScenesScript.objectToSaveInstance.Equals(gameObject))//if another already exsists destroy this instance
        {
            Destroy(gameObject);
            return;
        }

        objectToSaveInstance = gameObject;
        DontDestroyOnLoad(gameObject);
    }
}
