using System;

namespace Assets.Scripts.Core
{
    public class BindableProperty<T> : IBindable<T>, IDisposable
    {
        private T _value;
        private readonly string _name;

        public BindableProperty(T value, string name)
        {
            Value = value;
            _name = name;
        }

        public event Action<string,T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                {
                    return;
                }
                _value = value;
                Action<string, T> handler = OnValueChanged;
                handler?.Invoke(_name, value);
            }
        }

        public void Dispose()
        {
            OnValueChanged = null;
        }
    }
}