// stdafx.h : include file for standard system include files,
//      or project specific include files that are used frequently,
//      but are changed infrequently

#pragma once

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef WINVER
#define WINVER 0x501
#endif

#define STRICT

#define _WIN32_WINNT 0x0501
#define _ATL_APARTMENT_THREADED

#include <atlbase.h>
//You may derive a class from CComModule and use it if you want to override
//something, but do not change the name of _Module
extern CComModule _Module;
#include <atlcom.h>

#pragma warning( push )
#pragma warning( disable : 4100 )
#pragma warning( disable : 4189 )
#include "axdb.h"
#include "dbmain.h"
#include "dbents.h"
#include "dbsymtb.h"
#include "dbobjptr.h"
#include "rxregsvc.h"
#include "geassign.h"
#pragma warning( pop ) 

#define AXEntityDocLockNoDbOk(objId)                        \
    AcAxDocLock docLock(objId, AcAxDocLock::kNormal);       \
    if (docLock.lockStatus() != Acad::eNoDatabase && \
        docLock.lockStatus() != Acad::eOk)                  \
        throw docLock.lockStatus();

#define AXEntityDocLock(objId)                              \
    AcAxDocLock docLock(objId, AcAxDocLock::kNormal);       \
    if(docLock.lockStatus() != Acad::eOk)                   \
        throw docLock.lockStatus();

#define CHECKOUTPARAM(x) if (x==NULL) return E_POINTER;

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

//- This line allows us to get rid of the .def file in ARX projects
#ifndef NO_ARX_DEF
#define NO_ARX_DEF
#ifndef _WIN64
#pragma comment(linker, "/export:_acrxGetApiVersion,PRIVATE")
#else
#pragma comment(linker, "/export:acrxGetApiVersion,PRIVATE")
#endif
#endif
