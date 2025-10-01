using System;
using System.Collections.Generic;
using SavingSystem;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [NonSerialized] public UnityEvent<Trigger> OnTriggered = new();

    [SerializeField] private LayerMask _trigger_mask;
    [SerializeField] private bool _is_multitriggered = false;

    private bool _is_triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_is_multitriggered && _is_triggered)
            return;

        if ((_trigger_mask.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (other.GetComponent<TriggerCarrier>() == null) 
            return;

        _is_triggered = true;
        OnTriggered.Invoke(this);
    }
}