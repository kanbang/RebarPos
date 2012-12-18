// ComBOQTable.cpp : Implementation of CComBOQTable

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#pragma warning( push )
#pragma warning( disable : 4100 )
#include "dbsymtb.h"
#include "axpnt3d.h"
#include "axpnt2d.h"
#include "dbxutil.h"
#include "dbapserv.h"
#include "axlock.h"
#pragma warning( pop )

#include "COMRebarPos_i.h"
#include "COMBOQTable.h"

#include "../NativeRebarPos/BOQTable.h"

/////////////////////////////////////////////////////////////////////////////
// CComBOQTable

STDMETHODIMP CComBOQTable::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IComBOQTable,
        &IID_IAcadObject,
        &IID_IAcadEntity,
        &IID_IOPMPropertyExpander
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i], riid))
			return S_OK;
	}
	return S_FALSE;
}

//Override return custom property names
STDMETHODIMP CComBOQTable::GetDisplayName( 
	/* [in] */ DISPID dispID,
	/* [out] */ BSTR * propName)
{
	if(dispID == 0x401)
	{
		*propName = ::SysAllocString(L"BOQTable");

		return S_OK;
	}

	return IOPMPropertyExtensionImpl<CComBOQTable>::GetDisplayName(dispID, propName);
}

//Override to make property read-only
STDMETHODIMP CComBOQTable::Editable( 
	/* [in] */ DISPID dispID,
	/* [out] */ BOOL __RPC_FAR *bEditable)
{
	return IOPMPropertyExtensionImpl<CComBOQTable>::Editable(dispID, bEditable);
}

//Override to hide the property from display
STDMETHODIMP CComBOQTable::ShowProperty(
	/* [in] */ DISPID dispID, 
	/* [out] */ BOOL *pShow)
{
	return IOPMPropertyExtensionImpl<CComBOQTable>::ShowProperty(dispID, pShow);
}

//This is used to get the value for an element in a group.
//The element is identified by the dwCookie parameter
STDMETHODIMP CComBOQTable::GetElementValue(
	/* [in] */ DISPID dispID,
	/* [in] */ DWORD dwCookie,
	/* [out] */ VARIANT * pVarOut)
{
    CHECKOUTPARAM(pVarOut);

    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef, AcDb::kForRead, Adesk::kTrue);
        if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;

		AcAxPoint3d pt;
		switch(dispID)
		{
		case DISPID_TBASEPOINT:
			{
				if (dwCookie > 2)
					throw Acad::eInvalidInput;

				pt = pBOQTable->BasePoint();
				acdbWcs2Ucs(asDblArray(pt), asDblArray(pt), false);
				CComVariant var(pt[dwCookie]);
				
				::VariantCopy(pVarOut, &var);

				return S_OK;
			}
			break;
		default:
			;
        }
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComBOQTable, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument." ,IID_IComBOQTable, hr);
    }

	return E_NOTIMPL;
}

//This is used to set the value for an element in a group.
//The element is identified by the dwCookie parameter
STDMETHODIMP CComBOQTable::SetElementValue(
	/* [in] */ DISPID dispID,
	/* [in] */ DWORD dwCookie,
	/* [in] */ VARIANT VarIn)
{
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
        if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;

		AcAxPoint3d pt;
		switch(dispID)
		{
		case DISPID_TBASEPOINT:
			if (dwCookie > 2)
				throw Acad::eInvalidInput;

			pt = pBOQTable->BasePoint();
			acdbUcs2Wcs(asDblArray(pt), asDblArray(pt), false);
	        pt[dwCookie] = V_R8(&VarIn);

		    if ((es = pBOQTable->setBasePoint(pt)) != Acad::eOk)
			    throw es;

			Fire_Notification(dispID);

			return S_OK;
			break;
		default:
			;
        }
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComBOQTable, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComBOQTable, hr);
    }

	return E_NOTIMPL;
}

//This is called to get the display string for each
//element in a group.
STDMETHODIMP CComBOQTable::GetElementStrings( 
	/* [in] */ DISPID dispID,
	/* [out] */ OPMLPOLESTR __RPC_FAR *pCaStringsOut,
	/* [out] */ OPMDWORD __RPC_FAR *pCaCookiesOut)
{
    long size;

	switch(dispID)
	{
	case DISPID_TBASEPOINT:
        size = 3;
        pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
        pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);
        for (long i = 0; i < size; i++)
            pCaCookiesOut->pElems[i] = i;
        pCaStringsOut->cElems = size;
        pCaCookiesOut->cElems = size;

        pCaStringsOut->pElems[0] = ::SysAllocString(L"Base point X");
        pCaStringsOut->pElems[1] = ::SysAllocString(L"Base point Y");
		pCaStringsOut->pElems[2] = ::SysAllocString(L"Base point Z");

		return S_OK;
		break;
	default:
		;
    }

    return E_NOTIMPL;
}

//This function is called to determine the number of elements in a group
//It defaults to the number of elements in the array (3 in this case)
STDMETHODIMP CComBOQTable::GetElementGrouping(
    /* [in] */ DISPID /* dispID */,
	/* [out] */ short* /* groupingNumber */)
{
    return E_NOTIMPL;
}

