using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public float fireRate = 3f;
    public int pelletCount = 8;
    public float spreadAngle = 15f;

    private bool isAutomatic = false;
    private bool isShotgun = false;
    private float nextTimeToFire = 0f;

    void Update()
    {
        // 발사 모드 변경
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAutomatic = false;
            isShotgun = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isAutomatic = true;
            isShotgun = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isAutomatic = false;
            isShotgun = true;
        }

        // 발사 처리
        if (isAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else if (isShotgun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootShotgun();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        Destroy(bullet, 2.0f);
    }

    void ShootShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Spread calculation
            float spreadX = Random.Range(-spreadAngle, spreadAngle);
            float spreadY = Random.Range(-spreadAngle, spreadAngle);
            Vector3 spreadDirection = Quaternion.Euler(spreadY, spreadX, 0) * bulletSpawnPoint.forward;

            rb.velocity = spreadDirection * bulletSpeed;
            Destroy(bullet, 2.0f);
        }
    }
}
