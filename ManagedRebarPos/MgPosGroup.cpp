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

//*************************************************************************
// Static Properties
//*************************************************************************
String^ PosGroup::TableName::get()
{
	return WcharToString(CPosGroup::GetTableName());
}
