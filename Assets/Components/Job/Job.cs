using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Job {

    // this represents a players class
    public string jobName;          // the name of the job. i.e. 'fighter', 'cleric'
    public string roles;            // the roles a class normally fills - this is used in party generation
    public string description;      // this is a description of about 150 characters for the job

    public int maxLevel;            // current max level that this job can attain

}
