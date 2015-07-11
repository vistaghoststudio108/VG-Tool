/*=============================================================Aloka===========
Copyright (C) Hitachi Aloka Medical,Ltd. 2012. All rights reserved.

File Name:
	ScreenSetupGBPEI.cpp
File Description:

Contained Module:

Note:

History:
-- XA-161 --
2015/03/27 PhucLS	S213900-FPT  First create
2015/06/26 ThuanPV3 S213900-FPT	 [IDScreen8]
===============================================================Aloka=========*/

#include "stdafx.h"
#include "SetupGeneralBodyPartExam.h"
#include "InputBox.h"
//add start #S213900-FPT (20150626 ThuanPV3) IDSCreen8/
#include "idInput_defs.h"
#include "CustomizedMsgBox.h"
#include "MainIDDialog.h"
#include "idSetting.h"
using namespace idSetting;
extern CMainIDDialog* g_pIdInputDlg;
//add end #S213900-FPT (20150626 ThuanPV3)/

//del start #S213900-FPT (20150611 ToanDN3) IDScreen(6) Review code/
//#define EXAM_ITEM_NUM 25
//del end #S213900-FPT (20150611 ToanDN3)/
//add start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
#define MAX_USER_ITEM			100
#define	NEW_USER_TEXT			_T("USER")
//add end #S213900-FPT (20150629 ThuanPV3)/

TCHAR* ExamItemData[EXAM_ITEM_NUM] =
{
	_T("ABDOMEN"),
	_T("ANKLE"),
	_T("ARM"),
	_T("BREAST"),
	_T("CHEST"),
	_T("CLAVICLE"),
	_T("COCCYX"),
	_T("CSPINE"),
	_T("ELBOW"),
	_T("EXTREMITY"),
	_T("FOOT"),
	_T("HAND"),
	_T("HEAD"),
	_T("HEART"),
	_T("HIP"),
	_T("JAM"),
	_T("KNEE"),
	_T("LEG"),
	_T("LSPINE"),
	_T("NECK"),
	_T("PELVIS"),
	_T("SHOULDER"),
	_T("SKULL"),
	_T("SSPINE"),
	_T("TSPINE"),
};

IMPLEMENT_DYNAMIC(CSetupGeneralBodyPartExam, CCustomizedDialog)

/*=============================================================Aloka===========
Module Name:
	CSetupGeneralBodyPartExam
Function:
	コンストラクタ
Calling Sequence:
	CSetupGeneralBodyPartExam()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/27  ThuanPV3 S213900-FPT	 [IDScreen8] initialize variables
==============================================================Aloka==========*/
CSetupGeneralBodyPartExam::CSetupGeneralBodyPartExam(CWnd* parent) :CCustomizedDialog(CSetupGeneralBodyPartExam::IDD, parent)
{
	m_controlIdMap[IDC_BUTTON_UP]		= ML_ID_UP;
	m_controlIdMap[IDC_BUTTON_DOWN]		= ML_ID_DOWN;
	m_controlIdMap[IDC_BUTTON_ADD]		= ML_ID_ADD;
	m_controlIdMap[IDC_BUTTON_EDIT]		= ML_ID_EDIT_NAME;
	m_controlIdMap[IDC_BUTTON_DELETE]	= ML_ID_DELETE;
	m_controlIdMap[IDC_BUTTON_OK]		= ML_ID_OK;
	m_controlIdMap[IDC_BUTTON_CANCEL]	= ML_ID_CANCEL;
	m_controlIdMap[IDC_BUTTON_APPLY]	= ML_ID_APPLY;
	//add start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	this->m_pParent = (CIDSetupGeneral*)parent;
	m_bFirstAdd = TRUE;
	m_nCurMaxUserNumber = -1;
	//add end #S213900-FPT (20150629 ThuanPV3)/
}

/*=============================================================Aloka===========
Module Name:
	~CSetupGeneralBodyPartExam()
Function:
	デストラクタ
Calling Sequence:
	~CSetupGeneralBodyPartExam()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
==============================================================Aloka==========*/
CSetupGeneralBodyPartExam::~CSetupGeneralBodyPartExam()
{
}

