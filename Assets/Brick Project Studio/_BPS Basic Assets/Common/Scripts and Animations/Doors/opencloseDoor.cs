﻿using System.Collections;
using UnityEngine;


public class OpenCloseDoor : MonoBehaviour
{

    public Animator openandclose;
    public bool open;
    public Transform Player;

    void Start()
    {
        open = false;
    }
        
    //void OnMouseOver()
    //{
    //	{
    //		if (Player)
    //		{
    //			float dist = Vector3.Distance(Player.position, transform.position);
    //			if (dist < 15)
    //			{
    //				if (open == false)
    //				{
    //					if (Input.GetMouseButtonDown(0))
    //					{
    //						StartCoroutine(opening());
    //					}
    //				}
    //				else
    //				{
    //					if (open == true)
    //					{
    //						if (Input.GetMouseButtonDown(0))
    //						{
    //							StartCoroutine(closing());
    //						}
    //					}

    //				}

    //			}
    //		}

    //	}

    //}

    public void _OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(_opening());
    } 
    public void _CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(_closing());
    }

    IEnumerator _opening()
    {
        print("you are opening the door");
        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator _closing()
    {
        print("you are closing the door");
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }


}
