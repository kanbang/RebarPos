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
: Autodesk::AutoCAD::Runtime::DisposableWrapper(IntPtr(new CBOQStyle()), true)
{ }

BOQStyle::BOQStyle(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::Runtime::DisposableWrapper(unmanagedPointer, autoDelete)
{ }

//*************************************************************************
// Properties
//*************************************************************************
String^ BOQStyle::Name::get()
{
	return Marshal::WcharToString(GetImpObj()->Name());
}
void BOQStyle::Name::set(String^ value)
{
    GetImpObj()->setName(Marshal::StringToWchar(value));
}

String^ BOQStyle::Columns::get()
{
	return Marshal::WcharToString(GetImpObj()->Columns());
}
void BOQStyle::Columns::set(String^ value)
{
	GetImpObj()->setColumns(Marshal::StringToWchar(value));
}

String^ BOQStyle::PosLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->PosLabel());
}
void BOQStyle::PosLabel::set(String^ value)
{
    GetImpObj()->setPosLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::CountLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->CountLabel());
}
void BOQStyle::CountLabel::set(String^ value)
{
    GetImpObj()->setCountLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::DiameterLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->DiameterLabel());
}
void BOQStyle::DiameterLabel::set(String^ value)
{
    GetImpObj()->setDiameterLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::LengthLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->LengthLabel());
}
void BOQStyle::LengthLabel::set(String^ value)
{
    GetImpObj()->setLengthLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::ShapeLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->ShapeLabel());
}
void BOQStyle::ShapeLabel::set(String^ value)
{
    GetImpObj()->setShapeLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::TotalLengthLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->TotalLengthLabel());
}
void BOQStyle::TotalLengthLabel::set(String^ value)
{
    GetImpObj()->setTotalLengthLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::DiameterListLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->DiameterListLabel());
}
void BOQStyle::DiameterListLabel::set(String^ value)
{
    GetImpObj()->setDiameterListLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::DiameterLengthLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->DiameterLengthLabel());
}
void BOQStyle::DiameterLengthLabel::set(String^ value)
{
    GetImpObj()->setDiameterLengthLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::UnitWeightLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->UnitWeightLabel());
}
void BOQStyle::UnitWeightLabel::set(String^ value)
{
    GetImpObj()->setUnitWeightLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::WeightLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->WeightLabel());
}
void BOQStyle::WeightLabel::set(String^ value)
{
    GetImpObj()->setWeightLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::GrossWeightLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->GrossWeightLabel());
}
void BOQStyle::GrossWeightLabel::set(String^ value)
{
    GetImpObj()->setGrossWeightLabel(Marshal::StringToWchar(value));
}

String^ BOQStyle::MultiplierHeadingLabel::get()
{
	return Marshal::WcharToString(GetImpObj()->MultiplierHeadingLabel());
}
void BOQStyle::MultiplierHeadingLabel::set(String^ value)
{
    GetImpObj()->setMultiplierHeadingLabel(Marshal::StringToWchar(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::TextStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->TextStyleId());
}
void BOQStyle::TextStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	GetImpObj()->setTextStyleId(Marshal::FromObjectId(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::HeadingStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->HeadingStyleId());
}
void BOQStyle::HeadingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	GetImpObj()->setHeadingStyleId(Marshal::FromObjectId(value));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId BOQStyle::FootingStyleId::get()
{
	return Marshal::ToObjectId (GetImpObj()->FootingStyleId());
}
void BOQStyle::FootingStyleId::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	GetImpObj()->setFootingStyleId(Marshal::FromObjectId(value));
}

bool BOQStyle::IsBuiltIn::get()
{
	return (GetImpObj()->IsBuiltIn() == Adesk::kTrue);
}

//*************************************************************************
// DisposableWrapper implementation
//*************************************************************************
void BOQStyle::DeleteUnmanagedObject(void)
{
	if(AutoDelete)
	{
		delete GetImpObj();
	}
}

//*************************************************************************
// Static Methods
//*************************************************************************
void BOQStyle::AddBOQStyle(BOQStyle^ style)
{
	assert(!style->IsBuiltIn);
	CBOQStyle::AddBOQStyle(style->GetImpObj());
}

BOQStyle^ BOQStyle::GetBOQStyle(String^ name)
{
	CBOQStyle* style = CBOQStyle::GetBOQStyle(Marshal::StringToWstring(name));
	return gcnew BOQStyle(IntPtr(style), false);
}

BOQStyle^ BOQStyle::GetUnknownBOQStyle()
{
	CBOQStyle* style = CBOQStyle::GetUnknownBOQStyle();
	return gcnew BOQStyle(IntPtr(style), true);
}

bool BOQStyle::HasBOQStyle(String^ name)
{
	return CBOQStyle::HasBOQStyle(Marshal::StringToWstring(name));
}

int BOQStyle::GetBOQStyleCount()
{
	return CBOQStyle::GetBOQStyleCount(true, true);
}

System::Collections::Generic::List<String^>^ BOQStyle::GetAllBOQStyles()
{
	System::Collections::Generic::List<String^>^ dict = gcnew System::Collections::Generic::List<String^>();
	std::vector<std::wstring> map = CBOQStyle::GetAllStyles(true, true);
	for(std::vector<std::wstring>::iterator it = map.begin(); it != map.end(); it++)
	{
		dict->Add(Marshal::WstringToString((*it)));
	}
	return dict;
}

void BOQStyle::ClearBOQStyles()
{
	CBOQStyle::ClearBOQStyles(false, true);
}

void BOQStyle::ReadBOQStylesFromFile(String^ source)
{
	CBOQStyle::ReadBOQStylesFromFile(Marshal::StringToWstring(source));
}

void BOQStyle::SaveBOQStylesToFile(String^ source)
{
	CBOQStyle::SaveBOQStylesToFile(Marshal::StringToWstring(source));
}
