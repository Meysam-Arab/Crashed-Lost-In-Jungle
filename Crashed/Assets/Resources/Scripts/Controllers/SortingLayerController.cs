using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerController : MonoBehaviour
{

	public static Dictionary<int, Dictionary<string, int>> srs = new Dictionary<int, Dictionary<string, int>>();

	public static void SetDynamicSortingLayer(SpriteRenderer sr, int targetY, int targetX)
	{
		sr.sortingOrder = ((GameController.instance.bc.rows - targetY) * GameController.instance.bc.rows) + targetX;		
	}


	///// <summary>
	///// it's for tree blockades in which must check their right side neighbor and if the neighbor is an non-tree blockade so the sr of the tree must be higher than the neighbore
	///// </summary>
	///// <param name="sr"></param>
	///// <param name="targetY"></param>
	///// <param name="targetX"></param>
	///// <param name="directionToCheck"></param>
	///// <param name=""></param>
	//public static void TuneDynamicSortingLayer(SpriteRenderer sr, BoxCollider2D bc)
	//{
		
		

	//	//Hit will store whatever our linecast hits when Move is called.
	//	RaycastHit2D[] hits;

	//	//Store start position to move from, based on objects current transform position.
	//	Vector2 start = sr.gameObject.transform.position;

	//	// Calculate end position based on the direction parameters passed in when calling Move.
	//	Vector2 end = start + new Vector2(1, 0);


	//	//Disable the boxCollider so that linecast doesn't hit this object's own collider.
	//	bc.enabled = false;

	//	//Debug.Log("start:" + start);
	//	//Debug.Log("end:" + end);

	//	//Cast a line from start point to end point checking collision on blockingLayer.
	//	//ContactFilter2D cf2d = new ContactFilter2D();
	//	//cf2d.SetLayerMask(blockingLayer);
	//	LayerMask blockingLayer = LayerMask.GetMask("Default");
	//	hits = Physics2D.LinecastAll(start, end, blockingLayer);

 //       Debug.DrawLine(start, end, MeysamUtility.ColorBloodRed, 30000);
 //       //Debug.DrawRay(start, end, MeysamUtility.ColorBloodRed, 30000);

 //       //Re-enable boxCollider after linecast
 //       bc.enabled = true;


	//	if (hits.Length > 0)
 //       {
 //           foreach (RaycastHit2D item in hits)
 //           {
	//			if(BlockadeModel.IsTagBlockade(item.transform.gameObject.tag) && !BlockadeModel.IsblockadeTree(item.transform.gameObject.tag))
 //               {
	//				int slIndex = item.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
	//				if (sr.sortingOrder <= slIndex)
	//					sr.sortingOrder = slIndex + 1;

	//			}
 //           }
 //       }
	//}
}