/*=============================================================Aloka===========
Module Name:
	OnInitDialog()
Function:
	Processing when the dialog is initialized
Calling Sequence:
	BOOL OnInitDialog()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
==============================================================Aloka==========*/
BOOL CSetupGeneralBodyPartExam::OnInitDialog()
{
	BOOL _return = CCustomizedDialog::OnInitDialog();
	InitControls();
	InitControlByIdMap();
	ApplyAllSetting();
	return _return;
}

/*=============================================================Aloka===========
Module Name:
	InitDialog()
Function:
	Prepare data for control(s) of Dialog
Calling Sequence:
	void InitControls()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:

History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/14	NghiaPD	S213900-FPT Change display Dialod from DoModal to ShowWindow
		2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8] Load settings
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::InitControls()
{
	//add #start S213900 (20150614 NghiaPD) IDScreen6/
	RECT parentRect = { 0, 0, 0, 0 };
	RECT childRect = { 0, 0, 0, 0 };
	//add #end S213900 (20150614 NghiaPD)/
	m_ctrlLbxExamItem.ResetContent();
	m_ctrlLbxExamItem.SetCheckStyle(BS_AUTOCHECKBOX);
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//for (int index = 0; index < EXAM_ITEM_NUM; index++)
	//{
	//	m_ctrlLbxExamItem.AddString(ExamItemData[index]);
	//	m_ctrlLbxExamItem.SetCheck(index, BST_CHECKED);
	//}
	LoadSetting();
	//mod end #S213900-FPT (20150629 ThuanPV3)/

	GetDlgItem(IDC_BUTTON_UP)->EnableWindow(FALSE);
	GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(FALSE);
	//del start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(FALSE);
	//del end #S213900-FPT (20150629 ThuanPV3)/
	GetDlgItem(IDC_BUTTON_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_BUTTON_DELETE)->EnableWindow(FALSE);

	//add #start S213900 (20150614 NghiaPD) IDScreen6/
	this->GetWindowRect(&childRect);
	this->GetParent()->GetWindowRect(&parentRect);
	this->SetWindowPos(NULL,
		childRect.left + (parentRect.right - parentRect.left) / 2 - (childRect.right - childRect.left) / 2,
		childRect.top + (parentRect.bottom - parentRect.top) / 2 - (childRect.bottom - childRect.top) / 2,
		0,
		0,
		SWP_NOSIZE);
	//add #end S213900 (20150614 NghiaPD)/
}

/*=============================================================Aloka===========
Module Name:
   PreTranslateMessage
Calling Sequence:
   BOOL PreTranslateMessage(MSG * in_pMsg)
Function:
   Pre-translate window's message
Arguments:
   [Input]
      MSG  * in_pMsg
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/08     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
BOOL CSetupGeneralBodyPartExam::PreTranslateMessage(MSG* in_pMsg)
{
	if (in_pMsg->message == WM_KEYDOWN)
	{
		if (in_pMsg->wParam == VK_ESCAPE || in_pMsg->wParam == VK_RETURN){
			return TRUE;
		}
	}
	return CDialog::PreTranslateMessage(in_pMsg);
}

/*=============================================================Aloka===========
Module Name:
   LoadSetting
Calling Sequence:
   void LoadSetting()
Function:
   Load setting from database
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/06/26     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::LoadSetting()
{
	vector<BODY_PART_EXAM_t> vecTempUser = { };
	this->m_vecBodyPartInfo = idSetting::GetBodyPartExamList();
	/* Set default Body Part Exam item */
	if ((int)this->m_vecBodyPartInfo.size() == 0)
	{
		BODY_PART_EXAM_t tempBodyPart;
		for (int nIdx = 0; nIdx < EXAM_ITEM_NUM; nIdx++)
		{
			tempBodyPart.bCheckState = TRUE;
			_tcsncpy_s(tempBodyPart.lpszName, _countof(tempBodyPart.lpszName), ExamItemData[nIdx], _TRUNCATE);
			tempBodyPart.nID = nIdx;
			tempBodyPart.nAction = ITEM_INSERT;

			this->m_vecBodyPartInfo.push_back(tempBodyPart);
		}
	}

	/* Add to list view (only add item which is not deleted before) */
	for (int nIdx = 0; nIdx < (int)this->m_vecBodyPartInfo.size(); nIdx++)
	{
		if (this->m_vecBodyPartInfo[nIdx].nAction != ITEM_DELETE)
		{
			m_ctrlLbxExamItem.AddString(this->m_vecBodyPartInfo[nIdx].lpszName);
			m_ctrlLbxExamItem.SetCheck(nIdx, this->m_vecBodyPartInfo[nIdx].bCheckState);

			if (this->m_vecBodyPartInfo[nIdx].bIsUser)
			{
				m_ctrlLbxExamItem.SetFontStyle(nIdx, (FONTSTYLE)BOLD);
			}
		}
	}

	/* Get all user's items */
	m_vecUserTracking.resize(MAX_USER_ITEM, FALSE);
	vecTempUser = idSetting::GetBodyPartExamList(TRUE);

	for (int nIdx = 0; nIdx < (int)vecTempUser.size(); nIdx++)
	{
		m_vecUserTracking[vecTempUser[nIdx].nID - EXAM_ITEM_NUM] = TRUE;

		/* Find max number of user */
		if (vecTempUser[nIdx].nID > m_nCurMaxUserNumber)
		{
			m_nCurMaxUserNumber = vecTempUser[nIdx].nID - EXAM_ITEM_NUM;
		}
	}

	/* Initialize [Add] button status */
	if ((int)this->m_vecBodyPartInfo.size() == MAX_USER_ITEM + EXAM_ITEM_NUM)
	{
		GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(FALSE);
	}
}

