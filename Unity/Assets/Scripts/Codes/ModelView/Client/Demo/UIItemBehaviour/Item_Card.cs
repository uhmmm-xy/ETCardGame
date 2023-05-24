
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_Card : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Card BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EButton_CardButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EButton_CardButton == null )
     				{
		    			this.m_EButton_CardButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Card");
     				}
     				return this.m_EButton_CardButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Card");
     			}
     		}
     	}

		public UnityEngine.UI.Image EButton_CardImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EButton_CardImage == null )
     				{
		    			this.m_EButton_CardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Card");
     				}
     				return this.m_EButton_CardImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Card");
     			}
     		}
     	}

		public UnityEngine.UI.Text ELabel_CardValueText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_ELabel_CardValueText == null )
     				{
		    			this.m_ELabel_CardValueText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton_Card/ELabel_CardValue");
     				}
     				return this.m_ELabel_CardValueText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton_Card/ELabel_CardValue");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_CardButton = null;
			this.m_EButton_CardImage = null;
			this.m_ELabel_CardValueText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EButton_CardButton = null;
		private UnityEngine.UI.Image m_EButton_CardImage = null;
		private UnityEngine.UI.Text m_ELabel_CardValueText = null;
		public Transform uiTransform = null;
	}
}
