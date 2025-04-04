using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] GameObject ClearDoor;
    private void Awake()
    {
        int randomPoint = Random.Range(0, Points.Length);

        Instantiate(ClearDoor, Points[randomPoint].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
