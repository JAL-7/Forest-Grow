using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BulletFire : MonoBehaviour
{

    public int initBullets;
    [HideInInspector] public int currentBullets;

    public float horizontalSpeed;
    public float verticalSpeed;

    float coolDownStart;

    public TreesInZone tz;

    public Transform treePar;

    int[] tree = new int[8];

    public GameObject seedExplosion;

    public GameObject[] trees = new GameObject[5];
    Vector3[] newTreePos = new Vector3[8];

    float minAngle = -15;
    float maxAngle = 25;

    GameObject megaController;

    public Text bullet;

    bool deathFired;

    public GameObject mesh;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        currentBullets = initBullets;
        bullet.text = "Bullets Left: " + currentBullets;
        megaController = GameObject.Find("MegaController");
        tz = gameObject.GetComponent<TreesInZone>();
    }

    void Update()
    {

        if (currentBullets == 0 && !deathFired)
        {
            megaController.GetComponent<GameController>().Death();
            deathFired = true;
        }

        transform.RotateAround(transform.right, Time.deltaTime * verticalSpeed * -Input.GetAxis("Mouse Y"));

        transform.RotateAround(transform.up, Time.deltaTime * Input.GetAxis("Mouse X"));

        Vector3 currentRotation = transform.localRotation.eulerAngles;

        currentRotation.z = 0;

        currentRotation.x = Mathf.Clamp(((currentRotation.x + 540) % 360) - 180, minAngle, maxAngle);
        
        transform.eulerAngles = currentRotation;

        if (Input.GetMouseButtonDown(0) && coolDownStart + 0.3 < Time.time && currentBullets > 0)
        {
            coolDownStart = Time.time;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                Instantiate(seedExplosion, hit.point, Quaternion.identity);
                if (hit.transform.gameObject.tag == "NewTree")
                {
                    /*if (hit.transform.gameObject.GetComponent<TreeParent>().centTree) {
                        tz.trees --;
                    }*/
                    Destroy(hit.transform.gameObject);
                    StartCoroutine(SpawnTrees(hit.transform.position));

                }
                else if (hit.transform.gameObject.tag != "Finish")
                {
                    /*if (hit.transform.parent.gameObject.GetComponent<TreeParent>().centTree) {
                        tz.trees --;
                    }*/
                    Destroy(hit.transform.parent.gameObject);
                    StartCoroutine(SpawnTrees(hit.transform.parent.position));
                }
            }
            currentBullets --;
            bullet.text = "Bullets Left: " + currentBullets;
        }

    }

    IEnumerator SpawnTrees(Vector3 start) {

        yield return new WaitForSeconds(1f);

        newTreePos[0] = new Vector3(start.x+8,start.y,start.z);
        newTreePos[1] = new Vector3(start.x+8,start.y,start.z+8);
        newTreePos[2] = new Vector3(start.x,start.y,start.z+8);
        newTreePos[3] = new Vector3(start.x-8,start.y,start.z);
        newTreePos[4] = new Vector3(start.x-8,start.y,start.z-8);
        newTreePos[5] = new Vector3(start.x,start.y,start.z-8);
        newTreePos[6] = new Vector3(start.x-8,start.y,start.z+8);
        newTreePos[7] = new Vector3(start.x+8,start.y,start.z-8);

        int path = 0;

        for (int a = 0; a < 8; a ++) {
            tree[a] = Random.Range(0,5);
            path = 600 * (int)(newTreePos[a].x) + (int)(newTreePos[a].z) * 6;
            if (path > 0) {
                newTreePos[a].y = trees[tree[a]].transform.position.y + 
                                    mesh.GetComponent<MeshFilter>().mesh.vertices[path].y + 2.1f;
            }
        }

        for (int i = 0; i < 8; i++) {
            if (path > 0) {
                Instantiate(trees[tree[i]], newTreePos[i], Quaternion.identity, treePar);
            }
        }

        yield return new WaitForSeconds(0.3f);

        bool x = tz.UpdateCounter();

        if (x) {
            megaController.GetComponent<GameController>().lvlComplete = true;
        }


    }

    

}
