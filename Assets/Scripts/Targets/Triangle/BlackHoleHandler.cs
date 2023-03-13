using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHoleHandler : MonoBehaviour
{
    private List<BlackHore> _blackHores = new ();
    private TargetManager _targetManager;

    private bool _coreHasTouched;
    private bool _readyToRemove = true;

    private void Awake()
    {
        var randomAngle = Random.Range(-180, 180);
        transform.eulerAngles = new Vector3(0, 0, randomAngle);

        _blackHores = new List<BlackHore>(GetComponentsInChildren<BlackHore>());
        _targetManager = GetComponentInParent<TargetManager>();
    }

    public bool ReadyToRemove => _readyToRemove;

    private void Update()
    {
        if (_coreHasTouched && !GetComponentInChildren<BlackHore>())
        {
            _readyToRemove = true;
        }
    }

    public void CoreTouched()
    {
        _coreHasTouched = true;
        
        foreach (var blackHore in _blackHores)
        {
            if (!blackHore.HasTouched)
            {
                blackHore.TouchCore();
            }
        }
    }

    public void Spawn()
    {
        _readyToRemove = false;
        foreach (var blackHore in _blackHores)
        {
            blackHore.Play();
        }
    }
    
}
