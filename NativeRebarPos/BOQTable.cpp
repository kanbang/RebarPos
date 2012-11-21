//-----------------------------------------------------------------------------
//----- BOQTable.cpp : Implementation of CBOQTable
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "BOQTable.h"
#include "BOQStyle.h"
#include "Utility.h"

Adesk::UInt32 CBOQTable::kCurrentVersionNumber = 1;

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_DXF_DEFINE_MEMBERS(CBOQTable, CGenericTable,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, BOQTABLE,
	"OZOZRebarPos\
	|Product Desc:     Rebar BOQ Table Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CBOQTable::CBOQTable() :
	m_Multiplier(1), m_Heading(NULL), m_Footing(NULL), m_Note(NULL),
	m_Precision(0), m_DisplayUnit(CBOQTable::MM), m_Columns(NULL),
	m_TextColor(2), m_PosColor(3), m_LineColor(2), m_SeparatorColor(1), m_BorderColor(33), m_HeadingColor(9), m_FootingColor(9),
	m_TextStyleId(AcDbObjectId::kNull), m_HeadingStyleId(AcDbObjectId::kNull), m_FootingStyleId(AcDbObjectId::kNull),
	m_HeadingScale(1.5), m_FootingScale(1.0), m_RowSpacing(0.75),
	m_PosLabel(NULL), m_CountLabel(NULL), m_DiameterLabel(NULL), m_LengthLabel(NULL), m_ShapeLabel(NULL),
	m_TotalLengthLabel(NULL), m_DiameterListLabel(NULL), m_MultiplierHeadingLabel(NULL),
	m_DiameterLengthLabel(NULL), m_UnitWeightLabel(NULL), m_WeightLabel(NULL), m_GrossWeightLabel(NULL)
{
}

CBOQTable::~CBOQTable()
{
	ClearRows();

	acutDelString(m_Heading);
	acutDelString(m_Footing);
	acutDelString(m_Note);

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
const Adesk::Int32 CBOQTable::Multiplier(void) const
{
	assertReadEnabled();
	return m_Multiplier;
}

Acad::ErrorStatus CBOQTable::setMultiplier(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Multiplier = newVal;
	UpdateTable();
	return Acad::eOk;
}

const ACHAR* CBOQTable::Heading(void) const
{
	assertReadEnabled();
	return m_Heading;
}
Acad::ErrorStatus CBOQTable::setHeading(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Heading);
    m_Heading = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Heading);
    }
	UpdateTable();
	return Acad::eOk;
}

const ACHAR* CBOQTable::Footing(void) const
{
	assertReadEnabled();
	return m_Footing;
}
Acad::ErrorStatus CBOQTable::setFooting(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Footing);
    m_Footing = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Footing);
    }
	UpdateTable();
	return Acad::eOk;
}

const ACHAR* CBOQTable::Note(void) const
{
	assertReadEnabled();
	return m_Note;
}
Acad::ErrorStatus CBOQTable::setNote(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Note);
    m_Note = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Note);
    }
	UpdateTable();
	return Acad::eOk;
}

const Adesk::Int32 CBOQTable::Precision(void) const
{
	assertReadEnabled();
	return m_Precision;
}
Acad::ErrorStatus CBOQTable::setPrecision(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Precision = newVal;
	return Acad::eOk;
}

const CBOQTable::DrawingUnits CBOQTable::DisplayUnit(void) const
{
	assertReadEnabled();
	return m_DisplayUnit;
}
Acad::ErrorStatus CBOQTable::setDisplayUnit(const CBOQTable::DrawingUnits newVal)
{
	assertWriteEnabled();
	m_DisplayUnit = newVal;
	return Acad::eOk;
}

const ACHAR* CBOQTable::ColumnDef(void) const
{
	assertReadEnabled();
	return m_Columns;
}

Acad::ErrorStatus CBOQTable::setColumnDef(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_Columns != NULL)
		acutDelString(m_Columns);
    m_Columns = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Columns);
    }

	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::TextColor(void) const
{
	assertReadEnabled();
	return m_TextColor;
}
Acad::ErrorStatus CBOQTable::setTextColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_TextColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::PosColor(void) const
{
	assertReadEnabled();
	return m_PosColor;
}
Acad::ErrorStatus CBOQTable::setPosColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_PosColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::LineColor(void) const
{
	assertReadEnabled();
	return m_LineColor;
}
Acad::ErrorStatus CBOQTable::setLineColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_LineColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::SeparatorColor(void) const
{
	assertReadEnabled();
	return m_SeparatorColor;
}
Acad::ErrorStatus CBOQTable::setSeparatorColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_SeparatorColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::BorderColor(void) const
{
	assertReadEnabled();
	return m_BorderColor;
}
Acad::ErrorStatus CBOQTable::setBorderColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_BorderColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::HeadingColor(void) const
{
	assertReadEnabled();
	return m_HeadingColor;
}
Acad::ErrorStatus CBOQTable::setHeadingColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_HeadingColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQTable::FootingColor(void) const
{
	assertReadEnabled();
	return m_FootingColor;
}
Acad::ErrorStatus CBOQTable::setFootingColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_FootingColor = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQTable::TextStyleId(void) const
{
	assertReadEnabled();
	return m_TextStyleId;
}
Acad::ErrorStatus CBOQTable::setTextStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_TextStyleId = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQTable::HeadingStyleId(void) const
{
	assertReadEnabled();
	return m_HeadingStyleId;
}
Acad::ErrorStatus CBOQTable::setHeadingStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_HeadingStyleId = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQTable::FootingStyleId(void) const
{
	assertReadEnabled();
	return m_FootingStyleId;
}
Acad::ErrorStatus CBOQTable::setFootingStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_FootingStyleId = newVal;
	return Acad::eOk;
}

const double CBOQTable::HeadingScale(void) const
{
	assertReadEnabled();
	return m_HeadingScale;
}
Acad::ErrorStatus CBOQTable::setHeadingScale(const double newVal)
{
	assertWriteEnabled();
	m_HeadingScale = newVal;
	return Acad::eOk;
}

const double CBOQTable::FootingScale(void) const
{
	assertReadEnabled();
	return m_FootingScale;
}
Acad::ErrorStatus CBOQTable::setFootingScale(const double newVal)
{
	assertWriteEnabled();
	m_FootingScale = newVal;
	return Acad::eOk;
}

const double CBOQTable::RowSpacing(void) const
{
	assertReadEnabled();
	return m_RowSpacing;
}
Acad::ErrorStatus CBOQTable::setRowSpacing(const double newVal)
{
	assertWriteEnabled();
	m_RowSpacing = newVal;
	return Acad::eOk;
}

