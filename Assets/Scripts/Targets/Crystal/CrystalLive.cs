using UnityEngine;

public class CrystalLive : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        transform.eulerAngles = new Vector3(0, 0, 90);
    }

    public void TurnOn()
    {
        _animator.SetBool("spawned", true);
    }
}
