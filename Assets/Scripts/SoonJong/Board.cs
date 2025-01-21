using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    public Transform[] spawnPoint;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
    }

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        int[] arr = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 5, 5}; //stage - 1±âÁØ(10 User / 5 Bomb)
        arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();


        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject newCard = Instantiate(card, spawnPoint[i].position, Quaternion.identity);
            newCard.GetComponent<Card>().Setting(arr[i]);
        }
    }
}
