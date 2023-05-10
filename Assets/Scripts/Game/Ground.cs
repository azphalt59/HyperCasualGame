using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool haveRb = false;
    // Start is called before the first frame update
    void Start()
    {
        WallManager.Instance.Grounds.Add(this);
    }
    public void AddRb()
    {
        if (haveRb)
            return;

        haveRb = true;
        float rand = Random.Range(0.5f, 2.5f);
        Invoke("AddComp", rand);
        
    }
    public void AddComp()
    {
        gameObject.AddComponent<Rigidbody>();
        GameManager.Instance.OnLose(transform.position, 20);
    }
}
