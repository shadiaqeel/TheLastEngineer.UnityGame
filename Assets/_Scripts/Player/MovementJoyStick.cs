using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;


public class MovementJoyStick : Joystick
{
	

#region Variables
		Vector2 joystickCenter = Vector2.zero;



  	    [Header("Variable Joystick Options")]
    	 public bool isFixed = true;
   		 public Vector2 fixedScreenPosition;

		public int MovementRange = 100;
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

#endregion Variables



		void OnEnable()
		{
			CreateVirtualAxes();
		}

        void Start()
        {
            if (isFixed)
            OnFixed();
        	else
            OnFloat();
        }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
		}



		  public void ChangeFixed(bool joystickFixed)
    {
        if (joystickFixed)
            OnFixed();
        else
            OnFloat();
        isFixed = joystickFixed;
    }


#region Methodes

    	void OnFixed()
   		 {
        joystickCenter = fixedScreenPosition;
        background.gameObject.SetActive(true);
        handle.anchoredPosition = Vector2.zero;
        background.position = fixedScreenPosition;
   		 }

		 void OnFloat()
   		 {
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);
   		 }


		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (joystickMode == JoystickMode.AllAxis || joystickMode == JoystickMode.Horizontal);
			m_UseY = (joystickMode == JoystickMode.AllAxis || joystickMode == JoystickMode.Vertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}






#endregion Methodes

#region Override Methodes

		public override void OnPointerDown(PointerEventData eventData) {

			if (!isFixed)
       		 {
			 background.gameObject.SetActive(true);
        	background.position = eventData.position;
        	handle.anchoredPosition = Vector2.zero;
        	joystickCenter = eventData.position;
			}
		 }



		public override void  OnDrag(PointerEventData data)
		{
			

			Vector2 direction = data.position - joystickCenter;
       	 	inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        	ClampJoystick();
        	handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

			UpdateVirtualAxes(handle.anchoredPosition);
		}


		public override void  OnPointerUp(PointerEventData eventData)
		{


		if (!isFixed)
        {
            background.gameObject.SetActive(false);
        }
        inputVector = Vector2.zero;
		handle.anchoredPosition = Vector2.zero;
		UpdateVirtualAxes(inputVector);
		}


		
	#endregion Override Methodes
	}
