using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float impactForce = 30f;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public ParticleSystem cartridgeEffect;

    public Text ammoCountUI;
    public int maxAmmo = 80;
    private int currentAmmo;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;
    public Vector3 shootPointOffset;


    public EnemyController enemyController;




    void Start()
    {
        currentAmmo = maxAmmo;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        ammoCountUI.text = currentAmmo.ToString();


        if (isReloading)
        {

            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {    
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
     
    }

    IEnumerator Reload()
    {
        isReloading = true;
        GameController.Instance.cursorController.Reload(reloadTime);
        
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }


    void Shoot()
    {
        currentAmmo--; 
        RaycastHit hit;
        if (Physics.Raycast(transform.position + shootPointOffset, transform.forward, out hit, range)) 
        {        
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target != null)
            {
                
                target.TakeDamage(damage);
                
            }
            EnemyController enemyScript = hit.collider.gameObject.GetComponent<EnemyController>();
            if (enemyScript != null)
            {
                
                enemyScript.agent.destination = transform.position;
                enemyScript.enemyState = EnemyController.EnemyState.Chase;
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
        

        muzzleFlash.Play();
        cartridgeEffect.Play();

    }

}