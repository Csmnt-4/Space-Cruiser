using UnityEngine;

public class CollectableParticle : MonoBehaviour
{
    [SerializeReference]
    GameObject gameManager;
    public float lifeTime = 10.0f;
    public float speed;

    public void Initialize(float speed, GameObject gameManager)
    {
        this.speed = speed;
        this.gameManager = gameManager;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision:" + other.tag);
        
        if (other.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().CollectParticle();
            Destroy(gameObject); // Destroy the particle on collision with the player
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Time.deltaTime * speed);

    }
}
