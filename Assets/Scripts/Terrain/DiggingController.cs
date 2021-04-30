using UnityEngine;
using UnityEngine.Events;

public class DiggingController : MonoBehaviour
{
    [System.Serializable]
    public class SegmentDiggingFinishedEvent : UnityEvent<GameObject> {}

    TerrainDeformation TerrainDeformation;
    public Material avaiableMaterial;
    public UnityEvent diggingFinished;
    public SegmentDiggingFinishedEvent segmentDiggingFinished;
    [HideInInspector] 
    public bool segmentFinished = false;
    public float diggingDistance = 5f;
    Animator animator;

    
    void Start()
    {
        TerrainDeformation = FindObjectOfType<TerrainDeformation>();
        animator = Camera.main.gameObject.GetComponent<Animator>();
    }



    public bool UpdateDigging()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit[] hits = Physics.RaycastAll(ray, diggingDistance);

        foreach(var hit in hits) {
            if(hit.collider.name.Equals("Terrain"))
            {
                foreach(var h in hits)
                {
                    
                    if(h.collider.tag.Equals("BuildingObject"))
                    {
                        TerrainDeformation.LowerTerrain(hit.point, transform.position.y - .5f);
                        animator.SetTrigger("UseShovel");
                    }
                    
                }
                
            }
        }

        return false;
    }



    void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.collider.name.Equals("Terrain") && GameManager.instance.state == GameManager.State.digging)
        {
            GameManager.instance.state = GameManager.State.placeObject;
            GetComponent<Renderer>().material = avaiableMaterial;
            diggingFinished?.Invoke();
            segmentDiggingFinished?.Invoke(gameObject);
            segmentFinished = true;
        } 
    }
}
