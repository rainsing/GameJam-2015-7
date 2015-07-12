using UnityEngine;
using System.Collections;

public class chooseItem : MonoBehaviour 
{
	public GameObject _ItemRoot;

	private GameObject _SelectedItem;

	public Vector3 RotateSpeed;

	void Update()
	{
		if (Input.GetMouseButton (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				Debug.DrawLine (ray.origin, hit.point);
				print(hit.collider.gameObject.name);
			
				SelectItem(hit.collider.gameObject);
			}
		}

		Transform[] transList = _ItemRoot.GetComponentsInChildren<Transform>();
		foreach(Transform t in transList)
		{
			if(t.gameObject == _SelectedItem)
			{
				_SelectedItem.transform.Rotate(RotateSpeed);
				_SelectedItem.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
			}
			else
			{
				t.transform.localScale = Vector3.one;
			}
		}
	}

	void SelectItem(GameObject o)
	{
		_SelectedItem = o;
	}
}
