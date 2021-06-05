using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SelectTille : MonoBehaviour
{
    public void OnMouseEnter()
    {
        if(GetComponent<Tile>().IsPassable(false) && GetComponent<Tile>().CanBuild)
        {
            GetComponent<Tile>().SetColor(new Color32(123, 255, 80, 255));
        }
        
	    //Node n = new Node(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
	    //print(n.Position);
    }

    
    public void OnMouseExit()
    {
        if(GetComponent<Tile>().IsPassable(false) && GetComponent<Tile>().CanBuild)
        {
            GetComponent<Tile>().ResetColor();
        }
    }
    
}
