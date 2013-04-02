//-----------------------------------------------------------------------------
//----- BOQStyle.cpp : Implementation of CBOQStyle
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "BOQStyle.h"
#include "Utility.h"

#include <fstream>
#include <algorithm>

std::map<std::wstring, CBOQStyle*> CBOQStyle::m_BuiltInStyles;
std::map<std::wstring, CBOQStyle*> CBOQStyle::m_CustomStyles;

//-----------------------------------------------------------------------------
CBOQStyle::CBOQStyle () : m_Name(NULL), m_Columns(NULL),
	m_TextStyleId(AcDbObjectId::kNull), m_HeadingStyleId(AcDbObjectId::kNull), m_FootingStyleId(AcDbObjectId::kNull),
	m_PosLabel(NULL), m_CountLabel(NULL), m_DiameterLabel(NULL), m_LengthLabel(NULL), m_ShapeLabel(NULL),
	m_TotalLengthLabel(NULL), m_DiameterListLabel(NULL), m_MultiplierHeadingLabel(NULL),
	m_DiameterLengthLabel(NULL), m_UnitWeightLabel(NULL), m_WeightLabel(NULL), m_GrossWeightLabel(NULL),
	m_IsBuiltIn(Adesk::kFalse)
{ }

CBOQStyle::~CBOQStyle () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Columns);

	acutDelString(m_PosLabel);
	acutDelString(m_CountLabel);
	acutDelString(m_DiameterLabel);
	acutDelString(m_LengthLabel);
	acutDelString(m_ShapeLabel);
	acutDelString(m_TotalLengthLabel);
	acutDelString(m_DiameterListLabel);
	acutDelString(m_DiameterLengthLabel);
	acutDelString(m_UnitWeightLabel);
	acutDelString(m_WeightLabel);
	acutDelString(m_GrossWeightLabel);
	acutDelString(m_MultiplierHeadingLabel);
}

//*************************************************************************
// Properties
//*************************************************************************
const ACHAR* CBOQStyle::Name(void) const
{
	return m_Name;
}
Acad::ErrorStatus CBOQStyle::setName(const ACHAR* newVal)
{
    acutDelString(m_Name);
    m_Name = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Name);
    }
	return Acad::eOk;
}

const ACHAR* CBOQStyle::Columns(void) const
{
	return m_Columns;
}

Acad::ErrorStatus CBOQStyle::setColumns(const ACHAR* newVal)
{
	if(m_Columns != NULL)
		acutDelString(m_Columns);
    m_Columns = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Columns);
    }

	return Acad::eOk;
}

