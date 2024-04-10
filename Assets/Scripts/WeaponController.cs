using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _fireObject;
    [SerializeField] private GameObject _hitObject;

    [SerializeField] private Camera _camera;

    // Update is called once per frame
    void Update()
    {
       
        HandleFire();
    }

    void HandleFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _fireObject.Play();
            
            RaycastHit hit;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 1000))
            {
                var hitObj = Instantiate(_hitObject, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitObj, 1);
                
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 1000, Color.red, 10);
            }
        }
    }
}