// Get labels
const ACHAR* CBOQTable::PosLabel(void) const            { assertReadEnabled(); return m_PosLabel; }
const ACHAR* CBOQTable::CountLabel(void) const          { assertReadEnabled(); return m_CountLabel; }
const ACHAR* CBOQTable::DiameterLabel(void) const       { assertReadEnabled(); return m_DiameterLabel; }
const ACHAR* CBOQTable::LengthLabel(void) const         { assertReadEnabled(); return m_LengthLabel; }
const ACHAR* CBOQTable::ShapeLabel(void) const          { assertReadEnabled(); return m_ShapeLabel; }
const ACHAR* CBOQTable::TotalLengthLabel(void) const    { assertReadEnabled(); return m_TotalLengthLabel; }
const ACHAR* CBOQTable::DiameterListLabel(void) const   { assertReadEnabled(); return m_DiameterListLabel; }
const ACHAR* CBOQTable::DiameterLengthLabel(void) const { assertReadEnabled(); return m_DiameterLengthLabel; }
const ACHAR* CBOQTable::UnitWeightLabel(void) const     { assertReadEnabled(); return m_UnitWeightLabel; }
const ACHAR* CBOQTable::WeightLabel(void) const         { assertReadEnabled(); return m_WeightLabel; }
const ACHAR* CBOQTable::GrossWeightLabel(void) const    { assertReadEnabled(); return m_GrossWeightLabel; }
const ACHAR* CBOQTable::MultiplierHeadingLabel(void) const { assertReadEnabled(); return m_MultiplierHeadingLabel; }
// Set labels
Acad::ErrorStatus CBOQTable::setPosLabel(const ACHAR* newVal)            { assertWriteEnabled(); acutDelString(m_PosLabel); acutUpdString(newVal, m_PosLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setCountLabel(const ACHAR* newVal)          { assertWriteEnabled(); acutDelString(m_CountLabel); acutUpdString(newVal, m_CountLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setDiameterLabel(const ACHAR* newVal)       { assertWriteEnabled(); acutDelString(m_DiameterLabel); acutUpdString(newVal, m_DiameterLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setLengthLabel(const ACHAR* newVal)         { assertWriteEnabled(); acutDelString(m_LengthLabel); acutUpdString(newVal, m_LengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setShapeLabel(const ACHAR* newVal)          { assertWriteEnabled(); acutDelString(m_ShapeLabel); acutUpdString(newVal, m_ShapeLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setTotalLengthLabel(const ACHAR* newVal)    { assertWriteEnabled(); acutDelString(m_TotalLengthLabel); acutUpdString(newVal, m_TotalLengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setDiameterListLabel(const ACHAR* newVal)   { assertWriteEnabled(); acutDelString(m_DiameterListLabel); acutUpdString(newVal, m_DiameterListLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setDiameterLengthLabel(const ACHAR* newVal) { assertWriteEnabled(); acutDelString(m_DiameterLengthLabel); acutUpdString(newVal, m_DiameterLengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setUnitWeightLabel(const ACHAR* newVal)     { assertWriteEnabled(); acutDelString(m_UnitWeightLabel); acutUpdString(newVal, m_UnitWeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setWeightLabel(const ACHAR* newVal)         { assertWriteEnabled(); acutDelString(m_WeightLabel); acutUpdString(newVal, m_WeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setGrossWeightLabel(const ACHAR* newVal)    { assertWriteEnabled(); acutDelString(m_GrossWeightLabel); acutUpdString(newVal, m_GrossWeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQTable::setMultiplierHeadingLabel(const ACHAR* newVal) { assertWriteEnabled(); acutDelString(m_MultiplierHeadingLabel); acutUpdString(newVal, m_MultiplierHeadingLabel); return Acad::eOk; }

//*************************************************************************
// Class Methods
//*************************************************************************
void CBOQTable::AddRow(CBOQRow* const row)
{
	assertWriteEnabled();
	m_List.push_back(row);
	UpdateTable();
}

const CBOQRow* CBOQTable::GetRow(const RowListSize index) const
{
	assertReadEnabled();
	return m_List.at(index);
}

void CBOQTable::SetRow(const RowListSize index, CBOQRow* const row)
{
	assertWriteEnabled();
	delete m_List[index];
	m_List[index] = row;
	UpdateTable();
}

void CBOQTable::RemoveRow(const RowListSize index)
{
	assertWriteEnabled();
	delete m_List[index];
	RowListIt it = m_List.begin();
	m_List.erase(it + index);
	UpdateTable();
}

void CBOQTable::ClearRows()
{
	assertWriteEnabled();
	for(RowListIt it = m_List.begin(); it != m_List.end(); it++)
	{
		delete *it;
	}
	m_List.clear();
	UpdateTable();
}

const RowListSize CBOQTable::GetRowCount() const
{
	assertReadEnabled();
	return m_List.size();
}

//*************************************************************************
// Helper methods
//*************************************************************************

void CBOQTable::UpdateTable(void)
{
	assertReadEnabled();

	// Open style
	ACHAR* lastColumns = NULL;
	acutUpdString(ColumnDef(), lastColumns);

	// Parse columns
	std::vector<CBOQTable::ColumnType> columns = ParseColumns(lastColumns);
	bool hasdiameterlist = false;
	for(std::vector<CBOQTable::ColumnType>::iterator it = columns.begin(); it != columns.end(); it++)
	{
		if((*it) == CBOQTable::TOTALLENGTH)
		{
			hasdiameterlist = true;
			break;
		}
	}

	// Create text styles
	AcDbObjectId lastTextStyleId = TextStyleId();
	AcDbObjectId lastHeadingStyleId = HeadingStyleId();
	AcDbObjectId lastFootingStyleId = FootingStyleId();

	// Set colors
	Adesk::UInt16 lastTextColor = TextColor();
	Adesk::UInt16 lastPosColor = PosColor();
	Adesk::UInt16 lastLineColor = LineColor();
	Adesk::UInt16 lastSeparatorColor = SeparatorColor();
	Adesk::UInt16 lastBorderColor = BorderColor();
	Adesk::UInt16 lastHeadingColor = HeadingColor();
	Adesk::UInt16 lastFootingColor = FootingColor();

	// Get texts
	ACHAR* lastHeading = NULL;
	ACHAR* lastFooting = NULL;
	ACHAR* lastNote = NULL;
	if(m_Heading != NULL && m_Heading[0] != _T('\0'))
		acutUpdString(m_Heading, lastHeading);
	if(m_Footing != NULL && m_Footing[0] != _T('\0'))
		acutUpdString(m_Footing, lastFooting);
	if(m_Note != NULL && m_Note[0] != _T('\0'))
		acutUpdString(m_Note, lastNote);

	// Get column and row labels
	ACHAR* lastPosLabel = NULL;
	ACHAR* lastCountLabel = NULL;
	ACHAR* lastDiameterLabel = NULL;
	ACHAR* lastLengthLabel = NULL;
	ACHAR* lastShapeLabel = NULL;
	ACHAR* lastTotalLengthLabel = NULL;
	ACHAR* lastDiameterListLabel = NULL;
	ACHAR* lastDiameterLengthLabel = NULL;
	ACHAR* lastUnitWeightLabel = NULL;
	ACHAR* lastWeightLabel = NULL;
	ACHAR* lastGrossWeightLabel = NULL;
	ACHAR* lastMultiplierHeadingLabel = NULL;

	acutUpdString(PosLabel(), lastPosLabel);
	acutUpdString(CountLabel(), lastCountLabel);
	acutUpdString(DiameterLabel(), lastDiameterLabel);
	acutUpdString(LengthLabel(), lastLengthLabel);
	acutUpdString(ShapeLabel(), lastShapeLabel);
	acutUpdString(TotalLengthLabel(), lastTotalLengthLabel);
	acutUpdString(DiameterListLabel(), lastDiameterListLabel);
	acutUpdString(DiameterLengthLabel(), lastDiameterLengthLabel);
	acutUpdString(UnitWeightLabel(), lastUnitWeightLabel);
	acutUpdString(WeightLabel(), lastWeightLabel);
	acutUpdString(GrossWeightLabel(), lastGrossWeightLabel);
	acutUpdString(MultiplierHeadingLabel(), lastMultiplierHeadingLabel);

	// Get other styles
	double lastHeadingScale = HeadingScale();
	double lastFootingScale = FootingScale();
	double lastRowSpacing = RowSpacing();

	Adesk::Int32 lastPrecision = Precision();
	CBOQTable::DrawingUnits lastDisplayUnit = DisplayUnit();

	// Create diameter list
	std::map<double, int> diameters;
	for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CBOQRow* row = *it;
		if(!row->isEmpty)
		{
			diameters[row->diameter] = 0;
		}
	}
	if(diameters.empty())
	{
		diameters[12] = 0;
	}
	// Set column position
	int diacol = 0;
	for(std::map<double, int>::iterator it = diameters.begin(); it != diameters.end(); it++)
	{
		it->second = diacol;
		diacol++;
	}

	// Heading/footing
	int headingLines = 0;
	if(lastHeading != NULL && lastHeading[0] != _T('\0'))
	{
		headingLines = 1;
	}
	int footingLines = 0;
	if(lastFooting != NULL && lastFooting[0] != _T('\0'))
	{
		footingLines += 1;
	}
	int noteLines = 0;
	if(lastNote != NULL && lastNote[0] != _T('\0'))
	{
		noteLines = 1;
	}

	// Create base table
	// + 1 for gross total heading (only if multiplier > 1)
	// + 2 for columns headers
	// + 4 for total row
	// + 2 for gross total row (only if multiplier > 1)
	int totalrows = hasdiameterlist ? (4 + (m_Multiplier > 1 ? 2 : 0)) : 0;
	int rows = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + totalrows + footingLines + noteLines; 
	int cols = (int)columns.size() + (hasdiameterlist ? (int)diameters.size() - 1 : 0);

	SetSize(rows, cols);
	setRowsToRepeat(headingLines + (m_Multiplier > 1 ? 1 : 0) + 2);
	setCellMargin(lastRowSpacing);

	// Set cell properties
	for(int i = headingLines + (m_Multiplier > 1 ? 1 : 0); i < headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + totalrows + noteLines; i++)
	{
		for(int j = 0; j < Columns(); j++)
		{
			setCellHorizontalAlignment(i, j, CTableCell::eCENTER);
			setCellVerticalAlignment(i, j, CTableCell::eCENTER);
			setCellTextHeight(i, j, 1.0);
			setCellLeftBorder(i, j, true, lastLineColor);
			setCellRightBorder(i, j, true, lastLineColor);
			setCellTopBorder(i, j, true, lastLineColor);
			setCellBottomBorder(i, j, true, lastLineColor);
		}
	}
	// Separator lines
	for(int i = 0; i < (int)m_List.size(); i++)
	{
		for(int j = 0; j < Columns(); j++)
		{
			int k = i + headingLines + (m_Multiplier > 1 ? 1 : 0) + 2;
			if(i > 0) setCellTopBorder(k, j, true, lastSeparatorColor);
			if(i < (int)m_List.size() - 1) setCellBottomBorder(k, j, true, lastSeparatorColor);
		}
	}
	// Double borders
	int bi = 0;
	bi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 - 1;
	if(bi >= 0 && bi < Rows()) setRowBottomBorder(bi, true, lastLineColor, true);
	bi++;
	if(bi >= 0 && bi < Rows()) setRowTopBorder(bi, true, lastLineColor, true);
	if(hasdiameterlist)
	{
		bi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() - 1;
		if(bi >= 0 && bi < Rows()) setRowBottomBorder(bi, true, lastLineColor, true);
		bi++;
		if(bi >= 0 && bi < Rows()) setRowTopBorder(bi, true, lastLineColor, true);
		if(m_Multiplier > 1)
		{
			bi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + 4 - 1;
			if(bi >= 0 && bi < Rows()) setRowBottomBorder(bi, true, lastLineColor, true);
			bi++;
			if(bi >= 0 && bi < Rows()) setRowTopBorder(bi, true, lastLineColor, true);
		}
	}
	if(noteLines > 0)
	{
		bi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + totalrows - 1;
		if(bi >= 0 && bi < Rows()) setRowBottomBorder(bi, true, lastLineColor, true);
		bi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + totalrows;
		if(bi >= 0 && bi < Rows()) setRowTopBorder(bi, true, lastLineColor, true);
	}

	// Set column headers
	int j = 0;
	for(std::vector<CBOQTable::ColumnType>::iterator tit = columns.begin(); tit != columns.end(); tit++)
	{
		int i = headingLines + (m_Multiplier > 1 ? 1 : 0);
		CBOQTable::ColumnType type = *tit;
		ACHAR* text = NULL;

		switch(type)
		{
		case CBOQTable::POS:
			text = lastPosLabel;
			MergeDown(i, j, 2);
			break;
		case CBOQTable::POSDD:
			text = lastPosLabel;
			MergeDown(i, j, 2);
			break;
		case CBOQTable::COUNT:
			text = lastCountLabel;
			MergeDown(i, j, 2);
			break;
		case CBOQTable::DIAMETER:
			text = lastDiameterLabel;
			MergeDown(i, j, 2);
			break;
		case CBOQTable::LENGTH:
			text = lastLengthLabel;
			MergeDown(i, j, 2);
			break;
		case CBOQTable::SHAPE:
			text = lastShapeLabel;
			MergeDown(i, j, 2);
			break;
		case CBOQTable::TOTALLENGTH:
			text = lastTotalLengthLabel;
			MergeAcross(i, j, (int)diameters.size());
			if(lastDiameterListLabel != NULL && lastDiameterListLabel[0] != _T('\0'))
			{
				for(std::map<double ,int>::iterator dit = diameters.begin(); dit != diameters.end(); dit++)
				{
					std::wstring dtext(lastDiameterListLabel);
					std::wstring numtext;
					Utility::IntToStr(Utility::DoubleToInt((*dit).first), numtext);
					Utility::ReplaceString(dtext, L"[D]", numtext);
					int k = j + (*dit).second;
					setCellText(i + 1, k, dtext.c_str());
					setCellTextColor(i + 1, k, lastPosColor);
					setCellTextStyleId(i + 1, k, lastTextStyleId);
				}
			}
			break;
		}

		if(text != NULL && text[0] != _T('\0'))
		{
			std::wstring ctext(text);
			std::wstring utext;
			switch(lastDisplayUnit)
			{
			case CBOQStyle::MM:
				utext = L"mm";
				break;
			case CBOQStyle::CM:
				utext = L"cm";
				break;
			case CBOQStyle::DM:
				utext = L"dm";
				break;
			case CBOQStyle::M:
				utext = L"m";
				break;
			}
			Utility::ReplaceString(ctext, L"[U]", utext);
			setCellText(i, j, ctext.c_str());
		}
		setCellTextColor(i, j, lastPosColor);
		setCellTextStyleId(i, j, lastTextStyleId);

		j++;
	}

	// Set rows
	j = 0;
	for(std::vector<CBOQTable::ColumnType>::iterator tit = columns.begin(); tit != columns.end(); tit++)
	{
		int i = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2;
		CBOQTable::ColumnType type = *tit;

		for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
		{
			CBOQRow* row = *it;

			unsigned short textCol = lastTextColor;
			if(row->count == 0) textCol = 7;

			if(!row->isEmpty && type == CBOQTable::SHAPE)
			{
				setCellShape(i, j, row->shape.c_str());
				setCellTextStyleId(i, j, lastTextStyleId);
				setCellShapeText(i, j, row->a.c_str(), row->b.c_str(), row->c.c_str(), row->d.c_str(), row->e.c_str(), row->f.c_str());
			}
			else if(!row->isEmpty)
			{
				std::wstring text;
				double len;
				int doff = 0;

				switch(type)
				{
				case CBOQTable::POS:
					Utility::IntToStr(row->pos, text);
					break;
				case CBOQTable::POSDD:
					Utility::IntToStr(row->pos, text);
					if(text.length() == 1) text.insert(0, L"0");
					break;
				case CBOQTable::COUNT:
					Utility::IntToStr(row->count, text);
					break;
				case CBOQTable::DIAMETER:
					Utility::DoubleToStr(row->diameter, text);
					break;
				case CBOQTable::LENGTH:
					len = (row->length1 + row->length2) / 2.0;
					if(lastDisplayUnit == CBOQStyle::CM) len /= 10.0;
					Utility::DoubleToStr(len, lastPrecision, text);
					break;
				case CBOQTable::TOTALLENGTH:
					Utility::DoubleToStr((double)row->count * (row->length1 + row->length2) / 2.0 / 1000.0, lastPrecision, text);
					doff = diameters[row->diameter];
					break;
				}

				setCellTextColor(i, j + doff, textCol);
				setCellTextStyleId(i, j + doff, lastTextStyleId);
				setCellText(i, j + doff, text.c_str());
			}
			else if(type == CBOQTable::POS)
			{
				std::wstring text;
				Utility::IntToStr(row->pos, text);
				setCellTextColor(i, j, textCol);
				setCellTextStyleId(i, j, lastTextStyleId);
				setCellText(i, j, text.c_str());
			}

			i++;
		}
		j++;
	}

	// Set heading
	if(lastHeading != NULL && lastHeading[0] != _T('\0'))
	{
		int hi = 0;
		setCellTextColor(hi, 0, lastHeadingColor);
		setCellTextStyleId(hi, 0, lastHeadingStyleId);
		setCellHorizontalAlignment(hi, 0, CTableCell::eCENTER);
		setCellVerticalAlignment(hi, 0, CTableCell::eCENTER);
		setCellTextHeight(hi, 0, lastHeadingScale);
		MergeAcross(hi, 0, cols);

		setCellText(hi, 0, lastHeading);
	}

	// Set multiplier heading
	if(m_Multiplier > 1 && lastMultiplierHeadingLabel != NULL && lastMultiplierHeadingLabel[0] != _T('\0'))
	{
		int hi = headingLines;
		setCellTextColor(hi, 0, lastTextColor);
		setCellTextStyleId(hi, 0, lastTextStyleId);

		setCellHorizontalAlignment(hi, 0, CTableCell::eNEAR);
		setCellVerticalAlignment(hi, 0, CTableCell::eCENTER);
		setCellTextHeight(hi, 0, 1.0);
		MergeAcross(hi, 0, cols);

		std::wstring text(lastMultiplierHeadingLabel);
		std::wstring ntext;
		Utility::IntToStr(m_Multiplier, ntext);
		Utility::ReplaceString(text, L"[N]", ntext);

		setCellText(hi, 0, text.c_str());
	}

	// Set total rows
	if(hasdiameterlist)
	{
		std::map<double, double> totallengths;
		for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
		{
			CBOQRow* row = *it;
			totallengths[row->diameter] += (double)row->count * (row->length1 + row->length2) / 2.0 / 1000.0;
		}
		std::map<double, double> unitweights;
		double pi = atan(1.0) * 4.0;
		for(std::map<double, int>::iterator it = diameters.begin(); it != diameters.end(); it++)
		{
			double d = (*it).first;
			unitweights[d] = pi * d * d / 4 * 1000.0 * 7850.0 / 1000.0 / 1000.0 / 1000.0;
		}

		int ti = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size();
		int mulcol = cols - (int)diameters.size() - 1;
		for(int k = ti; k < ti + totalrows; k++)
		{
			setCellTextColor(k, 0, lastPosColor);
			setCellTextStyleId(k, 0, lastTextStyleId);
			setCellHorizontalAlignment(k, 0, CTableCell::eNEAR);
			setCellVerticalAlignment(k, 0, CTableCell::eCENTER);
			MergeAcross(k, 0, cols - (int)diameters.size() - 1);

			setCellTextColor(k, mulcol, lastPosColor);
			setCellTextStyleId(k, mulcol, lastTextStyleId);
			setCellHorizontalAlignment(k, mulcol, CTableCell::eFAR);
			setCellVerticalAlignment(k, mulcol, CTableCell::eCENTER);

			setCellRightBorder(k, mulcol - 1, false);
			setCellLeftBorder(k, mulcol, false);
		}

		if(lastDiameterLengthLabel != NULL && lastDiameterLengthLabel[0] != _T('\0'))
			setCellText(ti, 0, lastDiameterLengthLabel);
		if(lastUnitWeightLabel != NULL && lastUnitWeightLabel[0] != _T('\0'))
			setCellText(ti + 1, 0, lastUnitWeightLabel);
		if(lastWeightLabel != NULL && lastWeightLabel[0] != _T('\0'))
			setCellText(ti + 2, 0, lastWeightLabel);
		if(lastGrossWeightLabel != NULL && lastGrossWeightLabel[0] != _T('\0'))
			setCellText(ti + 3, 0, lastGrossWeightLabel);
		MergeAcross(ti + 3, cols - (int)diameters.size(), (int)diameters.size());

		for(int ki = ti; ki < ti + totalrows; ki++)
		{
			for(std::map<double, int>::iterator it = diameters.begin(); it != diameters.end(); it++)
			{
				int kj = cols - (int)diameters.size() + (*it).second;
				setCellTextColor(ki, kj, lastTextColor);
				setCellTextStyleId(ki, kj, lastTextStyleId);
				setCellHorizontalAlignment(ki, kj, CTableCell::eCENTER);
				setCellVerticalAlignment(ki, kj, CTableCell::eCENTER);
			}
		}

		double grosstotal = 0;
		for(std::map<double, int>::iterator it = diameters.begin(); it != diameters.end(); it++)
		{
			int k = cols - (int)diameters.size() + (*it).second;
			std::wstring text;
			// Total lengths
			double len = totallengths[(*it).first];
			Utility::DoubleToStr(len, lastPrecision, text);
			setCellText(ti, k, text.c_str());
			// Unit weights
			double uw = unitweights[(*it).first];
			Utility::DoubleToStr(uw, 3, text);
			setCellText(ti + 1, k, text.c_str());
			// Weights
			double w = len * uw;
			Utility::DoubleToStr(w, lastPrecision, text);
			setCellText(ti + 2, k, text.c_str());
			grosstotal += w;
		}
		std::wstring gtext;
		Utility::DoubleToStr(grosstotal, lastPrecision, gtext);
		setCellText(ti + 3, cols - (int)diameters.size(), gtext.c_str());

		if(m_Multiplier > 1)
		{
			if(lastWeightLabel != NULL && lastWeightLabel[0] != _T('\0'))
				setCellText(ti + 4, 0, lastWeightLabel);
			if(lastGrossWeightLabel != NULL && lastGrossWeightLabel[0] != _T('\0'))
				setCellText(ti + 5, 0, lastGrossWeightLabel);
			std::wstring mtext(L"x1");
			setCellText(ti + 2, mulcol, mtext.c_str());
			setCellText(ti + 3, mulcol, mtext.c_str());
			mtext.clear();
			Utility::IntToStr(m_Multiplier, mtext);
			mtext.insert(0, L"x");
			setCellText(ti + 4, mulcol, mtext.c_str());
			setCellText(ti + 5, mulcol, mtext.c_str());
			MergeAcross(ti + 5, cols - (int)diameters.size(), (int)diameters.size());

			double grossgrosstotal = 0;
			for(std::map<double, int>::iterator it = diameters.begin(); it != diameters.end(); it++)
			{
				int k = cols - (int)diameters.size() + (*it).second;
				std::wstring text;
				// Gross total weights
				double w = (double)m_Multiplier * totallengths[(*it).first] * unitweights[(*it).first];
				Utility::DoubleToStr(w, lastPrecision, text);
				setCellText(ti + 4, k, text.c_str());
				grossgrosstotal += w;
			}
			std::wstring gtext;
			Utility::DoubleToStr(grossgrosstotal, lastPrecision, gtext);
			setCellText(ti + 5, cols - (int)diameters.size(), gtext.c_str());
		}
	}

	// Set note
	if(lastNote != NULL && lastNote[0] != _T('\0'))
	{
		int fi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + totalrows;
		setCellTextColor(fi, 0, lastTextColor);
		setCellTextStyleId(fi, 0, lastTextStyleId);
		setCellHorizontalAlignment(fi, 0, CTableCell::eNEAR);
		setCellVerticalAlignment(fi, 0, CTableCell::eCENTER);
		setCellTextHeight(fi, 0, 1.0);
		MergeAcross(fi, 0, cols);

		setCellText(fi, 0, lastNote);
	}

	// Set footing
	if(lastFooting != NULL && lastFooting[0] != _T('\0'))
	{
		int fi = headingLines + (m_Multiplier > 1 ? 1 : 0) + 2 + (int)m_List.size() + totalrows + noteLines;
		setCellTextColor(fi, 0, lastFootingColor);
		setCellTextStyleId(fi, 0, lastFootingStyleId);
		setCellHorizontalAlignment(fi, 0, CTableCell::eNEAR);
		setCellVerticalAlignment(fi, 0, CTableCell::eCENTER);
		setCellTextHeight(fi, 0, lastFootingScale);
		MergeAcross(fi, 0, cols);

		setCellText(fi, 0, lastFooting);
	}

	acutDelString(lastColumns);
	acutDelString(lastHeading);
	acutDelString(lastFooting);

	acutDelString(lastPosLabel);
	acutDelString(lastCountLabel);
	acutDelString(lastDiameterLabel);
	acutDelString(lastLengthLabel);
	acutDelString(lastShapeLabel);
	acutDelString(lastTotalLengthLabel);
	acutDelString(lastDiameterListLabel);
	acutDelString(lastDiameterLengthLabel);
	acutDelString(lastUnitWeightLabel);
	acutDelString(lastWeightLabel);
	acutDelString(lastGrossWeightLabel);
}

const void CBOQTable::Update(void)
{
	assertWriteEnabled();

	UpdateTable();
}

//*************************************************************************
// Overridden methods from AcDbEntity
//*************************************************************************
Acad::ErrorStatus CBOQTable::subGetOsnapPoints(
    AcDb::OsnapMode       osnapMode,
    Adesk::GsMarker       gsSelectionMark,
    const AcGePoint3d&    pickPoint,
    const AcGePoint3d&    lastPoint,
    const AcGeMatrix3d&   viewXform,
    AcGePoint3dArray&     snapPoints,
    AcDbIntArray&         geomIds) const
{
	assertReadEnabled();

	return CGenericTable::subGetOsnapPoints(osnapMode, gsSelectionMark, pickPoint, lastPoint, viewXform, snapPoints, geomIds);
}

Acad::ErrorStatus CBOQTable::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
	assertReadEnabled();

	return CGenericTable::subGetGripPoints(gripPoints, osnapModes, geomIds);
}

Acad::ErrorStatus CBOQTable::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
	assertWriteEnabled();

	return CGenericTable::subMoveGripPointsAt(indices, offset);
}

Acad::ErrorStatus CBOQTable::subTransformBy(const AcGeMatrix3d& xform)
{
	assertWriteEnabled();
	
	return CGenericTable::subTransformBy(xform);
}

void CBOQTable::subList() const
{
    assertReadEnabled();

	// Call parent first
    CGenericTable::subList();

	// Base point in UCS
    acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Base Point:"));
    AcGePoint3d pt(BasePoint());
    acdbWcs2Ucs(asDblArray(pt), asDblArray(pt), false);
    acutPrintf(_T("X = %-9.16q0, Y = %-9.16q0, Z = %-9.16q0\n"), pt.x, pt.y, pt.z);

	// List all properties
	// TODO
}

Acad::ErrorStatus CBOQTable::subExplode(AcDbVoidPtrArray& entitySet) const
{
    assertReadEnabled();

	return CGenericTable::subExplode(entitySet);
}

Adesk::Boolean CBOQTable::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();

	return CGenericTable::subWorldDraw(worldDraw);
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CBOQTable::dwgOutFields(AcDbDwgFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = CGenericTable::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CBOQTable::kCurrentVersionNumber);

	pFiler->writeInt32(m_Multiplier);

	// Texts
	if (m_Heading)
		pFiler->writeString(m_Heading);
	else
		pFiler->writeString(_T(""));
	if (m_Footing)
		pFiler->writeString(m_Footing);
	else
		pFiler->writeString(_T(""));

	// Properties
	pFiler->writeInt32(m_Precision);
	pFiler->writeInt32(m_DisplayUnit);

	// Formula
	if (m_Columns)
		pFiler->writeString(m_Columns);
	else
		pFiler->writeString(_T(""));

    // Colors
    pFiler->writeUInt16(m_TextColor);
    pFiler->writeUInt16(m_PosColor);
	pFiler->writeUInt16(m_LineColor);
	pFiler->writeUInt16(m_SeparatorColor);
    pFiler->writeUInt16(m_BorderColor);
    pFiler->writeUInt16(m_HeadingColor);
    pFiler->writeUInt16(m_FootingColor);

	// Labels
	if (m_PosLabel) pFiler->writeString(m_PosLabel); else pFiler->writeString(_T(""));
	if (m_CountLabel) pFiler->writeString(m_CountLabel); else pFiler->writeString(_T(""));
	if (m_DiameterLabel) pFiler->writeString(m_DiameterLabel); else pFiler->writeString(_T(""));
	if (m_LengthLabel) pFiler->writeString(m_LengthLabel); else pFiler->writeString(_T(""));
	if (m_ShapeLabel) pFiler->writeString(m_ShapeLabel); else pFiler->writeString(_T(""));
	if (m_TotalLengthLabel) pFiler->writeString(m_TotalLengthLabel); else pFiler->writeString(_T(""));
	if (m_DiameterListLabel) pFiler->writeString(m_DiameterListLabel); else pFiler->writeString(_T(""));
	if (m_DiameterLengthLabel) pFiler->writeString(m_DiameterLengthLabel); else pFiler->writeString(_T(""));
	if (m_UnitWeightLabel) pFiler->writeString(m_UnitWeightLabel); else pFiler->writeString(_T(""));
	if (m_WeightLabel) pFiler->writeString(m_WeightLabel); else pFiler->writeString(_T(""));
	if (m_GrossWeightLabel) pFiler->writeString(m_GrossWeightLabel); else pFiler->writeString(_T(""));
	if (m_MultiplierHeadingLabel) pFiler->writeString(m_MultiplierHeadingLabel); else pFiler->writeString(_T(""));

    // Scales
    pFiler->writeDouble(m_HeadingScale);
	pFiler->writeDouble(m_FootingScale);
    pFiler->writeDouble(m_RowSpacing);

    // Styles
    pFiler->writeHardPointerId(m_TextStyleId);
    pFiler->writeHardPointerId(m_HeadingStyleId);
	pFiler->writeHardPointerId(m_FootingStyleId);

	// Rows
	pFiler->writeInt32((int)m_List.size());
	for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CBOQRow* row = *it;
		pFiler->writeInt32(row->pos);
		pFiler->writeInt32(row->count);
		pFiler->writeDouble(row->diameter);
		pFiler->writeDouble(row->length1);
		pFiler->writeDouble(row->length2);
		pFiler->writeBoolean(row->isVarLength);
		pFiler->writeBoolean(row->isEmpty);
		pFiler->writeString(row->shape.c_str());
		pFiler->writeString(row->a.c_str());
		pFiler->writeString(row->b.c_str());
		pFiler->writeString(row->c.c_str());
		pFiler->writeString(row->d.c_str());
		pFiler->writeString(row->e.c_str());
		pFiler->writeString(row->f.c_str());
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dwgInFields(AcDbDwgFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = CGenericTable::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CBOQTable::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	acutDelString(m_Heading);
	acutDelString(m_Footing);
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

	pFiler->readInt32(&m_Multiplier);

	pFiler->readString(&m_Heading);
	pFiler->readString(&m_Footing);

	// Properties
	pFiler->readInt32(&m_Precision);
	Adesk::Int32 displayunit = 0;
	pFiler->readInt32(&displayunit);
	m_DisplayUnit = (DrawingUnits)displayunit;

	pFiler->readString(&m_Columns);

	// Columns
    pFiler->readUInt16(&m_TextColor);
    pFiler->readUInt16(&m_PosColor);
    pFiler->readUInt16(&m_LineColor);
	pFiler->readUInt16(&m_SeparatorColor);
    pFiler->readUInt16(&m_BorderColor);
    pFiler->readUInt16(&m_HeadingColor);
    pFiler->readUInt16(&m_FootingColor);

	// Labels
	pFiler->readString(&m_PosLabel);
	pFiler->readString(&m_CountLabel);
	pFiler->readString(&m_DiameterLabel);
	pFiler->readString(&m_LengthLabel);
	pFiler->readString(&m_ShapeLabel);
	pFiler->readString(&m_TotalLengthLabel);
	pFiler->readString(&m_DiameterListLabel);
	pFiler->readString(&m_DiameterLengthLabel);
	pFiler->readString(&m_UnitWeightLabel);
	pFiler->readString(&m_WeightLabel);
	pFiler->readString(&m_GrossWeightLabel);
	pFiler->readString(&m_MultiplierHeadingLabel);

    pFiler->readDouble(&m_HeadingScale);
	pFiler->readDouble(&m_FootingScale);
    pFiler->readDouble(&m_RowSpacing);

	pFiler->readHardPointerId(&m_TextStyleId);
	pFiler->readHardPointerId(&m_HeadingStyleId);
	pFiler->readHardPointerId(&m_FootingStyleId);

	// Rows
	ClearRows();
	long count = 0;
	pFiler->readInt32(&count);
	for(long i = 0; i < count; i++)
	{
		CBOQRow* row = new CBOQRow();
		pFiler->readInt32(&row->pos);
		pFiler->readInt32(&row->count);
		pFiler->readDouble(&row->diameter);
		pFiler->readDouble(&row->length1);
		pFiler->readDouble(&row->length2);
		pFiler->readBoolean(&row->isVarLength);
		pFiler->readBoolean(&row->isEmpty);
		ACHAR* sh = NULL;
		ACHAR* a = NULL;
		ACHAR* b = NULL;
		ACHAR* c = NULL;
		ACHAR* d = NULL;
		ACHAR* e = NULL;
		ACHAR* f = NULL;
		pFiler->readString(&sh);
		pFiler->readString(&a);
		pFiler->readString(&b);
		pFiler->readString(&c);
		pFiler->readString(&d);
		pFiler->readString(&e);
		pFiler->readString(&f);
		row->shape.assign(sh);
		row->a.assign(a);
		row->b.assign(b);
		row->c.assign(c);
		row->d.assign(d);
		row->e.assign(e);
		row->f.assign(f);
		acutDelString(sh);
		acutDelString(a);
		acutDelString(b);
		acutDelString(c);
		acutDelString(d);
		acutDelString(e);
		acutDelString(f);
		m_List.push_back(row);
	}

	UpdateTable();

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dxfOutFields(AcDbDxfFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = CGenericTable::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("BOQTable"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CBOQTable::kCurrentVersionNumber);

	// Properties
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_Multiplier);
	
	// Texts
	if (m_Heading)
		pFiler->writeString(AcDb::kDxfXTextString, m_Heading);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_Footing)
		pFiler->writeString(AcDb::kDxfXTextString + 1, m_Footing);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 1, _T(""));

	// Properties
	pFiler->writeInt32(AcDb::kDxfInt32, m_Precision);
	pFiler->writeInt32(AcDb::kDxfInt32, m_DisplayUnit);

	if(m_Columns)
		pFiler->writeString(AcDb::kDxfXTextString, m_Columns);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));

    // Colors
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_TextColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_PosColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_LineColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_SeparatorColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_BorderColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_HeadingColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_FootingColor);

	// Labels
	if (m_PosLabel) pFiler->writeString(AcDb::kDxfXTextString, m_PosLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_CountLabel) pFiler->writeString(AcDb::kDxfXTextString, m_CountLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_DiameterLabel) pFiler->writeString(AcDb::kDxfXTextString, m_DiameterLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_LengthLabel) pFiler->writeString(AcDb::kDxfXTextString, m_LengthLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_ShapeLabel) pFiler->writeString(AcDb::kDxfXTextString, m_ShapeLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_TotalLengthLabel) pFiler->writeString(AcDb::kDxfXTextString, m_TotalLengthLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_DiameterListLabel) pFiler->writeString(AcDb::kDxfXTextString, m_DiameterListLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_DiameterLengthLabel) pFiler->writeString(AcDb::kDxfXTextString, m_DiameterLengthLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_UnitWeightLabel) pFiler->writeString(AcDb::kDxfXTextString, m_UnitWeightLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_WeightLabel) pFiler->writeString(AcDb::kDxfXTextString, m_WeightLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_GrossWeightLabel) pFiler->writeString(AcDb::kDxfXTextString, m_GrossWeightLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_MultiplierHeadingLabel) pFiler->writeString(AcDb::kDxfXTextString, m_MultiplierHeadingLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));

    // Scale
    pFiler->writeDouble(AcDb::kDxfXReal, m_HeadingScale);
	pFiler->writeDouble(AcDb::kDxfXReal, m_FootingScale);
    pFiler->writeDouble(AcDb::kDxfXReal, m_RowSpacing);

    // Styles
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_TextStyleId);
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_HeadingStyleId);
	pFiler->writeItem(AcDb::kDxfHardPointerId, m_FootingStyleId);

	// Rows
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, (int)m_List.size());
	for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CBOQRow* row = *it;
		pFiler->writeInt32(AcDb::kDxfInt32, row->pos);
		pFiler->writeInt32(AcDb::kDxfInt32, row->count);
		pFiler->writeDouble(AcDb::kDxfReal, row->diameter);
		pFiler->writeDouble(AcDb::kDxfReal, row->length1);
		pFiler->writeDouble(AcDb::kDxfReal, row->length2);
		pFiler->writeBoolean(AcDb::kDxfBool, row->isVarLength);
		pFiler->writeBoolean(AcDb::kDxfBool, row->isEmpty);
		pFiler->writeString(AcDb::kDxfText, row->shape.c_str());
		pFiler->writeString(AcDb::kDxfText, row->a.c_str());
		pFiler->writeString(AcDb::kDxfText, row->b.c_str());
		pFiler->writeString(AcDb::kDxfText, row->c.c_str());
		pFiler->writeString(AcDb::kDxfText, row->d.c_str());
		pFiler->writeString(AcDb::kDxfText, row->e.c_str());
		pFiler->writeString(AcDb::kDxfText, row->f.c_str());
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dxfInFields(AcDbDxfFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = CGenericTable::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("BOQTable")))
		return es;

	resbuf rb;
	// Object version number
    Adesk::UInt32 version;
    pFiler->readItem(&rb);
    if (rb.restype != AcDb::kDxfInt32) 
    {
        pFiler->pushBackItem();
        pFiler->setError(Acad::eInvalidDxfCode, _T("\nError: expected group code %d (version)"), AcDb::kDxfInt32);
        return pFiler->filerStatus();
    }
    version = rb.resval.rlong;
	if (version > CBOQTable::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	Adesk::Int32 t_Multiplier = 0;
	ACHAR* t_Heading = NULL;
	ACHAR* t_Footing = NULL;
	RowList t_List;
	long count;
	int t_Precision = 0;
	int t_DisplayUnit = 0;
	ACHAR* t_Columns = NULL;
	Adesk::UInt16 t_TextColor = 0;
	Adesk::UInt16 t_PosColor = 0;
	Adesk::UInt16 t_LineColor = 0;
	Adesk::UInt16 t_SeparatorColor = 0;
	Adesk::UInt16 t_BorderColor = 0;
	Adesk::UInt16 t_HeadingColor = 0;
	Adesk::UInt16 t_FootingColor = 0;
	ACHAR* t_PosLabel = NULL;
	ACHAR* t_CountLabel = NULL;
	ACHAR* t_DiameterLabel = NULL;
	ACHAR* t_LengthLabel = NULL;
	ACHAR* t_ShapeLabel = NULL;
	ACHAR* t_TotalLengthLabel = NULL;
	ACHAR* t_DiameterListLabel = NULL;
	ACHAR* t_DiameterLengthLabel = NULL;
	ACHAR* t_UnitWeightLabel = NULL;
	ACHAR* t_WeightLabel = NULL;
	ACHAR* t_GrossWeightLabel = NULL;
	ACHAR* t_MultiplierHeadingLabel = NULL;
	double t_HeadingScale = 0;
	double t_FootingScale = 0;
	double t_RowSpacing = 0;
	AcDbObjectId t_TextStyleId = AcDbObjectId::kNull;
	AcDbObjectId t_HeadingStyleId = AcDbObjectId::kNull;
	AcDbObjectId t_FootingStyleId = AcDbObjectId::kNull;

	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32 + 1, _T("multiplier"), t_Multiplier)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("heading"), t_Heading)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString + 1, _T("footing"), t_Footing)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("prevision"), t_Precision)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("display unit"), t_DisplayUnit)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("columns"), t_Columns)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("text color"), t_TextColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("pos color"), t_PosColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("line color"), t_LineColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("separator color"), t_SeparatorColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("border color"), t_BorderColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("heading color"), t_HeadingColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("footing color"), t_FootingColor)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("pos label"), t_PosLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("count label"), t_CountLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("diameter label"), t_DiameterLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("length label"), t_LengthLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("shape label"), t_ShapeLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("total length label"), t_TotalLengthLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("diameter list label"), t_DiameterListLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("diameter length label"), t_DiameterLengthLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("unit weight label"), t_UnitWeightLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("weight label"), t_WeightLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("gross weight label"), t_GrossWeightLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("multiplier heading label"), t_MultiplierHeadingLabel)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("heading scale"), t_HeadingScale)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("footing scale"), t_FootingScale)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("row spacing"), t_RowSpacing)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("text style"), t_TextStyleId)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("heading style"), t_HeadingStyleId)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("footing style"), t_FootingStyleId)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32 + 2, _T("row count"), count)) != Acad::eOk) return es;

	for(long i = 0; i < count; i++)
    {
		CBOQRow* row = new CBOQRow();
		if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("row pos"), row->pos)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("row count"), row->count)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("row diameter"), row->diameter)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("row length1"), row->length1)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("row length2"), row->length2)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("row var length"), row->isVarLength)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("row is empty"), row->isEmpty)) != Acad::eOk) return es;
		ACHAR* sh = NULL;
		ACHAR* a = NULL;
		ACHAR* b = NULL;
		ACHAR* c = NULL;
		ACHAR* d = NULL;
		ACHAR* e = NULL;
		ACHAR* f = NULL;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape"), sh)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape length a"), a)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape length b"), b)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape length c"), c)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape length d"), d)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape length e"), e)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("row shape length f"), f)) != Acad::eOk) return es;
		row->shape.assign(sh);
		row->a.assign(a);
		row->b.assign(b);
		row->c.assign(c);
		row->d.assign(d);
		row->e.assign(e);
		row->f.assign(f);
		acutDelString(sh);
		acutDelString(a);
		acutDelString(b);
		acutDelString(c);
		acutDelString(d);
		acutDelString(e);
		acutDelString(f);
		t_List.push_back(row);
    }

	// Successfully read DXF codes; set object properties.
	m_Multiplier = t_Multiplier;

	setHeading(t_Heading);
	setFooting(t_Footing);
	m_Precision = t_Precision;
    m_DisplayUnit = (DrawingUnits)t_DisplayUnit;
	setColumnDef(t_Columns);

	m_TextColor = t_TextColor;
	m_PosColor = t_PosColor;
	m_LineColor = t_LineColor;
	m_SeparatorColor = t_SeparatorColor;
	m_BorderColor = t_BorderColor;
	m_HeadingColor = t_HeadingColor;
	m_FootingColor = t_FootingColor;

	m_HeadingScale = t_HeadingScale;
	m_FootingScale = t_FootingScale;
	m_RowSpacing = t_RowSpacing;

	setPosLabel(t_PosLabel);
	setCountLabel(t_CountLabel);
	setDiameterLabel(t_DiameterLabel);
	setLengthLabel(t_LengthLabel);
	setShapeLabel(t_ShapeLabel);
	setTotalLengthLabel(t_TotalLengthLabel);
	setDiameterListLabel(t_DiameterListLabel);
	setDiameterLengthLabel(t_DiameterLengthLabel);
	setUnitWeightLabel(t_UnitWeightLabel);
	setWeightLabel(t_WeightLabel);
	setGrossWeightLabel(t_GrossWeightLabel);
	setMultiplierHeadingLabel(t_MultiplierHeadingLabel);

	m_TextStyleId = t_TextStyleId;
	m_HeadingStyleId = t_HeadingStyleId;
	m_FootingStyleId = t_FootingStyleId;

	ClearRows();
	m_List.clear();
	m_List = t_List;

	acutDelString(t_Heading);
	acutDelString(t_Footing);

	acutDelString(t_Columns);

	acutDelString(t_PosLabel);
	acutDelString(t_CountLabel);
	acutDelString(t_DiameterLabel);
	acutDelString(t_LengthLabel);
	acutDelString(t_ShapeLabel);
	acutDelString(t_TotalLengthLabel);
	acutDelString(t_DiameterListLabel);
	acutDelString(t_DiameterLengthLabel);
	acutDelString(t_UnitWeightLabel);
	acutDelString(t_WeightLabel);
	acutDelString(t_GrossWeightLabel);
	acutDelString(t_MultiplierHeadingLabel);

	UpdateTable();

    return es;
}

