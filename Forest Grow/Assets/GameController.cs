using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{

    int currentLevel = 0;
    public GameObject canvas;

    public GameObject canvasB;

    bool canvasOp;
    bool lvlCompleteA;
    [HideInInspector] public bool lvlComplete;
    bool beatScreen;

    public GameObject bf;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(canvasB);
    }

    void Update() {
        if (currentLevel == 0)
        {
            if (Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene("Intros");
                currentLevel = -1;
            }
        }
        if (canvasOp) {
            if (Input.GetMouseButtonDown(0)) {
                 SceneManager.LoadScene("Level " + currentLevel);
                canvas.SetActive(false);
                canvasOp = false;
            }
        }
        if (lvlComplete) {
            SceneManager.LoadScene("Empty");
            canvasB.SetActive(true);
            canvasB.transform.GetChild(0).GetComponent<Text>().text = "Level " + currentLevel + " Complete";
            lvlCompleteA = true;
            StartCoroutine(TurnOffLCA());
            lvlComplete = false;
            beatScreen = true;
            currentLevel ++;
        }
        if (beatScreen) {
            if (Input.GetMouseButtonDown(0)) {
                canvasB.SetActive(false);
                beatScreen = false;
                SceneManager.LoadScene("Level " + currentLevel);
            }
        }
    }

    IEnumerator TurnOffLCA() {
        yield return new WaitForSeconds(0.5f);
        lvlCompleteA = false;
    }

    public void LoadLevelOne() {
        SceneManager.LoadScene("Level 1");
        currentLevel = 1;
    }

    public void Death()
    {
        StartCoroutine(DeathIE());
    }

    IEnumerator DeathIE() {
        yield return new WaitForSeconds(1.4f);
        if (!lvlCompleteA) {
            bf = GameObject.FindWithTag("MainCamera");
            bf.GetComponent<BulletFire>().currentBullets = bf.GetComponent<BulletFire>().initBullets;
            canvas.SetActive(true);
            canvasOp = true;
            SceneManager.LoadScene("Empty");
        }
    }

}
