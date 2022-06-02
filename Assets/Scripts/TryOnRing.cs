using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryOnRing : MonoBehaviour
{

	[SerializeField] private GameObject ring;
	[SerializeField] private GameObject[] ringParts = new GameObject[2];


	private GestureInfo gestureInfo;
    private FingerInfo fingerInfo;

	// Start is called before the first frame update
	void Start()
	{
        ManomotionManager.Instance.ToggleFingerInfoFinger(4);
		Vector3 invertScale = new Vector3(-ringParts[1].transform.localScale.x,-ringParts[1].transform.localScale.y, -ringParts[1].transform.localScale.z);
		ringParts[1].transform.localScale = invertScale;
	}

	// Update is called once per frame
	void Update()
	{
		
		ManomotionManager.Instance.ShouldRunFingerInfo(true);
		ManomotionManager.Instance.ShouldCalculateGestures(true);

		gestureInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
	
		if (gestureInfo.mano_class == ManoClass.GRAB_GESTURE){
            ShowRing();
			
		}
		else
		{

			ring.transform.position = -Vector3.one;

		}
    }
 void ShowRing()
{
    ManomotionManager.Instance.ShouldCalculateSkeleton3D(true);
    fingerInfo= ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.fingerInfo;
	
	var depthEstimation = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.depth_estimation;
	var leftPoint = ManoUtils.Instance.CalculateNewPositionDepth(new Vector3(fingerInfo.left_point.x, fingerInfo.left_point.y, 0),depthEstimation);
	var rightPoint = ManoUtils.Instance.CalculateNewPositionDepth(new Vector3(fingerInfo.right_point.x, fingerInfo.right_point.y, 0),depthEstimation);
	
    Vector3 ringPosition = Vector3.Lerp(leftPoint, rightPoint, 0.5f);
    ring.transform.position = ringPosition;
    ring.transform.LookAt(leftPoint);
    float distanceFingerPoints = Vector3.Distance(fingerInfo.left_point,fingerInfo.right_point);
    ring.transform.localScale = new Vector3(distanceFingerPoints,distanceFingerPoints,distanceFingerPoints);

	if(gestureInfo.hand_side == HandSide.Palmside)
	{
	 ShowRingPart(true);
	}
	else
	{
		ShowRingPart(false);
	}

}

 void ShowRingPart( bool isFront){

     ringParts[0].SetActive(isFront);
	 ringParts[1].SetActive(!isFront);
     
}
}