const AcDbObjectId& CBOQStyle::TextStyleId(void) const
{
	return m_TextStyleId;
}
Acad::ErrorStatus CBOQStyle::setTextStyleId(const AcDbObjectId& newVal)
{
	m_TextStyleId = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQStyle::HeadingStyleId(void) const
{
	return m_HeadingStyleId;
}
Acad::ErrorStatus CBOQStyle::setHeadingStyleId(const AcDbObjectId& newVal)
{
	m_HeadingStyleId = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQStyle::FootingStyleId(void) const
{
	return m_FootingStyleId;
}
Acad::ErrorStatus CBOQStyle::setFootingStyleId(const AcDbObjectId& newVal)
{
	m_FootingStyleId = newVal;
	return Acad::eOk;
}

// Get labels
const ACHAR* CBOQStyle::PosLabel(void) const            { return m_PosLabel; }
const ACHAR* CBOQStyle::CountLabel(void) const          { return m_CountLabel; }
const ACHAR* CBOQStyle::DiameterLabel(void) const       { return m_DiameterLabel; }
const ACHAR* CBOQStyle::LengthLabel(void) const         { return m_LengthLabel; }
const ACHAR* CBOQStyle::ShapeLabel(void) const          { return m_ShapeLabel; }
const ACHAR* CBOQStyle::TotalLengthLabel(void) const    { return m_TotalLengthLabel; }
const ACHAR* CBOQStyle::DiameterListLabel(void) const   { return m_DiameterListLabel; }
const ACHAR* CBOQStyle::DiameterLengthLabel(void) const { return m_DiameterLengthLabel; }
const ACHAR* CBOQStyle::UnitWeightLabel(void) const     { return m_UnitWeightLabel; }
const ACHAR* CBOQStyle::WeightLabel(void) const         { return m_WeightLabel; }
const ACHAR* CBOQStyle::GrossWeightLabel(void) const    { return m_GrossWeightLabel; }
const ACHAR* CBOQStyle::MultiplierHeadingLabel(void) const { return m_MultiplierHeadingLabel; }
// Set labels
Acad::ErrorStatus CBOQStyle::setPosLabel(const ACHAR* newVal)            { acutDelString(m_PosLabel); acutUpdString(newVal, m_PosLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setCountLabel(const ACHAR* newVal)          { acutDelString(m_CountLabel); acutUpdString(newVal, m_CountLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setDiameterLabel(const ACHAR* newVal)       { acutDelString(m_DiameterLabel); acutUpdString(newVal, m_DiameterLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setLengthLabel(const ACHAR* newVal)         { acutDelString(m_LengthLabel); acutUpdString(newVal, m_LengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setShapeLabel(const ACHAR* newVal)          { acutDelString(m_ShapeLabel); acutUpdString(newVal, m_ShapeLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setTotalLengthLabel(const ACHAR* newVal)    { acutDelString(m_TotalLengthLabel); acutUpdString(newVal, m_TotalLengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setDiameterListLabel(const ACHAR* newVal)   { acutDelString(m_DiameterListLabel); acutUpdString(newVal, m_DiameterListLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setDiameterLengthLabel(const ACHAR* newVal) { acutDelString(m_DiameterLengthLabel); acutUpdString(newVal, m_DiameterLengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setUnitWeightLabel(const ACHAR* newVal)     { acutDelString(m_UnitWeightLabel); acutUpdString(newVal, m_UnitWeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setWeightLabel(const ACHAR* newVal)         { acutDelString(m_WeightLabel); acutUpdString(newVal, m_WeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setGrossWeightLabel(const ACHAR* newVal)    { acutDelString(m_GrossWeightLabel); acutUpdString(newVal, m_GrossWeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setMultiplierHeadingLabel(const ACHAR* newVal) { acutDelString(m_MultiplierHeadingLabel); acutUpdString(newVal, m_MultiplierHeadingLabel); return Acad::eOk; }

const Adesk::Boolean CBOQStyle::IsBuiltIn(void) const
{
	return m_IsBuiltIn;
}

Acad::ErrorStatus CBOQStyle::setIsBuiltIn(const Adesk::Boolean newVal)
{
	m_IsBuiltIn = newVal;
	return Acad::eOk;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************
void CBOQStyle::AddBOQStyle(CBOQStyle* style)
{
	if(style->IsBuiltIn())
		m_BuiltInStyles[style->Name()] = style;
	else
		m_CustomStyles[style->Name()] = style;
}

CBOQStyle* CBOQStyle::GetBOQStyle(const std::wstring name)
{
	std::map<std::wstring, CBOQStyle*>::iterator it;
	if((it = m_BuiltInStyles.find(name)) != m_BuiltInStyles.end())
		return (*it).second;
	if((it = m_CustomStyles.find(name)) != m_CustomStyles.end())
		return (*it).second;

	CBOQStyle* style = CBOQStyle::GetUnknownBOQStyle();
	style->setName(name.c_str());
	return style;
}

CBOQStyle* CBOQStyle::GetUnknownBOQStyle()
{
	CBOQStyle* style = new CBOQStyle();

	style->setName(_T("HATA"));

	return style;
}

bool CBOQStyle::HasBOQStyle(const std::wstring name)
{
	if(m_BuiltInStyles.find(name) != m_BuiltInStyles.end())
		return true;
	else if(m_CustomStyles.find(name) != m_CustomStyles.end())
		return true;

	return false;
}

int CBOQStyle::GetBOQStyleCount(const bool builtin, const bool custom)
{
	int count = 0;

	if(builtin) count += (int)m_BuiltInStyles.size();
	if(custom) count += (int)m_CustomStyles.size();

	return count;
}

void CBOQStyle::ClearBOQStyles(const bool builtin, const bool custom)
{
	if(builtin && !m_BuiltInStyles.empty())
	{
		for(std::map<std::wstring, CBOQStyle*>::iterator it = m_BuiltInStyles.begin(); it != m_BuiltInStyles.end(); it++)
		{
			delete (*it).second;
		}
		m_BuiltInStyles.clear();
	}
	if(custom && !m_CustomStyles.empty())
	{
		for(std::map<std::wstring, CBOQStyle*>::iterator it = m_CustomStyles.begin(); it != m_CustomStyles.end(); it++)
		{
			delete (*it).second;
		}
		m_CustomStyles.clear();
	}
}

std::vector<std::wstring> CBOQStyle::GetAllStyles(const bool builtin, const bool custom)
{
	std::vector<std::wstring> names;
	if(builtin && !m_BuiltInStyles.empty())
	{
		for(std::map<std::wstring, CBOQStyle*>::iterator it = m_BuiltInStyles.begin(); it != m_BuiltInStyles.end(); it++)
		{
			names.push_back(std::wstring(it->first));
		}
	}
	if(custom && !m_CustomStyles.empty())
	{
		for(std::map<std::wstring, CBOQStyle*>::iterator it = m_CustomStyles.begin(); it != m_CustomStyles.end(); it++)
		{
			names.push_back(std::wstring(it->first));
		}
	}

	std::sort(names.begin(), names.end(), CBOQStyle::SortStyleNames);
	return names;
}

bool CBOQStyle::SortStyleNames(const std::wstring p1, const std::wstring p2)
{
	return p1.compare(p2) < 0;
}

void CBOQStyle::ReadBOQStylesFromResource(HINSTANCE hInstance, const int resid)
{
	std::wstring source = Utility::StringFromResource(hInstance, L"TABLESTYLE", resid);
	ReadBOQStylesFromString(source, true);
}

void CBOQStyle::ReadBOQStylesFromFile(const std::wstring filename)
{
	std::wstring source = Utility::StringFromFile(filename);
	ReadBOQStylesFromString(source, false);
}

void CBOQStyle::ReadBOQStylesFromString(const std::wstring source, const bool builtin)
{
	std::wistringstream stream(source);
	std::wstring   line;
	
	while(std::getline(stream, line))
	{
		while(!stream.eof() && line.compare(0, 5, L"BEGIN") != 0)
			std::getline(stream, line);

		if(stream.eof())
			break;

		std::wstring name;
		std::wstring columns;
		std::wstring posLabel;
		std::wstring countLabel;
		std::wstring diameterLabel;
		std::wstring lengthLabel;
		std::wstring shapeLabel;
		std::wstring totalLengthLabel;
		std::wstring diameterListLabel;
		std::wstring diameterLengthLabel;
		std::wstring unitWeightLabel;
		std::wstring weightLabel;
		std::wstring grossWeightLabel;
		std::wstring multiplierHeadingLabel;

		std::wstring textStyleName; std::wstring textStyleFont; double textStyleWidth = 1.0;
		std::wstring headingStyleName; std::wstring headingStyleFont; double headingStyleWidth = 1.0;
		std::wstring footingStyleName; std::wstring footingStyleFont; double footingStyleWidth = 1.0;

		while(!stream.eof() && line.compare(0, 3, L"END") != 0)
		{
			std::getline(stream, line);

			// Read entire field
			std::wstringstream linestream;
			std::wstring fieldname;
			std::wstring fielddata;
			linestream.clear(); linestream.str(std::wstring());
			linestream << line;
			std::getline(linestream, fieldname, L'\t');
			std::getline(linestream, fielddata);
			if(fielddata.length() > 0)
			{
				if(fielddata[fielddata.length() - 1] == L'\r')
					fielddata.erase(fielddata.length() - 1);
			}

			// Assign field data
			if(fieldname == L"Name")
				name.assign(fielddata);
			else if(fieldname == L"Columns")
				columns.assign(fielddata);
			else if(fieldname == L"PosLabel")
				posLabel.assign(fielddata);
			else if(fieldname == L"CountLabel")
				countLabel.assign(fielddata);
			else if(fieldname == L"DiameterLabel")
				diameterLabel.assign(fielddata);
			else if(fieldname == L"LengthLabel")
				lengthLabel.assign(fielddata);
			else if(fieldname == L"ShapeLabel")
				shapeLabel.assign(fielddata);
			else if(fieldname == L"TotalLengthLabel")
				totalLengthLabel.assign(fielddata);
			else if(fieldname == L"DiameterListLabel")
				diameterListLabel.assign(fielddata);
			else if(fieldname == L"DiameterLengthLabel")
				diameterLengthLabel.assign(fielddata);
			else if(fieldname == L"UnitWeightLabel")
				unitWeightLabel.assign(fielddata);
			else if(fieldname == L"WeightLabel")
				weightLabel.assign(fielddata);
			else if(fieldname == L"GrossWeightLabel")
				grossWeightLabel.assign(fielddata);
			else if(fieldname == L"MultiplierHeadingLabel")
				multiplierHeadingLabel.assign(fielddata);
			else if(fieldname == L"TextStyle")
			{
				linestream.clear(); linestream.str(std::wstring());
				linestream << fielddata;
				std::getline(linestream, textStyleName, L'\t');
				std::getline(linestream, textStyleFont, L'\t');
				linestream >> textStyleWidth;
			}
			else if(fieldname == L"HeadingStyle")
			{
				linestream.clear(); linestream.str(std::wstring());
				linestream << fielddata;
				std::getline(linestream, headingStyleName, L'\t');
				std::getline(linestream, headingStyleFont, L'\t');
				linestream >> headingStyleWidth;
			}
			else if(fieldname == L"FootingStyle")
			{
				linestream.clear(); linestream.str(std::wstring());
				linestream << fielddata;		
				std::getline(linestream, footingStyleName, L'\t');
				std::getline(linestream, footingStyleFont, L'\t');
				linestream >> footingStyleWidth;
			}
		}

		// Create the style
		CBOQStyle *style = new CBOQStyle();

		style->setName(name.c_str());
		style->setColumns(columns.c_str());
		style->setPosLabel(posLabel.c_str());
		style->setCountLabel(countLabel.c_str());
		style->setDiameterLabel(diameterLabel.c_str());
		style->setLengthLabel(lengthLabel.c_str());
		style->setShapeLabel(shapeLabel.c_str());
		style->setTotalLengthLabel(totalLengthLabel.c_str());
		style->setDiameterListLabel(diameterListLabel.c_str());
		style->setDiameterLengthLabel(diameterLengthLabel.c_str());
		style->setUnitWeightLabel(unitWeightLabel.c_str());
		style->setWeightLabel(weightLabel.c_str());
		style->setGrossWeightLabel(grossWeightLabel.c_str());
		style->setMultiplierHeadingLabel(multiplierHeadingLabel.c_str());

		style->setTextStyleId(Utility::CreateTextStyle(textStyleName.c_str(), textStyleFont.c_str(), textStyleWidth));
		style->setHeadingStyleId(Utility::CreateTextStyle(headingStyleName.c_str(), headingStyleFont.c_str(), headingStyleWidth));
		style->setFootingStyleId(Utility::CreateTextStyle(footingStyleName.c_str(), footingStyleFont.c_str(), footingStyleWidth));

		style->setIsBuiltIn(builtin ? Adesk::kTrue : Adesk::kFalse);

		// Add the style to the dictionary
		if(builtin)
			m_BuiltInStyles[name] = style;
		else
			m_CustomStyles[name] = style;
	}
}


void CBOQStyle::SaveBOQStylesToFile(const std::wstring filename)
{
	std::wofstream ofs(filename.c_str());

	if(m_CustomStyles.empty()) return;

	for(std::map<std::wstring, CBOQStyle*>::iterator it = m_CustomStyles.begin(); it != m_CustomStyles.end(); it++)
	{
		CBOQStyle* style = (*it).second;

		ofs << L"BEGIN" << std::endl;

		ofs << L"Name"					<< L'\t' << style->Name()					<< std::endl;
		ofs << L"Columns"				<< L'\t' << style->Columns()				<< std::endl;
		ofs << L"PosLabel"				<< L'\t' << style->PosLabel()				<< std::endl;
		ofs << L"CountLabel"			<< L'\t' << style->CountLabel()				<< std::endl;
		ofs << L"DiameterLabel"			<< L'\t' << style->DiameterLabel()			<< std::endl;
		ofs << L"LengthLabel"			<< L'\t' << style->LengthLabel()			<< std::endl;
		ofs << L"ShapeLabel"			<< L'\t' << style->ShapeLabel()				<< std::endl;
		ofs << L"TotalLengthLabel"		<< L'\t' << style->TotalLengthLabel()		<< std::endl;
		ofs << L"DiameterListLabel"		<< L'\t' << style->DiameterListLabel()		<< std::endl;
		ofs << L"DiameterLengthLabel"	<< L'\t' << style->DiameterLengthLabel()	<< std::endl;
		ofs << L"UnitWeightLabel"		<< L'\t' << style->UnitWeightLabel()		<< std::endl;
		ofs << L"WeightLabel"			<< L'\t' << style->WeightLabel()			<< std::endl;
		ofs << L"GrossWeightLabel"		<< L'\t' << style->GrossWeightLabel()		<< std::endl;

		ACHAR* textStyleName = NULL;	ACHAR* textStyleFont = NULL;	double textStyleWidth = 1.0;
		ACHAR* headingStyleName = NULL;	ACHAR* headingStyleFont = NULL;	double headingStyleWidth = 1.0;
		ACHAR* footingStyleName = NULL;	ACHAR* footingStyleFont = NULL;	double footingStyleWidth = 1.0;

		AcDbObjectPointer<AcDbTextStyleTableRecord> pTextStyle(style->TextStyleId(), AcDb::kForRead);
		AcDbObjectPointer<AcDbTextStyleTableRecord> pHeadingStyle(style->HeadingStyleId(), AcDb::kForRead);
		AcDbObjectPointer<AcDbTextStyleTableRecord> pFootingStyle(style->FootingStyleId(), AcDb::kForRead);

		pTextStyle->getName(textStyleName);			pTextStyle->fileName(textStyleFont);		textStyleWidth = pTextStyle->xScale();
		pHeadingStyle->getName(headingStyleName);	pHeadingStyle->fileName(headingStyleFont);	headingStyleWidth = pHeadingStyle->xScale();
		pFootingStyle->getName(footingStyleName);	pFootingStyle->fileName(footingStyleFont);	footingStyleWidth = pFootingStyle->xScale();

		ofs << L"TextStyle"		<< L'\t' << textStyleName		<< L'\t' << textStyleFont		<< L'\t' << textStyleWidth		<< std::endl;
		ofs << L"HeadingStyle"	<< L'\t' << headingStyleName	<< L'\t' << headingStyleFont	<< L'\t' << headingStyleWidth	<< std::endl;
		ofs << L"FootingStyle"	<< L'\t' << footingStyleName	<< L'\t' << footingStyleFont	<< L'\t' << footingStyleWidth	<< std::endl;

		acutDelString(textStyleName);		acutDelString(textStyleFont);
		acutDelString(headingStyleName);	acutDelString(headingStyleFont);
		acutDelString(footingStyleName);	acutDelString(footingStyleFont);

		ofs << L"END" << std::endl;
		ofs << std::endl;
	}
}
