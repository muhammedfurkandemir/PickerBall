using UnityEngine;

public class _Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 target_Offset;
  

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + target_Offset, .125f);
    }
}
