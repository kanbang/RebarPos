//-----------------------------------------------------------------------------
//----- PosStyle.cpp : Implementation of PosStyle
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <gcroot.h>
#include "mgdinterop.h"

#include "MgPosStyle.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
PosStyle::PosStyle() 
:Autodesk::AutoCAD::DatabaseServices::DBObject(IntPtr(new CPosStyle()), true)
{
}

PosStyle::PosStyle(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::DBObject(unmanagedPointer,autoDelete)
{
}

//*************************************************************************
// Properties
//*************************************************************************
String^ PosStyle::Formula::get()
{
	return WcharToString(GetImpObj()->Formula());
}
void PosStyle::Formula::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormula(StringToWchar(value)));
}

Autodesk::AutoCAD::Colors::Color^ PosStyle::TextColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->TextColor());
}
void PosStyle::TextColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosStyle::PosColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->PosColor());
}
void PosStyle::PosColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPosColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosStyle::CircleColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->CircleColor());
}
void PosStyle::CircleColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCircleColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosStyle::MultiplierColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->MultiplierColor());
}
void PosStyle::MultiplierColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplierColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosStyle::GroupColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->GroupColor());
}
void PosStyle::GroupColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setGroupColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosStyle::CurrentGroupHighlightColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->CurrentGroupHighlightColor());
}
void PosStyle::CurrentGroupHighlightColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCurrentGroupHighlightColor(value->ColorIndex));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosStyle::TextStyleId::get()
{
	return ToObjectId (GetImpObj()->TextStyleId());
}
void PosStyle::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextStyleId(GETOBJECTID(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosStyle::NoteStyleId::get()
{
	return ToObjectId (GetImpObj()->NoteStyleId());
}
void PosStyle::NoteStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteStyleId(GETOBJECTID(value)));
}

double PosStyle::NoteScale::get()
{
	return GetImpObj()->NoteScale();
}
void PosStyle::NoteScale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteScale(value));
}

//*************************************************************************
// Static Properties
//*************************************************************************
String^ PosStyle::TableName::get()
{
	return WcharToString(CPosStyle::GetTableName());
}
