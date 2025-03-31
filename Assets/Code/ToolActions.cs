using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActions : MonoBehaviour
{
    void ActivateObject() {
        print("Se ha activado el objeto.");
    }

    public void ModifyDataOnInspector(string p_nameOfEvent){
        switch (p_nameOfEvent)
        {
            case "NONE":
                break;
            case "ACTIVATE":
                ActivateObject();
                break;
            case "DESACTIVATE":
                break;
        }
    }
}

[System.Serializable]
public enum Events
{
    NONE,
    ACTIVATE,
    DESACTIVATE
}