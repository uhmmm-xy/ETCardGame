
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

		public UnityEngine.UI.Text E_DownOpenDealText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DownOpenDealText == null )
     			{
		    		this.m_E_DownOpenDealText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/E_DownOpenDeal");
     			}
     			return this.m_E_DownOpenDealText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_LeftPlayerText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LeftPlayerText == null )
     			{
		    		this.m_ELabel_LeftPlayerText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/ELabel_LeftPlayer");
     			}
     			return this.m_ELabel_LeftPlayerText;
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

		public UnityEngine.UI.Text E_LeftOpenDealText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LeftOpenDealText == null )
     			{
		    		this.m_E_LeftOpenDealText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/E_LeftOpenDeal");
     			}
     			return this.m_E_LeftOpenDealText;
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

		public UnityEngine.UI.Text E_ReightOpenDealText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReightOpenDealText == null )
     			{
		    		this.m_E_ReightOpenDealText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/E_ReightOpenDeal");
     			}
     			return this.m_E_ReightOpenDealText;
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

		public UnityEngine.UI.Text E_UpOpenDealText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpOpenDealText == null )
     			{
		    		this.m_E_UpOpenDealText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_GamePlan/E_UpOpenDeal");
     			}
     			return this.m_E_UpOpenDealText;
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

		public UnityEngine.UI.Image E_OpatePlanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OpatePlanImage == null )
     			{
		    		this.m_E_OpatePlanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan");
     			}
     			return this.m_E_OpatePlanImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_PengButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PengButton == null )
     			{
		    		this.m_EButton_PengButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Peng");
     			}
     			return this.m_EButton_PengButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_PengImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PengImage == null )
     			{
		    		this.m_EButton_PengImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Peng");
     			}
     			return this.m_EButton_PengImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_ChiButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ChiButton == null )
     			{
		    		this.m_EButton_ChiButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Chi");
     			}
     			return this.m_EButton_ChiButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_ChiImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ChiImage == null )
     			{
		    		this.m_EButton_ChiImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Chi");
     			}
     			return this.m_EButton_ChiImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_GangButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_GangButton == null )
     			{
		    		this.m_EButton_GangButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Gang");
     			}
     			return this.m_EButton_GangButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_GangImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_GangImage == null )
     			{
		    		this.m_EButton_GangImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Gang");
     			}
     			return this.m_EButton_GangImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_HuButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_HuButton == null )
     			{
		    		this.m_EButton_HuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Hu");
     			}
     			return this.m_EButton_HuButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_HuImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_HuImage == null )
     			{
		    		this.m_EButton_HuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Hu");
     			}
     			return this.m_EButton_HuImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_PassButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PassButton == null )
     			{
		    		this.m_EButton_PassButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Pass");
     			}
     			return this.m_EButton_PassButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_PassImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PassImage == null )
     			{
		    		this.m_EButton_PassImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/EButton_Pass");
     			}
     			return this.m_EButton_PassImage;
     		}
     	}

		public UnityEngine.UI.Image E_SelectCardsImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SelectCardsImage == null )
     			{
		    		this.m_E_SelectCardsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards");
     			}
     			return this.m_E_SelectCardsImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_SelectOneButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SelectOneButton == null )
     			{
		    		this.m_EButton_SelectOneButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards/EButton_SelectOne");
     			}
     			return this.m_EButton_SelectOneButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_SelectOneImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SelectOneImage == null )
     			{
		    		this.m_EButton_SelectOneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards/EButton_SelectOne");
     			}
     			return this.m_EButton_SelectOneImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_SelectTwoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SelectTwoButton == null )
     			{
		    		this.m_EButton_SelectTwoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards/EButton_SelectTwo");
     			}
     			return this.m_EButton_SelectTwoButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_SelectTwoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SelectTwoImage == null )
     			{
		    		this.m_EButton_SelectTwoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards/EButton_SelectTwo");
     			}
     			return this.m_EButton_SelectTwoImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_SelectThreeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SelectThreeButton == null )
     			{
		    		this.m_EButton_SelectThreeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards/EButton_SelectThree");
     			}
     			return this.m_EButton_SelectThreeButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_SelectThreeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SelectThreeImage == null )
     			{
		    		this.m_EButton_SelectThreeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OpatePlan/E_SelectCards/EButton_SelectThree");
     			}
     			return this.m_EButton_SelectThreeImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_GamePlanImage = null;
			this.m_ELabel_PlayerDownText = null;
			this.m_ELabel_DownOutText = null;
			this.m_E_DownOpenDealText = null;
			this.m_ELabel_LeftPlayerText = null;
			this.m_ELabel_LeftOutText = null;
			this.m_E_LeftOpenDealText = null;
			this.m_ELabel_PlayerReightText = null;
			this.m_ELabel_ReightOutText = null;
			this.m_E_ReightOpenDealText = null;
			this.m_ELabel_PlayerUpText = null;
			this.m_ELabel_UpOutText = null;
			this.m_E_UpOpenDealText = null;
			this.m_ELoopScrollList_CardListLoopHorizontalScrollRect = null;
			this.m_E_BeginBtnPlanImage = null;
			this.m_EButton_ReadyButton = null;
			this.m_EButton_ReadyImage = null;
			this.m_EButton_ExitButton = null;
			this.m_EButton_ExitImage = null;
			this.m_ELabel_RoomIdText = null;
			this.m_E_OpatePlanImage = null;
			this.m_EButton_PengButton = null;
			this.m_EButton_PengImage = null;
			this.m_EButton_ChiButton = null;
			this.m_EButton_ChiImage = null;
			this.m_EButton_GangButton = null;
			this.m_EButton_GangImage = null;
			this.m_EButton_HuButton = null;
			this.m_EButton_HuImage = null;
			this.m_EButton_PassButton = null;
			this.m_EButton_PassImage = null;
			this.m_E_SelectCardsImage = null;
			this.m_EButton_SelectOneButton = null;
			this.m_EButton_SelectOneImage = null;
			this.m_EButton_SelectTwoButton = null;
			this.m_EButton_SelectTwoImage = null;
			this.m_EButton_SelectThreeButton = null;
			this.m_EButton_SelectThreeImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_GamePlanImage = null;
		private UnityEngine.UI.Text m_ELabel_PlayerDownText = null;
		private UnityEngine.UI.Text m_ELabel_DownOutText = null;
		private UnityEngine.UI.Text m_E_DownOpenDealText = null;
		private UnityEngine.UI.Text m_ELabel_LeftPlayerText = null;
		private UnityEngine.UI.Text m_ELabel_LeftOutText = null;
		private UnityEngine.UI.Text m_E_LeftOpenDealText = null;
		private UnityEngine.UI.Text m_ELabel_PlayerReightText = null;
		private UnityEngine.UI.Text m_ELabel_ReightOutText = null;
		private UnityEngine.UI.Text m_E_ReightOpenDealText = null;
		private UnityEngine.UI.Text m_ELabel_PlayerUpText = null;
		private UnityEngine.UI.Text m_ELabel_UpOutText = null;
		private UnityEngine.UI.Text m_E_UpOpenDealText = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_CardListLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_E_BeginBtnPlanImage = null;
		private UnityEngine.UI.Button m_EButton_ReadyButton = null;
		private UnityEngine.UI.Image m_EButton_ReadyImage = null;
		private UnityEngine.UI.Button m_EButton_ExitButton = null;
		private UnityEngine.UI.Image m_EButton_ExitImage = null;
		private UnityEngine.UI.Text m_ELabel_RoomIdText = null;
		private UnityEngine.UI.Image m_E_OpatePlanImage = null;
		private UnityEngine.UI.Button m_EButton_PengButton = null;
		private UnityEngine.UI.Image m_EButton_PengImage = null;
		private UnityEngine.UI.Button m_EButton_ChiButton = null;
		private UnityEngine.UI.Image m_EButton_ChiImage = null;
		private UnityEngine.UI.Button m_EButton_GangButton = null;
		private UnityEngine.UI.Image m_EButton_GangImage = null;
		private UnityEngine.UI.Button m_EButton_HuButton = null;
		private UnityEngine.UI.Image m_EButton_HuImage = null;
		private UnityEngine.UI.Button m_EButton_PassButton = null;
		private UnityEngine.UI.Image m_EButton_PassImage = null;
		private UnityEngine.UI.Image m_E_SelectCardsImage = null;
		private UnityEngine.UI.Button m_EButton_SelectOneButton = null;
		private UnityEngine.UI.Image m_EButton_SelectOneImage = null;
		private UnityEngine.UI.Button m_EButton_SelectTwoButton = null;
		private UnityEngine.UI.Image m_EButton_SelectTwoImage = null;
		private UnityEngine.UI.Button m_EButton_SelectThreeButton = null;
		private UnityEngine.UI.Image m_EButton_SelectThreeImage = null;
		public Transform uiTransform = null;
	}
}
