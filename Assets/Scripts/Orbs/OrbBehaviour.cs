using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    [SerializeField] private OrbInfo status;
    [SerializeField] private GameObject Fio;
    [SerializeField] private bool following;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = status.ORB_COLOR;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (following)
        {
            transform.position = Vector2.MoveTowards(transform.position, Fio.transform.position, status.ORB_SPEED * Time.deltaTime);
        }
    }
}
