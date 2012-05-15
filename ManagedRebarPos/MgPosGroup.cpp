//-----------------------------------------------------------------------------
//----- PosGroup.cpp : Implementation of PosGroup
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "MgPosGroup.h"
#include "Marshal.h"

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
	return Marshal::WcharToString(GetImpObj()->Name());
}
void PosGroup::Name::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setName(Marshal::StringToWchar(value)));
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
	return Marshal::WcharToString(GetImpObj()->Formula());
}
void PosGroup::Formula::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormula(Marshal::StringToWchar(value)));
}

String^ PosGroup::FormulaWithoutLength::get()
{
	return Marshal::WcharToString(GetImpObj()->FormulaWithoutLength());
}
void PosGroup::FormulaWithoutLength::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormulaWithoutLength(Marshal::StringToWchar(value)));
}

String^ PosGroup::FormulaPosOnly::get()
{
	return Marshal::WcharToString(GetImpObj()->FormulaPosOnly());
}
void PosGroup::FormulaPosOnly::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormulaPosOnly(Marshal::StringToWchar(value)));
}

String^ PosGroup::StandardDiameters::get()
{
	return Marshal::WcharToString(GetImpObj()->StandardDiameters());
}
void PosGroup::StandardDiameters::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setStandardDiameters(Marshal::StringToWchar(value)));
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

Autodesk::AutoCAD::Colors::Color^ PosGroup::NoteColor::get()
{
	return Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, GetImpObj()->NoteColor());
}
void PosGroup::NoteColor::set(Autodesk::AutoCAD::Colors::Color^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteColor(value->ColorIndex));
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
	return Marshal::ToObjectId (GetImpObj()->TextStyleId());
}
void PosGroup::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setTextStyleId(Marshal::FromObjectId(value)));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosGroup::NoteStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->NoteStyleId());
}
void PosGroup::NoteStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteStyleId(Marshal::FromObjectId(value)));
}

double PosGroup::NoteScale::get()
{
	return GetImpObj()->NoteScale();
}
void PosGroup::NoteScale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteScale(value));
}

//*************************************************************************
// Static Properties
//*************************************************************************
String^ PosGroup::TableName::get()
{
	return Marshal::WcharToString(CPosGroup::GetTableName());
}
