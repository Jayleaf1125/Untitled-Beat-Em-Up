using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float weaponRange = 1f;
    public LayerMask playerLayer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.GetComponent<HealthSystem>().DecereaseHealth(11);
        Collider2D player = Physics2D.OverlapCircle(this.transform.position, weaponRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<HealthSystem>().DecereaseHealth(10);
        }
    }
}
