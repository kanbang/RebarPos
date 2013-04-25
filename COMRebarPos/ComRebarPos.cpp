// ComRebarPos.cpp : Implementation of CComRebarPos

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
#include "ComRebarPos.h"

#include "../NativeRebarPos/RebarPos.h"
#include "../NativeRebarPos/PosShape.h"

/////////////////////////////////////////////////////////////////////////////
// CComRebarPos

STDMETHODIMP CComRebarPos::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IComRebarPos,
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
STDMETHODIMP CComRebarPos::GetDisplayName( 
	/* [in] */ DISPID dispID,
	/* [out] */ BSTR * propName)
{
	if(dispID == 0x401)
	{
		*propName = ::SysAllocString(L"RebarPos");

		return S_OK;
	}

	return IOPMPropertyExtensionImpl<CComRebarPos>::GetDisplayName(dispID, propName);
}

//Override to make property read-only
STDMETHODIMP CComRebarPos::Editable( 
	/* [in] */ DISPID dispID,
	/* [out] */ BOOL __RPC_FAR *bEditable)
{
	if(dispID >= DISPID_A && dispID <= DISPID_F)
	{
		try
		{
			Acad::ErrorStatus es;
			AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
			if((es = pRebarPos.openStatus()) != Acad::eOk)
				throw es;

			ACHAR* len = NULL;
			switch(dispID)
			{
			case DISPID_A:
				acutUpdString(pRebarPos->A(), len);
				break;
			case DISPID_B:
				acutUpdString(pRebarPos->B(), len);
				break;
			case DISPID_C:
				acutUpdString(pRebarPos->C(), len);
				break;
			case DISPID_D:
				acutUpdString(pRebarPos->D(), len);
				break;
			case DISPID_E:
				acutUpdString(pRebarPos->E(), len);
				break;
			case DISPID_F:
				acutUpdString(pRebarPos->F(), len);
				break;
			default:
				;
			}

			*bEditable = (len != NULL) && (len[0] != _T('\0'));
			acutDelString(len);
			return S_OK;
		}
		catch(const Acad::ErrorStatus)
		{
			return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
		}
	}
	else if(dispID == DISPID_SHAPE)
	{
		*bEditable = 0;
		return S_OK;
	}

	return IOPMPropertyExtensionImpl<CComRebarPos>::Editable(dispID, bEditable);
}

//Override to hide the property from display
STDMETHODIMP CComRebarPos::ShowProperty(
	/* [in] */ DISPID dispID, 
	/* [out] */ BOOL *pShow)
{
	try
	{
		Acad::ErrorStatus es;
		AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
		if((es = pRebarPos.openStatus()) != Acad::eOk)
			throw es;

		if(pRebarPos->Detached() == Adesk::kFalse)
		{
			*pShow = TRUE;
			return S_OK;
		}
		else
		{
			*pShow = (dispID == DISPID_BASEPOINT || dispID == DISPID_POS || dispID == DISPID_SCALE);
			return S_OK;
		}
	}
	catch(const Acad::ErrorStatus)
	{
		return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
	}

	return IOPMPropertyExtensionImpl<CComRebarPos>::ShowProperty(dispID, pShow);
}