Acad::ErrorStatus CBOQTable::subDeepClone(AcDbObject*    pOwner,
                    AcDbObject*&   pClonedObject,
                    AcDbIdMapping& idMap,
                    Adesk::Boolean isPrimary) const
{
    // You should always pass back pClonedObject == NULL
    // if, for any reason, you do not actually clone it
    // during this call.  The caller should pass it in
    // as NULL, but to be safe, we set it here as well.
    //
    pClonedObject = NULL;

    // If this object is in the idMap and is already
    // cloned, then return.
    //
    bool isPrim = false;
    if (isPrimary)
        isPrim = true;
    AcDbIdPair idPair(objectId(), (AcDbObjectId)NULL, false, isPrim);
    if (idMap.compute(idPair) && (idPair.value() != NULL))
        return Acad::eOk;    

    // Create the clone
    //
    CBOQTable *pClone = (CBOQTable*)isA()->create();
    if (pClone != NULL)
        pClonedObject = pClone;    // set the return value
    else
        return Acad::eOutOfMemory;

    AcDbDeepCloneFiler filer;
    dwgOut(&filer);

    filer.seek(0L, AcDb::kSeekFromStart);
    pClone->dwgIn(&filer);
    bool bOwnerXlated = false;
    if (isPrimary)
    {
        AcDbBlockTableRecord *pBTR = AcDbBlockTableRecord::cast(pOwner);
        if (pBTR != NULL)
        {
            pBTR->appendAcDbEntity(pClone);
            bOwnerXlated = true;
        }
        else
        {
            pOwner->database()->addAcDbObject(pClone);
        }
    } 
	else 
	{
        pOwner->database()->addAcDbObject(pClone);
        pClone->setOwnerId(pOwner->objectId());
        bOwnerXlated = true;
    }

    // This must be called for all newly created objects
    // in deepClone.  It is turned off by endDeepClone()
    // after it has translated the references to their
    // new values.
    //
    pClone->setAcDbObjectIdsInFlux();
    pClone->disableUndoRecording(true);


    // Add the new information to the idMap.  We can use
    // the idPair started above.
    //
    idPair.setValue(pClonedObject->objectId());
    idPair.setIsCloned(Adesk::kTrue);
    idPair.setIsOwnerXlated(bOwnerXlated);
    idMap.assign(idPair);

    // Using the filer list created above, find and clone
    // any owned objects.
    //
    AcDbObjectId id;
    while (filer.getNextOwnedObject(id)) 
	{
        AcDbObject *pSubObject;
        AcDbObject *pClonedSubObject;

        // Some object's references may be set to NULL, 
        // so don't try to clone them.
        //
        if (id == NULL)
            continue;

        // Open the object and clone it.  Note that we now
        // set "isPrimary" to kFalse here because the object
        // is being cloned, not as part of the primary set,
        // but because it is owned by something in the
        // primary set.
        //
        acdbOpenAcDbObject(pSubObject, id, AcDb::kForRead);
        pClonedSubObject = NULL;
        pSubObject->deepClone(pClonedObject, pClonedSubObject, idMap, Adesk::kFalse);

        // If this is a kDcInsert context, the objects
        // may be "cheapCloned".  In this case, they are
        // "moved" instead of cloned.  The result is that
        // pSubObject and pClonedSubObject will point to
        // the same object.  So, we only want to close
        // pSubObject if it really is a different object
        // than its clone.
        //
        if (pSubObject != pClonedSubObject)
            pSubObject->close();
        
        // The pSubObject may either already have been
        // cloned, or for some reason has chosen not to be
        // cloned.  In that case, the returned pointer will
        // be NULL.  Otherwise, since we have no immediate
        // use for it now, we can close the clone.
        //
        if (pClonedSubObject != NULL)
            pClonedSubObject->close();
    }

    // Leave pClonedObject open for the caller
    //
    return Acad::eOk;
}


