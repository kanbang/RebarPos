#include "StdAfx.h"
// ComRebarPos.cpp : Implementation of CComRebarPos
#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "COMRebarPos_i.h"
#include "ComRebarPos.h"
#include "dbsymtb.h"
#include "../NativeRebarPos/RebarPos.h"
#include "../NativeRebarPos/PosShape.h"
#include "../NativeRebarPos/PosGroup.h"
#include "axpnt3d.h"
#include "axpnt2d.h"
#include "dbxutil.h"
#include "dbapserv.h"

#include "axlock.h"

#define AXEntityDocLockNoDbOk(objId)                        \
    AcAxDocLock docLock(objId, AcAxDocLock::kNormal);       \
    if (docLock.lockStatus() != Acad::eNoDatabase && \
        docLock.lockStatus() != Acad::eOk)                  \
        throw docLock.lockStatus();

#define AXEntityDocLock(objId)                              \
    AcAxDocLock docLock(objId, AcAxDocLock::kNormal);       \
    if(docLock.lockStatus() != Acad::eOk)                   \
        throw docLock.lockStatus();

/////////////////////////////////////////////////////////////////////////////
// CComRebarPos
#define CHECKOUTPARAM(x) if (x==NULL) return E_POINTER;

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

	return IOPMPropertyExtensionImpl<CComRebarPos>::Editable(dispID, bEditable);
}

//Override to hide the property from display
STDMETHODIMP CComRebarPos::ShowProperty(
	/* [in] */ DISPID dispID, 
	/* [out] */ BOOL *pShow)
{
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
	default:
		;
    }

    return E_NOTIMPL;
}

//This function is called to determine the number of elements in a group
//It defaults to the number of elements in the array (3 in this case)
STDMETHODIMP CComRebarPos::GetElementGrouping(
    /* [in] */ DISPID dispID,
	/* [out] */ short *groupingNumber)
{
    return E_NOTIMPL;
}

//This function is called to determine how many groups there are in
//the array.
//For example in case of the polyline this is the number of vertices.
//We are not implementing this because it defaults to nGroupCnt = 0
STDMETHODIMP CComRebarPos::GetGroupCount(
    /* [in] */ DISPID dispID,
	/* [out] */ long *nGroupCnt)
{
    return E_NOTIMPL;
}