/*=============================================================Aloka===========
Module Name:
	DoDataExchange()
Function:
	Map between control variable and control ID
Calling Sequence:
	void DoDataExchange()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_EXAMITEMS, m_ctrlLbxExamItem);
}


BEGIN_MESSAGE_MAP(CSetupGeneralBodyPartExam, CCustomizedDialog)
	ON_BN_CLICKED(IDC_BUTTON_UP, &CSetupGeneralBodyPartExam::OnBnClickedButtonUp)
	ON_BN_CLICKED(IDC_BUTTON_DOWN, &CSetupGeneralBodyPartExam::OnBnClickedButtonDown)
	ON_BN_CLICKED(IDC_BUTTON_ADD, &CSetupGeneralBodyPartExam::OnBnClickedButtonAdd)
	ON_BN_CLICKED(IDC_BUTTON_EDIT, &CSetupGeneralBodyPartExam::OnBnClickedButtonEdit)
	ON_BN_CLICKED(IDC_BUTTON_DELETE, &CSetupGeneralBodyPartExam::OnBnClickedButtonDelete)
	ON_BN_CLICKED(IDC_BUTTON_OK, &CSetupGeneralBodyPartExam::OnBnClickedButtonOk)
	ON_LBN_SELCHANGE(IDC_LIST_EXAMITEMS, &CSetupGeneralBodyPartExam::OnLbnSelchangeListExamitems)
	ON_BN_CLICKED(IDC_BUTTON_CANCEL, &CSetupGeneralBodyPartExam::OnBnClickedButtonCancel)
	ON_BN_CLICKED(IDC_BUTTON_APPLY, &CSetupGeneralBodyPartExam::OnBnClickedButtonApply)
	//add start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	ON_CLBN_CHKCHANGE(IDC_LIST_EXAMITEMS, OnCheckchangeListDisplay)
	ON_MESSAGE(WM_NOTIFY_UPDATEDATA_ITEM_EDITED, OnMessageEndEditName)
	//add end #S213900-FPT (20150629 ThuanPV3)/
END_MESSAGE_MAP()

/*=============================================================Aloka===========
Module Name:
   OnMessageEndEditName
Calling Sequence:
   LRESULT OnMessageEndEditName(WPARAM wParam, LPARAM lParam)
Function:
   Process when finished the edit an item of listbox
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
LRESULT CSetupGeneralBodyPartExam::OnMessageEndEditName(WPARAM wParam, LPARAM lParam)
{
	CString strName = _T("");
	int nCurSel = (int)wParam;
	strName = (LPCTSTR)lParam;

	_tcsncpy_s(this->m_vecBodyPartInfo[nCurSel].lpszName, _countof(this->m_vecBodyPartInfo[nCurSel].lpszName), strName, _TRUNCATE);
	if (this->m_vecBodyPartInfo[nCurSel].nAction == ITEM_NONE)
	{
		this->m_vecBodyPartInfo[nCurSel].nAction = ITEM_UPDATE;
	}
	return TRUE;
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonUp()
Function:
	Process when click button [Up]
Calling Sequence:
	void OnBnClickedButtonUp()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/26 ThuanPV3 S213900-FPT	 [IDScreen8] Add code for update list
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonUp()
{
	CString data = _T("\0");
	int CheckData = 0;
	int recordNum = m_ctrlLbxExamItem.GetCount();
	int curSel = m_ctrlLbxExamItem.GetCurSel();

	if (!m_ctrlLbxExamItem.GetSel(curSel))
	{
		for (curSel = 0; curSel < recordNum; curSel++)
		{
			if (m_ctrlLbxExamItem.GetSel(curSel))
			{
				break;
			}
		}
	}
	//add start #S213900-FPT (20150626 ThuanPV3) IDSCreen8/
	UpdateListItems(curSel, MOVE_ITEM_UP);
	//add end #S213900-FPT (20150626 ThuanPV3)/
	GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(TRUE);
	if (curSel > 0)
	{
		curSel--;
		CheckData = m_ctrlLbxExamItem.GetCheck(curSel);
		m_ctrlLbxExamItem.GetText(curSel, data);
		m_ctrlLbxExamItem.InsertString(curSel + 2, data);
		m_ctrlLbxExamItem.SetCheck(curSel + 2, CheckData);
		m_ctrlLbxExamItem.DeleteString(curSel);
		m_ctrlLbxExamItem.SetSel(curSel, TRUE);
		if (curSel == 0)
		{
			GetDlgItem(IDC_BUTTON_UP)->EnableWindow(FALSE);
		}
	}
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonDown()
Function:
	Process when click button [Down]
Calling Sequence:
	void OnBnClickedButtonDown()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/26 ThuanPV3 S213900-FPT	 [IDScreen8] Add code for update list
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonDown()
{
	CString data = _T("\0");
	int CheckData = 0;
	int recordNum = m_ctrlLbxExamItem.GetCount();
	int curSel = m_ctrlLbxExamItem.GetCurSel();

	if (!m_ctrlLbxExamItem.GetSel(curSel))
	{
		for (curSel = 0; curSel < recordNum; curSel++)
		{
			if (m_ctrlLbxExamItem.GetSel(curSel))
			{
				break;
			}
		}
	}
	//add start #S213900-FPT (20150626 ThuanPV3) IDSCreen8/
	UpdateListItems(curSel, MOVE_ITEM_DOWN);
	//add end #S213900-FPT (20150626 ThuanPV3)/
	GetDlgItem(IDC_BUTTON_UP)->EnableWindow(TRUE);
	if (curSel < recordNum - 1)
	{
		CheckData = m_ctrlLbxExamItem.GetCheck(curSel);
		m_ctrlLbxExamItem.GetText(curSel, data);
		m_ctrlLbxExamItem.InsertString(curSel + 2, data);
		m_ctrlLbxExamItem.SetCheck(curSel + 2, CheckData);
		m_ctrlLbxExamItem.DeleteString(curSel);
		m_ctrlLbxExamItem.SetSel(curSel + 1, TRUE);
		if (curSel == recordNum - 2)
		{
			GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(FALSE);
		}
	}
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonAdd()
Function:
	Process when click button [Add]
Calling Sequence:
	void OnBnClickedButtonAdd()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/27 ThuanPV3 S213900-FPT	 [IDScreen8] Add more item to list
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonAdd()
{
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//int curSel = m_ctrlLbxExamItem.GetCurSel();
	//int recordNum = m_ctrlLbxExamItem.GetCount();
	//if (!m_ctrlLbxExamItem.GetSel(curSel))
	//{
	//	for (curSel = 0; curSel < recordNum; curSel++)
	//	{
	//		if (m_ctrlLbxExamItem.GetSel(curSel))
	//		{
	//			break;
	//		}
	//	}
	//}

	//CInputBox *dlg = new CInputBox(this);
	//if (dlg->DoModal() == IDOK)
	//{
	//	CString newItem = dlg->GetInputString();
	//	m_ctrlLbxExamItem.InsertString(curSel, newItem);
	//	m_ctrlLbxExamItem.SetSel(curSel + 1, TRUE);
	//}
	CString strTemp(_T(""));
	int nUserNum = 0;
	int nMatchID = 0;
	BODY_PART_EXAM_t tempBody = { };

	if (GetValidNumberForUSER(&nUserNum))
	{
		/* Search match ID in deleted items */
		if (IsDeletedItem(nUserNum, &nMatchID))
		{
			tempBody.nID = nMatchID;
			tempBody.nOrder = (int)m_vecBodyPartInfo.size();
			tempBody.nAction = ITEM_UPDATE;
		}
		/* No item deleted before */
		else
		{
			tempBody.nID = nUserNum + EXAM_ITEM_NUM;
			tempBody.nOrder = (int)m_vecBodyPartInfo.size();
			tempBody.nAction = ITEM_INSERT;
		}

		tempBody.bIsUser = TRUE;

		strTemp.Format(_T("%s%02d"), NEW_USER_TEXT, nUserNum);
		_tcsncpy_s(tempBody.lpszName, _countof(tempBody.lpszName), strTemp, _TRUNCATE);

		m_vecBodyPartInfo.push_back(tempBody);
		m_ctrlLbxExamItem.AddString(strTemp);
	}
	//mod end #S213900-FPT (20150629 ThuanPV3)/
}

