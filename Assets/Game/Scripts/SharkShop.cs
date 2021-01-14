using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    private UIManager _uiManager;
    private AudioSource winSound;
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        winSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if (_player != null && Input.GetKeyDown(KeyCode.E))
            {
                if (_player.hasCoin)
                {
                    _player.hasCoin = false;
                    _uiManager.HideCoin();
                    _player.EnableWeapon();
                    winSound.Play();
                }
                else
                {
                    Debug.Log("You have no coin!! Get out of here!!");
                }
            }
        }
    }
}
