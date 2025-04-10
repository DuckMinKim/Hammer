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

    //public void ParticleBreaking()
    //{
    //    Instantiate(obj, transform.position, Quaternion.identity);
    //    Destroy(gameObject);

    //}

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col != null && (breakableObject & (1 << col.gameObject.layer)) != 0 && isOnPlayer)
    //    {
    //        ParticleBreaking();
    //    }
    //}
}
