#include "StdAfx.h"

#include "rxregsvc.h"

#include "RebarPos.h"
#include "PosShape.h"
#include "PosGroup.h"
#include "BOQStyle.h"

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
		CPosShape::rxInit();
		CPosGroup::rxInit();
        CRebarPos::rxInit();
		CBOQStyle::rxInit();
        acrxBuildClassHierarchy();

        // Register a service using the class name.
        if (!acrxServiceIsRegistered(_T("CRebarPos")))
            acrxRegisterService(_T("CRebarPos"));

        break;

    case AcRx::kUnloadAppMsg:
        // Unregister the service
        AcRxObject *obj = acrxServiceDictionary->remove(_T("CRebarPos"));
        if (obj != NULL)
            delete obj;

		// Remove custom classes
		deleteAcRxClass(CBOQStyle::desc());
        deleteAcRxClass(CRebarPos::desc());
		deleteAcRxClass(CPosGroup::desc());
        deleteAcRxClass(CPosShape::desc());

        break;
    }
    return AcRx::kRetOK;
}
