using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPerfabs : MonoBehaviour {

    public GameObject prefab;
    public Vector3 range;
    public int SpawnMaxNum;
    public float spawnInterval;

    [HideInInspector]
    public int CurrentNum;

    private float time;
    private List<GameObject> objs = new List<GameObject>();

    private void Start()
    {
        for(int i=0;i < SpawnMaxNum; i++)
        {
            Spawn();
        }
        time = spawnInterval;
    }

    private void Update()
    {
        if (CurrentNum < SpawnMaxNum)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Spawn();
                time = spawnInterval;
            }
        }
        for(int i=0;i < objs.Count; i++)
        {
            if(objs[i] == null)
            {
                objs.Remove(objs[i]);
                CurrentNum--;
            }
        }
    }

    void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-0.5f, 0.5f) * range.x, Random.Range(-0.5f, 0.5f) * range.y, Random.Range(-0.5f, 0.5f) * range.z);
        pos += transform.position;
        objs.Add(Instantiate(prefab, pos, Quaternion.identity));
        CurrentNum++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, range);
    }

}
