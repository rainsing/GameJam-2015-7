using UnityEngine;
using System.Collections;

public enum EGameState
{
	GameStart,
	GamePlaying,
	GameResult,
}

public class chooseItem : MonoBehaviour 
{
	public GameObject _ItemRoot;
	public Vector3 RotateSpeed;
	public RuntimeAnimatorController _AnimController;

	public GameObject _AnswerObject;

	private EGameState _CurGameState;

	void Start()
	{
		_AnswerObject = GameObject.FindGameObjectWithTag("Answer");
		_CurGameState = EGameState.GameStart;
	}

	void Update()
	{


		switch(_CurGameState)
		{
		case EGameState.GameStart:
			CheckItemClick();
			break;
		case EGameState.GamePlaying:
			CheckItemClick();
			break;
		case EGameState.GameResult:
			CheckItemClick();
			break;
		}


	}

	void CheckItemClick()
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
				//Debug.DrawLine (ray.origin, hit.point);
				//print(hit.collider.gameObject.name);
				
				_SelectedItem = hit.collider.gameObject;
				SelectItem(hit.collider.gameObject);
			}
		}
		else
		{
			_SelectedItem = null;
		}
		
		Transform[] transList = _ItemRoot.GetComponentsInChildren<Transform>();
		foreach(Transform t in transList)
		{
			if(t.gameObject.name == _ItemRoot.name)
			{
				continue;
			}
			
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
				//Debug.Log(t.gameObject.name);
				
				
				Animator anim = t.gameObject.GetComponent<Animator>();
				if(_SelectedItem == t.gameObject)
				{
					//Debug.Log("t.gameObject.name="+t.gameObject.name+", set anim");
					anim.runtimeAnimatorController = _AnimController;
					anim.Play("FlippingBottle");
				}
				else
				{
					anim.runtimeAnimatorController = null;
				}
				
			}
		}
	}



	void SelectItem(GameObject o)
	{
		Debug.Log("_AnswerObject.name="+_AnswerObject.name+", SelectItem.name="+o.name+"!");

		//if(o.name.Equals(_AnswerObject.name) == true)
		if(_AnswerObject.name == o.name)
		{
			Debug.Log("Answer Is Right");
		}
		else
		{
			Debug.Log("Answer Is wrong");
		}

		_CurGameState = EGameState.GameResult;
	}
}
