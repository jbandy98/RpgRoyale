using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Encounter encounter;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector3(encounter.xLoc, encounter.yLoc, 0);
    }

}
