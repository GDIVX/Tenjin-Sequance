using Combat;
using UnityEngine;

namespace Game.Combat
{
    public interface ITarget : IDestroyable
    {
        Vector3 Position { get; }
        GameObject GameObject { get; }
        IDamageable Damageable { get; }
        bool CompareTag(string tag);
    }
}