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
PosShape::PosShape(CPosShape* shape) 
{
	m_PosShape = shape;
	m_Shapes = gcnew PosShape::ShapeCollection(m_PosShape);
}

PosShape::PosShape() 
{
	m_PosShape = new CPosShape();
	m_Shapes = gcnew PosShape::ShapeCollection(m_PosShape);
}

//*************************************************************************
// Properties
//*************************************************************************
String^ PosShape::Name::get()
{
	return Marshal::WcharToString(m_PosShape->Name());
}
void PosShape::Name::set(String^ value)
{
	m_PosShape->setName(Marshal::StringToWchar(value));
}

int PosShape::Fields::get()
{
	return m_PosShape->Fields();
}
void PosShape::Fields::set(int value)
{
	m_PosShape->setFields(value);
}

String^ PosShape::Formula::get()
{
	return Marshal::WcharToString(m_PosShape->Formula());
}
void PosShape::Formula::set(String^ value)
{
	m_PosShape->setFormula(Marshal::StringToWchar(value));
}

String^ PosShape::FormulaBending::get()
{
    return Marshal::WcharToString(m_PosShape->FormulaBending());
}
void PosShape::FormulaBending::set(String^ value)
{
	m_PosShape->setFormulaBending(Marshal::StringToWchar(value));
}

PosShape::ShapeCollection^ PosShape::Items::get()
{
	return m_Shapes;
}

int PosShape::Priority::get()
{
	return m_PosShape->Priority();
}
void PosShape::Priority::set(int value)
{
	m_PosShape->setPriority(value);
}


bool PosShape::IsBuiltIn::get()
{
	return (m_PosShape->IsBuiltIn() == Adesk::kTrue);
}
void PosShape::IsBuiltIn::set(bool value)
{
	m_PosShape->setIsBuiltIn(value ? Adesk::kTrue : Adesk::kFalse);
}

bool PosShape::IsUnknown::get()
{
    return (m_PosShape->IsUnknown() == Adesk::kTrue);
}
void PosShape::IsUnknown::set(bool value)
{
	m_PosShape->setIsUnknown(value ? Adesk::kTrue : Adesk::kFalse);
}

//*************************************************************************
// Shape Collection
//*************************************************************************
PosShape::ShapeCollection::ShapeCollection(CPosShape* parent)
{
	m_Parent = parent;
}

void PosShape::ShapeCollection::Add(PosShape::Shape^ value)
{
	m_Parent->AddShape(value->ToNative());
}

void PosShape::ShapeCollection::RemoveAt(int index)
{
	m_Parent->RemoveShape(index);
}

void PosShape::ShapeCollection::Clear()
{
	m_Parent->ClearShapes();
}

int PosShape::ShapeCollection::Count::get()
{
	return (int)m_Parent->GetShapeCount();
}

PosShape::Shape^ PosShape::ShapeCollection::default::get(int index)
{
	return Shape::FromNative(m_Parent->GetShape(index));
}
void PosShape::ShapeCollection::default::set(int index, PosShape::Shape^ value)
{
	m_Parent->SetShape(index, value->ToNative());
}

//*************************************************************************
// Static Methods
//*************************************************************************
void PosShape::AddPosShape(PosShape^ shape)
{
	CPosShape::AddPosShape(shape->m_PosShape);
}

PosShape^ PosShape::GetPosShape(String^ name)
{
	CPosShape* shape = CPosShape::GetPosShape(Marshal::StringToWstring(name));
	return gcnew PosShape(shape);
}

PosShape^ PosShape::GetUnknownPosShape()
{
	CPosShape* shape = CPosShape::GetUnknownPosShape();
	return gcnew PosShape(shape);
}

int PosShape::GetPosShapeCount()
{
	return (int)CPosShape::GetPosShapeCount();
}

System::Collections::Generic::Dictionary<String^, PosShape^>^ PosShape::GetAllPosShapes()
{
	System::Collections::Generic::Dictionary<String^, PosShape^>^ dict = gcnew System::Collections::Generic::Dictionary<String^, PosShape^>();
	std::map<std::wstring, CPosShape*> map = CPosShape::GetMap();
	for(std::map<std::wstring, CPosShape*>::iterator it = map.begin(); it != map.end(); it++)
	{
		dict->Add(Marshal::WstringToString(it->first), gcnew PosShape(it->second));
	}
	return dict;
}

void PosShape::ClearPosShapes(bool builtin, bool custom)
{
	CPosShape::ClearPosShapes(builtin, custom);
}

void PosShape::ReadPosShapesFromFile(String^ source, bool builtin)
{
	CPosShape::ReadPosShapesFromFile(Marshal::StringToWstring(source), builtin);
}

void PosShape::SavePosShapesToFile(String^ source, bool builtin, bool custom)
{
	CPosShape::SavePosShapesToFile(Marshal::StringToWstring(source), builtin, custom);
}
