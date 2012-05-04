//-----------------------------------------------------------------------------
//----- PosGroup.cpp : Implementation of PosGroup
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <gcroot.h>
#include "mgdinterop.h"

#include "MgPosGroup.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
PosGroup::PosGroup() 
:Autodesk::AutoCAD::DatabaseServices::DBObject(IntPtr(new CPosGroup()), true)
{
}

PosGroup::PosGroup(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::DBObject(unmanagedPointer,autoDelete)
{
}

//*************************************************************************
// Properties
//*************************************************************************
String^ PosGroup::Name::get()
{
    return WcharToString(GetImpObj()->Name());
}
void PosGroup::Name::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setName(StringToWchar(value)));
}

bool PosGroup::Bending::get()
{
	return (GetImpObj()->Bending() == Adesk::kTrue);
}
void PosGroup::Bending::set(bool value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBending(value ? Adesk::kTrue : Adesk::kFalse));
}

double PosGroup::MaxBarLength::get()
{
	return GetImpObj()->MaxBarLength();
}
void PosGroup::MaxBarLength::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMaxBarLength(value));
}

int PosGroup::Precision::get()
{
	return GetImpObj()->Precision();
}
void PosGroup::Precision::set(int value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPrecision(value));
}

PosGroup::DrawingUnits PosGroup::DrawingUnit::get()
{
	return static_cast<PosGroup::DrawingUnits>(GetImpObj()->DrawingUnit());
}
void PosGroup::DrawingUnit::set(PosGroup::DrawingUnits value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDrawingUnit(static_cast<CPosGroup::DrawingUnits>(value)));
}

PosGroup::DrawingUnits PosGroup::DisplayUnit::get()
{
	return static_cast<PosGroup::DrawingUnits>(GetImpObj()->DisplayUnit());
}
void PosGroup::DisplayUnit::set(PosGroup::DrawingUnits value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDisplayUnit(static_cast<CPosGroup::DrawingUnits>(value)));
}

String^ PosGroup::Formula::get()
{
	return WcharToString(GetImpObj()->Formula());
}
void PosGroup::Formula::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormula(StringToWchar(value)));
}

String^ PosGroup::FormulaWithoutLength::get()
{
	return WcharToString(GetImpObj()->FormulaWithoutLength());
}
void PosGroup::FormulaWithoutLength::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormulaWithoutLength(StringToWchar(value)));
}

String^ PosGroup::FormulaPosOnly::get()
{
	return WcharToString(GetImpObj()->FormulaPosOnly());
}
void PosGroup::FormulaPosOnly::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormulaPosOnly(StringToWchar(value)));
}

String^ PosGroup::StandardDiameters::get()
{
	return WcharToString(GetImpObj()->StandardDiameters());
}
void PosGroup::StandardDiameters::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setStandardDiameters(StringToWchar(value)));
}

Autodesk::AutoCAD::Colors::Color^ PosGroup::TextColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->TextColor());
}
void PosGroup::TextColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosGroup::PosColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->PosColor());
}
void PosGroup::PosColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPosColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosGroup::CircleColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->CircleColor());
}
void PosGroup::CircleColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCircleColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosGroup::MultiplierColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->MultiplierColor());
}
void PosGroup::MultiplierColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplierColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosGroup::GroupColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->GroupColor());
}
void PosGroup::GroupColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setGroupColor(value->ColorIndex));
}

Autodesk::AutoCAD::Colors::Color^ PosGroup::CurrentGroupHighlightColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->CurrentGroupHighlightColor());
}
void PosGroup::CurrentGroupHighlightColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCurrentGroupHighlightColor(value->ColorIndex));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosGroup::TextStyleId::get()
{
	return ToObjectId (GetImpObj()->TextStyleId());
}
void PosGroup::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextStyleId(GETOBJECTID(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosGroup::NoteStyleId::get()
{
	return ToObjectId (GetImpObj()->NoteStyleId());
}
void PosGroup::NoteStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteStyleId(GETOBJECTID(value)));
}

double PosGroup::NoteScale::get()
{
	return GetImpObj()->NoteScale();
}
void PosGroup::NoteScale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteScale(value));
}

bool PosGroup::Current::get()
{
	return (GetImpObj()->Current() == Adesk::kTrue);
}
void PosGroup::Current::set(bool value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCurrent(value ? Adesk::kTrue : Adesk::kFalse));
}

//*************************************************************************
// Static Properties
//*************************************************************************
String^ PosGroup::TableName::get()
{
	return WcharToString(CPosGroup::GetTableName());
}
