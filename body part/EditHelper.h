#if !defined(AFX_EDITHELPER_H__546FD3AFD_CFEC_4226A_8896_B6896DEERE86__INCLUDED_)
#define AFX_EDITHELPER_H__546FD3AFD_CFEC_4226A_8896_B6896DEERE86__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
#ifndef IsUSASCII
#define IsUSASCII(c)		 ( ( c >= 0x20   ) && ( c <= 0x7E   ) )
#endif
#ifndef IsEurope
#define IsEurope(c)	 		 ( ( c >= 0xA0   ) && ( c <= 0xFF   ) ) 
#endif

#define		WM_APP_ED_EDIT_FINISHED			( WM_APP + 04101 )		

/////////////////////////////////////////////////////////////////////////////

class CEditHelper : public CEdit
{
private:
	bool m_bIsEditing;

	// Construction
public:
	CEditHelper();
	void SetEditStatus(bool bValue);
	bool GetEditStatus();
	virtual ~CEditHelper();
	// Generated message map functions
protected:
	//{{AFX_MSG(CEditHelper)
	afx_msg void OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnKillFocus(CWnd* pNewWnd);
	LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////
#endif // !defined(AFX_EDITHELPER_H__546FD3AFD_CFEC_4226A_8896_B6896DEERE86__INCLUDED_)
