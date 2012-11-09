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

double BOQTable::MaxHeight::get()
{
    return GetImpObj()->MaxHeight();
}
void BOQTable::MaxHeight::set(double value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMaxHeight(value));
}

double BOQTable::TableSpacing::get()
{
	return (GetImpObj()->TableSpacing());
}
void BOQTable::TableSpacing::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTableSpacing(value));
}

int BOQTable::Multiplier::get()
{
    return GetImpObj()->Multiplier();
}
void BOQTable::Multiplier::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplier(value));
}

String^ BOQTable::Heading::get()
{
	return Marshal::WcharToString(GetImpObj()->Heading());
}
void BOQTable::Heading::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeading(Marshal::StringToWchar(value)));
}

String^ BOQTable::Footing::get()
{
	return Marshal::WcharToString(GetImpObj()->Footing());
}
void BOQTable::Footing::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFooting(Marshal::StringToWchar(value)));
}

String^ BOQTable::Note::get()
{
	return Marshal::WcharToString(GetImpObj()->Note());
}
void BOQTable::Note::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNote(Marshal::StringToWchar(value)));
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

void BOQTable::BOQRowCollection::Add(int pos, int count, double diameter, double length1, double length2, bool isVarLength, System::String^ shape, System::String^ a, System::String^ b, System::String^ c, System::String^ d, System::String^ e, System::String^ f)
{
	m_Parent->GetImpObj()->AddRow(new CBOQRow(pos, count, diameter, length1, length2, (isVarLength ? Adesk::kTrue : Adesk::kFalse), OZOZ::RebarPosWrapper::Marshal::StringToWchar(shape), 
		OZOZ::RebarPosWrapper::Marshal::StringToWchar(a), OZOZ::RebarPosWrapper::Marshal::StringToWchar(b), OZOZ::RebarPosWrapper::Marshal::StringToWchar(c), OZOZ::RebarPosWrapper::Marshal::StringToWchar(d), OZOZ::RebarPosWrapper::Marshal::StringToWchar(e), OZOZ::RebarPosWrapper::Marshal::StringToWchar(f)));
}

void BOQTable::BOQRowCollection::Add(int pos, int count, double diameter, double length, System::String^ shape, System::String^ a, System::String^ b, System::String^ c, System::String^ d, System::String^ e, System::String^ f)
{
	Add(pos, count, diameter, length, length, false, shape, a, b, c, d, e, f);
}

void BOQTable::BOQRowCollection::Add(int pos)
{
	m_Parent->GetImpObj()->AddRow(new CBOQRow(pos));
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

//*************************************************************************
// Methods
//*************************************************************************
void BOQTable::Update()
{
	GetImpObj()->Update();
}