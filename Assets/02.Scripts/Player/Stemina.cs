using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stemina : MonoBehaviour
{
    public Slider slider;
    public PlayerMove playerMove;
    
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        slider.value = playerMove.Stamina / 100f;
    }
}
