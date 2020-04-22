using UnityEngine;
using System.Collections;


    //[RequireComponent(typeof(Interactable))]
    public class Movable : MonoBehaviour
    {

        public bool inHand = false;
		public GameObject mainObject;
		public bool keepUpright = true;
		public bool keepOnGround = true;

        //private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

		private void HandHoverUpdate(OVRGrabbable grabbable)
        {

            //GrabTypes startingGrabType = hand.GetGrabStarting();
            //bool isGrabEnding = hand.IsGrabEnding(this.gameObject);
                        
            
            if (grabbable.isGrabbed)
            {
                if (grabbable.grabbedBy.grabbedObject != mainObject)
                {
                    // Call this to continue receiving HandHoverUpdate messages,
                    // and prevent the hand from hovering over anything else
                    //hand.HoverLock(GetComponent<Interactable>());

                    // Attach this object to the hand
                    //hand.AttachObject(mainObject, startingGrabType, attachmentFlags);
					
					inHand = true;
                } else {

                    // Detach this object from the hand
                    //hand.DetachObject(mainObject);

                    // Call this to undo HoverLock
                    //hand.HoverUnlock(GetComponent<Interactable>());

					if (keepUpright) {
						Quaternion rotation = mainObject.transform.localRotation;
						rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
						mainObject.transform.localRotation = rotation;
					}

					if (keepOnGround) {
						mainObject.transform.localPosition = new Vector3(mainObject.transform.localPosition.x, 0, mainObject.transform.localPosition.z);
					}
                    

                    inHand = false;
                }
            }
        }
    }