/*=============================================================Aloka===========
Module Name:
   IsDeletedItem
Calling Sequence:
   BOOL IsDeletedItem(int nUserNum, int * nMatchID)
Function:
   
Arguments:
   [Input]
      int  nUserNum
      int  * nMatchID
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/01     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
BOOL CSetupGeneralBodyPartExam::IsDeletedItem(int nUserNum, int* nMatchID)
{
	if ((int)this->m_vecDeletedItems.size() == 0)
	{
		return FALSE;
	}

	for (int nIdx = 0; nIdx < (int)this->m_vecDeletedItems.size(); nIdx++)
	{
		if (this->m_vecDeletedItems[nIdx].nID - nUserNum == EXAM_ITEM_NUM)
		{
			*nMatchID = this->m_vecDeletedItems[nIdx].nID;
			std::vector<BODY_PART_EXAM_t>::iterator itorfound = this->m_vecDeletedItems.begin();
			std::advance(itorfound, nIdx);
			this->m_vecDeletedItems.erase(itorfound);
			return TRUE;
		}
	}

	return FALSE;
}

/*=============================================================Aloka===========
Module Name:
   GetValidNumberForUSER
Calling Sequence:
   BOOL GetValidNumberForUSER(int * nUserNum)
Function:
   
Arguments:
   [Input]
      int  * nUserNum
   [Output]
      なし
History:
   -- XA-161 --
   2015/06/26     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
BOOL CSetupGeneralBodyPartExam::GetValidNumberForUSER(int * nUserNum)
{
	CString strTemp(_T(""));
	BOOL bFound = FALSE;

	if (m_nCurMaxUserNumber < MAX_USER_ITEM - 1)
	{
		*nUserNum = m_nCurMaxUserNumber + 1;
		m_vecUserTracking[*nUserNum] = TRUE;
		m_nCurMaxUserNumber++;
		bFound = TRUE;
	}
	else
	{
		for (int nIdx = 0; nIdx < MAX_USER_ITEM; nIdx++)
		{
			if (!m_vecUserTracking[nIdx])
			{
				*nUserNum = nIdx;
				m_vecUserTracking[nIdx] = TRUE;
				bFound = TRUE;
				break;
			}
		}
	}

	return bFound;
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonEdit()
Function:
	Process when click button [Edit Name]
Calling Sequence:
	void OnBnClickedButtonEdit()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonEdit()
{
	int curSel = m_ctrlLbxExamItem.GetCurSel();
	int recordNum = m_ctrlLbxExamItem.GetCount();
	int checkData = 0;
	if (!m_ctrlLbxExamItem.GetSel(curSel))
	{
		for (curSel = 0; curSel < recordNum; curSel++)
		{
			if (m_ctrlLbxExamItem.GetSel(curSel))
			{
				break;
			}
		}
	}
	
	//mod start #S213900-FPT (20150706 ThuanPV3) IDSCreen8---->/
	//CInputBox *dlg = new CInputBox(this);
	//if (dlg->DoModal() == IDOK)
	//{
	//	CString newItem = dlg->GetInputString();
	//	checkData = m_ctrlLbxExamItem.GetCheck(curSel);
	//	m_ctrlLbxExamItem.DeleteString(curSel);
	//	m_ctrlLbxExamItem.InsertString(curSel,newItem);
	//	m_ctrlLbxExamItem.SetCheck(curSel, checkData);
	//	m_ctrlLbxExamItem.SetSel(curSel, TRUE);
	//}
	m_ctrlLbxExamItem.StartEdit(curSel);
	//mod end #S213900-FPT (20150706 ThuanPV3)<----/
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonDelete()
Function:
	Process when click button [Delete]
Calling Sequence:
	void OnBnClickedButtonDelete()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8] Delete items from list
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonDelete()
{
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//int recordNum = m_ctrlLbxExamItem.GetCount();
	//for (int i = recordNum - 1; i >= 0; i--)
	//{
	//	if (m_ctrlLbxExamItem.GetSel(i))
	//	{
	//		m_ctrlLbxExamItem.DeleteString(i);
	//	}
	//}
	mfcMSG_t pInf = { NULL };

	pInf.bPoint = FALSE;	/* 表示位置有効 */
	pInf.bSize = FALSE;		/* サイズ有効 */
	pInf.nBtn = 2;			/* ボタン数 */
	pInf.nMsg = 1;			/* メッセージ数 */

	_safe_strcpy_st(pInf.Btn[0].cLabel, lngGetString(lngCMF_STD_OK));
	_safe_strcpy_st(pInf.Btn[1].cLabel, lngGetString(lngCMF_STD_CANCEL));

	pInf.Msg[0].nAlign = mfcMSG_ALIGN_LEFT;
	_safe_strcpy_st(pInf.Msg[0].cStr, GetMLString(ML_ID_DELETE_ITEM_CONFIRMATION));

	/* WINDOWポインタ */
	pInf.Btn[0].pWnd = this;
	pInf.Btn[1].pWnd = this;

	/* メッセージID */
	pInf.Btn[0].nID = IDC_BTN_MSG_OK;
	pInf.Btn[1].nID = IDC_BTN_MSG_CANCEL;

	/*デフォルトボタン番号*/
	pInf.nDefBtn = 1;

	/*デフォルトボタン番号*/
	CCustomizedMsgBox modalMsgBox;
	modalMsgBox.m_inf = pInf;
	modalMsgBox.m_bIsModal = TRUE;

	INT_PTR msgRet = modalMsgBox.DoModal();
	if (IDC_BTN_MSG_CANCEL == msgRet)
	{
		return;
	}

	/* Get all items selected */
	for (int nIdx = m_ctrlLbxExamItem.GetCount() - 1; nIdx >= 0; nIdx--)
	{
		if (m_ctrlLbxExamItem.GetSel(nIdx))
		{
			m_ctrlLbxExamItem.DeleteString(nIdx);
			m_vecUserTracking[m_vecBodyPartInfo[nIdx].nID - EXAM_ITEM_NUM] = FALSE;

			if (m_vecBodyPartInfo[nIdx].nAction != ITEM_INSERT)
			{
				m_vecBodyPartInfo[nIdx].nAction = ITEM_DELETE;
				m_vecDeletedItems.push_back(m_vecBodyPartInfo[nIdx]);
			}

			//Remove this element out of m_vecBodyPart array
			std::vector<BODY_PART_EXAM_t>::iterator itorfound = this->m_vecBodyPartInfo.begin();
			std::advance(itorfound, nIdx);
			this->m_vecBodyPartInfo.erase(itorfound);

			// Re-update order of all items
			ShiftItemOrder(nIdx);
		}
	}

	/* Re-calculate the maximum number of user */
	for (int nIdx = (int)this->m_vecUserTracking.size() - 1; nIdx >= 0; nIdx--)
	{
		if (this->m_vecUserTracking[nIdx])
		{
			m_nCurMaxUserNumber = nIdx;
			break;
		}
	}
	//mod end #S213900-FPT (20150629 ThuanPV3)/
	GetDlgItem(IDC_BUTTON_UP)->EnableWindow(FALSE);
	GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(FALSE);
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(FALSE);
	GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(TRUE);
	//mod end #S213900-FPT (20150629 ThuanPV3)/
	GetDlgItem(IDC_BUTTON_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_BUTTON_DELETE)->EnableWindow(FALSE);
}

