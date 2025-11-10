using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject trashThrowablePrefab;
    private float spawnForce = 100;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable")==false)
        {
            Instantiate(trashThrowablePrefab, null, spawnPoint);
            trashThrowablePrefab.GetComponent<Rigidbody>().AddForce(Vector3.up*spawnForce,ForceMode.Impulse);
        }
    }
}
