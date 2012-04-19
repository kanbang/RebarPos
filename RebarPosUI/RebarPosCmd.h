#pragma once

// This file contains an object for creating a rebarpos object using
// the Property Palette.

#define DEFINE_CMDFLAG(flagName)                \
private:                                        \
    bool b##flagName;                           \
public:                                         \
    bool flagName() { return b##flagName;};

class ATL_NO_VTABLE CComRebarPosCmd : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public IPropertyNotifySink
{
public:
	CComRebarPosCmd()
        : m_pDoc(NULL),
          bGotPos(false),
          bGotBasePoint(false)
	{
	}

DECLARE_NOT_AGGREGATABLE(CComRebarPosCmd)

BEGIN_COM_MAP(CComRebarPosCmd)
	COM_INTERFACE_ENTRY(IPropertyNotifySink)
END_COM_MAP()


// IPropertyNotifySink
	STDMETHOD(OnChanged)(DISPID dispid);
	STDMETHOD(OnRequestEdit)(DISPID dispid);

// C++ Methods
public:
    void SetDocument(AcApDocument *pDoc) {m_pDoc = pDoc;};

protected:
    void InterruptPrompt();

private:
    AcApDocument *m_pDoc;
    
    DEFINE_CMDFLAG(GotPos);
    DEFINE_CMDFLAG(GotBasePoint);
};
