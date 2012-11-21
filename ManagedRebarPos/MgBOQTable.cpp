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

BOQTable::BOQRowCollection^ BOQTable::Items::get()
{
	return m_Rows;
}

int BOQTable::Precision::get()
{
	return GetImpObj()->Precision();
}
void BOQTable::Precision::set(int value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPrecision(value));
}

BOQTable::DrawingUnits BOQTable::DisplayUnit::get()
{
	return static_cast<BOQTable::DrawingUnits>(GetImpObj()->DisplayUnit());
}
void BOQTable::DisplayUnit::set(BOQTable::DrawingUnits value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDisplayUnit(static_cast<CBOQTable::DrawingUnits>(value)));
}

String^ BOQTable::ColumnDef::get()
{
	return Marshal::WcharToString(GetImpObj()->ColumnDef());
}
void BOQTable::ColumnDef::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setColumnDef(Marshal::StringToWchar(value)));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::TextColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->TextColor());
}
void BOQTable::TextColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::PosColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->PosColor());
}
void BOQTable::PosColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPosColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::LineColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->LineColor());
}
void BOQTable::LineColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setLineColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::SeparatorColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->SeparatorColor());
}
void BOQTable::SeparatorColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setSeparatorColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::BorderColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->BorderColor());
}
void BOQTable::BorderColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBorderColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::HeadingColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->HeadingColor());
}
void BOQTable::HeadingColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeadingColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQTable::FootingColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->FootingColor());
}
void BOQTable::FootingColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFootingColor(value->ColorIndex));
}

String^ BOQTable::PosLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->PosLabel());
}
void BOQTable::PosLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPosLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::CountLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->CountLabel());
}
void BOQTable::CountLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCountLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::DiameterLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->DiameterLabel());
}
void BOQTable::DiameterLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDiameterLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::LengthLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->LengthLabel());
}
void BOQTable::LengthLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setLengthLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::ShapeLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->ShapeLabel());
}
void BOQTable::ShapeLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setShapeLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::TotalLengthLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->TotalLengthLabel());
}
void BOQTable::TotalLengthLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTotalLengthLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::DiameterListLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->DiameterListLabel());
}
void BOQTable::DiameterListLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDiameterListLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::DiameterLengthLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->DiameterLengthLabel());
}
void BOQTable::DiameterLengthLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDiameterLengthLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::UnitWeightLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->UnitWeightLabel());
}
void BOQTable::UnitWeightLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setUnitWeightLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::WeightLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->WeightLabel());
}
void BOQTable::WeightLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setWeightLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::GrossWeightLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->GrossWeightLabel());
}
void BOQTable::GrossWeightLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setGrossWeightLabel(Marshal::StringToWchar(value)));
}

String^ BOQTable::MultiplierHeadingLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->MultiplierHeadingLabel());
}
void BOQTable::MultiplierHeadingLabel::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplierHeadingLabel(Marshal::StringToWchar(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQTable::TextStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->TextStyleId());
}
void BOQTable::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextStyleId(Marshal::FromObjectId(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQTable::HeadingStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->HeadingStyleId());
}
void BOQTable::HeadingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeadingStyleId(Marshal::FromObjectId(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQTable::FootingStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->FootingStyleId());
}
void BOQTable::FootingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFootingStyleId(Marshal::FromObjectId(value)));
}

double BOQTable::HeadingScale::get()
{
	return GetImpObj()->HeadingScale();
}
void BOQTable::HeadingScale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeadingScale(value));
}

double BOQTable::FootingScale::get()
{
	return GetImpObj()->FootingScale();
}
void BOQTable::FootingScale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFootingScale(value));
}

double BOQTable::RowSpacing::get()
{
	return GetImpObj()->RowSpacing();
}
void BOQTable::RowSpacing::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setRowSpacing(value));
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