#if !defined(AFX_EDITABLELISTBOX_H__345SDFC_1440_414B_3442_11723F43335F__INCLUDED_)
#define AFX_EDITABLELISTBOX_H__345SDFC_1440_414B_3442_11723F43335F__INCLUDED_

/////////////////////////////////////////////////////////////////////////////

#include "EditHelper.h"

#define		WM_NOTIFY_UPDATEDATA_ITEM_EDITED			( WM_APP + 04100 )

typedef enum 
{ 
	NORMAL = 0, 
	BOLD, 
	ITALIC 
} FONTSTYLE;

/////////////////////////////////////////////////////////////////////////////

class CEditableListBox : public CCheckListBox
{
	DECLARE_DYNAMIC(CEditableListBox)
	// Construction
public:
	CEditableListBox();

	// Attributes
protected:
	CEditHelper		m_ceEdit;
	int				m_nItemBeingEdited;
	// Operations
protected:
	BOOL IsNameExisted(const CString& strName);
	void ShowErrorMessageBox(const CString& strName);
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);

public:
	void	StartEdit(int nCurSel);
	void	EndEdit(BOOL bCommitText = TRUE);
	void	FinishEdit();
	FONTSTYLE GetFontStyle(int nIndex);
	void SetFontStyle(int nIndex, FONTSTYLE fontStyle);

	// Implementation
public:
	virtual ~CEditableListBox();
	virtual void DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct);
	HWND IsEditingName();

	// Generated message map functions
protected:
	//{{AFX_MSG(CEditableListBox)
	afx_msg LRESULT OnEditFinished(WPARAM wParam, LPARAM lParam);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////
#endif // !defined(AFX_EDITABLELISTBOX_H__345SDFC_1440_414B_3442_11723F43335F__INCLUDED_)
