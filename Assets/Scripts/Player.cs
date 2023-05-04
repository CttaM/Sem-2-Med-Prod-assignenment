using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private int _score = 0;
    AudioClip _clip;
    AudioSource _moneySound;
    private GameManager _gameManager;
    private GameObject[] Houses;
    private GameObject HouseText;

    
    // Start is called before the first frame update
    void Start()
    {
        _moneySound = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Text pop up").GetComponent<GameManager>();
        Houses = GameObject.FindGameObjectsWithTag("House");


        if (Houses != null)
        {
            Debug.Log("Houses" + Houses.Length);
        }

        HouseText = GameObject.FindGameObjectWithTag("BuyHousetext");
        
    }

    // Update is called once per frame
    void Update()
    {


        double min = 1.0e6;
        GameObject nearest = null;
        if (Houses != null)
        {
            foreach (var House in Houses)
            {
                double distance = Vector3.Distance(House.transform.position, transform.position);
                if (distance <= min)
                {
                    nearest = House;
                    min = distance;
                }
            }
        }


        if (nearest != null && min <= 15)
        {
            _gameManager.UpdateHouseText("Buy house for £100");
            Debug.Log("can buy");
        }
        else
        {
            Debug.Log("not house found" + min);
            _gameManager.UpdateHouseText("");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
       Debug.Log("hit " + collider.tag); 
        if (collider.tag == "Coin")
        {
            _score += 10;
            Debug.Log("Score " + _score);
            _gameManager.UpdateScore(_score);
            _moneySound.Play();
            Destroy(collider.gameObject);
        }
        
    }



}
