using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Login {
    public string user;
    public string status;
    public bool active;
    public string sessionId;

    public override string ToString()
    {
        return " user: " + user + " status: " + status.ToString() + " active: " + active.ToString() + " sessionId: " + sessionId;
    }
}
