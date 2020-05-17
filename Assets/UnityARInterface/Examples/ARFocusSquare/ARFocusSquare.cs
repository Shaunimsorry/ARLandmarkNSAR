﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace UnityARInterface
{
	public class ARFocusSquare : MonoBehaviour {

		public enum FocusState {
			Initializing,
			Finding,
			Found
		}

		public GameObject findingSquare;
		public GameObject foundSquare;

		//for editor version
		public float maxRayDistance = 30.0f;
		public LayerMask collisionLayerMask; 
		public float findingSquareDist = 0.5f;

		private FocusState squareState;
		public FocusState SquareState { 
			get {
				return squareState;
			}
			set {
				squareState = value;
				foundSquare.SetActive (squareState == FocusState.Found);
				findingSquare.SetActive (squareState != FocusState.Found);
			} 
		}

		bool trackingInitialized;

		//Modifying this for to pull the hit data out without raycasting again
		public Vector3 focusSquareRayCastHitVector;

		// Use this for initialization
		void Start () {
			int layerIndex = LayerMask.NameToLayer("ARGameObject");
			collisionLayerMask = 1 << layerIndex;
			SquareState = FocusState.Initializing;
			trackingInitialized = true;
		}


		// Update is called once per frame
		void Update () {

			//use center of screen for focusing
			Vector3 center = new Vector3(Screen.width/2, Screen.height/2, findingSquareDist);

			Ray ray = Camera.main.ScreenPointToRay (center);
			RaycastHit hit;

			//we'll try to hit one of the plane collider gameobjects that were generated by the plugin
			//effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
			if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayerMask)) {
				//we're going to get the position from the contact point
				foundSquare.transform.position = hit.point;
				Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", foundSquare.transform.position.x, foundSquare.transform.position.y, foundSquare.transform.position.z));

				//and the rotation from the transform of the plane collider
				SquareState = FocusState.Found;
				foundSquare.transform.rotation = hit.transform.rotation;

				//Save the vector
				focusSquareRayCastHitVector = hit.transform.position;



				return;
			}


			//if you got here, we have not found a plane, so if camera is facing below horizon, display the focus "finding" square
			if (trackingInitialized) {
				SquareState = FocusState.Finding;

				//check camera forward is facing downward
				if (Vector3.Dot(Camera.main.transform.forward, Vector3.down) > 0)
				{

					//position the focus finding square a distance from camera and facing up
					findingSquare.transform.position = Camera.main.ScreenToWorldPoint(center);

					//vector from camera to focussquare
					Vector3 vecToCamera = findingSquare.transform.position - Camera.main.transform.position;

					//find vector that is orthogonal to camera vector and up vector
					Vector3 vecOrthogonal = Vector3.Cross(vecToCamera, Vector3.up);

					//find vector orthogonal to both above and up vector to find the forward vector in basis function
					Vector3 vecForward = Vector3.Cross(vecOrthogonal, Vector3.up);


					findingSquare.transform.rotation = Quaternion.LookRotation(vecForward,Vector3.up);

				}
				else
				{
					//we will not display finding square if camera is not facing below horizon
					findingSquare.SetActive(false);
				}

			}

		}


	}
}