using UnityEngine;

public class Player_Animations : MonoBehaviour
{
   [SerializeField] private Animator _animator;

   private void Awake()
   {
      _animator = GetComponentInChildren<Animator>();
   }

   public void SetPlayerArmed()
   {
       _animator.ResetTrigger("NoWeapons");
       _animator.SetTrigger("WeaponTaked");
   }

   public void SetPlayerUnarmed()
   {
       _animator.SetTrigger("NoWeapons");
   }
}
