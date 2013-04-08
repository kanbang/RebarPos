// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#using <mscorlib.dll>
#using <system.dll>
#using <system.drawing.dll>
#using <acdbmgd.dll>

#include <use_ansi.h>

#include <WTypes.h>
#include <basetsd.h>
#include <winbase.h>
#include <winnt.h>
#include <vcclr.h>

// ARX headers
#pragma warning( push )
#pragma warning( disable : 4100 )
#pragma warning( disable : 4201 )
#pragma warning( disable : 4512 )
#include "rxdefs.h"
#include "dbmain.h"
#pragma warning( pop )

#ifndef WINVER
#define WINVER 0x500
#endif

//- This line allows us to get rid of the .def file in ARX projects
#ifndef NO_ARX_DEF
#define NO_ARX_DEF
#ifndef _WIN64
#pragma comment(linker, "/export:_acrxGetApiVersion,PRIVATE")
#else
#pragma comment(linker, "/export:acrxGetApiVersion,PRIVATE")
#endif
#endif