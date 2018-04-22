using UnityEngine;
using System.Collections;

public class DestroySystem : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(DestroyOnCompletion());
    }

    IEnumerator DestroyOnCompletion()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
