using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public GameObject weapon;
    public GameObject CuttingPlane;
    public AnimationClip animationClip;
    public bool canCut;

    private Cutting cutting;
    private Animator animator;
    private bool isSlashing;

    // Start is called before the first frame update
    void Start()
    {
        cutting = CuttingPlane.GetComponent<Cutting>();
        animator = weapon.GetComponent<Animator>();
        animator.SetBool("isSlashing", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire1"), 1) && !isSlashing && !animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip.name))
        {
            isSlashing = true;
            animator.SetBool("isSlashing", true);
            if (canCut)
            {
                cutting.Slice();
            }
        }

        if (Mathf.Approximately(Input.GetAxis("Fire1"), 0) && isSlashing)
        {
            isSlashing = false;
            animator.SetBool("isSlashing", false);
        }
    }
}
