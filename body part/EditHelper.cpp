#include "stdafx.h"
#include "EditHelper.h"
#include "idInput_defs.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#pragma comment(lib, "imm32.lib")
#include "imm.h"
#define VK_COPY			3
#define VK_CUT			24
#define VK_BS			8

/////////////////////////////////////////////////////////////////////////////
// CEditHelper

/*=============================================================Aloka===========
Module Name:
   CEditHelper
Calling Sequence:
   CEditHelper()
Function:
   Constructor method
Arguments:
   [Input]
      ‚È‚µ
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
CEditHelper::CEditHelper()
{
	m_bIsEditing = false;
}

/*=============================================================Aloka===========
Module Name:
   ~CEditHelper
Calling Sequence:
   ~CEditHelper()
Function:
   destructor method
Arguments:
   [Input]
      ‚È‚µ
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
CEditHelper::~CEditHelper()
{
}


BEGIN_MESSAGE_MAP(CEditHelper, CEdit)
	//{{AFX_MSG_MAP(CEditHelper)
	ON_WM_KEYUP()
	ON_WM_KILLFOCUS()
	ON_WM_CHAR()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CEditHelper message handlers

/*=============================================================Aloka===========
Module Name:
   OnKeyUp
Calling Sequence:
   void OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags)
Function:
   Process when key up
Arguments:
   [Input]
      UINT  nChar		key's char
      UINT  nRepCnt
      UINT  nFlags
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditHelper::OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	switch (nChar)
	{
	case VK_RETURN:
	{
		m_bIsEditing = false;
		GetParent()->SendMessage(WM_APP_ED_EDIT_FINISHED, (WPARAM)TRUE);	// Commit changes
		break;
	}
	case VK_ESCAPE:
	{
		m_bIsEditing = false;
		GetParent()->SendMessage(WM_APP_ED_EDIT_FINISHED, (WPARAM)FALSE);	// Disregard changes
		break;
	}
	default:
	{
		m_bIsEditing = true;
		break;
	}
	}

	CEdit::OnKeyUp(nChar, nRepCnt, nFlags);
}

/*=============================================================Aloka===========
Module Name:
   OnKillFocus
Calling Sequence:
   void OnKillFocus(CWnd * pNewWnd)
Function:
   Process when focus is lost of this control
Arguments:
   [Input]
      CWnd  * pNewWnd
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditHelper::OnKillFocus(CWnd* pNewWnd)
{
	if (m_bIsEditing == true)
	{
		GetParent()->SendMessage(WM_APP_ED_EDIT_FINISHED, (WPARAM)TRUE);	// Commit changes
	}

	CEdit::OnKillFocus(pNewWnd);
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
      UINT    message
      WPARAM  wParam
      LPARAM  lParam
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
LRESULT CEditHelper::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	//When edit control receive the keyboard focus or after it gained the keyboard focus, disable IME
	if ((message == EN_SETFOCUS) || (message == WM_SETFOCUS))
	{
		ImmAssociateContext(m_hWnd, NULL);
	}

	//When Ctr + V, validate length of paste string. If it longer than 64 DLU, ignore it.
	if (message == WM_PASTE)
	{
		OutputDebugString(_T("Processing Paste Operation\n"));
		// Open clipboard success
		if (OpenClipboard() == TRUE)
		{
			CString strBackup, strClipboard, strNew;
			int nStart, nEnd;
			strBackup.Empty();
			strClipboard.Empty();
			strNew.Empty();
			GetWindowText(strBackup);

			//Get handle of clipboard
			HANDLE hClipboardData = GetClipboardData(CF_UNICODETEXT);
			//Lock global memory and get string in clipboard
			strClipboard = (LPCTSTR)GlobalLock(hClipboardData);
			GetSel(nStart, nEnd);
			//Paste to a selected string inside the exist name
			if (((nEnd - nStart) > 0) && (nEnd - nStart) < strBackup.GetLength())
			{
				CString str1, str2;
				str1.Empty();
				str2.Empty();
				str1 = strBackup.Mid(0, nStart);
				str2 = strBackup.Mid(nEnd, strBackup.GetLength());
				//Join the begin part, the clipboard's string, the end part
				strNew.Format(_T("%s%s%s"), (LPCTSTR)str1, (LPCTSTR)strClipboard, (LPCTSTR)str2);
			}
			else if ((nEnd - nStart) == strBackup.GetLength())
			{
				// replace all clipboard string
				strNew = strClipboard;
			}
			else
			{
				//Paste to a position in editbox. Length of new string is sum of clipboard string and backup string.			
				strNew.Format(_T("%s%s"), (LPCTSTR)strClipboard, (LPCTSTR)strBackup);

			}
			if (strNew.GetLength() > BODYPART_ITEM_MAX_LEN)
			{
				// undo to previous name
				SetWindowText(strBackup);
				SetSel(nStart, nEnd, TRUE);
				CloseClipboard();
				return TRUE;
			}
			else
			{
				bool bNotValid = false;
				for (int i = 0; i < strClipboard.GetLength(); i++)
				{
					TCHAR tcLetter = strClipboard[i];
					// Vaildate input data is non ascii and non Europe language 
					if ((IsUSASCII(tcLetter) == false) && (IsEurope(tcLetter) == false))
					{
						bNotValid = true;
						break;
					}
				}
				if (bNotValid)
				{
					// undo to previous name
					SetWindowText(strBackup);
					SetSel(nStart, nEnd, TRUE);
					CloseClipboard();
					return TRUE;
				}
			}
			CloseClipboard();
		}
	}
	return CEdit::WindowProc(message, wParam, lParam);
}

/*=============================================================Aloka===========
Module Name:
   SetEditStatus
Calling Sequence:
   void SetEditStatus(bool bValue)
Function:
   Set edit status for this control
Arguments:
   [Input]
      bool  bValue
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CEditHelper::SetEditStatus(bool bValue)
{
	m_bIsEditing = bValue;
}

/*=============================================================Aloka===========
Module Name:
   GetEditStatus
Calling Sequence:
   bool GetEditStatus()
Function:
   Set edit status for this control
Arguments:
   [Input]
      ‚È‚µ
   [Output]
      ‚È‚µ
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
bool CEditHelper::GetEditStatus()
{
	return m_bIsEditing;
}