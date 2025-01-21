using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    public Transform[] spawnPoint;
    public float fixedScale = 1.0f;

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
        //1stage
        int[] arr1 = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 5, 5}; //stage - 1기준(10 User / 5 Bomb)
        arr1 = arr1.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject newCard = Instantiate(card, spawnPoint[i].position, Quaternion.identity,transform);
            newCard.GetComponent<Card>().Setting(arr1[i]);
        }



        ////2stage
        //int[] arr2 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 5, 5, 5, 5 }; //stage - 2기준(10 User / 7 Bomb)
        //arr2 = arr2.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        //for (int i = 0; i < spawnPoint.Length; i++)
        //{
        //    GameObject newCard = Instantiate(card, spawnPoint[i].position, Quaternion.identity);
        //    newCard.GetComponent<Card>().Setting(arr2[i]);
        //}

        ////3stage
        //int[] arr3 = { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5}; //stage - 2기준(20 User / 13 Bomb)
        //arr3 = arr3.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        //for (int i = 0; i < spawnPoint.Length; i++)
        //{
        //    GameObject newCard = Instantiate(card, spawnPoint[i].position, Quaternion.identity);
        //    newCard.GetComponent<Card>().Setting(arr3[i]);
        //}

    }
}
