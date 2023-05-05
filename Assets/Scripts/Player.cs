using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private int _score = 0;
    private int _price = 10;
    AudioClip _clip;
    AudioSource _moneySound;
    private GameManager _gameManager;    
    private List<GameObject[]> HouseTypes = new List<GameObject[]>();
    private List<GameObject> HousesBought = new List<GameObject>();
    private GameObject HouseText;
    private GameObject SpotLight;
    
    // Start is called before the first frame update
    void Start()
    {
        _moneySound = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Text pop up").GetComponent<GameManager>();
        HouseTypes.Add (GameObject.FindGameObjectsWithTag("House"));
        HouseTypes.Add (GameObject.FindGameObjectsWithTag("Low houses"));
        HouseTypes.Add(GameObject.FindGameObjectsWithTag("Big House"));
        Debug.Log("types " + HouseTypes.Count);
        Debug.Log("houses" + HouseTypes[1].Length);
        SpotLight = GameObject.FindGameObjectWithTag("spotLight");
        SpotLight.SetActive(false);
        HouseText = GameObject.FindGameObjectWithTag("BuyHousetext");        
    }

    // Update is called once per frame
    void Update()
    {


        double min = 1.0e6;
        GameObject nearest = null;
        if (HouseTypes != null)
        {
            foreach (var Type in HouseTypes)
            {
                foreach (var House in Type)
                {
                    double distance = Vector3.Distance(House.transform.position, transform.position);
                    if (distance <= min)
                    {
                        nearest = House;
                        min = distance;
                    }
                }
            }            
        }


        if (nearest != null && min <= 15)
        {
            if (nearest.CompareTag("Low houses"))
            {
                _price = 50;
            }
            else if(nearest.CompareTag("House"))
            {
                _price = 100;
            }
            else
            {
                _price = 200;
            }

            //Debug.Log("can buy");

            SpotLight.SetActive(true);
            SpotLight.transform.position = nearest.transform.position + new Vector3(0,25,0);

            if (!HousesBought.Contains(nearest))
            {
                SpotLight.GetComponent<Light>().color = Color.green;
                _gameManager.UpdateHouseText($"Buy house for £{_price} (press Enter)");               
            }
            else
            {
                SpotLight.GetComponent<Light>().color = Color.red;
                _gameManager.UpdateHouseText("You bought this house");
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!HousesBought.Contains(nearest))
                {
                    
                    if (_score >= _price)
                    {
                        HousesBought.Add(nearest);
                        _score -= _price;
                        _gameManager.UpdateScore(_score);
                    }
                }
            }
        }
        else
        {
            //Debug.Log("not house found" + min);
            _gameManager.UpdateHouseText("");
            //SpotLight.SetActive(false);
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
