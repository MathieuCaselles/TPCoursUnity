using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundMask;
    
    private Animator animator;
    private bool isGrounded = false;
    private GameObject detectGround;
    
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        detectGround = transform.Find("DetectGround").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 detectGroundPosition = detectGround.transform.position;
        Vector2 detectPosition = new Vector2(detectGroundPosition.x,detectGroundPosition.y);
        if (Physics2D.OverlapCircle(detectPosition, 0.01f, groundMask))
        {
            isGrounded = true;
            animator.GetBool(IsJumping);
            
        }
    }
}
