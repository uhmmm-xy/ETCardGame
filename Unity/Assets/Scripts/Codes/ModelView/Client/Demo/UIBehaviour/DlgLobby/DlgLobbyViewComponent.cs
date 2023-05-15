
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgLobby))]
	[EnableMethod]
	public  class DlgLobbyViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EGBackGroundRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EGBackGroundRectTransform == null )
     			{
		    		this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EGBackGround");
     			}
     			return this.m_EGBackGroundRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_ExitGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExitGameButton == null )
     			{
		    		this.m_E_ExitGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_ExitGame");
     			}
     			return this.m_E_ExitGameButton;
     		}
     	}

		public UnityEngine.UI.Image E_ExitGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExitGameImage == null )
     			{
		    		this.m_E_ExitGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_ExitGame");
     			}
     			return this.m_E_ExitGameImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_GlodText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_GlodText == null )
     			{
		    		this.m_ELabel_GlodText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/ELabel_Glod");
     			}
     			return this.m_ELabel_GlodText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PlayerIdText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PlayerIdText == null )
     			{
		    		this.m_ELabel_PlayerIdText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/ELabel_PlayerId");
     			}
     			return this.m_ELabel_PlayerIdText;
     		}
     	}

		public UnityEngine.UI.Button E_EnterGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterGameButton == null )
     			{
		    		this.m_E_EnterGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_EnterGame");
     			}
     			return this.m_E_EnterGameButton;
     		}
     	}

		public UnityEngine.UI.Image E_EnterGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterGameImage == null )
     			{
		    		this.m_E_EnterGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_EnterGame");
     			}
     			return this.m_E_EnterGameImage;
     		}
     	}

		public UnityEngine.UI.Image E_PushGamePlanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PushGamePlanImage == null )
     			{
		    		this.m_E_PushGamePlanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan");
     			}
     			return this.m_E_PushGamePlanImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_CreateRoomButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateRoomButton == null )
     			{
		    		this.m_EButton_CreateRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/EButton_CreateRoom");
     			}
     			return this.m_EButton_CreateRoomButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CreateRoomImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateRoomImage == null )
     			{
		    		this.m_EButton_CreateRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/EButton_CreateRoom");
     			}
     			return this.m_EButton_CreateRoomImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_PushRoomButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PushRoomButton == null )
     			{
		    		this.m_EButton_PushRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/EButton_PushRoom");
     			}
     			return this.m_EButton_PushRoomButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_PushRoomImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PushRoomImage == null )
     			{
		    		this.m_EButton_PushRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/EButton_PushRoom");
     			}
     			return this.m_EButton_PushRoomImage;
     		}
     	}

		public UnityEngine.UI.InputField E_RoomIdInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoomIdInputField == null )
     			{
		    		this.m_E_RoomIdInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/E_RoomId");
     			}
     			return this.m_E_RoomIdInputField;
     		}
     	}

		public UnityEngine.UI.Image E_RoomIdImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoomIdImage == null )
     			{
		    		this.m_E_RoomIdImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/E_RoomId");
     			}
     			return this.m_E_RoomIdImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_closeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_closeButton == null )
     			{
		    		this.m_EButton_closeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/EButton_close");
     			}
     			return this.m_EButton_closeButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_closeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_closeImage == null )
     			{
		    		this.m_EButton_closeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_PushGamePlan/EButton_close");
     			}
     			return this.m_EButton_closeImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_ExitGameButton = null;
			this.m_E_ExitGameImage = null;
			this.m_ELabel_GlodText = null;
			this.m_ELabel_PlayerIdText = null;
			this.m_E_EnterGameButton = null;
			this.m_E_EnterGameImage = null;
			this.m_E_PushGamePlanImage = null;
			this.m_EButton_CreateRoomButton = null;
			this.m_EButton_CreateRoomImage = null;
			this.m_EButton_PushRoomButton = null;
			this.m_EButton_PushRoomImage = null;
			this.m_E_RoomIdInputField = null;
			this.m_E_RoomIdImage = null;
			this.m_EButton_closeButton = null;
			this.m_EButton_closeImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_ExitGameButton = null;
		private UnityEngine.UI.Image m_E_ExitGameImage = null;
		private UnityEngine.UI.Text m_ELabel_GlodText = null;
		private UnityEngine.UI.Text m_ELabel_PlayerIdText = null;
		private UnityEngine.UI.Button m_E_EnterGameButton = null;
		private UnityEngine.UI.Image m_E_EnterGameImage = null;
		private UnityEngine.UI.Image m_E_PushGamePlanImage = null;
		private UnityEngine.UI.Button m_EButton_CreateRoomButton = null;
		private UnityEngine.UI.Image m_EButton_CreateRoomImage = null;
		private UnityEngine.UI.Button m_EButton_PushRoomButton = null;
		private UnityEngine.UI.Image m_EButton_PushRoomImage = null;
		private UnityEngine.UI.InputField m_E_RoomIdInputField = null;
		private UnityEngine.UI.Image m_E_RoomIdImage = null;
		private UnityEngine.UI.Button m_EButton_closeButton = null;
		private UnityEngine.UI.Image m_EButton_closeImage = null;
		public Transform uiTransform = null;
	}
}
