﻿
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

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_EnterGameButton = null;
			this.m_E_EnterGameImage = null;
			this.m_ELabel_PlayerIdText = null;
			this.m_ELabel_GlodText = null;
			this.m_E_ExitGameButton = null;
			this.m_E_ExitGameImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_EnterGameButton = null;
		private UnityEngine.UI.Image m_E_EnterGameImage = null;
		private UnityEngine.UI.Text m_ELabel_PlayerIdText = null;
		private UnityEngine.UI.Text m_ELabel_GlodText = null;
		private UnityEngine.UI.Button m_E_ExitGameButton = null;
		private UnityEngine.UI.Image m_E_ExitGameImage = null;
		public Transform uiTransform = null;
	}
}
