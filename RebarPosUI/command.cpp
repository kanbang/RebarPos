// (C) Copyright 1996-2007 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to 
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//
// COMMAND.CC 
//
// DESCRIPTION:
//
// This file contains the command interface code for Arx sample app -
// POLYSAMP.

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif


#include <afxdisp.h>        // MFC OLE automation classes

#include <atlbase.h>
CComModule _Module;
#include <atlcom.h>


#include "rxmfcapi.h"
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <math.h>
#include <string.h>

#include "adslib.h"
#include "acestext.h"
#include "dbents.h"
#include "dbregion.h"
#include "dbsol3d.h"

#include "gepnt3d.h"

#include "../NativeRebarPos/RebarPos.h"
#include "../NativeRebarPos/PosShape.h"
#include "../NativeRebarPos/PosStyle.h"
#include "../NativeRebarPos/PosGroup.h"
#include "../NativeRebarPos/Utility.h"
#include "dbxutil.h"
#include "dbapserv.h"

#include <comdef.h>
#include "acdocman.h"

#include "acadi.h"
#include "../COMRebarPos/COMRebarPos_i.h"
#include "../COMRebarPos/COMRebarPos_i.c"
#include "axpnt2d.h"
#include "initguid.h"
#include "axboiler.h"
#include "axpnt2d.h"
#include "axpnt3d.h"

#include "RebarPosCmd.h"

// Invoked by the command - POLY
//
void polyCommand()
{
    ads_point center;
    
    if (acedGetPoint(NULL, _T("\nInsertion point: "), center) != RTNORM)
        return;
      
    TCHAR nameBuf[133];
    if (acedGetString(Adesk::kTrue, _T("\nText: "), nameBuf) != RTNORM)
        return;   
    
    AcGePoint3d cen = asPnt3d(center);

	AcDbObjectId shapeId = CPosShape::CreateDefault();
	AcDbObjectId styleId = CPosStyle::CreateDefault();
	AcDbObjectId groupId = CPosGroup::CreateDefault();

	Acad::ErrorStatus es;
	CPosGroup* pGroup;
	if((es = acdbOpenAcDbObject((AcDbObject*&)pGroup, groupId, AcDb::kForWrite, false)) == Acad::eOk)
	{
		pGroup->setStyleId(styleId);
		pGroup->close();
	}
    CRebarPos* pPos = new CRebarPos;
    
	pPos->setBasePoint(asPnt3d(center));
	pPos->setPos(nameBuf);
	pPos->setA(_T("100"));
	pPos->setB(_T("20"));
	pPos->setShapeId(shapeId);
	pPos->setGroupId(groupId);
    
    pPos->setDatabaseDefaults(acdbHostApplicationServices()->workingDatabase());
    postToDb(pPos);
}
