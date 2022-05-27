using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumization : MonoBehaviour
{
    public Image buttonGlasses;
    public Image buttonBoots;

    public Toggle toogleGlasses;
    public Toggle toogleBoots;
    public Renderer galassesObject;
    public Renderer bootsObject;
    public bool Glasses {get;set;}
    public bool Boots {get;set;}
    public void ColorChangeGlasses()
    {
       buttonGlasses.color = new Color(Random.value, Random.value, Random.value);;
       galassesObject.material.color = buttonGlasses.color;
    }

    public void ColorChangeBoots()
    {
       buttonBoots.color = new Color(Random.value, Random.value, Random.value);;
       bootsObject.material.color = buttonBoots.color;
    }

    void Update()
    {
        if(Glasses == true)
        {
            galassesObject.gameObject.SetActive(true);
            toogleGlasses.isOn = true;
        }
        else
        {
            galassesObject.gameObject.SetActive(false);
            toogleGlasses.isOn = false;
        }

        if(Boots == true)
        {
            bootsObject.gameObject.SetActive(true);
            toogleBoots.isOn = true;
        }
        else
        {
            bootsObject.gameObject.SetActive(false);
            toogleBoots.isOn = false;
        }

    }

    

    
}
