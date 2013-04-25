// ComBOQTable.h : Declaration of the CComBOQTable

#pragma once

#include "resource.h"       // main symbols
#include "dynprops.h"
#include "axtempl.h"
#include "tchar.h"

// DISPIDs for resources
#define	DISPID_TBASEPOINT		0x00000001
#define	DISPID_TMULTIPLIER		0x00000002
#define	DISPID_TSCALE			0x00000004
#define	DISPID_THEADING			0x00000005
#define	DISPID_TFOOTING			0x00000006
#define	DISPID_TMAXROWS			0x00000007
#define	DISPID_TTABLESPACING	0x00000008

/////////////////////////////////////////////////////////////////////////////
// CComBOQTable
class ATL_NO_VTABLE CComBOQTable : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CComBOQTable, &CLSID_ComBOQTable>,
	public ISupportErrorInfo,
	public IAcadEntityDispatchImpl<CComBOQTable, &CLSID_ComBOQTable, 
                                   IComBOQTable, &IID_IComBOQTable, 
                                   &LIBID_COMREBARPOSLib>,
    //These are the OPM related interfaces. These interfaces
    //are usually not used by any other ObjectDBX host than 
    //AutoCAD, although they could be. AutoCAD uses them
    //to customize the display of this object's properties
    //in the Object Property Manager pane.
    public IOPMPropertyExtensionImpl<CComBOQTable>,
    //If a property is VT_ARRAY then IOPMPropertyExpander is QueryInterface-d for
    public IOPMPropertyExpander
{
public:
	CComBOQTable()
	{
	}

	static HRESULT WINAPI UpdateRegistry(BOOL /* bRegister */) throw()
	{
		// Do nothing. COM registration is handled by the resource script.
		return S_OK;
	}
	
	DECLARE_NOT_AGGREGATABLE(CComBOQTable)

BEGIN_COM_MAP(CComBOQTable)
	COM_INTERFACE_ENTRY(IComBOQTable)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
    COM_INTERFACE_ENTRY(IAcadBaseObject)
    COM_INTERFACE_ENTRY(IAcadBaseObject2)
	COM_INTERFACE_ENTRY(IAcadObject)
	COM_INTERFACE_ENTRY(IAcadEntity)
    COM_INTERFACE_ENTRY(IRetrieveApplication)
	COM_INTERFACE_ENTRY_IMPL(IConnectionPointContainer)
    COM_INTERFACE_ENTRY(IOPMPropertyExtension)
    COM_INTERFACE_ENTRY(ICategorizeProperties)
    COM_INTERFACE_ENTRY(IPerPropertyBrowsing)
    COM_INTERFACE_ENTRY(IOPMPropertyExpander)
END_COM_MAP()


// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// IAcadBaseObjectImpl
    virtual HRESULT CreateNewObject(AcDbObjectId& objId, AcDbObjectId& ownerId, TCHAR* keyName);

// IAcadBaseObject2Impl
    STDMETHOD(ForceDbResident)(VARIANT_BOOL *forceDbResident);
    STDMETHOD(AddToDb)(AcDbObjectId& objId, AcDbObjectId ownerId = AcDbObjectId::kNull, TCHAR* keyName = NULL);
    STDMETHOD(CreateObject)(AcDbObjectId ownerId = AcDbObjectId::kNull, TCHAR *keyName = NULL);

// IComBOQTable
public:
	STDMETHOD(get_BasePoint)(/*[out, retval]*/ VARIANT *pVal);
	STDMETHOD(put_BasePoint)(/*[in]*/ VARIANT newVal);
	STDMETHOD(get_Multiplier)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_Multiplier)(/*[in]*/ long newVal);
	STDMETHOD(get_MaxRows)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_MaxRows)(/*[in]*/ long newVal);
	STDMETHOD(get_TableSpacing)(/*[out, retval]*/ double *pVal);
	STDMETHOD(put_TableSpacing)(/*[in]*/ double newVal);
	STDMETHOD(get_Scale)(/*[out, retval]*/ double *pVal);
	STDMETHOD(put_Scale)(/*[in]*/ double newVal);
	STDMETHOD(get_Heading)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Heading)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Footing)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Footing)(/*[in]*/ BSTR newVal);
//
// OPM
//

// IOPMPropertyExtension
BEGIN_OPMPROP_MAP()
    OPMPROP_ENTRY(0, DISPID_TBASEPOINT, PROPCAT_Position, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_TMULTIPLIER, PROPCAT_Data, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_TMAXROWS, PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_TTABLESPACING, PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_TSCALE, PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_THEADING, PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_TFOOTING, PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
END_OPMPROP_MAP()

	STDMETHOD(GetDisplayName)(
		/* [in] */ DISPID dispID, 
		/* [out] */ BSTR * propName);

    STDMETHOD(GetCategoryName)(THIS_
                           /* [in]  */ PROPCAT /* propcat */, 
                           /* [in]  */ LCID /* lcid */,
                           /* [out] */ BSTR* /* pbstrName*/ ) { return S_FALSE;}

    //Override to make property read-only
	STDMETHOD(Editable)( 
		/* [in] */ DISPID dispID,
		/* [out] */ BOOL __RPC_FAR *bEditable);

    //Override to hide the property from display
	STDMETHOD(ShowProperty)(
		/* [in] */ DISPID dispID, 
		/* [out] */ BOOL *pShow);

    virtual HINSTANCE GetResourceInstance()
    {
        return _Module.GetResourceInstance();
    }
    //Used for property expansion (currently variant types)
	STDMETHOD(GetElementValue)(
		/* [in] */ DISPID dispID,
		/* [in] */ DWORD dwCookie,
		/* [out] */ VARIANT * pVarOut);

    //Used for property expansion (currently variant types)
	STDMETHOD(SetElementValue)(
		/* [in] */ DISPID dispID,
		/* [in] */ DWORD dwCookie,
		/* [in] */ VARIANT VarIn);       

    //Used for property expansion (currently variant types)
	STDMETHOD(GetElementStrings)( 
		/* [in] */ DISPID dispID,
		/* [out] */ OPMLPOLESTR __RPC_FAR *pCaStringsOut,
		/* [out] */ OPMDWORD __RPC_FAR *pCaCookiesOut);

    //Used for property expansion (currently variant types)
    STDMETHOD(GetElementGrouping)(
        /* [in] */ DISPID dispID,
		/* [out] */ short *groupingNumber);

    //Used for property expansion (currently variant types)
    STDMETHOD(GetGroupCount)(
        /* [in] */ DISPID dispID,
		/* [out] */ long *nGroupCnt);
    STDMETHOD(GetPredefinedStrings)(
        /* [in] */ DISPID dispID,
        /* [out] */ CALPOLESTR *pCaStringsOut,
        /* [out] */ CADWORD *pCaCookiesOut);
    STDMETHOD(GetPredefinedValue)(
        /* [in] */ DISPID dispID, 
        /* [out] */ DWORD dwCookie, 
        /* [out] */ VARIANT *pVarOut);
};