//This function is called to determine how many groups there are in
//the array.
//For example in case of the polyline this is the number of vertices.
//We are not implementing this because it defaults to nGroupCnt = 0
STDMETHODIMP CComBOQTable::GetGroupCount(
    /* [in] */ DISPID /* dispID */,
	/* [out] */ long* /* nGroupCnt */)
{
    return E_NOTIMPL;
}

//OPM calls this function for each property to obtain a list of strings and cookies if they are available.
STDMETHODIMP CComBOQTable::GetPredefinedStrings(DISPID dispID, CALPOLESTR *pCaStringsOut, CADWORD *pCaCookiesOut)
{
	USES_CONVERSION;

    return IOPMPropertyExtensionImpl<CComBOQTable>::GetPredefinedStrings(dispID, pCaStringsOut, pCaCookiesOut);
}

//OPM calls this function when the user selects an element from a drop down list. OPM provides
//the cookie that we associated with the element in the GetPredefinedStrings function. We are
//responsible for mapping this cookie back to a value that the properties corresponding put_ function
//can understand. 
STDMETHODIMP CComBOQTable::GetPredefinedValue(DISPID dispID, DWORD dwCookie, VARIANT *pVarOut)
{
	USES_CONVERSION;

	return IOPMPropertyExtensionImpl<CComBOQTable>::GetPredefinedValue(dispID, dwCookie, pVarOut);
}

HRESULT CComBOQTable::CreateNewObject(AcDbObjectId& objId, AcDbObjectId& ownerId, TCHAR* keyName)
{
    try 
    {
        HRESULT hr;
        
        if (FAILED(hr = CreateObject(ownerId, keyName)))
            throw hr;
        if (FAILED(hr = AddToDb(objId, ownerId, keyName)))
            throw hr;
    }
    catch(HRESULT hr)
    {
        //we can become more sophisticated 
        return hr;
    }

    return S_OK;
}

// IAcadBaseObject2Impl
STDMETHODIMP CComBOQTable::ForceDbResident(VARIANT_BOOL *forceDbResident)
{
	if (NULL == forceDbResident)
		return E_POINTER;

	*forceDbResident = ACAX_VARIANT_FALSE;
	return S_OK;
}

STDMETHODIMP CComBOQTable::CreateObject(AcDbObjectId ownerId, TCHAR* /* keyName */) 
{
    try 
    {
        Acad::ErrorStatus es;
        AcDbObjectPointer<CBOQTable> pBOQTable;
        if((es = pBOQTable.create()) != Acad::eOk)
            throw es;

        pBOQTable->setDatabaseDefaults(ownerId.database());
        CBOQTable *pTmp = NULL;
        pBOQTable.release(pTmp);
        
        SetObject((AcDbObject*&)pTmp);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to create object", IID_IComBOQTable, E_FAIL);
    }
    
    return S_OK;
}

STDMETHODIMP CComBOQTable::AddToDb(AcDbObjectId& objId, AcDbObjectId ownerId, TCHAR* /* keyName */)
{
    try 
    {
        AXEntityDocLock(ownerId);

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef, AcDb::kForRead);

        AcDbBlockTableRecordPointer pBlockTableRecord(ownerId, AcDb::kForWrite);
        if((es = pBlockTableRecord.openStatus()) != Acad::eOk)
            throw es;

        if((es = pBlockTableRecord->appendAcDbEntity(objId, pBOQTable.object())) != Acad::eOk)
            throw es;

    }
    catch(const Acad::ErrorStatus)
    {
        //we can became more sophisticated 
        return Error(L"Failed to add object to database", IID_IComBOQTable, E_FAIL);
    }

    return SetObjectId(objId);
}

/************************************************
  OPM Properties
*************************************************/
STDMETHODIMP CComBOQTable::get_BasePoint(VARIANT * pVal)
{
	CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef, AcDb::kForRead, Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        AcAxPoint3d pt;
		pt = pBOQTable->BasePoint();
        pt.setVariant(pVal);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object.", IID_IComBOQTable, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComBOQTable, hr);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_BasePoint(VARIANT newVal)
{
    try
    {
        AcAxPoint3d pt = newVal;
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        if ((es = pBOQTable->setBasePoint(pt)) != Acad::eOk)
            throw es;
        else
            Fire_Notification(DISPID_TBASEPOINT);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComBOQTable, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComBOQTable, hr);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::get_Multiplier(long * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;

        *pVal = pBOQTable->Multiplier();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_Multiplier(long newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pBOQTable->setMultiplier(newVal)) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_TMULTIPLIER);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::get_MaxRows(long * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;

        *pVal = pBOQTable->MaxRows();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_MaxRows(long newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pBOQTable->setMaxRows(newVal)) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_TMAXROWS);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::get_TableSpacing(double * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;

        *pVal = pBOQTable->TableSpacing();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_TableSpacing(double newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pBOQTable->setTableSpacing(newVal)) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_TTABLESPACING);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::get_Scale(double * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;

        *pVal = pBOQTable->Scale();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_Scale(double newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pBOQTable->setScale(newVal)) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_TSCALE);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComBOQTable, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::get_Heading(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pBOQTable->Heading()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_Heading(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pBOQTable->setHeading(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_THEADING);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::get_Footing(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pBOQTable->Footing()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComBOQTable::put_Footing(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CBOQTable> pBOQTable(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pBOQTable.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pBOQTable->setFooting(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_TFOOTING);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}
