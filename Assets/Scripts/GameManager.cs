using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI houseText;
    public TextMeshProUGUI countText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Money: ";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Money: �" + score;
    }

    public void UpdateHouseText(string text)
    {
        houseText.text = text;
    }

    public void UpdateHouseCount (int count)
    {
        countText.text = "Houses bought: " + count;
    }
   
}
