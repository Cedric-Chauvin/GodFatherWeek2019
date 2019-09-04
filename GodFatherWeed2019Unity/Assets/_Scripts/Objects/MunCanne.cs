using UnityEngine;

public class MunCanne : MonoBehaviour
{
    private float speed;
    private float distanceMax;
    private float damage;

    private LineRenderer lineRenderer;
    private PlayerController player;
    private Vector3 dir;
    private bool retour;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, player.transform.position);
        Vector3 Pos = lineRenderer.GetPosition(1);
        if (retour)
        {
            dir = player.transform.position - Pos;
            if(target!=null)
                target.position = Pos;
        }
        Pos += dir.normalized * speed * Time.deltaTime;
        lineRenderer.SetPosition(1, Pos);
        if (Pos.magnitude >= distanceMax)
            retour = true;
        foreach (var item in PlayerController._players)
        {
            if(item!= player)
            {
                if ((Pos - item.transform.position).magnitude <= 0.5)
                {
                    target = item.transform;
                    item.Damage(damage);
                    retour = true;
                }
            }
        }

        if ((Pos - player.transform.position).magnitude <= 1&& retour)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(PlayerController PC, Vector3 direction,float distance,float degat,float _speed)
    {
        player = PC;
        dir = direction;
        distanceMax = distance;
        damage = degat;
        speed = _speed;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, player.transform.position);
        lineRenderer.SetPosition(0, player.transform.position);
    }
}
