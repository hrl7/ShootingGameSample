using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerMotion : MonoBehaviour
{
	
		private Animator animator;
		private int isWalk;
		private int isRun;
		private float speed;

		// Use this for initialization
		void Start ()
		{
				animator = GetComponent<Animator> ();
				isWalk = Animator.StringToHash ("Do Walk");
				isRun = Animator.StringToHash ("Do Run");

		} 
	
		// Update is called once per frame
		void Update ()
		{		
	
				if (Input.GetKey (KeyCode.LeftShift)) {
						Run ();
				} else {
						Walk (); 
				}

				if (Input.GetKey (KeyCode.UpArrow)) {
						transform.position += transform.rotation * Vector3.forward * speed;
				} else if (Input.GetKey (KeyCode.DownArrow)) {
						transform.position += transform.rotation * Vector3.back * speed;
				
				} else {
						Idle ();
				}

				
				if (Input.GetKey (KeyCode.LeftArrow)) {
						transform.Rotate (Vector3.down*2);
				} else if (Input.GetKey (KeyCode.RightArrow)) {
						transform.Rotate (Vector3.up*2);

				}
			
		}

		void Walk ()
		{
				animator.SetBool (isWalk, true);
				animator.SetBool (isRun, false);
				speed = 0.05f;
		}

		void Run ()
		{
				animator.SetBool (isWalk, false);
				animator.SetBool (isRun, true);
				speed = 0.1f;
		}

		void Idle ()
		{
				animator.SetBool (isWalk, false);
				animator.SetBool (isRun, false);
		}
}

