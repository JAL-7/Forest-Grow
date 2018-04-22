using UnityEngine;
using System.Collections;

public class TreeParent : MonoBehaviour 
{

    float start;
    public bool centTree;
    bool destroy = false;

    void Awake()
    {
        start = Time.time;
        foreach (Transform child in transform) {
            child.gameObject.GetComponent<Renderer>().enabled = false;
        }
        StartCoroutine(CheckForCollision());
    }

    void OnTriggerEnter(Collider col)
    {
        if (/*col.tag != "Range" && */start + 0.3 > Time.time) {
            destroy = true;
        }
        /*if (col.tag == "Range") {
            centTree = true;
            GameObject.Find("Main Camera").GetComponent<TreesInZone>().trees ++;
        }*/
    }

    IEnumerator CheckForCollision() {
        yield return null;
        yield return null;
        yield return null;
        if (destroy) {
            Destroy(gameObject);
        }
        else {
            foreach (Transform child in transform) {
                child.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }

    }

}
