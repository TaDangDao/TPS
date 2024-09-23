using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigame.ZombieSurvival
{
    public class EnemyStats : MonoBehaviour
    {
        // Start is called before the first frame update]
        public EnemyScriptableObject enemyData;
        [HideInInspector]
        public float currentSpeed;
        [HideInInspector]
        public float currentHealth;
        [HideInInspector]
        public float currentDamage;
        public event EventHandler onDead;
        public static int count = 0;
        private void Awake()
        {
            count++;
            currentDamage = enemyData.damage;
            currentHealth = enemyData.maxHealth;
            currentSpeed = enemyData.moveSpeed;
        }
        private void Update()
        {
            Debug.Log("enemy" + currentHealth);
        }
        public void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            gameObject.SetActive(false);
            onDead?.Invoke(this, EventArgs.Empty);
            count--;
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
                player.TakeDmg(currentDamage);
            }
        }
    }
}
