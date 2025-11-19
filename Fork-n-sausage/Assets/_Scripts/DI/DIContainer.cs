using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class DIContainer
{
    private readonly Dictionary<Type, object> _bindings = new();
    private readonly List<ITickable> _tickables = new();

    public void BindInstance<T>(T implementation)
    {
        var type = typeof(T);
        if (_bindings.ContainsKey(type))
        {
            Debug.LogWarning($"Overwriting binding for {type}");
        }
        if (implementation is ITickable tickable && !_tickables.Contains(tickable))
        {
            _tickables.Add(tickable);
        }

        _bindings[type] = implementation;
    }

    public void InjectInto<T>(T target)
    {
        var fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (Attribute.IsDefined(field, typeof(InjectAttribute)))
            {
                var fieldType = field.FieldType;

                if (_bindings.TryGetValue(fieldType, out var dependency))
                {
                    field.SetValue(target, dependency);
                }
                else
                {
                    Debug.Log(target);
                    throw new Exception($"Instance of {fieldType} not found");
                }
            }
        }
    }

    public void TickAll()
    {
        foreach (var tickable in _tickables)
        {
            tickable.Tick();
        }
    }

    public void DisposeAll()
    {
        foreach (var instance in _bindings.Values)
        {
            if (instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        _bindings.Clear();
    }
}