/*=============================================================Aloka===========
Module Name:
   ShiftOrder
Calling Sequence:
   void ShiftOrder(int nIndex)
Function:
   
Arguments:
   [Input]
      int  nIndex
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/01     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::ShiftItemOrder(int nIndex)
{
	for (int nIdx = nIndex; nIdx < (int)this->m_vecBodyPartInfo.size(); nIdx++)
	{
		this->m_vecBodyPartInfo[nIdx].nOrder--;
	}
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonOk()
Function:
	Process when click button [OK]
Calling Sequence:
	void OnBnClickedButtonOk()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/03/27	PhucLS	S213900-FPT First create
		2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8] Add code for destroy window
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonOk()
{
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//CDialog::OnOK();
	if (m_pParent != NULL)
	{
		m_pParent->DestroyChildWindow();
	}
	//mod end #S213900-FPT (20150629 ThuanPV3)/
}

/*=============================================================Aloka===========
Module Name:
   ExistedFixedItem
Calling Sequence:
   BOOL ExistedFixedItem()
Function:
   Check the existing of un-remove item in selected items
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/07/01     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
BOOL CSetupGeneralBodyPartExam::ExistedFixedItem(int nSelCount)
{
	CArray<int, int> arrSel = { };
	arrSel.SetSize(nSelCount);

	/* Get all items selected */
	m_ctrlLbxExamItem.GetSelItems(nSelCount, arrSel.GetData());

	for (int nIdx = 0; nIdx < nSelCount; nIdx++)
	{
		if (!this->m_vecBodyPartInfo[arrSel[nIdx]].bIsUser)
		{
			return TRUE;
		}
	}

	return FALSE;
}

