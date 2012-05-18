//-----------------------------------------------------------------------------
//----- BOQStyle.cpp : Implementation of BOQStyle
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "MgBOQStyle.h"
#include "Marshal.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
BOQStyle::BOQStyle() 
:Autodesk::AutoCAD::DatabaseServices::DBObject(IntPtr(new CBOQStyle()), true)
{
}

BOQStyle::BOQStyle(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::DBObject(unmanagedPointer,autoDelete)
{
}

//*************************************************************************
// Properties
//*************************************************************************
String^ BOQStyle::Name::get()
{
	return Marshal::WcharToString(GetImpObj()->Name());
}
void BOQStyle::Name::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setName(Marshal::StringToWchar(value)));
}

int BOQStyle::Precision::get()
{
	return GetImpObj()->Precision();
}
void BOQStyle::Precision::set(int value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPrecision(value));
}

BOQStyle::DrawingUnits BOQStyle::DisplayUnit::get()
{
	return static_cast<BOQStyle::DrawingUnits>(GetImpObj()->DisplayUnit());
}
void BOQStyle::DisplayUnit::set(BOQStyle::DrawingUnits value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDisplayUnit(static_cast<CBOQStyle::DrawingUnits>(value)));
}

String^ BOQStyle::Columns::get()
{
	return Marshal::WcharToString(GetImpObj()->Columns());
}
void BOQStyle::Columns::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setColumns(Marshal::StringToWchar(value)));
}

Autodesk::AutoCAD::Colors::Color^ BOQStyle::TextColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->TextColor());
}
void BOQStyle::TextColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQStyle::PosColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->PosColor());
}
void BOQStyle::PosColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPosColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQStyle::LineColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->LineColor());
}
void BOQStyle::LineColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setLineColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQStyle::BorderColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->BorderColor());
}
void BOQStyle::BorderColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBorderColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQStyle::HeadingColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->HeadingColor());
}
void BOQStyle::HeadingColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeadingColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ BOQStyle::FootingColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->FootingColor());
}
void BOQStyle::FootingColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFootingColor(value->ColorIndex));
}

String^ BOQStyle::Heading::get()
{
	return Marshal::WcharToString(GetImpObj()->Heading());
}
void BOQStyle::Heading::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeading(Marshal::StringToWchar(value)));
}

String^ BOQStyle::Footing::get()
{
	return Marshal::WcharToString(GetImpObj()->Footing());
}
void BOQStyle::Footing::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFooting(Marshal::StringToWchar(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::TextStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->TextStyleId());
}
void BOQStyle::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextStyleId(Marshal::FromObjectId(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::HeadingStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->HeadingStyleId());
}
void BOQStyle::HeadingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeadingStyleId(Marshal::FromObjectId(value)));
}

double BOQStyle::HeadingScale::get()
{
	return GetImpObj()->HeadingScale();
}
void BOQStyle::HeadingScale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setHeadingScale(value));
}

double BOQStyle::RowSpacing::get()
{
	return GetImpObj()->RowSpacing();
}
void BOQStyle::RowSpacing::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setRowSpacing(value));
}

//*************************************************************************
// Static Properties
//*************************************************************************
String^ BOQStyle::TableName::get()
{
	return Marshal::WcharToString(CBOQStyle::GetTableName());
}
