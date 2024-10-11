using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject FirstAchivementLocked;
    public GameObject SecondAchivementLocked;
    public GameObject ThirdAchivementLocked;
    public GameObject FirstAchivement;
    public GameObject SecondAchivement;
    public GameObject ThirdAchivement;
    void Start()
    {

    }

    public void OpenAchievement()
    {
        int firstAchivement = PlayerPrefs.GetInt("firstAchivement", 0);
        int secondAchivement = PlayerPrefs.GetInt("secondAchivement", 0);
        int thirdAchivement = PlayerPrefs.GetInt("thirdAchivement", 0);
        if (firstAchivement == 0)
        {
            FirstAchivementLocked.SetActive(true);
            FirstAchivement.SetActive(false);
        }
        else
        {
            FirstAchivementLocked.SetActive(false);
            FirstAchivement.SetActive(true);
        }
        if (secondAchivement == 0)
        {
            SecondAchivementLocked.SetActive(true);
            SecondAchivement.SetActive(false);
        }
        else
        {
            SecondAchivementLocked.SetActive(false);
            SecondAchivement.SetActive(true);
        }
        if (thirdAchivement == 0)
        {
            ThirdAchivementLocked.SetActive(true);
            ThirdAchivement.SetActive(false);
        }
        else
        {
            ThirdAchivementLocked.SetActive(false);
            ThirdAchivement.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
