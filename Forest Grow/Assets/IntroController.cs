using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{

    int slide;

    public Text txt;
    public Image img;
    public Image img1;
    public Image img2;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            slide ++;
        }

        if (slide == 1) {
            txt.text = "When you shoot a tree it will be destroyed but it will also spawn new trees";
            txt.gameObject.transform.localPosition = new Vector3(0,150,0);
            img.gameObject.SetActive(false);
        }

        if (slide == 2) {
            txt.text = "The new trees will be spawned in a square around the old tree, so try find trees in open space";
            txt.rectTransform.sizeDelta = new Vector2(650,300);
            img.gameObject.SetActive(true);
            img2.gameObject.SetActive(true);
            img.transform.localPosition = new Vector3(180,-10,0);
        }

        if (slide == 3) {
            txt.text = "You have a limited number of bullets, so think through where you use them";
            img.gameObject.SetActive(false);
            txt.transform.GetChild(0).gameObject.SetActive(false);
            img2.gameObject.SetActive(false);
            img1.gameObject.SetActive(true);
        }

        if (slide == 4) {
            txt.text = "Shoot trees that are further away from you to avoid getting trapped";
            img1.gameObject.SetActive(false);
        }

        if (slide == 5) {
            txt.text = "Note: There is a short bullet cooldown and a couple seconds between running out of bullets and the game resetting";
        }

        if (slide == 6) {
            txt.text = "Good luck and have fun";
        }

        if (slide > 6) {
            GameObject.Find("MegaController").GetComponent<GameController>().LoadLevelOne();
        }
    }

}