Acad::ErrorStatus CBOQTable::subWblockClone(AcRxObject*    pOwner,
                      AcDbObject*&   pClonedObject,
                      AcDbIdMapping& idMap,
                      Adesk::Boolean isPrimary) const
{
    // You should always pass back pClonedObject == NULL
    // if, for any reason, you do not actually clone it
    // during this call.  The caller should pass it in
    // as NULL, but to be safe, we set it here as well.
    //
    pClonedObject = NULL;

    // If this is a fast wblock operation then no cloning
    // should take place, so we simply call the base class's
    // wblockClone() and return whatever it returns.
    //
    // For fast wblock, the source and destination databases
    // are the same, so we can use that as the test to see
    // if a fast wblock is in progress.
    //
    AcDbDatabase *pDest, *pOrig;
    idMap.destDb(pDest);
    idMap.origDb(pOrig);
    if (pDest == pOrig)
        return CGenericTable::subWblockClone(pOwner, pClonedObject, idMap, isPrimary);

    // If this is an Xref bind operation and this 
    // entity is in Paper Space,  then we don't want to
    // clone because Xref bind doesn't support cloning
    // entities in Paper Space.  So we simply return
    // Acad::eOk
    //
    AcDbObjectId pspace;
    AcDbBlockTable *pTable;
    database()->getSymbolTable(pTable, AcDb::kForRead);
    pTable->getAt(ACDB_PAPER_SPACE, pspace);
    pTable->close(); 

    if (idMap.deepCloneContext() == AcDb::kDcXrefBind && ownerId() == pspace)
        return Acad::eOk;
    
    // If this object is in the idMap and is already
    // cloned, then return.
    //
    bool isPrim = false;
    if (isPrimary)
        isPrim = true;

    AcDbIdPair idPair(objectId(), (AcDbObjectId)NULL, false, isPrim);
    if (idMap.compute(idPair) && (idPair.value() != NULL))
        return Acad::eOk;    

    // The owner object can be either an AcDbObject, or an
    // AcDbDatabase.  AcDbDatabase is used if the caller is
    // not the owner of the object being cloned (because it
    // is being cloned as part of an AcDbHardPointerId
    // reference).  In this case, the correct ownership
    // will be set during reference translation.  So, if
    // the owner is an AcDbDatabase, then pOwn will be left
    // NULL here, and is used as a "flag" later.
    //

    AcDbObject   *pOwn = AcDbObject::cast(pOwner);
    AcDbDatabase *pDb = AcDbDatabase::cast(pOwner);
    if (pDb == NULL) 
        pDb = pOwn->database();

    // STEP 1:
    // Create the clone
    //
    CBOQTable *pClone = (CBOQTable*)isA()->create();
    if (pClone != NULL)
        pClonedObject = pClone;    // set the return value
    else
        return Acad::eOutOfMemory;

    // STEP 2:
    // If the owner is an AcDbBlockTableRecord, go ahead
    // and append the clone.  If not, but we know who the
    // owner is, set the clone's ownerId to it.  Otherwise,
    // we set the clone's ownerId to our own ownerId (in
    // other words, the original owner Id).  This Id will
    // then be used later, in reference translation, as
    // a key to finding who the new owner should be.  This
    // means that the original owner must also be cloned at
    // some point during the wblock operation. 
    // EndDeepClone's reference translation aborts if the
    // owner is not found in the idMap.
    //
    // The most common situation where this happens is
    // AcDbEntity references to Symbol Table Records, such
    // as the Layer an Entity is on.  This is when you will
    // have to pass in the destination database as the owner
    // of the Layer Table Record.  Since all Symbol Tables
    // are always cloned in Wblock, you do not need to make
    // sure that Symbol Table Record owners are cloned. 
    //
    // However, if the owner is one of your own classes,
    // then it is up to you to make sure that it gets
    // cloned.  This is probably easiest to do at the end
    // of this function.  Otherwise you may have problems
    // with recursion when the owner, in turn, attempts
    // to clone this object as one of its subObjects.
    // 
    AcDbBlockTableRecord *pBTR = NULL;
    if (pOwn != NULL)
        pBTR = AcDbBlockTableRecord::cast(pOwn);
    if (pBTR != NULL && isPrimary) 
	{
        pBTR->appendAcDbEntity(pClone);
    } 
	else 
	{
        pDb->addAcDbObject(pClonedObject);
    }

    // STEP 3:
    // The AcDbWblockCloneFiler makes a list of
    // AcDbHardOwnershipIds and AcDbHardPointerIds.  These
    // are the references which must be cloned during a
    // Wblock operation.
    //
    AcDbWblockCloneFiler filer;
    dwgOut(&filer);

    // STEP 4:
    // Rewind the filer and read the data into the clone.
    //
    filer.seek(0L, AcDb::kSeekFromStart);
    pClone->dwgIn(&filer);

    idMap.assign(AcDbIdPair(objectId(), pClonedObject->objectId(), Adesk::kTrue, isPrim, (Adesk::Boolean)(pOwn != NULL)));

   pClonedObject->setOwnerId((pOwn != NULL) ? pOwn->objectId() : ownerId());

    // STEP 5:
    // This must be called for all newly created objects
    // in wblockClone.  It is turned off by endDeepClone()
    // after it has translated the references to their
    // new values.
    //
    pClone->setAcDbObjectIdsInFlux();

    // STEP 6:
    // Add the new information to the idMap.  We can use
    // the idPair started above.  We must also let the
    // idMap entry know whether the clone's owner is
    // correct, or needs to be translated later.
    //

    // STEP 7:
    // Using the filer list created above, find and clone
    // any hard references.
    //
    AcDbObjectId id;
    while (filer.getNextHardObject(id)) 
	{
        AcDbObject *pSubObject;
        AcDbObject *pClonedSubObject;

        // Some object's references may be set to NULL, 
        // so don't try to clone them.
        //
        if (id == NULL)
            continue;

        // If the referenced object is from a different
        // database, such as an xref, do not clone it.
        //
        acdbOpenAcDbObject(pSubObject, id, AcDb::kForRead);
        if (pSubObject->database() != database()) 
		{
            pSubObject->close();
            continue;
        }

        // We can find out if this is an AcDbHardPointerId
        // verses an AcDbHardOwnershipId, by seeing if we
        // are the owner of the pSubObject.  If we are not,
        // then we cannot pass our clone in as the owner
        // for the pSubObject's clone.  In that case, we
        // pass in our clone's database (the destination
        // database).
        // 
        // Note that we now set "isPrimary" to kFalse here
        // because the object is being cloned, not as part
        // of the primary set, but because it is owned by
        // something in the primary set.
        //
        pClonedSubObject = NULL;
        if (pSubObject->ownerId() == objectId()) 
		{
            pSubObject->wblockClone(pClone, pClonedSubObject, idMap, Adesk::kFalse);
        }
		else 
		{
            pSubObject->wblockClone(pClone->database(), pClonedSubObject, idMap, Adesk::kFalse);
        }
        pSubObject->close();
        
        // The pSubObject may either already have been
        // cloned, or for some reason has chosen not to be
        // cloned.  In that case, the returned pointer will
        // be NULL.  Otherwise, since we have no immediate
        // use for it now, we can close the clone.
        //
        if (pClonedSubObject != NULL)
            pClonedSubObject->close();
    }

    // Leave pClonedObject open for the caller.
    //
    return Acad::eOk;
}

