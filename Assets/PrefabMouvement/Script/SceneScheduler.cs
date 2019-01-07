using UnityEngine;
using System.Collections;

public class SceneScheduler : MonoBehaviour {

    public GameObject canvas;

    private static SceneScheduler instance;

    public static SceneScheduler Instance
    {
        get
        {
            if (instance == null)
            {
                // search for object of same kind
                instance = FindObjectOfType<SceneScheduler>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<SceneScheduler>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this as SceneScheduler;
            DontDestroyOnLoad(transform.gameObject);
            Debug.Log(instance.gameObject.name);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { canvas.SetActive(true); }
    }

    public void Quit() { Application.Quit(); }

    public void LoadScene() { canvas.SetActive(true); }
}
