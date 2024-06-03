using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTile : MonoBehaviour
{

    // CACHE
    [SerializeField] private GameObject tilePrefab;

    // STATE
    private bool shouldResetPosition = false;
    private int nameCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void createTile() {

        GameObject clone = Instantiate(tilePrefab, transform.position, Quaternion.identity);
        clone.name = nameCnt.ToString();
        nameCnt++;
        
        //if shouldnt reset position
        adjustPosition();
        //if should reset position TODO

    }

    private void adjustPosition() {

        transform.position = transform.position + new Vector3(0.2f, -0.2f);

    }

}
