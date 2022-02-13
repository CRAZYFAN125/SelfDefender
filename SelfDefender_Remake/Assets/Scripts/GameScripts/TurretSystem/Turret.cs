using System.Collections;
using UnityEngine;

namespace Crazy.TurretSystem
{
    public class Turret : MonoBehaviour
    {

        #region ShootProp
        [Header("ShootProp")]
        public float range = 10f;
        public float fireRate = 1f;
        private float fireCountdown = 0f;
        public GameObject bulletPrefab;
        public Transform[] firePoints;
        public float DamageAmount = 5f;
        [Header("If Laser")]
        public bool Laser = false;
        public LineRenderer lineRenderer;
        bool Lasering = false;

        #endregion
        #region MainProp
        [Header("Main")]
        [SerializeField] bool isRotating = true;
        private Transform target;
        public string enemyTag = "Enemy";

        public Transform partToRotate;
        #endregion

        //public Transform image;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, .5f);
        }

        void UpdateTarget()
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag(enemyTag);
            GameObject nearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemys)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < nearestEnemyDistance)
                {
                    nearestEnemyDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null && nearestEnemyDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) {
                if (Laser)
                {
                    lineRenderer.enabled = false;
                }
                return; 
             
            }

            #region Rotate
            if (isRotating)
            {
                LockOnTarget();
            }
            #endregion

            if (Laser)
            {
                LaserShoot();
            }
            else
            {
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }
        void LaserShoot()
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            if (firePoints[0]!=null)
            {
                lineRenderer.SetPosition(0, firePoints[0].position);
            }
            else
            {
                lineRenderer.SetPosition(0, new Vector3(0,0,-1));
            }
            lineRenderer.SetPosition(1, target.position);
            if (!Lasering)
            {
                Lasering = true;
                StartCoroutine(LaserShootByTime());
            }
            
        }
        IEnumerator LaserShootByTime()
        {
            if (target!=null)
            {
                Enemy x = target.GetComponent<Enemy>();
                while (target!=null)
                {
                        x.Hit(DamageAmount);
                        yield return new WaitForSeconds(2f);
                }
            }
            Lasering=false;
        }
        void LockOnTarget()
        {
            Vector2 lookDir = target.position - transform.position;
            float angleZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            partToRotate.rotation = Quaternion.Euler(0, 0, angleZ);
        }
        void Shoot()
        {
            foreach (Transform firePoint in firePoints)
            {
                Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Init(target, DamageAmount);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (range > 0f)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, range);
            }
        }
    }
}
