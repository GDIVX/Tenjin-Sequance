using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public interface IBindable<T>
    {
        public event Action<string, T> OnValueChanged;

        T Value { get; set; }
    }
}