using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealDamage : MonoBehaviour
{
   [SerializeField] private int damageToDeal = 5;
   [SerializeField] private bool destroyOneHit = true;
   public UnityEvent onDealingDamage;


   private void OnTriggerEnter2D(Collider2D other)
   {
      if (gameObject.layer ==  other.gameObject.layer )
      {
         return;
      }
      
      TakeDamage _takeDamage;
      if (other.TryGetComponent(out _takeDamage))
      {
         onDealingDamage.Invoke();
         if (destroyOneHit)
         {
            _takeDamage.TakeDestructiveDamage();
            if (gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
               Destroy(gameObject);
            }
         }
         else
         {
            _takeDamage.TakeDamageAmount(damageToDeal);
         }
      }
      
   }

}
