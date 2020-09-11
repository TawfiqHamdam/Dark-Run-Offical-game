using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    bool spawned = false;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (spawned == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Instantiate(zombie);
                spawned = true;
            }
        }
    }
}
