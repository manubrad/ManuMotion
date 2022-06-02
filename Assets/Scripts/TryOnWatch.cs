
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryOnWatch : MonoBehaviour
{
	[SerializeField] private GameObject watch;
	[SerializeField] private GameObject[] watchSides = new GameObject[4];
    int entero=0;
	

	private GestureInfo gestureInfo;

	// Start is called before the first frame update
	void Start()
	{
		Vector3 invertScale = new Vector3(-watchSides[1].transform.localScale.x, -watchSides[1].transform.localScale.y, -watchSides[1].transform.localScale.z);
		watchSides[1].transform.localScale = invertScale;
	}

	// Update is called once per frame
	void Update()
	{ 
		
		ManomotionManager.Instance.ShouldRunWristInfo(true);
		ManomotionManager.Instance.ShouldCalculateGestures(true);

		gestureInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
	
		if (gestureInfo.mano_class != ManoClass.NO_HAND){
            ShowWatch();
			
			
		}
		else
		{

			watch.transform.position = -Vector3.one;

		}
		

 void ShowWatch()
{
	var wristInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.wristInfo;
	var depthEstimation = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.depth_estimation;

	Vector3 leftPoint = ManoUtils.Instance.CalculateNewPositionDepth(new Vector3(wristInfo.left_point.x, wristInfo.left_point.y, 0),depthEstimation);
	Vector3 rightPoint = ManoUtils.Instance.CalculateNewPositionDepth(new Vector3(wristInfo.right_point.x, wristInfo.right_point.y, 0), depthEstimation);

	Vector3 watchPosition = Vector3.Lerp(leftPoint, rightPoint, 0.5f);

	watch.transform.position = watchPosition;
	watch.transform.LookAt(leftPoint);
	if(gestureInfo.hand_side != HandSide.Palmside)
	{
	 ShowWatchSide(true);
     
	}
	else
	{
		ShowWatchSide(false);
		entero++;
	}

}

 void ShowWatchSide( bool isFront){

    if(entero<=2)
	{

     watchSides[0].SetActive(isFront);
	 watchSides[1].SetActive(!isFront);
	 watchSides[2].SetActive(false);
	 watchSides[3].SetActive(false);

	

	}else if(entero<=4){


     watchSides[0].SetActive(false);
	 watchSides[1].SetActive(!isFront);
	 watchSides[2].SetActive(isFront);
	 watchSides[3].SetActive(false);

	

	}else if( entero<=6){
  
	 watchSides[0].SetActive(false);
	 watchSides[1].SetActive(!isFront);
	 watchSides[2].SetActive(false);
	 watchSides[3].SetActive(isFront);


	}else if(entero==7){

     entero=0;

	}
}
}

}

