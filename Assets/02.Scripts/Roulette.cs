using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] int rouletteCount;
    [SerializeField] GameObject roulette;
    //[SerializeField] GameObject[] roulettes;
    [SerializeField] List<GameObject> roulettes;
    void Start()
    {
        rouletteCount = 0;
    }


    void Update()
    {
        
    }


    public void AddRoulette()
    {
        roulettes.Add(Instantiate(roulette, transform));
        //roulettes[rouletteCount] = ;
        float zRot = 0;
        if (rouletteCount > 0)
            zRot = 360 / rouletteCount;

        roulettes[rouletteCount].GetComponent<Image>().rectTransform.rotation = Quaternion.Euler(0, 0, zRot);
        roulettes[rouletteCount].GetComponent<Image>().fillAmount = 1.0f / roulettes.Count;
        roulettes[rouletteCount].GetComponent<Image>().color = Random.ColorHSV();

        for (int i = 1; i <= rouletteCount; i++) {
            float zRot_ = 0;
            if (rouletteCount > 0)
                zRot_ = (360 / roulettes.Count * i);
            roulettes[i].GetComponent<Image>().rectTransform.rotation = Quaternion.Euler(0, 0, zRot_);
            roulettes[i].GetComponent<Image>().fillAmount = 1.0f / roulettes.Count;
        }


        rouletteCount+=1;
    }

    public void RemoveRoulette()
    {
        if(roulettes.Count > 0)
        {
            roulettes.Remove(roulettes[roulettes.Count-1]);
            Destroy(roulettes[roulettes.Count - 1].gameObject);
            rouletteCount--;

            for (int i = 1; i <= rouletteCount; i++)
            {
                float zRot_ = 0;
                if (rouletteCount > 0)
                    zRot_ = (360 / roulettes.Count * i);
                roulettes[i].GetComponent<Image>().rectTransform.rotation = Quaternion.Euler(0, 0, zRot_);
                roulettes[i].GetComponent<Image>().fillAmount = 1.0f / roulettes.Count;
            }
        }
        

    }
}
