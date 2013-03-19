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
BOQStyle::BOQStyle(CBOQStyle* style) 
{
	m_BOQStyle = style;
}

BOQStyle::BOQStyle() 
{
	m_BOQStyle = new CBOQStyle();
}


//*************************************************************************
// Properties
//*************************************************************************
String^ BOQStyle::Name::get()
{
	return Marshal::WcharToString(m_BOQStyle->Name());
}
void BOQStyle::Name::set(String^ value)
{
    m_BOQStyle->setName(Marshal::StringToWchar(value));
}

String^ BOQStyle::Columns::get()
{
	return Marshal::WcharToString(m_BOQStyle->Columns());
}
void BOQStyle::Columns::set(String^ value)
{
	m_BOQStyle->setColumns(Marshal::StringToWchar(value));
}

String^ BOQStyle::PosLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->PosLabel());
}
void BOQStyle::PosLabel::set(String^ value)
{
    m_BOQStyle->setPosLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::CountLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->CountLabel());
}
void BOQStyle::CountLabel::set(String^ value)
{
    m_BOQStyle->setCountLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::DiameterLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->DiameterLabel());
}
void BOQStyle::DiameterLabel::set(String^ value)
{
    m_BOQStyle->setDiameterLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::LengthLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->LengthLabel());
}
void BOQStyle::LengthLabel::set(String^ value)
{
    m_BOQStyle->setLengthLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::ShapeLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->ShapeLabel());
}
void BOQStyle::ShapeLabel::set(String^ value)
{
    m_BOQStyle->setShapeLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::TotalLengthLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->TotalLengthLabel());
}
void BOQStyle::TotalLengthLabel::set(String^ value)
{
    m_BOQStyle->setTotalLengthLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::DiameterListLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->DiameterListLabel());
}
void BOQStyle::DiameterListLabel::set(String^ value)
{
    m_BOQStyle->setDiameterListLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::DiameterLengthLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->DiameterLengthLabel());
}
void BOQStyle::DiameterLengthLabel::set(String^ value)
{
    m_BOQStyle->setDiameterLengthLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::UnitWeightLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->UnitWeightLabel());
}
void BOQStyle::UnitWeightLabel::set(String^ value)
{
    m_BOQStyle->setUnitWeightLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::WeightLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->WeightLabel());
}
void BOQStyle::WeightLabel::set(String^ value)
{
    m_BOQStyle->setWeightLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::GrossWeightLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->GrossWeightLabel());
}
void BOQStyle::GrossWeightLabel::set(String^ value)
{
    m_BOQStyle->setGrossWeightLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::MultiplierHeadingLabel::get()
{
	return Marshal::WcharToString(m_BOQStyle->MultiplierHeadingLabel());
}
void BOQStyle::MultiplierHeadingLabel::set(String^ value)
{
    m_BOQStyle->setMultiplierHeadingLabel(Marshal::StringToWchar(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::TextStyleId::get()
{
	return Marshal::ToObjectId (m_BOQStyle->TextStyleId());
}
void BOQStyle::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	m_BOQStyle->setTextStyleId(Marshal::FromObjectId(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::HeadingStyleId::get()
{
	return Marshal::ToObjectId (m_BOQStyle->HeadingStyleId());
}
void BOQStyle::HeadingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	m_BOQStyle->setHeadingStyleId(Marshal::FromObjectId(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::FootingStyleId::get()
{
	return Marshal::ToObjectId (m_BOQStyle->FootingStyleId());
}
void BOQStyle::FootingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	m_BOQStyle->setFootingStyleId(Marshal::FromObjectId(value));
}

bool BOQStyle::IsBuiltIn::get()
{
	return (m_BOQStyle->IsBuiltIn() == Adesk::kTrue);
}
void BOQStyle::IsBuiltIn::set(bool value)
{
	m_BOQStyle->setIsBuiltIn(value ? Adesk::kTrue : Adesk::kFalse);
}

//*************************************************************************
// Static Methods
//*************************************************************************
void BOQStyle::AddBOQStyle(BOQStyle^ style)
{
	CBOQStyle::AddBOQStyle(style->m_BOQStyle);
}

BOQStyle^ BOQStyle::GetBOQStyle(String^ name)
{
	CBOQStyle* style = CBOQStyle::GetBOQStyle(Marshal::StringToWstring(name));
	return gcnew BOQStyle(style);
}

int BOQStyle::GetBOQStyleCount()
{
	return (int)CBOQStyle::GetBOQStyleCount();
}

System::Collections::Generic::Dictionary<String^, BOQStyle^>^ BOQStyle::GetAllBOQStyles()
{
	System::Collections::Generic::Dictionary<String^, BOQStyle^>^ dict = gcnew System::Collections::Generic::Dictionary<String^, BOQStyle^>();
	std::map<std::wstring, CBOQStyle*> map = CBOQStyle::GetMap();
	for(std::map<std::wstring, CBOQStyle*>::iterator it = map.begin(); it != map.end(); it++)
	{
		dict->Add(Marshal::WstringToString(it->first), gcnew BOQStyle(it->second));
	}
	return dict;
}

void BOQStyle::ClearBOQStyles()
{
	CBOQStyle::ClearBOQStyles(false, true);
}

void BOQStyle::ReadBOQStylesFromFile(String^ source)
{
	CBOQStyle::ReadBOQStylesFromFile(Marshal::StringToWstring(source), true);
}

void BOQStyle::SaveBOQStylesToFile(String^ source)
{
	CBOQStyle::SaveBOQStylesToFile(Marshal::StringToWstring(source), false, true);
}
