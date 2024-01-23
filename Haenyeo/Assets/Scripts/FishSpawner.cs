using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SeaItem
{
    public GameObject prefab;

    public float chance = 0;

    public float weight { set; get; }

}

public class FishSpawner : MonoBehaviour
{
    public SeaItem[] seaItems;

    public int maxItemCount = 100;
    public Vector2 min = new Vector2(-10f, 4f);
    public Vector2 max = new Vector2(10f, -46f);

    float accumulatedWeights;

    private void Awake()
    {
        CalculateWeights();
    }


    IEnumerator Start()
    {
        int count = 0;

        while( count < maxItemCount)
        {
            SpawnItem(new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)));
            count++;

            yield return new WaitForSeconds(0.01f);
        }
    }


    void CalculateWeights()
    {
        accumulatedWeights = 0;
        foreach(var item in seaItems)
        {
            accumulatedWeights += item.chance;
            item.weight = accumulatedWeights;
        }
    }

    void SpawnItem(Vector2 position)
    {
        var clone = seaItems[GetRandomIndex()];

        Instantiate(clone.prefab, position, Quaternion.identity);
    }

    int GetRandomIndex()
    {
        float random = Random.value * accumulatedWeights;

        for (int i = 0; i < seaItems.Length; ++i)
        {
            if(seaItems[i].weight >= random)
            {
                return i;
            }    
        }

        return 0;
    }

}
