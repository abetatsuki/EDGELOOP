using Develop.Gun.Interface;
using UnityEngine;
namespace Develop.Player.Entity
{
    public class HealthEntity 
    {
        public HealthEntity(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
        public int MaxHealth { get; }
        public int CurrentHealth { get; private set; }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            Debug.Log($"Took {damage} damage, current health: {CurrentHealth}/{MaxHealth}");
        }
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            Debug.Log($"Healed {amount}, current health: {CurrentHealth}/{MaxHealth}");
        }
    }
}
