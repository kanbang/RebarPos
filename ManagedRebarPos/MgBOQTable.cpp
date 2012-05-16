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
	m_Rows = gcnew BOQTable::BOQRowCollection(this);
}

BOQTable::BOQTable(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::Entity(unmanagedPointer,autoDelete)
{
	m_Rows = gcnew BOQTable::BOQRowCollection(this);	
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

BOQTable::BOQRowCollection^ BOQTable::Items::get()
{
	return m_Rows;
}

//*************************************************************************
// Shape Collection
//*************************************************************************
BOQTable::BOQRowCollection::BOQRowCollection(BOQTable^ parent)
{
	m_Parent = parent;
}

void BOQTable::BOQRowCollection::Add(int pos, int count, double diameter, double length1, double length2, bool isVarLength, Autodesk::AutoCAD::DatabaseServices::ObjectId shapeId)
{
	m_Parent->GetImpObj()->AddRow(new CBOQRow(pos, count, diameter, length1, length2, (isVarLength ? Adesk::kTrue : Adesk::kFalse), OZOZ::RebarPosWrapper::Marshal::FromObjectId(shapeId)));
}

int BOQTable::BOQRowCollection::Count::get()
{
	return (int)m_Parent->GetImpObj()->GetRowCount();
}

void BOQTable::BOQRowCollection::Remove(int index)
{
	m_Parent->GetImpObj()->RemoveRow((int)index);
}

void BOQTable::BOQRowCollection::Clear()
{
	m_Parent->GetImpObj()->ClearRows();
}

BOQTable::BOQRow^ BOQTable::BOQRowCollection::default::get(int index)
{
	return BOQTable::BOQRow::FromNative(m_Parent->GetImpObj()->GetRow(index));
}

void BOQTable::BOQRowCollection::default::set(int index, BOQTable::BOQRow^ value)
{
	m_Parent->GetImpObj()->SetRow(index, value->ToNative());
}
