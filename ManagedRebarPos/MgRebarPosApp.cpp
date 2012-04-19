
#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <gcroot.h>
#include <vcclr.h>
#include "mgdinterop.h" 

static AcMgObjectFactoryBase **g_PEs = NULL;

extern "C" AcRx::AppRetCode __declspec(dllexport)
acrxEntryPoint(AcRx::AppMsgCode msg, void* pkt)
{
	switch(msg) 
	{
		// onload of arx
		case AcRx::kInitAppMsg: 
		{
			acrxDynamicLinker->registerAppMDIAware(pkt);

			// create a new object factory array
			static AcMgObjectFactoryBase* PEs[] = 
			{
				new AcMgObjectFactory<OZOZ::RebarPosWrapper::RebarPos, CRebarPos>(), 
				// end the array with a NULL
				NULL
			};

			g_PEs = PEs;
			
		}break;

		case AcRx::kPreQuitMsg:
		{
			// clean up
			int i=0;
			while (g_PEs[i]!=NULL)
				delete g_PEs[i++];

		}break;
	}

	return AcRx::kRetOK;
}
