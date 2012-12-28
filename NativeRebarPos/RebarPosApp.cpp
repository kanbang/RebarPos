#include "StdAfx.h"

#include "rxregsvc.h"

#include "PosShape.h"
#include "PosGroup.h"
#include "RebarPos.h"
#include "TableCell.h"
#include "GenericTable.h"
#include "BOQTable.h"
#include "resource.h"

HINSTANCE _hdllInstance = NULL;

/////////////////////////////////////////////////////////////////////////////
// DLL Entry Point
extern "C"
BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID /*lpReserved*/)
{
    if (dwReason == DLL_PROCESS_ATTACH)
    {
		_hdllInstance = hInstance;
    }
    return TRUE;
}

// locally defined entry point invoked by Rx.
extern "C" AcRx::AppRetCode __declspec(dllexport)
acrxEntryPoint(AcRx::AppMsgCode msg, void* pkt)
{
    switch(msg) 
	{
    case AcRx::kInitAppMsg:
        acrxUnlockApplication(pkt);     // Try to allow unloading
        acrxRegisterAppMDIAware(pkt);

		// Register custom classes
		CPosGroup::rxInit();
        CRebarPos::rxInit();
		CGenericTable::rxInit();
		CBOQTable::rxInit();
        acrxBuildClassHierarchy();

        // Register a service using the class name.
        if (!acrxServiceIsRegistered(_T("CRebarPos")))
            acrxRegisterService(_T("CRebarPos"));

		// Create default shapes
		CPosShape::ClearPosShapes(true, true);
		CPosShape::MakePosShapesFromResource(_hdllInstance);

        break;

    case AcRx::kUnloadAppMsg:
        // Unregister the service
        AcRxObject *obj = acrxServiceDictionary->remove(_T("CRebarPos"));
        if (obj != NULL)
            delete obj;

		// Remove shapes from memory
		CPosShape::ClearPosShapes(true, true);

		// Remove custom classes
		deleteAcRxClass(CBOQTable::desc());
		deleteAcRxClass(CGenericTable::desc());
        deleteAcRxClass(CRebarPos::desc());
		deleteAcRxClass(CPosGroup::desc());

        break;
    }
    return AcRx::kRetOK;
}
