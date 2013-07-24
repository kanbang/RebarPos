#include "StdAfx.h"

#include "rxregsvc.h"

#include "PosShape.h"
#include "PosGroup.h"
#include "RebarPos.h"
#include "TableCell.h"
#include "GenericTable.h"
#include "BOQTable.h"
#include "BOQStyle.h"
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
		CPosShape::rxInit();
		CPosGroup::rxInit();
		CRebarPos::rxInit();
		CGenericTable::rxInit();
		CBOQStyle::rxInit();
		CBOQTable::rxInit();
		acrxBuildClassHierarchy();
		
		// Register a service using the class name.
		if (!acrxServiceIsRegistered(_T("CRebarPos")))
			acrxRegisterService(_T("CRebarPos"));

		break;

	case AcRx::kLoadDwgMsg:
		// Create default group
		CPosGroup::CreateGroup();

		// Create default shapes
		if(CPosShape::GetPosShapeCount(true, true, true) == 0)
		{
			CPosShape::ClearPosShapes(true, true, true);
			CPosShape::ReadPosShapesFromResource(_hdllInstance, IDR_SHAPES, false);
			CPosShape::ReadPosShapesFromResource(_hdllInstance, IDR_INTERNALSHAPES, true);
		}
		
		// Create default table styles
		if(CBOQStyle::GetBOQStyleCount(true, true) == 0)
		{
			CBOQStyle::ClearBOQStyles(true, true);
			CBOQStyle::ReadBOQStylesFromResource(_hdllInstance, IDR_TABLESTYLES);
		}

		// Update all entities
		CRebarPos::UpdateAll();
		CBOQTable::UpdateAll();

		break;

	case AcRx::kUnloadDwgMsg:

		break;

	case AcRx::kUnloadAppMsg:
		// Remove shapes from memory
		CPosShape::ClearPosShapes(true, true, true);

		// Remove table styles from memory
		CBOQStyle::ClearBOQStyles(true, true);

		// Unregister the service
		AcRxObject *obj = acrxServiceDictionary->remove(_T("CRebarPos"));
		if (obj != NULL)
			delete obj;

		// Remove custom classes
		deleteAcRxClass(CBOQTable::desc());
		deleteAcRxClass(CBOQStyle::desc());
		deleteAcRxClass(CGenericTable::desc());
        deleteAcRxClass(CRebarPos::desc());
		deleteAcRxClass(CPosGroup::desc());
		deleteAcRxClass(CPosShape::desc());

		break;
	}
	return AcRx::kRetOK;
}
