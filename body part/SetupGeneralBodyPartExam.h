/*=============================================================Aloka===========
Copyright (C) Hitachi Aloka Medical,Ltd. 2012. All rights reserved.

File Name:
	ScreenSetupGBPEI.h
File Description:

Contained Module:

Note:

History:
-- XA-161 --
2015/03/27 PhucLS	S213900-FPT  First create
2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8]
===============================================================Aloka=========*/

#pragma once

//add start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
#include "idSetting_def.h"
#include "EditableListBox.h"
#include "IDSetupGeneral.h"
class CIDSetupGeneral;
//add end #S213900-FPT (20150629 ThuanPV3)/

//add start #S213900-FPT (20150611 ToanDN3) IDScreen(6) Review code/
#define EXAM_ITEM_NUM 25
//add end #S213900-FPT (20150611 ToanDN3)/

using namespace std;

class CSetupGeneralBodyPartExam : public CCustomizedDialog
{
private:
	CEditableListBox m_ctrlLbxExamItem;
	//add start #S213900-FPT (20150626 ThuanPV3) IDSCreen8/
	vector<BODY_PART_EXAM_t> m_vecBodyPartInfo;
	vector<BODY_PART_EXAM_t> m_vecDeletedItems;
	CIDSetupGeneral* m_pParent;
	vector<BOOL> m_vecUserTracking;
	BOOL	m_bFirstAdd;
	int		m_nCurMaxUserNumber;
	//add end #S213900-FPT (20150626 ThuanPV3)
	DECLARE_DYNAMIC(CSetupGeneralBodyPartExam)
	
	void InitControls();
protected:
	DECLARE_MESSAGE_MAP()
	virtual void DoDataExchange(CDataExchange* pDX);

	virtual BOOL OnInitDialog();
	//add start #S213900-FPT (20150626 ThuanPV3) IDSCreen8/
	virtual BOOL PreTranslateMessage(MSG* in_pMsg);
	BOOL GetValidNumberForUSER(int * nUserNum);
	void UpdateListItems(int nCurSel, int nDirection);
	void LoadSetting();
	void ShiftItemOrder(int nIndex);
	BOOL IsDeletedItem(int nUserNum, int* nMatchID);
	BOOL ExistedFixedItem(int nSelCount);
	//add end #S213900-FPT (20150626 ThuanPV3)/
public:
	enum{ IDD = IDD_SETUP_G_BODY_PART_EXAM_DLG };
	CSetupGeneralBodyPartExam(CWnd* parent = NULL);
	virtual ~CSetupGeneralBodyPartExam();

	afx_msg void OnBnClickedButtonUp();
	afx_msg void OnBnClickedButtonDown();
	afx_msg void OnBnClickedButtonAdd();
	afx_msg void OnBnClickedButtonEdit();
	afx_msg void OnBnClickedButtonDelete();
	afx_msg void OnBnClickedButtonOk();
	afx_msg void OnLbnSelchangeListExamitems();
	afx_msg void OnBnClickedButtonCancel();
	afx_msg void OnBnClickedButtonApply();
	//add start #S213900-FPT (20150626 ThuanPV3) IDSCreen8/
	afx_msg void OnCheckchangeListDisplay();
	afx_msg LRESULT OnMessageEndEditName(WPARAM, LPARAM);
	//add end #S213900-FPT (20150626 ThuanPV3)/
};
