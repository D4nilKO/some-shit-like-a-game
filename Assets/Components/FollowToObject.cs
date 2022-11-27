using UnityEngine;

public class FollowToObject : MonoBehaviour
{
    //public float speed;
    [SerializeField] private Transform target;
    
    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
    
    private void Update()
    {
        var position = target.position;
        position.z = transform.position.z;
        transform.position = position;

        //для плавного следования за позицией игрока
        //Vector3 position = target.position;
        //position.z = transform.position.z;
        //transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}