/*=============================================================Aloka===========
Module Name:
	OnLbnSelchangeListExamitems()
Function:
	Process when select items on list
Calling Sequence:
	void OnLbnSelchangeListExamitems()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/04/21	PhucLS	S213900-FPT First create
		2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8] Change status of some buttons
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnLbnSelchangeListExamitems()
{
	int curSel = m_ctrlLbxExamItem.GetCurSel();
	int selNum = m_ctrlLbxExamItem.GetSelCount();
	int recordNum = m_ctrlLbxExamItem.GetCount();

	if (selNum > 0)
	{
		//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
		//GetDlgItem(IDC_BUTTON_DELETE)->EnableWindow(TRUE);
		BOOL bExisted = ExistedFixedItem(selNum);
		GetDlgItem(IDC_BUTTON_DELETE)->EnableWindow(!bExisted);
		//mod end #S213900-FPT (20150629 ThuanPV3)/
		if (selNum > 1)
		{
			GetDlgItem(IDC_BUTTON_UP)->EnableWindow(FALSE);
			GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(FALSE);
			//del start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
			//GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(FALSE);
			//del end #S213900-FPT (20150629 ThuanPV3)/
			GetDlgItem(IDC_BUTTON_EDIT)->EnableWindow(FALSE);
			return;
		}
		GetDlgItem(IDC_BUTTON_UP)->EnableWindow(!m_ctrlLbxExamItem.GetSel(0));
		GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(!m_ctrlLbxExamItem.GetSel(recordNum - 1));
		//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
		//GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(TRUE);
		//GetDlgItem(IDC_BUTTON_EDIT)->EnableWindow(TRUE);
		GetDlgItem(IDC_BUTTON_EDIT)->EnableWindow(!bExisted);
		//mod end #S213900-FPT (20150629 ThuanPV3)/
	}
	else
	{
		GetDlgItem(IDC_BUTTON_UP)->EnableWindow(FALSE);
		GetDlgItem(IDC_BUTTON_DOWN)->EnableWindow(FALSE);
		//del start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
		//GetDlgItem(IDC_BUTTON_ADD)->EnableWindow(FALSE);
		//del end #S213900-FPT (20150629 ThuanPV3)/
		GetDlgItem(IDC_BUTTON_EDIT)->EnableWindow(FALSE);
		GetDlgItem(IDC_BUTTON_DELETE)->EnableWindow(FALSE);
	}
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonCancel()
Function:
	Process when click button [Cancel]
Calling Sequence:
	void OnBnClickedButtonCancel()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/04/21	PhucLS	S213900-FPT First create
		2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8] Destroy dialog
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonCancel()
{
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//CDialog::OnCancel();
	if (m_pParent != NULL)
	{
		m_pParent->DestroyChildWindow();
	}
	//mod end #S213900-FPT (20150629 ThuanPV3)/
}

/*=============================================================Aloka===========
Module Name:
	OnBnClickedButtonApply()
Function:
	Process when click button [Apply]
Calling Sequence:
	void OnBnClickedButtonApply()
Arguments:
	[Inputs]
		なし
	[Outputs]
		なし
Return Value:
	なし
Required Module:

Note:
	なし
History:
	-- XA-161 --
		2015/04/21	PhucLS	S213900-FPT First create
		2015/06/29 ThuanPV3 S213900-FPT	 [IDScreen8] save data and notify
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnBnClickedButtonApply()
{
	//mod start #S213900-FPT (20150629 ThuanPV3) IDSCreen8/
	//CDialog::OnOK();
	if ((int)this->m_vecDeletedItems.size() != 0)
	{
		this->m_vecBodyPartInfo.insert(m_vecBodyPartInfo.end(), this->m_vecDeletedItems.begin(), this->m_vecDeletedItems.end());
	}

	idSetting::SetBodyPartExamList(this->m_vecBodyPartInfo);
	g_pIdInputDlg->m_dlgMainSetting.m_dlgMiddle.UpdateBodyPartExamList();
	if (m_pParent != NULL)
	{
		m_pParent->DestroyChildWindow();
	}
	//mod end #S213900-FPT (20150629 ThuanPV3)/
}

/*=============================================================Aloka===========
Module Name:
   OnCheckchangeListDisplay
Calling Sequence:
   void OnCheckchangeListDisplay()
Function:
   
Arguments:
   [Input]
      なし
   [Output]
      なし
History:
   -- XA-161 --
   2015/06/26     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::OnCheckchangeListDisplay()
{
	CArray<int, int> arrSel = {};
	int nSelCount = 0;

	nSelCount = m_ctrlLbxExamItem.GetSelCount();
	arrSel.SetSize(nSelCount);

	/* Get all items selected */
	m_ctrlLbxExamItem.GetSelItems(nSelCount, arrSel.GetData());
	for (int nIdx = 0; nIdx < nSelCount; nIdx++)
	{
		this->m_vecBodyPartInfo[arrSel[nIdx]].bCheckState = m_ctrlLbxExamItem.GetCheck(arrSel[nIdx]);
		/* Only set action for non-insert item */
		if (this->m_vecBodyPartInfo[arrSel[nIdx]].nAction != ITEM_INSERT)
		{
			this->m_vecBodyPartInfo[arrSel[nIdx]].nAction = ITEM_UPDATE;
		}
	}
}

