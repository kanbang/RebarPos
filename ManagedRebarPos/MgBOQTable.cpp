//-----------------------------------------------------------------------------
//----- BOQTable.cpp : Implementation of BOQTable
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "MgBOQTable.h"
#include "Marshal.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
BOQTable::BOQTable() 
:Autodesk::AutoCAD::DatabaseServices::Entity(IntPtr(new CBOQTable()), true)
{
	
}

BOQTable::BOQTable(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::Entity(unmanagedPointer,autoDelete)
{
	
}

//*************************************************************************
// Properties
//*************************************************************************
Vector3d BOQTable::DirectionVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->DirectionVector());
}

Vector3d BOQTable::UpVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->UpVector());
}

Vector3d BOQTable::NormalVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->NormalVector());
}

double BOQTable::Scale::get()
{
	return (GetImpObj()->Scale());
}
void BOQTable::Scale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setScale(value));
}

double BOQTable::Width::get()
{
	return (GetImpObj()->Width());
}

double BOQTable::Height::get()
{
	return (GetImpObj()->Height());
}

Point3d BOQTable::BasePoint::get()
{
    return Marshal::ToPoint3d (GetImpObj()->BasePoint());
}
void BOQTable::BasePoint::set(Point3d point)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBasePoint(Marshal::FromPoint3d(point)));
}

int BOQTable::Multiplier::get()
{
    return GetImpObj()->Multiplier();
}
void BOQTable::Multiplier::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplier(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQTable::StyleId::get()
{
	return Marshal::ToObjectId(GetImpObj()->StyleId());
}
void BOQTable::StyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setStyleId(Marshal::FromObjectId(value)));
}

