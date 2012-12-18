//-----------------------------------------------------------------------------
//----- BOQTable.cpp : Implementation of BOQTable
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "MgGenericTable.h"
#include "Marshal.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
GenericTable::GenericTable() 
:Autodesk::AutoCAD::DatabaseServices::Entity(IntPtr(new CGenericTable()), true)
{
}

GenericTable::GenericTable(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::Entity(unmanagedPointer,autoDelete)
{
}

//*************************************************************************
// Methods
//*************************************************************************
void GenericTable::SuspendUpdate()
{
	GetImpObj()->SuspendUpdate();
}
void GenericTable::ResumeUpdate()
{
	GetImpObj()->ResumeUpdate();
}

//*************************************************************************
// Properties
//*************************************************************************
Vector3d GenericTable::DirectionVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->DirectionVector());
}

Vector3d GenericTable::UpVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->UpVector());
}

Vector3d GenericTable::NormalVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->NormalVector());
}

double GenericTable::Scale::get()
{
	return (GetImpObj()->Scale());
}
void GenericTable::Scale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setScale(value));
}

double GenericTable::Width::get()
{
	return (GetImpObj()->Width());
}

double GenericTable::Height::get()
{
	return (GetImpObj()->Height());
}

Point3d GenericTable::BasePoint::get()
{
    return Marshal::ToPoint3d (GetImpObj()->BasePoint());
}
void GenericTable::BasePoint::set(Point3d point)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBasePoint(Marshal::FromPoint3d(point)));
}
