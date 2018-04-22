using UnityEngine;

public class Hack : MonoBehaviour {

    GameObject temp;

    void Awake() {
        DontDestroyOnLoad(gameObject.transform.parent);
    }
    void Start() {
        temp = GameObject.Find("MegaController");
    }

    public void click() {
        temp.GetComponent<GameController>().lvlComplete = true;
    }

}
