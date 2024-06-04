using System;

namespace Combat
{
    public interface IDestroyable
    {
        public event Action<IDestroyable> OnDestroyed;
    }
}