#include "stdafx.h"
#include "EditableListBox.h"
#include "idInput_msg_box.h"
#include "..\idInput\id\Maintenance\id_datamng_defs.h"
#include "CustomizedMsgBox.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

IMPLEMENT_DYNAMIC(CEditableListBox, CCheckListBox)

/////////////////////////////////////////////////////////////////////////////
// CEditableListBox

/*=============================================================Aloka===========
Module Name:
   CEditableListBox
Calling Sequence:
   CEditableListBox()
Function:
   constructor method
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
CEditableListBox::CEditableListBox()
{
	m_nItemBeingEdited = -1;
}

/*=============================================================Aloka===========
Module Name:
   ~CEditableListBox
Calling Sequence:
   ~CEditableListBox()
Function:
   destructor method
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
CEditableListBox::~CEditableListBox()
{
	if (m_ceEdit.GetSafeHwnd() != NULL)
		m_ceEdit.DestroyWindow();
}

/////////////////////////////////////////////////////////////////////////////
//	PROTECTED
/////////////////////////////////////////////////////////////////////////////
/*=============================================================Aloka===========
Module Name:
   StartEdit
Calling Sequence:
   void StartEdit(int nCurSel)
Function:
   Start the edit an item of listbox
Arguments:
   [Input]
      int  nCurSel	current selected item
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditableListBox::StartEdit(int nCurSel)
{
	if (nCurSel != LB_ERR)
	{
		CRect rItem;

		// Get the rectangle and position this item occupies in the listbox
		GetItemRect(nCurSel, rItem);

		// Make the rectangle a bit larger top-to-bottom.
		rItem.InflateRect(0, 2, 0, 2);

		// Create the edit control
		m_ceEdit.Create(WS_VISIBLE | WS_CHILD | WS_BORDER | ES_LEFT | ES_AUTOHSCROLL, rItem, this, 1);
		m_ceEdit.ModifyStyleEx(0, WS_EX_CLIENTEDGE);
		m_ceEdit.LimitText(BODYPART_ITEM_MAX_LEN);

		// Give it the same font as the listbox
		m_ceEdit.SetFont(this->GetFont());

		// Now add the item's text to it and selected for convenience
		CString csItem(_T(""));

		GetText(nCurSel, csItem);

		m_ceEdit.SetWindowText(csItem);
		m_ceEdit.SetSel(0, -1, TRUE);

		// Set the focus on it
		m_ceEdit.SetFocus();

		// Record the item position
		m_nItemBeingEdited = nCurSel;

		// Set start edit
		m_ceEdit.SetEditStatus(true);
	}
}

/*=============================================================Aloka===========
Module Name:
   IsNameExisted
Calling Sequence:
   BOOL IsNameExisted(CString strNewName)
Function:
   Check for existing of item's name
Arguments:
   [Input]
      CString  strNewName
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
BOOL CEditableListBox::IsNameExisted(const CString& strName)
{
	int nCount = GetCount();
	CString strTemp(_T(""));

	for (int nIdx = 0; nIdx < nCount; nIdx++)
	{
		GetText(nIdx, strTemp);
		if (!GetSel(nIdx) && strTemp.Compare(strName) == 0)
		{
			return TRUE;
		}
	}
	
	return FALSE;
}

/*=============================================================Aloka===========
Module Name:
   ShowErrorMessageBox
Calling Sequence:
   void ShowErrorMessageBox(const CString & strName)
Function:
   Show error messagebox
Arguments:
   [Input]
      const  CString & strName		new name of item
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditableListBox::ShowErrorMessageBox(const CString& strName)
{
	mfcMSG_t	pInf;			/* メッセージボックス情報 */
	_TCHAR lpszMsg[MAX_PATH];

	/* エラーメッセージの表示 */
	pInf.bPoint = FALSE;						/* 表示位置有効 */
	pInf.bSize = FALSE;							/* サイズ有効 */

	pInf.nMsg = 1;								/* メッセージ数 */
	pInf.Msg[0].nAlign = mfcMSG_ALIGN_LEFT;		/* メッセージ表示位置 */
	_sntprintf_s(lpszMsg, _countof(lpszMsg), _TRUNCATE, _T("%s %s"), strName, GetMLString(ML_ID_NAME_EXISTED_CONFIRMATION));
	_safe_strcpy_st(pInf.Msg[0].cStr, lpszMsg);

	pInf.nBtn = 1;						/* ボタン数 */
	_safe_strcpy_st(pInf.Btn[0].cLabel, GetMLString(ML_ID_BUTTON_CLOSE));					/* ボタンラベル1 */

	pInf.Btn[0].pWnd = this;					/* WINDOWポインタ */
	pInf.Btn[0].nID = IDC_BTN_MSG_OK;				/* メッセージID */
	pInf.nDefBtn = 0;							/*デフォルトボタン番号*/

	/*デフォルトボタン番号*/
	CCustomizedMsgBox modalMsgBox;
	modalMsgBox.m_inf = pInf;
	modalMsgBox.m_bIsModal = TRUE;

	INT_PTR msgRet = modalMsgBox.DoModal();
	if (msgRet == IDC_BTN_MSG_OK)
	{
		m_ceEdit.DestroyWindow();
	}
}

