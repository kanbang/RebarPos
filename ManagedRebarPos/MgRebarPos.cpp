// (C) Copyright 2004-2006 by Autodesk, Inc. 
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
// This is the main DLL file.
#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif


//////////////////////////////////////////////////////////////////////////
#include <gcroot.h>
#include <dbdate.h>
#include "mgdinterop.h"

using namespace OZOZ::RebarPosWrapper;

//////////////////////////////////////////////////////////////////////////
// constructor
RebarPos::RebarPos() 
:Autodesk::AutoCAD::DatabaseServices::Entity(IntPtr(new CRebarPos()), true)
{
	acutPrintf(_T("\n*********************Constructor"));
}

//////////////////////////////////////////////////////////////////////////
RebarPos::RebarPos(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::Entity(unmanagedPointer,autoDelete)
{
}

//////////////////////////////////////////////////////////////////////////
// set the centre of the poly
void RebarPos::Center::set(Point3d point)
{
  Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCenter(GETPOINT3D(point)));
}
//////////////////////////////////////////////////////////////////////////
// get the center point
Point3d RebarPos::Center::get()
{
    return ToPoint3d (GetImpObj()->getCenter());
}

//////////////////////////////////////////////////////////////////////////
// set the string name
void RebarPos::Name::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setName(StringToWchar(value)));
}
//////////////////////////////////////////////////////////////////////////
// get the string name
String^ RebarPos::Name::get()
{
    return WcharToString(GetImpObj()->name());
}

//////////////////////////////////////////////////////////////////////////
// set the text style record 
void RebarPos::TextStyle::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextStyle(GETOBJECTID(value)));
}

//////////////////////////////////////////////////////////////////////////
// get the text style record 
Autodesk::AutoCAD::DatabaseServices::ObjectId RebarPos::TextStyle::get()
{
	return ToObjectId(GetImpObj()->styleId());
}
