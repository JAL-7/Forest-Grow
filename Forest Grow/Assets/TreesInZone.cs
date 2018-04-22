using UnityEngine;
using UnityEngine.UI;

public class TreesInZone : MonoBehaviour
{

    public Transform treePar;

    public int goal;

    public Text countDis;

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    public int trees = 0;
    int i;

    /*public void UpdateCounter() {
        countDis.text = "Trees: " + trees + "\nGoal: " + goal; 
    }*/

    public bool UpdateCounter()
    {
        trees = 0;
        foreach (Transform child in treePar)
        {
            i ++;
            if (child.position.x > minX && child.position.x < maxX && child.position.z > minZ && child.position.z < maxZ) {
                //Debug.Log(i + " " + minX + " " + maxX + " " + minZ + " " + maxZ + " " + child.position.x + " " + child.position.z + " YES");
                trees ++;
            }
            else {
                //Debug.Log(i + " " + minX + " " + maxX + " " + minZ + " " + maxZ + " " + child.position.x + " " + child.position.z + " NO");
            }
        }
        countDis.text = "Trees: " + trees + "\nTarget: " + goal; 
        if (trees >= goal) {
            return true;
        }
        else {
            return false;
        }
    }

}