
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameRoom))]
	[EnableMethod]
	public  class DlgGameRoomViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_GamePlanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GamePlanImage == null )
     			{
		    		this.m_E_GamePlanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_GamePlan");
     			}
     			return this.m_E_GamePlanImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PlayerDownText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PlayerDownText == null )
     			{
		    		this.m_ELabel_PlayerDownText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_PlayerDown");
     			}
     			return this.m_ELabel_PlayerDownText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_DownOutText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_DownOutText == null )
     			{
		    		this.m_ELabel_DownOutText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_DownOut");
     			}
     			return this.m_ELabel_DownOutText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PlayerLeftText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PlayerLeftText == null )
     			{
		    		this.m_ELabel_PlayerLeftText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_PlayerLeft");
     			}
     			return this.m_ELabel_PlayerLeftText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_LeftOutText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LeftOutText == null )
     			{
		    		this.m_ELabel_LeftOutText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_LeftOut");
     			}
     			return this.m_ELabel_LeftOutText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PlayerReightText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PlayerReightText == null )
     			{
		    		this.m_ELabel_PlayerReightText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_PlayerReight");
     			}
     			return this.m_ELabel_PlayerReightText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_ReightOutText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_ReightOutText == null )
     			{
		    		this.m_ELabel_ReightOutText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_ReightOut");
     			}
     			return this.m_ELabel_ReightOutText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PlayerUpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PlayerUpText == null )
     			{
		    		this.m_ELabel_PlayerUpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_PlayerUp");
     			}
     			return this.m_ELabel_PlayerUpText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_UpOutText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_UpOutText == null )
     			{
		    		this.m_ELabel_UpOutText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_UpOut");
     			}
     			return this.m_ELabel_UpOutText;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_CardListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_CardListLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_CardListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"E_GamePlan/ELoopScrollList_CardList");
     			}
     			return this.m_ELoopScrollList_CardListLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.Image E_BeginBtnPlanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BeginBtnPlanImage == null )
     			{
		    		this.m_E_BeginBtnPlanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BeginBtnPlan");
     			}
     			return this.m_E_BeginBtnPlanImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_ReadyButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReadyButton == null )
     			{
		    		this.m_EButton_ReadyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BeginBtnPlan/EButton_Ready");
     			}
     			return this.m_EButton_ReadyButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_ReadyImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReadyImage == null )
     			{
		    		this.m_EButton_ReadyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BeginBtnPlan/EButton_Ready");
     			}
     			return this.m_EButton_ReadyImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_ExitButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ExitButton == null )
     			{
		    		this.m_EButton_ExitButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BeginBtnPlan/EButton_Exit");
     			}
     			return this.m_EButton_ExitButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_ExitImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ExitImage == null )
     			{
		    		this.m_EButton_ExitImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BeginBtnPlan/EButton_Exit");
     			}
     			return this.m_EButton_ExitImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_RoomIdText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_RoomIdText == null )
     			{
		    		this.m_ELabel_RoomIdText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ELabel_RoomId");
     			}
     			return this.m_ELabel_RoomIdText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_GamePlanImage = null;
			this.m_ELabel_PlayerDownText = null;
			this.m_ELabel_DownOutText = null;
			this.m_ELabel_PlayerLeftText = null;
			this.m_ELabel_LeftOutText = null;
			this.m_ELabel_PlayerReightText = null;
			this.m_ELabel_ReightOutText = null;
			this.m_ELabel_PlayerUpText = null;
			this.m_ELabel_UpOutText = null;
			this.m_ELoopScrollList_CardListLoopHorizontalScrollRect = null;
			this.m_E_BeginBtnPlanImage = null;
			this.m_EButton_ReadyButton = null;
			this.m_EButton_ReadyImage = null;
			this.m_EButton_ExitButton = null;
			this.m_EButton_ExitImage = null;
			this.m_ELabel_RoomIdText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_GamePlanImage = null;
		private UnityEngine.UI.Text m_ELabel_PlayerDownText = null;
		private UnityEngine.UI.Text m_ELabel_DownOutText = null;
		private UnityEngine.UI.Text m_ELabel_PlayerLeftText = null;
		private UnityEngine.UI.Text m_ELabel_LeftOutText = null;
		private UnityEngine.UI.Text m_ELabel_PlayerReightText = null;
		private UnityEngine.UI.Text m_ELabel_ReightOutText = null;
		private UnityEngine.UI.Text m_ELabel_PlayerUpText = null;
		private UnityEngine.UI.Text m_ELabel_UpOutText = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_CardListLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_E_BeginBtnPlanImage = null;
		private UnityEngine.UI.Button m_EButton_ReadyButton = null;
		private UnityEngine.UI.Image m_EButton_ReadyImage = null;
		private UnityEngine.UI.Button m_EButton_ExitButton = null;
		private UnityEngine.UI.Image m_EButton_ExitImage = null;
		private UnityEngine.UI.Text m_ELabel_RoomIdText = null;
		public Transform uiTransform = null;
	}
}
