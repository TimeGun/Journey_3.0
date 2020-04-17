using UnityEngine;

public class HitGroundKinematic : MonoBehaviour
{
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            _rb.isKinematic = true;
            Destroy(this);
        }
    }
}
