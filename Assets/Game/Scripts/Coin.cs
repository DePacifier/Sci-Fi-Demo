using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip _coinPickUp;
    private Player _player;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            throw new Exception("Can't find UIManager");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player = other.GetComponent<Player>();
                if (_player != null)
                {
                    _player.hasCoin = true;
                    _uiManager.ShowCoin();
                    AudioSource.PlayClipAtPoint(_coinPickUp, transform.position, 1f); //Camera.main.transform.position
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
