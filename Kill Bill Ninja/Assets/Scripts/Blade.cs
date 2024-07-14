
using UnityEngine;

public class Blade : MonoBehaviour
{
    private bool slicing;
    private Collider bladeCollider;
    /* permette di gestire un bug sullo slice quando facciamo 
       un touch da una posizione a una diversa crea un falso slice, 
       dato dal fatto che rimane memorizzata l'ultima posizione*/
    private TrailRenderer bladeTrail;
    private Camera mainCamera;

    public Vector3 direction { get; private set; } 
    public float sliceForce = 15f;
    public float minSliceVelocity = 0.01f;


    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)) {
            StartSlicing();
        } else if (Input.GetMouseButtonUp(0)) {
            StopSlicing();
        } else if (slicing) {
            ContinueSlicing();
        }
    }
    
    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void StartSlicing(){
        Vector3 nuovaPosizione = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        nuovaPosizione.z = 0f;

        transform.position = nuovaPosizione;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }


    private void StopSlicing(){
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;

    }

    private void ContinueSlicing(){
        Vector3 nuovaPosizione = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        nuovaPosizione.z = 0f;

        direction = nuovaPosizione - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;

        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = nuovaPosizione;

    }
}
