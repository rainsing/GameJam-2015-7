using UnityEngine;
using System.Collections;

public class chooseItem : MonoBehaviour 
{
	public GameObject _ItemRoot;

	//private GameObject _SelectedItem;

	public Vector3 RotateSpeed;

	//private GameObject _HoverItem;

	void Update()
	{
		GameObject _SelectedItem = null;
		GameObject _HoveredItem = null;

		//hover logic
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{	
				_HoveredItem = hit.collider.gameObject;
			}
		}

		if (Input.GetMouseButton (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				Debug.DrawLine (ray.origin, hit.point);
				print(hit.collider.gameObject.name);
			
				_SelectedItem = hit.collider.gameObject;
			}
		}
		else
		{
			_SelectedItem = null;
		}

		Transform[] transList = _ItemRoot.GetComponentsInChildren<Transform>();
		foreach(Transform t in transList)
		{
			if(_SelectedItem == null)
			{
				if(t.gameObject == _HoveredItem)
				{
					t.transform.Rotate(RotateSpeed);
					t.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
				}
				else
				{
					t.transform.localScale = Vector3.one;
				}
			}
			else
			{
				//show select effect
			}
		}
	}

	void ShowHoverEffect()
	{
	}
}
