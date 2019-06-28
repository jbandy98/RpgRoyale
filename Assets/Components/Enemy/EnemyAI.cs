using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Encounter encounter;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector3(encounter.xLoc, encounter.yLoc, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy encountered! " + encounter.encounterId);
    }

}
