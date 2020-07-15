using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text currentCarNoText;
    public Text LevelNoText;
    public GameObject finishText;
    public GameObject levelUpText;


    private void Start()
    {
        finishText.gameObject.SetActive(false);
        levelUpText.gameObject.SetActive(false);
    }

    public void DisplayCarNo(int carNo, int totalCarNum)
    {
        currentCarNoText.text = carNo.ToString() + "/" + totalCarNum.ToString();
    }

    public void DisplayLevelNo(int levelNo)
    {
        LevelNoText.text = levelNo.ToString();
    }

    public void DisplayGameFinishText()
    {
        finishText.gameObject.SetActive(true);
    }

    public void DisplayLevelUp()
    {
        levelUpText.SetActive(true);
    }

    

}
