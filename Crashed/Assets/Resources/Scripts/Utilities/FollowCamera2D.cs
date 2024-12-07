using UnityEngine;
using System.Collections;
using System;

namespace CustomCamera
{
    [Flags]
    public enum Direction
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = 3
    }

    public class FollowCamera2D : MonoBehaviour
    {
        public Transform target;
        public float dampTime = 0.15f;
        public Direction followType = Direction.Horizontal;
        [Range(0.0f,1.0f)]
        public float
            cameraCenterX = 0.5f;
        [Range(0.0f,1.0f)]
        public float
            cameraCenterY = 0.5f;
        //public Direction boundType = Direction.None;
        //public float leftBound = 0;
        //public float rightBound = 0;
        //public float upperBound = 0;
        //public float lowerBound = 0;
        //public Direction deadZoneType = Direction.None;
        //public bool hardDeadZone = false;
        //public float leftDeadBound = 0;
        //public float rightDeadBound = 0;
        //public float upperDeadBound = 0;
        //public float lowerDeadBound = 0;

        //public GameObject LeftBoundLimitGO;
        //public GameObject RightBoundLimitGO;
        //public GameObject UpperBoundLimitGO;
        //public GameObject LowerBoundLimitGO;

        //private float LeftBoundLimit;
        //private float RightBoundLimit;
        //private float UpperBoundLimit;
        //private float LowerBoundLimit;

        // private
        Camera mainCamera;
        Vector3 velocity = Vector3.zero;
        float vertExtent;
        float horzExtent;
        Vector3 tempVec = Vector3.one;
        bool isBoundHorizontal;
        bool isBoundVertical;
        bool isFollowHorizontal;
        bool isFollowVertical;
        bool isDeadZoneHorizontal;
        bool isDeadZoneVertical;
        Vector3 deltaCenterVec;

        void Start ()
        {

            target = GameObject.FindGameObjectWithTag("Player").transform;
            /////meysam - added by meysam
            //LeftBoundLimit = LeftBoundLimitGO.transform.position.x;
            //RightBoundLimit = RightBoundLimitGO.transform.position.x;
            //UpperBoundLimit = UpperBoundLimitGO.transform.position.y;
            //LowerBoundLimit = LowerBoundLimitGO.transform.position.y;
            ///////////////////////////////

        mainCamera = GetComponent<Camera> ();
			vertExtent = mainCamera.orthographicSize;
            horzExtent = vertExtent * Screen.width / Screen.height;
			deltaCenterVec = mainCamera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0))
				- mainCamera.ViewportToWorldPoint (new Vector3 (cameraCenterX, cameraCenterY, 0));


            isFollowHorizontal = (followType & Direction.Horizontal) == Direction.Horizontal;
            isFollowVertical = (followType & Direction.Vertical) == Direction.Vertical;
            //isBoundHorizontal = (boundType & Direction.Horizontal) == Direction.Horizontal;
            //isBoundVertical = (boundType & Direction.Vertical) == Direction.Vertical;

            //isDeadZoneHorizontal = ((deadZoneType & Direction.Horizontal) == Direction.Horizontal) && isFollowHorizontal;
            //isDeadZoneVertical = ((deadZoneType & Direction.Vertical) == Direction.Vertical) && isFollowVertical;
            tempVec = Vector3.one;
        }

        void LateUpdate ()
        {
            
            if (target) {
				Vector3 delta = target.position - mainCamera.ViewportToWorldPoint (new Vector3 (cameraCenterX, cameraCenterY, 0));
              
                if (!isFollowHorizontal) {
                    delta.x = 0;
                }
                if (!isFollowVertical) {
                    delta.y = 0;
                }
                Vector3 destination = transform.position + delta;

                //if (!hardDeadZone) {
                    tempVec = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
                //} else {
                //    tempVec.Set (transform.position.x, transform.position.y, transform.position.z);
                //}

                //if (isDeadZoneHorizontal) {
                //    if (delta.x > rightDeadBound) {
                //        tempVec.x = target.position.x - rightDeadBound + deltaCenterVec.x;
                //    }
                //    if (delta.x < -leftDeadBound) {
                //        tempVec.x = target.position.x + leftDeadBound + deltaCenterVec.x;
                //    }
                //}
                //if (isDeadZoneVertical) {
                //    if (delta.y > upperDeadBound) {
                //        tempVec.y = target.position.y - upperDeadBound + deltaCenterVec.y;
                //    }
                //    if (delta.y < -lowerDeadBound) {
                //        tempVec.y = target.position.y + lowerDeadBound + deltaCenterVec.y;
                //    }
                //}

                //if (isBoundHorizontal) {
                //    tempVec.x = Mathf.Clamp (tempVec.x, leftBound + horzExtent, rightBound - horzExtent);
                //}

                //if (isBoundVertical) {
                //    tempVec.y = Mathf.Clamp (tempVec.y, lowerBound + vertExtent, upperBound - vertExtent);
                //}

                tempVec.z = transform.position.z;

                //if (tempVec.x < LeftBoundLimit ||
                //    tempVec.x > RightBoundLimit)
                //{
                //    tempVec.x = transform.position.x;
                //}


                //if (tempVec.y > UpperBoundLimit ||
                //    tempVec.y < LowerBoundLimit)
                //{
                //    tempVec.y = transform.position.y;
                //}

                transform.position = tempVec;
                
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

		public void ChangeCameraCenterX(float valueX)
		{
			cameraCenterX = valueX;
		}
    }

}