// Return the CLSID of the class here
Acad::ErrorStatus CBOQTable::subGetClassID(CLSID* pClsid) const
{
    assertReadEnabled();
	// See the interface definition file for the CLASS ID
	CLSID clsid = { 0xba77cfff, 0x0274, 0x4d4c, { 0xbf, 0xe2, 0x64, 0xa5, 0x73, 0x1b, 0xad, 0x37 } };
    *pClsid = clsid;
    return Acad::eOk;
}

const std::vector<CBOQTable::ColumnType> CBOQTable::ParseColumns(const ACHAR* columns) const
{
	assertReadEnabled();

	// Column definition text
	// [M] Pos marker
	// [MM] Pos marker with double digits
	// [N] Bar count
	// [D] Bar diameter
	// [L] Length
	// [SH] Shape
	// [TL] Total length
	// e.g. [MM][D][N][L][SH][TL]

	std::vector<CBOQTable::ColumnType> list;

	// First pass: separate parts
	std::wstring str;
	if(columns != NULL)
		str.assign(columns);
	std::wstring part;
	bool indeco = false;
	for(unsigned int i = 0; i < str.length(); i++)
	{
		wchar_t c = str.at(i);
		if((!indeco && c == L'[') || (indeco && (c == L']')) || (i == str.length() - 1))
		{
			if((i == str.length() - 1) && (c != L'[') && (c != L']'))
			{
				part += c;
			}
			if(indeco && !part.empty())
			{
				CBOQTable::ColumnType type = CBOQTable::NONE;

				if(part == L"M")
					type = CBOQTable::POS;
				else if(part == L"MM")
					type = CBOQTable::POSDD;
				else if(part == L"N")
					type = CBOQTable::COUNT;
				else if(part == L"D")
					type = CBOQTable::DIAMETER;
				else if(part == L"L")
					type = CBOQTable::LENGTH;
				else if(part == L"SH")
					type = CBOQTable::SHAPE;
				else if(part == L"TL")
					type = CBOQTable::TOTALLENGTH;

				if(type != CBOQTable::NONE)
					list.push_back(type);
			}
			indeco = (c == L'[');

			part.clear();
		}
		else
		{
			part += c;
		}
	}

	return list;
}
