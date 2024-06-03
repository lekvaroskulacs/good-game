using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSort : MonoBehaviour
{
    [SerializeField] private List<Transform> holders;


    public void sortItems(List<Transform> items) {

        for (int i = 0; i < items.Count; i++) {
            items[i].position = holders[i].position;
        }

    }

}