//This is used to get the value for an element in a group.
//The element is identified by the dwCookie parameter
STDMETHODIMP CComRebarPos::GetElementValue(
	/* [in] */ DISPID dispID,
	/* [in] */ DWORD dwCookie,
	/* [out] */ VARIANT * pVarOut)
{
    CHECKOUTPARAM(pVarOut);

    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
        if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

		AcAxPoint3d pt;
		switch(dispID)
		{
		case DISPID_BASEPOINT:
			{
				if (dwCookie > 2)
					throw Acad::eInvalidInput;

				pt = pRebarPos->BasePoint();
				acdbWcs2Ucs(asDblArray(pt), asDblArray(pt), false);
				CComVariant var(pt[dwCookie]);
				
				::VariantCopy(pVarOut, &var);

				return S_OK;
			}
			break;
		case DISPID_NOTEGRIP:
			{
				if (dwCookie > 2)
					throw Acad::eInvalidInput;

				pt = pRebarPos->NoteGrip();
				acdbWcs2Ucs(asDblArray(pt), asDblArray(pt), false);
				CComVariant var(pt[dwCookie]);
				::VariantCopy(pVarOut, &var);

				return S_OK;
			}
			break;
		case DISPID_LENGTHGRIP:
			{
				if (dwCookie > 2)
					throw Acad::eInvalidInput;

				pt = pRebarPos->LengthGrip();
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
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument." ,IID_IComRebarPos, hr);
    }

	return E_NOTIMPL;
}

//This is used to set the value for an element in a group.
//The element is identified by the dwCookie parameter
STDMETHODIMP CComRebarPos::SetElementValue(
	/* [in] */ DISPID dispID,
	/* [in] */ DWORD dwCookie,
	/* [in] */ VARIANT VarIn)
{
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
        if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

		AcAxPoint3d pt;
		switch(dispID)
		{
		case DISPID_BASEPOINT:
			if (dwCookie > 2)
				throw Acad::eInvalidInput;

			pt = pRebarPos->BasePoint();
			acdbUcs2Wcs(asDblArray(pt), asDblArray(pt), false);
	        pt[dwCookie] = V_R8(&VarIn);

		    if ((es = pRebarPos->setBasePoint(pt)) != Acad::eOk)
			    throw es;

			Fire_Notification(dispID);

			return S_OK;
			break;
		case DISPID_NOTEGRIP:
			if (dwCookie > 2)
				throw Acad::eInvalidInput;

			pt = pRebarPos->NoteGrip();
			acdbUcs2Wcs(asDblArray(pt), asDblArray(pt), false);
	        pt[dwCookie] = V_R8(&VarIn);

		    if ((es = pRebarPos->setNoteGrip(pt)) != Acad::eOk)
			    throw es;

			Fire_Notification(dispID);

			return S_OK;
			break;
		case DISPID_LENGTHGRIP:
			if (dwCookie > 2)
				throw Acad::eInvalidInput;

			pt = pRebarPos->LengthGrip();
			acdbUcs2Wcs(asDblArray(pt), asDblArray(pt), false);
	        pt[dwCookie] = V_R8(&VarIn);

		    if ((es = pRebarPos->setLengthGrip(pt)) != Acad::eOk)
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
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }

	return E_NOTIMPL;
}

//This is called to get the display string for each
//element in a group.
STDMETHODIMP CComRebarPos::GetElementStrings( 
	/* [in] */ DISPID dispID,
	/* [out] */ OPMLPOLESTR __RPC_FAR *pCaStringsOut,
	/* [out] */ OPMDWORD __RPC_FAR *pCaCookiesOut)
{
    long size;

	switch(dispID)
	{
	case DISPID_BASEPOINT:
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
	case DISPID_NOTEGRIP:
        size = 3;
        pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
        pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);
        for (long i = 0; i < size; i++)
            pCaCookiesOut->pElems[i] = i;
        pCaStringsOut->cElems = size;
        pCaCookiesOut->cElems = size;

        pCaStringsOut->pElems[0] = ::SysAllocString(L"Note location X");
        pCaStringsOut->pElems[1] = ::SysAllocString(L"Note location Y");
		pCaStringsOut->pElems[2] = ::SysAllocString(L"Note location Z");

		return S_OK;
		break;
	case DISPID_LENGTHGRIP:
        size = 3;
        pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
        pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);
        for (long i = 0; i < size; i++)
            pCaCookiesOut->pElems[i] = i;
        pCaStringsOut->cElems = size;
        pCaCookiesOut->cElems = size;

        pCaStringsOut->pElems[0] = ::SysAllocString(L"Length location X");
        pCaStringsOut->pElems[1] = ::SysAllocString(L"Length location Y");
		pCaStringsOut->pElems[2] = ::SysAllocString(L"Length location Z");

		return S_OK;
		break;
	default:
		;
    }

    return E_NOTIMPL;
}

//This function is called to determine the number of elements in a group
//It defaults to the number of elements in the array (3 in this case)
STDMETHODIMP CComRebarPos::GetElementGrouping(
    /* [in] */ DISPID /* dispID */,
	/* [out] */ short* /* groupingNumber */)
{
    return E_NOTIMPL;
}

//This function is called to determine how many groups there are in
//the array.
//For example in case of the polyline this is the number of vertices.
//We are not implementing this because it defaults to nGroupCnt = 0
STDMETHODIMP CComRebarPos::GetGroupCount(
    /* [in] */ DISPID /* dispID */,
	/* [out] */ long* /* nGroupCnt */)
{
    return E_NOTIMPL;
}

