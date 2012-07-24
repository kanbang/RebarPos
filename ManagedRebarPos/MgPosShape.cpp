//-----------------------------------------------------------------------------
//----- PosShape.cpp : Implementation of PosShape
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "MgPosShape.h"
#include "Marshal.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
PosShape::PosShape() 
:Autodesk::AutoCAD::DatabaseServices::DBObject(IntPtr(new CPosShape()), true)
{
	m_Shapes = gcnew PosShape::ShapeCollection(this);
}

PosShape::PosShape(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::DBObject(unmanagedPointer,autoDelete)
{
	m_Shapes = gcnew PosShape::ShapeCollection(this);
}

//*************************************************************************
// Properties
//*************************************************************************
String^ PosShape::Name::get()
{
	return Marshal::WcharToString(GetImpObj()->Name());
}
void PosShape::Name::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setName(Marshal::StringToWchar(value)));
}

void PosShape::Fields::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFields(value));
}
int PosShape::Fields::get()
{
    return GetImpObj()->Fields();
}

void PosShape::Formula::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormula(Marshal::StringToWchar(value)));
}
String^ PosShape::Formula::get()
{
    return Marshal::WcharToString(GetImpObj()->Formula());
}

void PosShape::FormulaBending::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormulaBending(Marshal::StringToWchar(value)));
}
String^ PosShape::FormulaBending::get()
{
    return Marshal::WcharToString(GetImpObj()->FormulaBending());
}

PosShape::ShapeCollection^ PosShape::Items::get()
{
	return m_Shapes;
}

void PosShape::Priority::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPriority(value));
}
int PosShape::Priority::get()
{
    return GetImpObj()->Priority();
}

//*************************************************************************
// Shape Collection
//*************************************************************************
PosShape::ShapeCollection::ShapeCollection(PosShape^ parent)
{
	m_Parent = parent;
}

void PosShape::ShapeCollection::AddLine(double x1, double y1, double x2, double y2, Autodesk::AutoCAD::Colors::Color^ color, bool visible)
{
	m_Parent->GetImpObj()->AddShape(new CShapeLine(color->ColorIndex, x1, y1, x2, y2, (visible ? Adesk::kTrue : Adesk::kFalse)));
}

void PosShape::ShapeCollection::AddArc(double x, double y, double r, double startAngle, double endAngle, Autodesk::AutoCAD::Colors::Color^ color, bool visible)
{
	m_Parent->GetImpObj()->AddShape(new CShapeArc(color->ColorIndex, x, y, r, startAngle, endAngle, (visible ? Adesk::kTrue : Adesk::kFalse)));
}

void PosShape::ShapeCollection::AddText(double x, double y, double height, String^ str, Autodesk::AutoCAD::Colors::Color^ color, TextHorizontalMode horizontalAlignment, TextVerticalMode verticalAlignment, bool visible)
{
	m_Parent->GetImpObj()->AddShape(new CShapeText(color->ColorIndex, x, y, height, (wchar_t*)Marshal::StringToWchar(str),
		static_cast<AcDb::TextHorzMode>(horizontalAlignment), static_cast<AcDb::TextVertMode>(verticalAlignment), (visible ? Adesk::kTrue : Adesk::kFalse)));
}

int PosShape::ShapeCollection::Count::get()
{
	return (int)m_Parent->GetImpObj()->GetShapeCount();
}

void PosShape::ShapeCollection::Remove(int index)
{
	m_Parent->GetImpObj()->RemoveShape((int)index);
}

void PosShape::ShapeCollection::Clear()
{
	m_Parent->GetImpObj()->ClearShapes();
}

PosShape::Shape^ PosShape::ShapeCollection::default::get(int index)
{
	return Shape::FromNative(m_Parent->GetImpObj()->GetShape(index));
}

void PosShape::ShapeCollection::default::set(int index, PosShape::Shape^ value)
{
	m_Parent->GetImpObj()->SetShape(index, value->ToNative());
}

//*************************************************************************
// Static Properties
//*************************************************************************
String^ PosShape::TableName::get()
{
	return Marshal::WcharToString(CPosShape::GetTableName());
}
