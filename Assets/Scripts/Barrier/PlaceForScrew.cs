using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceForScrew : MonoBehaviour
{
    public Transform playerTransform;
    public Transform parentForScrew;
    public Transform screwSpawnPoint;
    public GameObject screwPrefab;
    public float speed = 5f;
    private GameObject screwReference;


    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 mouse = new Vector3(mousePos.x, mousePos.y, 0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouse);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag.Equals("PlaceForScrew") && GameObject.ReferenceEquals(hit.collider.gameObject, this.gameObject))
                {
                    screwReference = Instantiate(screwPrefab, playerTransform.position, Quaternion.identity);
                    screwReference.transform.SetParent(parentForScrew);
                    screwReference.transform.rotation = parentForScrew.transform.rotation;
                    StartCoroutine(MoveTransform(screwReference.transform, screwSpawnPoint.position, hit.collider));
                }
            }
        }
    }

     IEnumerator MoveTransform(Transform transformFirst, Vector3 hitPosition, Collider collider) 
    {
        while(Vector3.Distance(transformFirst.position, hitPosition) > 0.005f)
        {
            transformFirst.position = Vector3.Lerp(transformFirst.position, hitPosition, Time.deltaTime * speed);
            yield return null;
        }
        Destroy(collider.gameObject);
        yield return null;
    }
}
