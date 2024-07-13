using UnityEngine;

public class Frutto : MonoBehaviour
{
   public GameObject whole;
   public GameObject sliced;

   private Rigidbody fruttoRigidbody;
   private Collider fruttoCollider;
   private ParticleSystem juiceEffect;

   private void Awake()
    {
        fruttoRigidbody = GetComponent<Rigidbody>();
        fruttoCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direzione, Vector3 posizione, float forza)
    {
        //GameManager.Instance.IncreaseScore(points);

        whole.SetActive(false);
        sliced.SetActive(true);

        fruttoCollider.enabled = false;

        juiceEffect.Play();

        float angle = Mathf.Atan2(direzione.y, direzione.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices){
            slice.velocity = fruttoRigidbody.velocity;
            slice.AddForceAtPosition(direzione * forza, posizione , ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
