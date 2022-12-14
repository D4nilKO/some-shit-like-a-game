using Components.Skills.Orbital_Strike;
using Components.Skills.Plasma;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MoveTrack))]

[RequireComponent(typeof(PlasmaV2))]
[RequireComponent(typeof(Whip))]
[RequireComponent(typeof(Flamethrower))]
[RequireComponent(typeof(OrbitalStrike))]
[RequireComponent(typeof(Shield))]
[RequireComponent(typeof(Experience))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    #region FIELDS

    #region STATIC CLASSES

    public static Transform playerTransform;
    public static GameObject playerGameObject;
    public static Player playerScr;
    public static PlasmaV2 plasmaV2Scr;
    public static Whip whipScr;
    public static Flamethrower flamethrowerScr;
    public static OrbitalStrike orbitalStrikeScr;
    public static Shield shieldScr;
    public static Experience experienceScr;
    public static PlayerHealth playerHealthScr;

    #endregion

    #region MOVEMENT

    [HideInInspector] public float speed;
    [SerializeField] public float startSpeed;
    [HideInInspector] public Vector2 moveVelocity;
    [SerializeField] private Rigidbody2D rb;

    #endregion

    #region ANIMATOR

    [SerializeField] private Animator animator; //будущий аниматор(надо)
    public Joystick joystick;
    private Vector2 vectorTrack;
    public MoveTrack moveTrackScr;
    private bool facingRight = true; //Спрайт направо -_-
    private SpriteRenderer spriteRender;
    private static readonly int Run = Animator.StringToHash("Run");

    #endregion

    #endregion

    #region LYFECYCLE

    private void Awake()
    {
        moveTrackScr = GetComponent<MoveTrack>();
        spriteRender = GetComponent<SpriteRenderer>();
        playerScr = GetComponent<Player>();
        plasmaV2Scr = GetComponent<PlasmaV2>();
        whipScr = GetComponent<Whip>();
        flamethrowerScr = GetComponent<Flamethrower>();
        orbitalStrikeScr = GetComponent<OrbitalStrike>();
        shieldScr = GetComponent<Shield>();
        experienceScr = GetComponent<Experience>();
        playerHealthScr = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();

        playerGameObject = gameObject;
        playerTransform = transform;
    }

    private void Start()
    {
        speed = startSpeed;
    }

    #endregion

    #region UPDATES
    
    // физ составляющая движка
    private void FixedUpdate()
    {
        Movement(); // новая система движения
    }

    #endregion

    private void Movement()
    {
        vectorTrack = moveTrackScr.MovementLogic();

        moveVelocity = vectorTrack.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // движение

        //поворот персонажа
        switch (facingRight)
        {
            case false when vectorTrack.x > 0:
                facingRight = !facingRight;
                transform.localScale = Vector3.one;
                break;
            case true when vectorTrack.x < 0:
                facingRight = !facingRight;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }

        //аниматор
        if (vectorTrack.x != 0 || vectorTrack.y != 0)
        {
            //transform.rotation = Quaternion.LookRotation(rb.velocity);
            animator.SetBool(Run, true);
        }
        else
            animator.SetBool(Run, false);
    }
}