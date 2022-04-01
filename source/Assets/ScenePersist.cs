using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenePersist : MonoBehaviour
{
    int startingSceneIndex;

    private void Awake()
    {
        int scenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (scenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        
        // Update is called once per frame
        void Update()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex != startingSceneIndex)
            {
                Destroy(gameObject);
            }
        }
    }
}