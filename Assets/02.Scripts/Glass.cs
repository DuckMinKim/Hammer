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
        GameObject brokenGlass = Instantiate(obj, transform.position, Quaternion.identity);
        brokenGlass.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}
