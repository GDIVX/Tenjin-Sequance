using System;

namespace Combat
{
    public interface IDamageable : IDestroyable
    {
        void HandleDamage(float damage);
        event Action<float, IDamageable> OnUpdateValue;
        bool CanBeDamaged { get; set; }
        void Init(int modelHealth);
    }
}
