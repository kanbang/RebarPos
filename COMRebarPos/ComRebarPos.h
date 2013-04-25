// ComRebarPos.h : Declaration of the CComRebarPos

#pragma once

#include "resource.h"       // main symbols
#include "dynprops.h"
#include "axtempl.h"
#include "tchar.h"

// DISPIDs for resources
#define	DISPID_BASEPOINT	0x00000001
#define	DISPID_NOTEGRIP		0x00000002
#define	DISPID_LENGTHGRIP	0x00000012
#define	DISPID_POS			0x00000003
#define	DISPID_COUNT		0x00000004
#define	DISPID_DIAMETER		0x00000005
#define	DISPID_SPACING		0x00000006
#define	DISPID_MULTIPLIER	0x00000007
#define	DISPID_SHOWLENGTH	0x00000008
#define	DISPID_NOTE			0x00000009
#define	DISPID_A			0x0000000A
#define	DISPID_B			0x0000000B
#define	DISPID_C			0x0000000C
#define	DISPID_D			0x0000000D
#define	DISPID_E			0x0000000E
#define	DISPID_F			0x0000000F
#define	DISPID_LENGTH		0x00000010
#define	DISPID_SHAPE		0x00000011
#define	DISPID_SCALE		0x00000013
#define	DISPID_LENGTHAL		0x00000014
#define	DISPID_NOTEAL		0x00000015

/////////////////////////////////////////////////////////////////////////////
// CComRebarPos
class ATL_NO_VTABLE CComRebarPos : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CComRebarPos, &CLSID_ComRebarPos>,
	public ISupportErrorInfo,
	public IAcadEntityDispatchImpl<CComRebarPos, &CLSID_ComRebarPos, 
                                   IComRebarPos, &IID_IComRebarPos, 
                                   &LIBID_COMREBARPOSLib>,
    //These are the OPM related interfaces. These interfaces
    //are usually not used by any other ObjectDBX host than 
    //AutoCAD, although they could be. AutoCAD uses them
    //to customize the display of this object's properties
    //in the Object Property Manager pane.
    public IOPMPropertyExtensionImpl<CComRebarPos>,
    //If a property is VT_ARRAY then IOPMPropertyExpander is QueryInterface-d for
    public IOPMPropertyExpander
{
public:
	CComRebarPos()
	{
	}

	static HRESULT WINAPI UpdateRegistry(BOOL /* bRegister */) throw()
	{
		// Do nothing. COM registration is handled by the resource script.
		return S_OK;
	}

DECLARE_NOT_AGGREGATABLE(CComRebarPos)

BEGIN_COM_MAP(CComRebarPos)
	COM_INTERFACE_ENTRY(IComRebarPos)
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

// IComRebarPos
public:
	STDMETHOD(get_BasePoint)(/*[out, retval]*/ VARIANT *pVal);
	STDMETHOD(put_BasePoint)(/*[in]*/ VARIANT newVal);
	STDMETHOD(get_NoteGrip)(/*[out, retval]*/ VARIANT *pVal);
	STDMETHOD(put_NoteGrip)(/*[in]*/ VARIANT newVal);
	STDMETHOD(get_LengthGrip)(/*[out, retval]*/ VARIANT *pVal);
	STDMETHOD(put_LengthGrip)(/*[in]*/ VARIANT newVal);
	STDMETHOD(get_Pos)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Pos)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Count)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Count)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Diameter)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Diameter)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Spacing)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Spacing)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Multiplier)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_Multiplier)(/*[in]*/ long newVal);
	STDMETHOD(get_ShowLength)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_ShowLength)(/*[in]*/ long newVal);
	STDMETHOD(get_Note)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Note)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_A)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_A)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_B)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_B)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_C)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_C)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_D)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_D)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_E)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_E)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_F)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_F)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Length)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(get_Shape)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Shape)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Scale)(/*[out, retval]*/ double *pVal);
	STDMETHOD(put_Scale)(/*[in]*/ double newVal);
	STDMETHOD(get_LengthAlignment)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_LengthAlignment)(/*[in]*/ long newVal);
	STDMETHOD(get_NoteAlignment)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_NoteAlignment)(/*[in]*/ long newVal);

//
// OPM
//

// IOPMPropertyExtension
BEGIN_OPMPROP_MAP()
    OPMPROP_ENTRY(0, DISPID_BASEPOINT,  PROPCAT_Position,   0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_NOTEGRIP,   PROPCAT_Position,   0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_LENGTHGRIP, PROPCAT_Position,   0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_POS,        PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_COUNT,      PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_DIAMETER,   PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_SPACING,    PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_MULTIPLIER, PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_SHOWLENGTH, PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_NOTE,       PROPCAT_Text,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_A,          PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_B,          PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_C,          PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_D,          PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_E,          PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_F,          PROPCAT_Data,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_LENGTH,     PROPCAT_Data,       0, 0, 0, _T(""), 0, 0, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_SHAPE,      PROPCAT_Misc,       0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_SCALE,      PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_LENGTHAL,   PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
    OPMPROP_ENTRY(0, DISPID_NOTEAL,     PROPCAT_Appearance, 0, 0, 0, _T(""), 0, 1, IID_NULL, IID_NULL, "")
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

