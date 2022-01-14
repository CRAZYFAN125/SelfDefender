using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crazy.TurretSystem
{
    public class Turret : MonoBehaviour
    {
        public Transform target;
        public float range = 10f;
        public string enemyTag = "Enemy";

        public Transform partToRotate;
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
                target=null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            #region Rotate

            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;

            partToRotate.rotation = Quaternion.Euler(0,0,rotation.x);
            if (rotation.x < 180 || rotation.x > 0)
            {
                partToRotate.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                partToRotate.localScale = new Vector3(-1, 1, 1);
            }
            #endregion
        }

        private void OnDrawGizmosSelected()
        {
            if (range>0f)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, range);
            }
        }
    }
}
