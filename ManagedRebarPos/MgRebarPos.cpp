//-----------------------------------------------------------------------------
//----- RebarPos.cpp : Implementation of RebarPos
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <gcroot.h>
#include "mgdinterop.h"

#include "MgRebarPos.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
RebarPos::RebarPos() 
:Autodesk::AutoCAD::DatabaseServices::Entity(IntPtr(new CRebarPos()), true)
{
}

RebarPos::RebarPos(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::Entity(unmanagedPointer,autoDelete)
{
}

//*************************************************************************
// Properties
//*************************************************************************
void RebarPos::BasePoint::set(Point3d point)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBasePoint(GETPOINT3D(point)));
}
Point3d RebarPos::BasePoint::get()
{
    return ToPoint3d (GetImpObj()->BasePoint());
}

void RebarPos::NoteGrip::set(Point3d point)
{
  Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteGrip(GETPOINT3D(point)));
}
Point3d RebarPos::NoteGrip::get()
{
    return ToPoint3d (GetImpObj()->NoteGrip());
}

void RebarPos::Pos::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPos(StringToWchar(value)));
}
String^ RebarPos::Pos::get()
{
    return WcharToString(GetImpObj()->Pos());
}

void RebarPos::Note::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNote(StringToWchar(value)));
}
String^ RebarPos::Note::get()
{
    return WcharToString(GetImpObj()->Note());
}

void RebarPos::Count::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCount(StringToWchar(value)));
}
String^ RebarPos::Count::get()
{
    return WcharToString(GetImpObj()->Count());
}

void RebarPos::Diameter::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDiameter(StringToWchar(value)));
}
String^ RebarPos::Diameter::get()
{
    return WcharToString(GetImpObj()->Diameter());
}

void RebarPos::Spacing::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setSpacing(StringToWchar(value)));
}
String^ RebarPos::Spacing::get()
{
    return WcharToString(GetImpObj()->Spacing());
}

void RebarPos::Multiplier::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplier(value));
}
int RebarPos::Multiplier::get()
{
    return GetImpObj()->Multiplier();
}

void RebarPos::Display::set(RebarPos::DisplayStyle value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDisplay(static_cast<CRebarPos::DisplayStyle>(value)));
}
RebarPos::DisplayStyle RebarPos::Display::get()
{
	return static_cast<RebarPos::DisplayStyle>(GetImpObj()->Display());
}

void RebarPos::A::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setA(StringToWchar(value)));
}
String^ RebarPos::A::get()
{
    return WcharToString(GetImpObj()->A());
}

void RebarPos::B::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setB(StringToWchar(value)));
}
String^ RebarPos::B::get()
{
    return WcharToString(GetImpObj()->B());
}

void RebarPos::C::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setC(StringToWchar(value)));
}
String^ RebarPos::C::get()
{
    return WcharToString(GetImpObj()->C());
}

void RebarPos::D::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setD(StringToWchar(value)));
}
String^ RebarPos::D::get()
{
    return WcharToString(GetImpObj()->D());
}

void RebarPos::E::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setE(StringToWchar(value)));
}
String^ RebarPos::E::get()
{
    return WcharToString(GetImpObj()->E());
}

void RebarPos::F::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setF(StringToWchar(value)));
}
String^ RebarPos::F::get()
{
    return WcharToString(GetImpObj()->F());
}

bool RebarPos::IsVarLength::get()
{
	return GetImpObj()->IsVarLength();
}

String^ RebarPos::Length::get()
{
    return WcharToString(GetImpObj()->Length());
}

String^ RebarPos::PosKey::get()
{
    return WcharToString(GetImpObj()->PosKey());
}

void RebarPos::ShapeId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setShapeId(GETOBJECTID(value)));
}
Autodesk::AutoCAD::DatabaseServices::ObjectId RebarPos::ShapeId::get()
{
	return ToObjectId(GetImpObj()->ShapeId());
}

void RebarPos::GroupId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setGroupId(GETOBJECTID(value)));
}
Autodesk::AutoCAD::DatabaseServices::ObjectId RebarPos::GroupId::get()
{
	return ToObjectId(GetImpObj()->GroupId());
}

//*************************************************************************
// Methods
//*************************************************************************
RebarPos::HitTestResult RebarPos::HitTest(Point3d pt)
{
	return static_cast<RebarPos::HitTestResult>(GetImpObj()->HitTest(GETPOINT3D(pt)));
}