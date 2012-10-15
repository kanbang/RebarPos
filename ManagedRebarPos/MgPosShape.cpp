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
	m_Name = Marshal::WcharToString(shape->Name());
	m_Fields = shape->Fields();
	m_Formula = Marshal::WcharToString(shape->Formula());
	m_FormulaBending = Marshal::WcharToString(shape->FormulaBending());
	m_Priority = shape->Priority();

	m_Shapes = gcnew PosShape::ShapeCollection(shape);
}

//*************************************************************************
// Properties
//*************************************************************************
String^ PosShape::Name::get()
{
	return m_Name;
}

int PosShape::Fields::get()
{
    return m_Fields;
}

String^ PosShape::Formula::get()
{
    return m_Formula;
}

String^ PosShape::FormulaBending::get()
{
    return m_FormulaBending;
}

PosShape::ShapeCollection^ PosShape::Items::get()
{
	return m_Shapes;
}

int PosShape::Priority::get()
{
    return m_Priority;
}

//*************************************************************************
// Shape Collection
//*************************************************************************
PosShape::ShapeCollection::ShapeCollection(CPosShape* parent)
{
	m_Parent = parent;
}

int PosShape::ShapeCollection::Count::get()
{
	return (int)m_Parent->GetShapeCount();
}

PosShape::Shape^ PosShape::ShapeCollection::default::get(int index)
{
	return Shape::FromNative(m_Parent->GetShape(index));
}

//*************************************************************************
// Static Methods
//*************************************************************************
PosShape^ PosShape::GetPosShape(String^ name)
{
	CPosShape* shape = CPosShape::GetPosShape(Marshal::StringToWstring(name));
	return gcnew PosShape(shape);
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