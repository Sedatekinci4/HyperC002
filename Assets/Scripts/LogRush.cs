﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LogRush : MonoBehaviour
{
    public static LogRush instance;
    public float movementDelay = 0.25f;
    public int cubeCount= 0;
    public int deletion;
    public int deletionCount = 0;

    public List<GameObject> cubes = new List<GameObject>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            MoveListElements();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            MoveOrigin();
        }
    }

    public void StackCube(GameObject other, int index)
    {
        other.transform.parent = transform;
        Vector3 newPos = cubes[index].transform.localPosition;
        newPos.z += 1;
        other.transform.localPosition = newPos;
        cubes.Add(other);
        cubeCount++;
        Debug.Log("yoyo");

        StartCoroutine(ObjectsBigger());
    }

    private IEnumerator ObjectsBigger()
    {
        for(int i = cubes.Count - 1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1, 1, 1);
            scale *= 1.5f;

            cubes[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            cubes[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(0.05f);
        }
    }


    private void MoveListElements()
    {
        for (int i = 1 ; i < cubes.Count; i++)
        {
            Vector3 pos = cubes[i].transform.localPosition;
            pos.x = cubes[i - 1].transform.localPosition.x;
            cubes[i].transform.DOLocalMove(pos, movementDelay);
        }
    }

    private void MoveOrigin()
    {
        for (int i = 1; i < cubes.Count; i++)
        {
            Vector3 pos = cubes[i].transform.localPosition;
            pos.x = cubes[0].transform.localPosition.x;
            cubes[i].transform.DOLocalMove(pos, 0.70f);
            
        }
    }

    public void Delete(int dex)
    {
        Destroy(cubes[dex], 0.3f);
        cubes.RemoveAt(dex);
        Debug.Log(dex);
    }

}
