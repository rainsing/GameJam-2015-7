using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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


	public List<GameObject> _ItemPrefabList = new List<GameObject>(); 
	public List<GameObject> _PosList = new List<GameObject>();
	public Transform _MidPos;

	public List<GameObject> _ItemList = new List<GameObject>();
	private GameObject _MidItem;

	public int _CurItemGroupIndex = 0;
	public int _TotalItemGroupIndex = 2;

	public GameObject _RightResult;
	public GameObject _WrongResult;

	private List<GameObject> _ResultList = new List<GameObject>();
	public GameObject _RefBox;

	public Material _Material;

	void Start()
	{

		_CurGameState = EGameState.GameStart;

		Init();
	}



	void Clear()
	{
		for(int i = _ItemList.Count-1; i>=0; i--)
		{
			Destroy(_ItemList[i]);

		}

		_ItemList.Clear();

		for(int i = _ResultList.Count-1; i>=0; i--)
		{
			Destroy(_ResultList[i]);
			
		}
		
		_ResultList.Clear();
	}

	void Init()
	{
		Clear();

		for(int i = 0; i < 4; i++)
		{
			GameObject o = GameObject.Instantiate(_ItemPrefabList[_CurItemGroupIndex*4 + i], _PosList[i].transform.position, Quaternion.identity) as GameObject;
			o.AddComponent<MeshCollider>();
			o.name = _ItemPrefabList[_CurItemGroupIndex*4 + i].name;
			_ItemList.Add(o);
		}

		int radomIndex = Random.Range(_CurItemGroupIndex*4, _CurItemGroupIndex*4 + 4);

		Destroy(_MidItem);

		_MidItem = GameObject.Instantiate(_ItemPrefabList[radomIndex], _MidPos.position, Quaternion.identity) as GameObject;
		_MidItem.transform.localScale = new Vector3(2,2,2);
		_MidItem.transform.position = new Vector3(_MidItem.transform.position.x, -1.97f, _MidItem.transform.position.z);
		_MidItem.GetComponent<MeshRenderer>().material = _Material;
		_MidItem.name = _ItemPrefabList[radomIndex].name;

		float randomAngle = Random.Range(-30.0f,30.0f);
		_MidItem.transform.Rotate(new Vector3(0,0,randomAngle));

		_RefBox.transform.rotation = Quaternion.identity;
		_RefBox.transform.Rotate(new Vector3(0,0,randomAngle));

		_AnswerObject = _MidItem;
	}

	private float cdTime = 0.0f;
	private float curCDTime = 0;
	void Update()
	{
		switch(_CurGameState)
		{
		case EGameState.GameStart:
			CheckItemClick();
			break;
		case EGameState.GamePlaying:
		{
			if(curCDTime < cdTime)
			{
				curCDTime += Time.deltaTime;
			}
			else{
				CheckItemClick();
			}


			break;
		}
		case EGameState.GameResult:
			{
				if(Input.anyKeyDown)
				{
					_CurItemGroupIndex = (_CurItemGroupIndex+1)%_TotalItemGroupIndex;
					Init();
					
					_CurGameState = EGameState.GamePlaying;

					cdTime = 1.0f;
					curCDTime = 0;
				}
				break;
			}
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
				_SelectedItem = hit.collider.gameObject;
				SelectItem(hit.collider.gameObject);
			}
		}
		else
		{
			_SelectedItem = null;
		}
		
		//Transform[] transList = _ItemRoot.GetComponentsInChildren<Transform>();
		foreach(GameObject t in _ItemList)
		{
			if(_SelectedItem == null)
			{
				if(t == _HoveredItem)
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
				
				
				Animator anim = t.GetComponent<Animator>();
				if(_SelectedItem == t)
				{
					//Debug.Log("t.gameObject.name="+t.gameObject.name+", set anim");
					anim.runtimeAnimatorController = _AnimController;
					//anim.Play("FlippingBottle");
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

		//show all result
		foreach(GameObject item in _ItemList)
		{
			if(item.name == _AnswerObject.name)
			{
				GameObject result =  GameObject.Instantiate(_RightResult, item.transform.position, _RightResult.transform.rotation) as GameObject;

				result.transform.position = new Vector3(result.transform.position.x, result.transform.position.y, -14.7f);

				_ResultList.Add(result);
			}
			else
			{
				GameObject result =  GameObject.Instantiate(_WrongResult, item.transform.position, _WrongResult.transform.rotation) as GameObject;

				result.transform.position = new Vector3(result.transform.position.x, result.transform.position.y, -14.7f);

				_ResultList.Add(result);
			}
		}
		
		_CurGameState = EGameState.GameResult;
	}
}
