using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonCollorController : MonoBehaviour
{
    public Image playButton;
    public Image achievementButton;
    public Image creditButton;
    public Image quitButton;
    public Color hoverColor;
    private Color originalColor;
    public GameObject pencil1;
    public GameObject pencil2;
    public GameObject pencil3;
    public GameObject pencil4;
    Vector3 pencilPosition;
    void Start()
    {
        originalColor = Color.white;
        pencilPosition = new Vector3(-256f, 0f, 0f);
    }

    public void PointerEnterPlay()
    {
        playButton.color = hoverColor;
        pencil1.SetActive(true);
    }
    public void PointerExitPlay()
    {
        playButton.color = originalColor;
        pencil1.SetActive(false);
    }
    public void PointerEnterAchievement()
    {
        achievementButton.color = hoverColor;
        pencil2.SetActive(true);
    }
    public void PointerExitAchievement()
    {
        achievementButton.color = originalColor;
        pencil2.SetActive(false);
    }
    public void PointerEnterCredit()
    {
        creditButton.color = hoverColor;
        pencil3.SetActive(true);
    }
    public void PointerExitCredit()
    {
        creditButton.color = originalColor;
        pencil3.SetActive(false);
    }
    public void PointerEnterQuit()
    {
        quitButton.color = hoverColor;
        pencil4.SetActive(true);
    }
    public void PointerExitQuit()
    {
        quitButton.color = originalColor;
        pencil4.SetActive(false);
    }
}
