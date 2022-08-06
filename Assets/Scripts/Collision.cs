using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private int count = 0;
    public int index, x;
    //private List lst = 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            if (! LogRush.instance.cubes.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Untagged";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                count++;
                //Debug.Log("yey");
                LogRush.instance.StackCube(other.gameObject, LogRush.instance.cubes.Count - 1);
            }
        }

        if(other.gameObject.tag == "Obstacle")
        {
            if(gameObject.name != "Player")
            {
                other.gameObject.tag = "Untagged";
                index = gameObject.transform.GetSiblingIndex();
                Debug.Log(index);
                x = LogRush.instance.cubes.Count;
                Debug.Log(x);
                for (int i = index; i < x; i++)
                {
                    LogRush.instance.delete(i);
                    i--;
                }
               
            }
        }
    }
}
