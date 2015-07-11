using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectRotate : MonoBehaviour 
{
	public float _WheelSpeed = 10.0f;
	public float _MaxY = 10;
	public float _MinY = -10;
	public Slider _UISlider;

	public bool _IsRotating = false;
	public float _RotateSpeed = 10;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		ScrollWheel();

		//RotateView();
	}

	void ScrollWheel()
	{
		float wheelValue =  Input.GetAxis("Mouse ScrollWheel");
		float addedY = transform.position.y + _WheelSpeed * wheelValue;
		if(wheelValue != 0)
		{
			if(addedY > _MaxY)
			{
				transform.position = new Vector3(transform.position.x, _MaxY, transform.position.z);
			}
			else if(addedY < _MinY)
			{
				transform.position = new Vector3(transform.position.x, _MinY, transform.position.z);
			}
			else
			{
				transform.position = new Vector3(transform.position.x, addedY, transform.position.z);
			}
		}

		float curY = transform.position.y;
		_UISlider.value = (_MaxY - curY) / (_MaxY - _MinY);
	}

	void RotateView()
	{
		//Input.GetAxis("Mouse X");//得到鼠标在水平方向的滑动
		//Input.GetAxis("Mouse Y");//得到鼠标在垂直方向的滑动
		
		if(Input.GetMouseButtonDown(0))
		{
			_IsRotating = true;
		}
		if(Input.GetMouseButtonUp(0))
		{
			_IsRotating = false;
		}
		
		if(_IsRotating)
		{
			transform.Rotate(Input.GetAxis("Mouse X") * _RotateSpeed, 0,  Input.GetAxis("Mouse Y") * _RotateSpeed);
		}

	}

}
