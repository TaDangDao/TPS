using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using Unity.VisualScripting;
using Cinemachine.Utility;
using TMPro;
using UnityEngine.Animations.Rigging;

public class Aim : MonoBehaviour
{
    private StarterAssetsInputs starterAssets;
    //private bool isAim;
   [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Rig rig;
    [SerializeField]   private Transform gun;
     // Start is called before the first frame update
    [SerializeField] private LayerMask aimColliderMask=new LayerMask();
    [SerializeField] private Transform Dtransform;
    private Animator animator;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform bulletSpawn;
    private const string isWalk = "walk_forward";
   
    private void Start()
    {
        starterAssets = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();

        //isAim = false;
        animator.SetBool(isWalk, false);
        rig.weight = 0f;
       

    }
   

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
       
        Vector2 screenPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask))
        {
            Dtransform.position = hit.point;
            dir = hit.point;

        }
        Vector3 aimDir = dir;
        aimDir.y = transform.position.y;
        Vector3 target = (aimDir - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, target, Time.deltaTime * 20f);
        if (starterAssets.aim)
        {
            
                virtualCamera.gameObject.SetActive(true);
                animator.SetLayerWeight(1, 1f);
              rig.weight = 1f;
                gun.gameObject.SetActive(true);
            Vector3 changex = target;

            if (starterAssets.shoot)
            {
                Vector3 shootDir = (dir - bulletSpawn.position).normalized;
                Instantiate(bullet, bulletSpawn.position, Quaternion.LookRotation(shootDir, Vector3.up));
                starterAssets.shoot = false;
            }
            if (starterAssets.move != Vector2.zero)
                {
               
                animator.SetBool(isWalk, true);
                    if (starterAssets.move.y != 0 && starterAssets.move.x == 0)
                    {
                        animator.SetFloat("x", 0);
                        animator.SetFloat("y", 1);

                    }
                    else if (starterAssets.move.x != 0 && starterAssets.move.y == 0)
                    {
                        animator.SetFloat("x", 1);
                        animator.SetFloat("y", 0);
                    }

                }

                else
                {
                    animator.SetBool(isWalk, false);

                }





            }
            else
            {
                virtualCamera.gameObject.SetActive(false);
                rig.weight = 0f;
            gun.gameObject.SetActive(false);
                animator.SetLayerWeight(1, 0f);
                //isAim=true;
            }
        

        
      
       

       

    }
}
