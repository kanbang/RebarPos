#define WIN32_LEAN_AND_MEAN
#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <windows.h>
#include <objbase.h>

#include "rxregsvc.h"

#include "assert.h"
#include "math.h"

#include "gepnt3d.h"
#include "gevec3d.h"
#include "gelnsg3d.h"
#include "gearc3d.h"

#include "dbents.h"
#include "dbsymtb.h"
#include "dbcfilrs.h"
#include "dbspline.h"
#include "dbproxy.h"
#include "dbxutil.h"
#include "acutmem.h"

#include "RebarPos.h"
#include "PosShape.h"
#include "PosGroup.h"

#include "acdb.h"
#include "dbidmap.h"
#include "adesk.h"

#include "dbapserv.h"
#include "appinfo.h"
#include "tchar.h"

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
        deleteAcRxClass(CRebarPos::desc());
		deleteAcRxClass(CPosGroup::desc());
        deleteAcRxClass(CPosShape::desc());

        break;
    }
    return AcRx::kRetOK;
}