//OPM calls this function for each property to obtain a list of strings and cookies if they are available.
STDMETHODIMP CComRebarPos::GetPredefinedStrings(DISPID dispID, CALPOLESTR *pCaStringsOut, CADWORD *pCaCookiesOut)
{
	USES_CONVERSION;

	long size;

	switch(dispID)
	{
	case DISPID_SHOWLENGTH:
		size = 2;

		pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
		pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);

		pCaStringsOut->pElems[0] = ::SysAllocString(L"Yes"); pCaCookiesOut->pElems[0] = 1;
		pCaStringsOut->pElems[1] = ::SysAllocString(L"No");  pCaCookiesOut->pElems[1] = 0;

		pCaStringsOut->cElems = size;
		pCaCookiesOut->cElems = size;

		return S_OK;
		break;
	case DISPID_LENGTHAL:
	case DISPID_NOTEAL:
		size = 4;

		pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
		pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);

		pCaStringsOut->pElems[0] = ::SysAllocString(L"Free");   pCaCookiesOut->pElems[0] = 0;
		pCaStringsOut->pElems[1] = ::SysAllocString(L"Right");  pCaCookiesOut->pElems[1] = 2;
		pCaStringsOut->pElems[2] = ::SysAllocString(L"Top");    pCaCookiesOut->pElems[2] = 3;
		pCaStringsOut->pElems[3] = ::SysAllocString(L"Bottom"); pCaCookiesOut->pElems[3] = 4;

		pCaStringsOut->cElems = size;
		pCaCookiesOut->cElems = size;

		return S_OK;
		break;
	default:
        return IOPMPropertyExtensionImpl<CComRebarPos>::GetPredefinedStrings(dispID, pCaStringsOut, pCaCookiesOut);
	}
}

//OPM calls this function when the user selects an element from a drop down list. OPM provides
//the cookie that we associated with the element in the GetPredefinedStrings function. We are
//responsible for mapping this cookie back to a value that the properties corresponding put_ function
//can understand. 
STDMETHODIMP CComRebarPos::GetPredefinedValue(DISPID dispID, DWORD dwCookie, VARIANT *pVarOut)
{
	USES_CONVERSION;

	switch(dispID)
	{
	case DISPID_SHOWLENGTH:
		{
			assert((INT_PTR)dwCookie <= 1);

			CComVariant var((long)dwCookie);
			VariantCopy(pVarOut, &var);

			return S_OK;
		}
		break;
	case DISPID_LENGTHAL:
	case DISPID_NOTEAL:
		{
			assert((INT_PTR)dwCookie <= 4);

			CComVariant var((long)dwCookie);
			VariantCopy(pVarOut, &var);

			return S_OK;
		}
		break;
	default:
		return IOPMPropertyExtensionImpl<CComRebarPos>::GetPredefinedValue(dispID, dwCookie, pVarOut);
	}
}

HRESULT CComRebarPos::CreateNewObject(AcDbObjectId& objId, AcDbObjectId& ownerId, TCHAR* keyName)
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
STDMETHODIMP CComRebarPos::ForceDbResident(VARIANT_BOOL *forceDbResident)
{
	if (NULL == forceDbResident)
		return E_POINTER;

	*forceDbResident = ACAX_VARIANT_FALSE;
	return S_OK;
}

STDMETHODIMP CComRebarPos::CreateObject(AcDbObjectId ownerId, TCHAR* /* keyName */) 
{
    try 
    {
        Acad::ErrorStatus es;
        AcDbObjectPointer<CRebarPos> pRebarPos;
        if((es = pRebarPos.create()) != Acad::eOk)
            throw es;

        pRebarPos->setDatabaseDefaults(ownerId.database());
        CRebarPos *pTmp = NULL;
        pRebarPos.release(pTmp);
        
        SetObject((AcDbObject*&)pTmp);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to create object", IID_IComRebarPos, E_FAIL);
    }
    
    return S_OK;
}

STDMETHODIMP CComRebarPos::AddToDb(AcDbObjectId& objId, AcDbObjectId ownerId, TCHAR* /* keyName */)
{
    try 
    {
        AXEntityDocLock(ownerId);

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead);

        AcDbBlockTableRecordPointer pBlockTableRecord(ownerId, AcDb::kForWrite);
        if((es = pBlockTableRecord.openStatus()) != Acad::eOk)
            throw es;

        if((es = pBlockTableRecord->appendAcDbEntity(objId, pRebarPos.object())) != Acad::eOk)
            throw es;

    }
    catch(const Acad::ErrorStatus)
    {
        //we can became more sophisticated 
        return Error(L"Failed to add object to database", IID_IComRebarPos, E_FAIL);
    }

    return SetObjectId(objId);
}