/*=============================================================Aloka===========
Module Name:
   EndEdit
Calling Sequence:
   void EndEdit(BOOL bCommitText)
Function:
   Finished editing item
Arguments:
   [Input]
      BOOL  bCommitText
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditableListBox::EndEdit(BOOL bCommitText /* = TRUE */)
{
	CString csOldItem, csNewItem;
	csOldItem.Empty();
	csNewItem.Empty();

	// Is there an edit window lurking about? If so, we are in business
	if (m_ceEdit.GetSafeHwnd() != NULL)
	{
		// Do we want the text entered by the user to replace the selected label item's text?
		if (bCommitText && m_nItemBeingEdited != -1)
		{
			GetText(m_nItemBeingEdited, csOldItem);
			m_ceEdit.GetWindowText(csNewItem);
			// remove backspace in left and right of edit name
			csNewItem.TrimRight();
			// Name is changed same with factory name
			if (IsNameExisted(csNewItem))
			{
				//Show Message Box to confirm
				ShowErrorMessageBox(csNewItem);
				//exit function
				return;
			}
			// If the new text is the same as the old, do nothing
			else if ((csNewItem.IsEmpty() == FALSE) && (csNewItem.GetLength() != 0) && (csOldItem.Compare(csNewItem) != 0))
			{
				BOOL bUpdateName = FALSE;
				// Let the parent know if LBS_NOTIFY is flagged
				if (GetStyle() & LBS_NOTIFY)
				{
					bUpdateName = GetParent()->SendMessage(WM_NOTIFY_UPDATEDATA_ITEM_EDITED, (WPARAM)m_nItemBeingEdited, (LPARAM)(LPCTSTR)csNewItem);
				}
				if (bUpdateName == TRUE)
				{
					/* store previous check status */
					int nCheck = GetCheck(m_nItemBeingEdited);

					DeleteString(m_nItemBeingEdited);
					m_nItemBeingEdited = InsertString(m_nItemBeingEdited, csNewItem);
					SetSel(m_nItemBeingEdited);

					/* restore check statuss */
					SetCheck(m_nItemBeingEdited, nCheck);

					/* Set font style */
					SetFontStyle(m_nItemBeingEdited, (FONTSTYLE)BOLD);
				}
			}
		}
		// Release input context, destroy edit window
		FinishEdit();
	}
}

