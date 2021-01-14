using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 1.0f;
    [SerializeField]
    private Vector3 playerVelocity;
    
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool _isReloading = false;

    [SerializeField]
    public bool hasCoin = false;

    private UIManager _uiManager;
    // private float fireRate = 0.5f;
    // private float nextFire;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
        currentAmmo = maxAmmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        // nextFire = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (_weapon.activeInHierarchy)
        {
            if (Input.GetMouseButton(0) && currentAmmo > 0)
            {
                // && Time.time > nextFire
                // nextFire = Time.time + fireRate;
                shoot();
            }
            else
            {
                _muzzleFlash.SetActive(false);
                _weaponAudio.Stop();
            }

            if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
            {
                _isReloading = true;
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity * -1;
        velocity = transform.transform.TransformDirection(velocity);
        
        _controller.Move(velocity * Time.deltaTime);

    }

    void shoot()
    {
        // Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0));
        _muzzleFlash.SetActive(true);
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (!_weaponAudio.isPlaying)
        {
            _weaponAudio.Play();
        }
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin,out hitInfo))
        {
            Debug.Log($"Hit: {hitInfo.transform.name}");
            Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