//OPM calls this function for each property to obtain a list of strings and cookies if they are available.
STDMETHODIMP CComRebarPos::GetPredefinedStrings(DISPID dispID, CALPOLESTR *pCaStringsOut, CADWORD *pCaCookiesOut)
{
	USES_CONVERSION;

	long size, i;
	AcDbDatabase* pDb = NULL;
	AcDbDictionary* pNamedObj = NULL;
	AcDbDictionary* pDict = NULL;
	AcDbDictionaryIterator* pIter = NULL;

	switch(dispID)
	{
	case DISPID_SHAPE:
		pDb = m_objRef.objectId().database();
		if (NULL == pDb)
			pDb = acdbHostApplicationServices()->workingDatabase();
	    
		if (pDb->getNamedObjectsDictionary(pNamedObj, AcDb::kForRead) == Acad::eOk)
		{
			if (pNamedObj->getAt(CPosShape::GetTableName(), (AcDbObject*&)pDict, AcDb::kForRead) == Acad::eOk)
			{
				size = pDict->numEntries();
				if(size > 0)
				{
					pIter = pDict->newIterator();

					mShapeIdArray.removeAll();

					pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
					pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);

					i = 0;
					for( ; !pIter->done(); pIter->next())
					{
						AcDbObjectPointer<CPosShape> pShape (pIter->objectId(), AcDb::kForRead);
						if(pShape.openStatus() == Acad::eOk)
						{
							pCaStringsOut->pElems[i] = ::SysAllocString(CT2W(pShape->Name()));
						}
						pCaCookiesOut->pElems[i] = mShapeIdArray.append(pIter->objectId());
						i++;
					}
					pCaStringsOut->cElems = i;
					pCaCookiesOut->cElems = i;

					if (pIter)
						delete pIter;
				}
				pDict->close();
			}
			pNamedObj->close();
		}
		return S_OK;
		break;
	case DISPID_GROUP:
		pDb = m_objRef.objectId().database();
		if (NULL == pDb)
			pDb = acdbHostApplicationServices()->workingDatabase();
	    
		if (pDb->getNamedObjectsDictionary(pNamedObj, AcDb::kForRead) == Acad::eOk)
		{
			if (pNamedObj->getAt(CPosGroup::GetTableName(), (AcDbObject*&)pDict, AcDb::kForRead) == Acad::eOk)
			{
				size = pDict->numEntries();
				if(size > 0)
				{
					pIter = pDict->newIterator();

					mGroupIdArray.removeAll();

					pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
					pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);

					i = 0;
					for( ; !pIter->done(); pIter->next())
					{
						AcDbObjectPointer<CPosGroup> pGroup (pIter->objectId(), AcDb::kForRead);
						if(pGroup.openStatus() == Acad::eOk)
						{
							pCaStringsOut->pElems[i] = ::SysAllocString(CT2W(pGroup->Name()));
						}
						pCaCookiesOut->pElems[i] = mGroupIdArray.append(pIter->objectId());
						i++;
					}
					pCaStringsOut->cElems = i;
					pCaCookiesOut->cElems = i;

					if (pIter)
						delete pIter;
				}
				pDict->close();
			}
			pNamedObj->close();
		}

		return S_OK;
		break;
	case DISPID_DISPLAY:
		size = 3;

		pCaStringsOut->pElems = (LPOLESTR *)::CoTaskMemAlloc(sizeof(LPOLESTR) * size);
		pCaCookiesOut->pElems = (DWORD *)::CoTaskMemAlloc(sizeof(DWORD) * size);

		pCaStringsOut->pElems[0] = ::SysAllocString(L"Normal");
		pCaCookiesOut->pElems[0] = 0;
		pCaStringsOut->pElems[1] = ::SysAllocString(L"Toplam Boyu Gösterme");
		pCaCookiesOut->pElems[1] = 1;
		pCaStringsOut->pElems[2] = ::SysAllocString(L"Sadece Poz Numarasý");
		pCaCookiesOut->pElems[2] = 2;

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

	AcDbObjectId id = AcDbObjectId::kNull;
	HRESULT hr = E_FAIL;

	switch(dispID)
	{
	case DISPID_SHAPE:
		{
			assert((INT_PTR)dwCookie >= 0);
			assert((INT_PTR)dwCookie < mShapeIdArray.length());
			id = mShapeIdArray[dwCookie];

			AcDbObjectPointer<CPosShape> pShape (id, AcDb::kForRead);
			if(pShape.openStatus() == Acad::eOk)
			{
				hr = S_OK;
				CComVariant var(CT2W(pShape->Name()));
				VariantCopy(pVarOut, &var);
			}	    
			return hr;
		}
		break;
	case DISPID_GROUP:
		{
			assert((INT_PTR)dwCookie >= 0);
			assert((INT_PTR)dwCookie < mGroupIdArray.length());
			id = mGroupIdArray[dwCookie];

			AcDbObjectPointer<CPosGroup> pGroup (id, AcDb::kForRead);
			if(pGroup.openStatus() == Acad::eOk)
			{
				hr = S_OK;
				CComVariant var(CT2W(pGroup->Name()));
				VariantCopy(pVarOut, &var);
			}
			return hr;
		}
		break;
	case DISPID_DISPLAY:
	    assert((INT_PTR)dwCookie >= 0);
		assert((INT_PTR)dwCookie < 3);

		if(dwCookie == 0)
		{
			CComVariant var(L"Normal");
			VariantCopy(pVarOut, &var);
		}
		else if(dwCookie == 1)
		{
			CComVariant var(L"Toplam Boyu Gösterme");
			VariantCopy(pVarOut, &var);
		}
		else
		{
			CComVariant var(L"Sadece Poz Numarasý");
			VariantCopy(pVarOut, &var);
		}

		return S_OK;
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

STDMETHODIMP CComRebarPos::CreateObject(AcDbObjectId ownerId, TCHAR *keyName) 
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

STDMETHODIMP CComRebarPos::AddToDb(AcDbObjectId& objId, AcDbObjectId ownerId, TCHAR* keyName)
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

STDMETHODIMP CComRebarPos::get_DisplayStyle(BSTR * pVal)
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
			*pVal = SysAllocString(L"Normal");
		else if(pRebarPos->Display() == CRebarPos::WITHOUTLENGTH)
			*pVal = SysAllocString(L"Toplam Boyu Gösterme");
		else
			*pVal = SysAllocString(L"Sadece Poz Numarasý");
    }
    catch(const Acad::ErrorStatus)
    {
        return Error(L"Failed to open object", IID_IComRebarPos, E_FAIL);
    }
	return S_OK;
}

