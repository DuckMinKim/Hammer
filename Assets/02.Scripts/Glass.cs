using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] GameObject obj;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Breaking()
    {
        Instantiate(obj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
