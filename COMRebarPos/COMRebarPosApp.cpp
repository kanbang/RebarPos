// COMRebarPosApp.cpp : Implementation of DLL Exports.

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "resource.h"
#include "initguid.h"

#include "COMRebarPos_i.h"
#include "COMRebarPos_i.c"

#include "ComRebarPos.h"
#include "ComBOQTable.h"

CComModule _Module;

BEGIN_OBJECT_MAP(ObjectMap)
    OBJECT_ENTRY(CLSID_ComRebarPos, CComRebarPos)
	OBJECT_ENTRY(CLSID_ComBOQTable, CComBOQTable)
END_OBJECT_MAP()

/////////////////////////////////////////////////////////////////////////////
// DLL Entry Point
extern "C"
BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID /*lpReserved*/)
{
    if (dwReason == DLL_PROCESS_ATTACH)
    {
        _Module.Init(ObjectMap, hInstance);
        DisableThreadLibraryCalls(hInstance);
    }
    else if (dwReason == DLL_PROCESS_DETACH)
    {
        _Module.Term();
    }
    return TRUE;    // ok
}

/////////////////////////////////////////////////////////////////////////////
// Used to determine whether the DLL can be unloaded by OLE

STDAPI DllCanUnloadNow(void)
{
    return (_Module.GetLockCount()==0) ? S_OK : S_FALSE;
}

/////////////////////////////////////////////////////////////////////////////
// Returns a class factory to create an object of the requested type

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
    return _Module.GetClassObject(rclsid, riid, ppv);
}

/////////////////////////////////////////////////////////////////////////////
//make this additional entry point available so when user loads
//this dll as an arx it will register itself 
extern "C" AcRx::AppRetCode __declspec(dllexport)
acrxEntryPoint(AcRx::AppMsgCode msg, void* pkt) 
{
    switch (msg) 
	{
    case AcRx::kInitAppMsg:
        //unlock the application
        acrxDynamicLinker->unlockApplication(pkt);
        acrxRegisterAppMDIAware(pkt);
        break;
    case AcRx::kUnloadAppMsg:
        break;
    case AcRx::kOleUnloadAppMsg :
        //respond to this message according to our current state
        return (DllCanUnloadNow() == S_OK ? AcRx::kRetOK : AcRx::kRetError);
    }
    return AcRx::kRetOK;
}