/*=============================================================Aloka===========
Module Name:
   UpdateListItems
Calling Sequence:
   void UpdateListItems(int nCurSel, int nDirection)
Function:
   
Arguments:
   [Input]
      int  nCurSel
      int  nDirection
   [Output]
      なし
History:
   -- XA-161 --
   2015/06/26     ThuanPV3     [IDScreen8]    Create new
==============================================================Aloka==========*/
void CSetupGeneralBodyPartExam::UpdateListItems(int nCurSel, int nDirection)
{
	BODY_PART_EXAM_t tempBodyPartInfo = { };

	switch (nDirection)
	{
		/* selected item is moved up */
	case MOVE_ITEM_UP:
		tempBodyPartInfo.Copy(this->m_vecBodyPartInfo[nCurSel - 1]);
		this->m_vecBodyPartInfo[nCurSel - 1].Copy(this->m_vecBodyPartInfo[nCurSel]);
		this->m_vecBodyPartInfo[nCurSel].Copy(tempBodyPartInfo);
		break;

		/* selected item is moved down */
	case MOVE_ITEM_DOWN:
		tempBodyPartInfo.Copy(this->m_vecBodyPartInfo[nCurSel + 1]);
		this->m_vecBodyPartInfo[nCurSel + 1].Copy(this->m_vecBodyPartInfo[nCurSel]);
		this->m_vecBodyPartInfo[nCurSel].Copy(tempBodyPartInfo);
		break;

	default:
		break;
	}
}