STDMETHODIMP CComRebarPos::put_DisplayStyle(BSTR pVal)
{
    try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef,AcDb::kForWrite,Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;
        
        USES_CONVERSION;
		if(wcscmp(pVal, L"Normal") == 0)
			es = pRebarPos->setDisplay(CRebarPos::ALL);
		else if(wcscmp(pVal, L"Toplam Boyu Gösterme") == 0)
			es = pRebarPos->setDisplay(CRebarPos::WITHOUTLENGTH);
		else
			es = pRebarPos->setDisplay(CRebarPos::MARKERONLY);

        if (es != Acad::eOk)
            throw es;
        else 
            Fire_Notification(DISPID_DISPLAY);
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

		AcDbObjectPointer<CPosShape> pShape (pRebarPos->ShapeId(), AcDb::kForRead);
		if((es = pShape.openStatus()) != Acad::eOk)
            throw es;

		USES_CONVERSION;
		*pVal = SysAllocString(CT2W(pShape->Name()));
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

		AcDbDatabase* pDb = NULL;
		AcDbDictionary* pNamedObj = NULL;
		AcDbDictionary* pDict = NULL;
		AcDbDictionaryIterator* it = NULL;
		AcDbObjectId id;

		pDb = pRebarPos->database();
		if (NULL == pDb)
			pDb = acdbHostApplicationServices()->workingDatabase();
	    
		if (pDb->getNamedObjectsDictionary(pNamedObj, AcDb::kForRead) == Acad::eOk)
		{
			if (pNamedObj->getAt(CPosShape::GetTableName(), (AcDbObject*&)pDict, AcDb::kForRead) == Acad::eOk)
			{
				it = pDict->newIterator();
				while(it->next())
				{
					AcDbObjectPointer<CPosShape> pShape (it->objectId(), AcDb::kForRead);
					if((es = pShape.openStatus()) != Acad::eOk)
						throw es;
					if(wcscmp(W2T(newVal), pShape->Name()) == 0)
					{
						if ((es = pRebarPos->setShapeId(it->objectId())) != Acad::eOk)
							throw es;
						else
							Fire_Notification(DISPID_SHAPE);
						break;
					}
				}
				if(it)
					delete it;
				pDict->close();
			}
			pNamedObj->close();
		}
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

STDMETHODIMP CComRebarPos::get_Group(BSTR * pVal)
{
	CHECKOUTPARAM(pVal);
    try
    {
        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForRead, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

		AcDbObjectPointer<CPosGroup> pGroup (pRebarPos->GroupId(), AcDb::kForRead);
		if((es = pGroup.openStatus()) != Acad::eOk)
            throw es;

		USES_CONVERSION;
		*pVal = SysAllocString(CT2W(pGroup->Name()));
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

STDMETHODIMP CComRebarPos::put_Group(BSTR newVal)
{
	try
    {
        AXEntityDocLockNoDbOk(m_objRef.objectId());

        Acad::ErrorStatus es;
        AcAxObjectRefPtr<CRebarPos> pRebarPos(&m_objRef, AcDb::kForWrite, Adesk::kTrue);
	    if((es = pRebarPos.openStatus()) != Acad::eOk)
            throw es;

		AcDbDatabase* pDb = NULL;
		AcDbDictionary* pNamedObj = NULL;
		AcDbDictionary* pDict = NULL;
		AcDbDictionaryIterator* it = NULL;
		AcDbObjectId id;

		pDb = pRebarPos->database();
		if (NULL == pDb)
			pDb = acdbHostApplicationServices()->workingDatabase();
	    
		if (pDb->getNamedObjectsDictionary(pNamedObj, AcDb::kForRead) == Acad::eOk)
		{
			if (pNamedObj->getAt(CPosGroup::GetTableName(), (AcDbObject*&)pDict, AcDb::kForRead) == Acad::eOk)
			{
				it = pDict->newIterator();
				while(it->next())
				{
					AcDbObjectPointer<CPosGroup> pGroup (it->objectId(), AcDb::kForRead);
					if((es = pGroup.openStatus()) != Acad::eOk)
						throw es;
					if(wcscmp(W2T(newVal), pGroup->Name()) == 0)
					{
						if ((es = pRebarPos->setGroupId(it->objectId())) != Acad::eOk)
							throw es;
						else
							Fire_Notification(DISPID_GROUP);
						break;
					}
				}
				if(it)
					delete it;
				pDict->close();
			}
			pNamedObj->close();
		}
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