/************************************************
  OPM Properties
*************************************************/
STDMETHODIMP CComRebarPos::get_BasePoint(VARIANT * pVal)
{
	CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        AcAxPoint3d pt;
		pt = pRebarPos->BasePoint();
        pt.setVariant(pVal);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_BasePoint(VARIANT newVal)
{
    try
    {
        AcAxPoint3d pt = newVal;
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        if ((es = pRebarPos->setBasePoint(pt)) != Acad::eOk)
            throw es;
        else
            Fire_Notification(DISPID_BASEPOINT);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_NoteGrip(VARIANT * pVal)
{
	CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        AcAxPoint3d pt;
		pt = pRebarPos->NoteGrip();
        pt.setVariant(pVal);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_NoteGrip(VARIANT newVal)
{
    try
    {
        AcAxPoint3d pt = newVal;
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        if ((es = pRebarPos->setNoteGrip(pt)) != Acad::eOk)
            throw es;
        else
            Fire_Notification(DISPID_NOTEGRIP);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_LengthGrip(VARIANT * pVal)
{
	CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        AcAxPoint3d pt;
		pt = pRebarPos->LengthGrip();
        pt.setVariant(pVal);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_LengthGrip(VARIANT newVal)
{
    try
    {
        AcAxPoint3d pt = newVal;
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        if ((es = pRebarPos->setLengthGrip(pt)) != Acad::eOk)
            throw es;
        else
            Fire_Notification(DISPID_LENGTHGRIP);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Pos(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->Pos()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Pos(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setPos(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_POS);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Count(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->Count()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Count(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setCount(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_COUNT);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Diameter(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->Diameter()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Diameter(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setDiameter(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_DIAMETER);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Spacing(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->Spacing()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Spacing(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setSpacing(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_SPACING);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Multiplier(long * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

        *pVal = pRebarPos->Multiplier();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Multiplier(long newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setMultiplier(newVal)) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_MULTIPLIER);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_ShowLength(long * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		if(pRebarPos->Display() == CRebarPos::ALL)
			*pVal = 1;
		else // if(pRebarPos->Display() == CRebarPos::WITHOUTLENGTH)
			*pVal = 0;
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_ShowLength(long pVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		if(pVal == 1)
			es = pRebarPos->setDisplay(CRebarPos::ALL);
		else // if(pVal == 0)
			es = pRebarPos->setDisplay(CRebarPos::WITHOUTLENGTH);

        if (es != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_SHOWLENGTH);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Note(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->Note()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Note(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setNote(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_NOTE);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_A(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->A()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_A(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setA(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_A);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_B(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->B()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_B(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setB(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_B);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_C(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->C()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_C(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setC(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_C);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_D(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->D()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_D(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setD(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_D);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_E(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->E()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_E(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setE(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_E);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_F(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->F()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_F(BSTR newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setF(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_F);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Length(BSTR * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        *pVal = SysAllocString(CT2W(pRebarPos->Length()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Shape(BSTR * pVal)
{
	CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

		USES_CONVERSION;
		*pVal = SysAllocString(CT2W(pRebarPos->Shape()));
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Shape(BSTR newVal)
{
	try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

        USES_CONVERSION;
        if ((es = pRebarPos->setShape(W2T(newVal))) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_SHAPE);   
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
    catch(const HRESULT hr)
    {
        return Error(L"Invalid argument.", IID_IComRebarPos, hr);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_Scale(double * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

        *pVal = pRebarPos->Scale();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_Scale(double newVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
        if ((es = pRebarPos->setScale(newVal)) != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_SCALE);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_LengthAlignment(long * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		*pVal = (long)pRebarPos->LengthAlignment();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_LengthAlignment(long pVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		es = pRebarPos->setLengthAlignment(static_cast<CRebarPos::SubTextAlignment>(pVal));

        if (es != Acad::eOk)
            throw es;
        else 
			Fire_Notification(DISPID_LENGTHAL);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::get_NoteAlignment(long * pVal)
{
    CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForRead,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		*pVal = (long)pRebarPos->NoteAlignment();
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_NoteAlignment(long pVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		es = pRebarPos->setNoteAlignment(static_cast<CRebarPos::SubTextAlignment>(pVal));

        if (es != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_NOTEAL);
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to set property.", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}
