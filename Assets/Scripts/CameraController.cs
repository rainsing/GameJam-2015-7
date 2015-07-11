using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform _Player;
	private Vector3 _OffsetPosition;
	public float _Distance = 0;
	public bool _IsRotating = false;
	public float _RotateSpeed = 10;
	
	void Start()
	{
		//_Player = GameObject.FindGameObjectWithTag (Tags._Player).transform;
		//transform.LookAt (_Player.position);
		_OffsetPosition = transform.position - _Player.position;
	}
	
	void Update()
	{
		//transform.position = _Player.position + _OffsetPosition;
		
		RotateView ();
	}

	
	void RotateView()
	{
		//Input.GetAxis("Mouse X");//得到鼠标在水平方向的滑动
		//Input.GetAxis("Mouse Y");//得到鼠标在垂直方向的滑动
		
		if(Input.GetMouseButtonDown(1))
		{
			_IsRotating = true;
		}
		if(Input.GetMouseButtonUp(1))
		{
			_IsRotating = false;
		}
		
		if(_IsRotating)
		{
			transform.RotateAround(_Player.position, Vector3.up, Input.GetAxis("Mouse X") * _RotateSpeed);
			transform.RotateAround(_Player.position, transform.right, Input.GetAxis("Mouse Y") * -_RotateSpeed);
			_OffsetPosition = transform.position - _Player.position;
		}
	}
}