/*=============================================================Aloka===========
Module Name:
   FinishEdit
Calling Sequence:
   void FinishEdit()
Function:
   Refresh context and destroy edit control
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditableListBox::FinishEdit()
{
	// Get IME context
	HIMC hImc = ImmGetContext(m_hWnd);
	if (hImc != NULL)
	{
		// Check IME status is open or not
		int iResult = ImmGetOpenStatus(hImc);
		// In the case of IME status opened
		if (iResult != 0)
		{
			// Notify IME that it must clear composition string
			ImmNotifyIME(hImc, NI_COMPOSITIONSTR, CPS_CANCEL, NULL);
		}
	}
	// Release the input context and unlock the memory associated in the input context
	ImmReleaseContext(m_hWnd, hImc);

	// The editing is done, nothing is marked for editing
	m_nItemBeingEdited = -1;
	::SetFocus(GetParent()->GetSafeHwnd());

	if (m_ceEdit.GetSafeHwnd() != NULL)
	{
		// Set end status
		m_ceEdit.SetEditStatus(false);
		m_ceEdit.DestroyWindow();
		// Get rid of the editing window
		Invalidate();
	}
}

BEGIN_MESSAGE_MAP(CEditableListBox, CCheckListBox)
	//{{AFX_MSG_MAP(CEditableListBox)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_CHAR()
	//}}AFX_MSG_MAP
	ON_MESSAGE(WM_APP_ED_EDIT_FINISHED, OnEditFinished)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CEditableListBox message handlers

/*=============================================================Aloka===========
Module Name:
   DrawItem
Calling Sequence:
   void DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct)
Function:
   Draw item event
Arguments:
   [Input]
      LPDRAWITEMSTRUCT  lpDrawItemStruct
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditableListBox::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct)
{
	// TODO: Add your code to draw the specified item

	CDC* pDC = CDC::FromHandle(lpDrawItemStruct->hDC);
	CRect rect;

	// Draw the colored rectangle portion
	rect.CopyRect(&lpDrawItemStruct->rcItem);

	pDC->SetBkMode(TRANSPARENT);

	if (lpDrawItemStruct->itemState & ODS_SELECTED)
	{
		pDC->FillSolidRect (rect, GetSysColor(COLOR_HIGHLIGHT));
		pDC->SetTextColor (GetSysColor(COLOR_HIGHLIGHTTEXT));
	}
	else
	{
		pDC->FillSolidRect (rect, GetSysColor (COLOR_WINDOW));
		pDC->SetTextColor (GetSysColor(COLOR_WINDOWTEXT));
	}

	if ((int)(lpDrawItemStruct->itemID) >= 0) // Valid ID
	{
		CString sText;
		int     nIndex;

		CFont newFont;
		CFont *pOldFont;

		nIndex = lpDrawItemStruct->itemID;
		GetText(nIndex, sText);

		FONTSTYLE fontStyle = (FONTSTYLE)GetItemData(nIndex);

		// To avoid unnecessary processing
		if (fontStyle == NORMAL)
		{
			pDC->DrawText(sText, rect, DT_LEFT | DT_VCENTER | DT_SINGLELINE);
			return;
		}
		
		LOGFONT logFont;
		pOldFont = pDC->GetCurrentFont();
		pOldFont->GetLogFont(&logFont);

		switch (fontStyle) 
		{
			case BOLD:   
				logFont.lfWeight = FW_BOLD;
				break;
			case ITALIC:
				logFont.lfItalic = TRUE;
				break;
		}

		newFont.CreateFontIndirect(&logFont);
		pDC->SelectObject (&newFont);
		pDC->DrawText(sText, rect, DT_LEFT | DT_VCENTER | DT_SINGLELINE);
		pDC->SelectObject (pOldFont);
		newFont.DeleteObject ();
	}
}

/*=============================================================Aloka===========
Module Name:
   GetFontStyle
Calling Sequence:
   FONTSTYLE GetFontStyle(int nIndex)
Function:
   Get fontstyle of an item
Arguments:
   [Input]
      int  nIndex		item's index
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
FONTSTYLE CEditableListBox::GetFontStyle(int nIndex)
{
	return (FONTSTYLE)GetItemData(nIndex);
}


/*=============================================================Aloka===========
Module Name:
   SetFontStyle
Calling Sequence:
   void SetFontStyle(int nIndex, FONTSTYLE fontStyle)
Function:
   Set fontstyle for an item
Arguments:
   [Input]
      int        nIndex		item's index
      FONTSTYLE  fontStyle	item's fontstyle
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditableListBox::SetFontStyle(int nIndex, FONTSTYLE fontStyle)
{
	SetItemData(nIndex, (DWORD)fontStyle);
	Invalidate();

}


/*=============================================================Aloka===========
Module Name:
   WindowProc
Calling Sequence:
   LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
Function:
   Window procedure
Arguments:
   [Input]
      UINT    message		window message
      WPARAM  wParam
      LPARAM  lParam
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
LRESULT CEditableListBox::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	switch(message)
	{
		case WM_VSCROLL:  
		case WM_MOUSEWHEEL :
		{
			if(m_ceEdit.GetEditStatus())
			{
				return 1;	
			}
			break;
		}
		default :
			break;
	}
	return CListBox::WindowProc(  message,  wParam,  lParam);
}


/*=============================================================Aloka===========
Module Name:
   OnEditFinished
Calling Sequence:
   LRESULT OnEditFinished(WPARAM wParam, LPARAM lParam)
Function:
   Process when editting is finished
Arguments:
   [Input]
      WPARAM  wParam
      LPARAM  lParam
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
LRESULT CEditableListBox::OnEditFinished(WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	EndEdit((BOOL)wParam);

	return FALSE;
}

/*=============================================================Aloka===========
Module Name:
   IsEditingName
Calling Sequence:
   HWND IsEditingName()
Function:
   Check if edit control is changed or not
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
HWND CEditableListBox::IsEditingName()
{
	if(m_ceEdit.GetEditStatus())
	{
		return m_ceEdit.m_hWnd;	
	}
	else
	{
		return NULL;
	}
}
