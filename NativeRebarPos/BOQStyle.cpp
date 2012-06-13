//-----------------------------------------------------------------------------
//----- BOQStyle.cpp : Implementation of CBOQStyle
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "BOQStyle.h"
#include "Utility.h"

//-----------------------------------------------------------------------------
Adesk::UInt32 CBOQStyle::kCurrentVersionNumber = 1;

ACHAR* CBOQStyle::Table_Name = _T("OZOZ_REBAR_BOQSTYLES");

//-----------------------------------------------------------------------------
ACRX_DXF_DEFINE_MEMBERS(CBOQStyle, AcDbObject,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyObject::kEraseAllowed, BOQSTYLE,
	"OZOZRebarPos\
	|Product Desc:     BOQStyle Entity\
	|Company:          OZOZ");

//-----------------------------------------------------------------------------
CBOQStyle::CBOQStyle () : m_Name(NULL), m_Precision(0),	m_DisplayUnit(CBOQStyle::MM), m_Columns(NULL),
	m_TextColor(2), m_PosColor(4), m_LineColor(1), m_BorderColor(33), m_HeadingColor(9), m_FootingColor(9),
	m_TextStyleId(AcDbObjectId::kNull), m_HeadingStyleId(AcDbObjectId::kNull),
	m_HeadingScale(1.5), m_RowSpacing(0.2), m_Heading(NULL), m_Footing(NULL),
	m_PosLabel(NULL), m_CountLabel(NULL), m_DiameterLabel(NULL), m_LengthLabel(NULL), m_ShapeLabel(NULL),
	m_TotalLengthLabel(NULL), m_DiameterLengthLabel(NULL), m_UnitWeightLabel(NULL), m_WeightLabel(NULL), m_GrossWeightLabel(NULL)
{ }

CBOQStyle::~CBOQStyle () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Columns);
	acutDelString(m_Heading);
	acutDelString(m_Footing);

	acutDelString(m_PosLabel);
	acutDelString(m_CountLabel);
	acutDelString(m_DiameterLabel);
	acutDelString(m_LengthLabel);
	acutDelString(m_ShapeLabel);
	acutDelString(m_TotalLengthLabel);
	acutDelString(m_DiameterLengthLabel);
	acutDelString(m_UnitWeightLabel);
	acutDelString(m_WeightLabel);
	acutDelString(m_GrossWeightLabel);
}

//*************************************************************************
// Properties
//*************************************************************************
const ACHAR* CBOQStyle::Name(void) const
{
	assertReadEnabled();
	return m_Name;
}
Acad::ErrorStatus CBOQStyle::setName(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Name);
    m_Name = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Name);
    }
	return Acad::eOk;
}

const ACHAR* CBOQStyle::Heading(void) const
{
	assertReadEnabled();
	return m_Heading;
}
Acad::ErrorStatus CBOQStyle::setHeading(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Heading);
    m_Heading = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Heading);
    }
	return Acad::eOk;
}

const ACHAR* CBOQStyle::Footing(void) const
{
	assertReadEnabled();
	return m_Footing;
}
Acad::ErrorStatus CBOQStyle::setFooting(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Footing);
    m_Footing = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Footing);
    }
	return Acad::eOk;
}

const Adesk::Int32 CBOQStyle::Precision(void) const
{
	assertReadEnabled();
	return m_Precision;
}
Acad::ErrorStatus CBOQStyle::setPrecision(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Precision = newVal;
	return Acad::eOk;
}

const CBOQStyle::DrawingUnits CBOQStyle::DisplayUnit(void) const
{
	assertReadEnabled();
	return m_DisplayUnit;
}
Acad::ErrorStatus CBOQStyle::setDisplayUnit(const CBOQStyle::DrawingUnits newVal)
{
	assertWriteEnabled();
	m_DisplayUnit = newVal;
	return Acad::eOk;
}

const ACHAR* CBOQStyle::Columns(void) const
{
	assertReadEnabled();
	return m_Columns;
}

Acad::ErrorStatus CBOQStyle::setColumns(const ACHAR* newVal)
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

const Adesk::UInt16 CBOQStyle::TextColor(void) const
{
	assertReadEnabled();
	return m_TextColor;
}
Acad::ErrorStatus CBOQStyle::setTextColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_TextColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQStyle::PosColor(void) const
{
	assertReadEnabled();
	return m_PosColor;
}
Acad::ErrorStatus CBOQStyle::setPosColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_PosColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQStyle::LineColor(void) const
{
	assertReadEnabled();
	return m_LineColor;
}
Acad::ErrorStatus CBOQStyle::setLineColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_LineColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQStyle::BorderColor(void) const
{
	assertReadEnabled();
	return m_BorderColor;
}
Acad::ErrorStatus CBOQStyle::setBorderColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_BorderColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQStyle::HeadingColor(void) const
{
	assertReadEnabled();
	return m_HeadingColor;
}
Acad::ErrorStatus CBOQStyle::setHeadingColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_HeadingColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CBOQStyle::FootingColor(void) const
{
	assertReadEnabled();
	return m_FootingColor;
}
Acad::ErrorStatus CBOQStyle::setFootingColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_FootingColor = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQStyle::TextStyleId(void) const
{
	assertReadEnabled();
	return m_TextStyleId;
}
Acad::ErrorStatus CBOQStyle::setTextStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_TextStyleId = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CBOQStyle::HeadingStyleId(void) const
{
	assertReadEnabled();
	return m_HeadingStyleId;
}
Acad::ErrorStatus CBOQStyle::setHeadingStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_HeadingStyleId = newVal;
	return Acad::eOk;
}

const double CBOQStyle::HeadingScale(void) const
{
	assertReadEnabled();
	return m_HeadingScale;
}
Acad::ErrorStatus CBOQStyle::setHeadingScale(const double newVal)
{
	assertWriteEnabled();
	m_HeadingScale = newVal;
	return Acad::eOk;
}

const double CBOQStyle::RowSpacing(void) const
{
	assertReadEnabled();
	return m_RowSpacing;
}
Acad::ErrorStatus CBOQStyle::setRowSpacing(const double newVal)
{
	assertWriteEnabled();
	m_RowSpacing = newVal;
	return Acad::eOk;
}

// Get labels
const ACHAR* CBOQStyle::PosLabel(void) const            { assertReadEnabled(); return m_PosLabel; }
const ACHAR* CBOQStyle::CountLabel(void) const          { assertReadEnabled(); return m_CountLabel; }
const ACHAR* CBOQStyle::DiameterLabel(void) const       { assertReadEnabled(); return m_DiameterLabel; }
const ACHAR* CBOQStyle::LengthLabel(void) const         { assertReadEnabled(); return m_LengthLabel; }
const ACHAR* CBOQStyle::ShapeLabel(void) const          { assertReadEnabled(); return m_ShapeLabel; }
const ACHAR* CBOQStyle::TotalLengthLabel(void) const    { assertReadEnabled(); return m_TotalLengthLabel; }
const ACHAR* CBOQStyle::DiameterLengthLabel(void) const { assertReadEnabled(); return m_DiameterLengthLabel; }
const ACHAR* CBOQStyle::UnitWeightLabel(void) const     { assertReadEnabled(); return m_UnitWeightLabel; }
const ACHAR* CBOQStyle::WeightLabel(void) const         { assertReadEnabled(); return m_WeightLabel; }
const ACHAR* CBOQStyle::GrossWeightLabel(void) const    { assertReadEnabled(); return m_GrossWeightLabel; }
// Set labels
Acad::ErrorStatus CBOQStyle::setPosLabel(const ACHAR* newVal)            { assertWriteEnabled(); acutDelString(m_PosLabel); acutUpdString(newVal, m_PosLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setCountLabel(const ACHAR* newVal)          { assertWriteEnabled(); acutDelString(m_CountLabel); acutUpdString(newVal, m_CountLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setDiameterLabel(const ACHAR* newVal)       { assertWriteEnabled(); acutDelString(m_DiameterLabel); acutUpdString(newVal, m_DiameterLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setLengthLabel(const ACHAR* newVal)         { assertWriteEnabled(); acutDelString(m_LengthLabel); acutUpdString(newVal, m_LengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setShapeLabel(const ACHAR* newVal)          { assertWriteEnabled(); acutDelString(m_ShapeLabel); acutUpdString(newVal, m_ShapeLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setTotalLengthLabel(const ACHAR* newVal)    { assertWriteEnabled(); acutDelString(m_TotalLengthLabel); acutUpdString(newVal, m_TotalLengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setDiameterLengthLabel(const ACHAR* newVal) { assertWriteEnabled(); acutDelString(m_DiameterLengthLabel); acutUpdString(newVal, m_DiameterLengthLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setUnitWeightLabel(const ACHAR* newVal)     { assertWriteEnabled(); acutDelString(m_UnitWeightLabel); acutUpdString(newVal, m_UnitWeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setWeightLabel(const ACHAR* newVal)         { assertWriteEnabled(); acutDelString(m_WeightLabel); acutUpdString(newVal, m_WeightLabel); return Acad::eOk; }
Acad::ErrorStatus CBOQStyle::setGrossWeightLabel(const ACHAR* newVal)    { assertWriteEnabled(); acutDelString(m_GrossWeightLabel); acutUpdString(newVal, m_GrossWeightLabel); return Acad::eOk; }

//*************************************************************************
// Overrides
//*************************************************************************

//-----------------------------------------------------------------------------
//----- AcDbObject protocols
//- Dwg Filing protocol
Acad::ErrorStatus CBOQStyle::dwgOutFields(AcDbDwgFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CBOQStyle::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeItem(m_Name);
	else
		pFiler->writeItem(_T(""));

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
	if (m_DiameterLengthLabel) pFiler->writeString(m_DiameterLengthLabel); else pFiler->writeString(_T(""));
	if (m_UnitWeightLabel) pFiler->writeString(m_UnitWeightLabel); else pFiler->writeString(_T(""));
	if (m_WeightLabel) pFiler->writeString(m_WeightLabel); else pFiler->writeString(_T(""));
	if (m_GrossWeightLabel) pFiler->writeString(m_GrossWeightLabel); else pFiler->writeString(_T(""));

    // Scales
    pFiler->writeDouble(m_HeadingScale);
    pFiler->writeDouble(m_RowSpacing);

	// Texts
	if (m_Heading)
		pFiler->writeString(m_Heading);
	else
		pFiler->writeString(_T(""));
	if (m_Footing)
		pFiler->writeString(m_Footing);
	else
		pFiler->writeString(_T(""));

    // Styles
    pFiler->writeHardPointerId(m_TextStyleId);
    pFiler->writeHardPointerId(m_HeadingStyleId);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQStyle::dwgInFields(AcDbDwgFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CBOQStyle::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		acutDelString(m_Name);
		acutDelString(m_Columns);
		acutDelString(m_Heading);
		acutDelString(m_Footing);

		// Properties
		pFiler->readItem(&m_Name);

		pFiler->readInt32(&m_Precision);
		Adesk::Int32 displayunit = 0;
		pFiler->readInt32(&displayunit);
		m_DisplayUnit = (DrawingUnits)displayunit;

		pFiler->readString(&m_Columns);

		// Columns
        pFiler->readUInt16(&m_TextColor);
        pFiler->readUInt16(&m_PosColor);
        pFiler->readUInt16(&m_LineColor);
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
		pFiler->readString(&m_DiameterLengthLabel);
		pFiler->readString(&m_UnitWeightLabel);
		pFiler->readString(&m_WeightLabel);
		pFiler->readString(&m_GrossWeightLabel);

        pFiler->readDouble(&m_HeadingScale);
        pFiler->readDouble(&m_RowSpacing);

		pFiler->readString(&m_Heading);
		pFiler->readString(&m_Footing);

		pFiler->readHardPointerId(&m_TextStyleId);
		pFiler->readHardPointerId(&m_HeadingStyleId);
	}

	return pFiler->filerStatus();
}

//-----------------------------------------------------------------------------
//----- AcDbObject protocols
//- Dxf Filing protocol
Acad::ErrorStatus CBOQStyle::dxfOutFields(AcDbDxfFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("BOQStyle"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CBOQStyle::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeString(AcDb::kDxfXTextString, m_Name);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
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
	if (m_DiameterLengthLabel) pFiler->writeString(AcDb::kDxfXTextString, m_DiameterLengthLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_UnitWeightLabel) pFiler->writeString(AcDb::kDxfXTextString, m_UnitWeightLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_WeightLabel) pFiler->writeString(AcDb::kDxfXTextString, m_WeightLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_GrossWeightLabel) pFiler->writeString(AcDb::kDxfXTextString, m_GrossWeightLabel); else pFiler->writeString(AcDb::kDxfXTextString, _T(""));

    // Note scale
    pFiler->writeDouble(AcDb::kDxfXReal, m_HeadingScale);
    pFiler->writeDouble(AcDb::kDxfXReal, m_RowSpacing);

	// Texts
	if (m_Heading)
		pFiler->writeString(AcDb::kDxfXTextString, m_Heading);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if (m_Footing)
		pFiler->writeString(AcDb::kDxfXTextString, m_Footing);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));

    // Styles
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_TextStyleId);
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_HeadingStyleId);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQStyle::dxfInFields(AcDbDxfFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbObject::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("BOQStyle")))
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
	if (version > CBOQStyle::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Properties
	ACHAR* t_Name = NULL;
	int t_Precision = 0;
	int t_DisplayUnit = 0;
	ACHAR* t_Columns = NULL;
	Adesk::UInt16 t_TextColor = 0;
	Adesk::UInt16 t_PosColor = 0;
	Adesk::UInt16 t_LineColor = 0;
	Adesk::UInt16 t_BorderColor = 0;
	Adesk::UInt16 t_HeadingColor = 0;
	Adesk::UInt16 t_FootingColor = 0;
	ACHAR* t_PosLabel = NULL;
	ACHAR* t_CountLabel = NULL;
	ACHAR* t_DiameterLabel = NULL;
	ACHAR* t_LengthLabel = NULL;
	ACHAR* t_ShapeLabel = NULL;
	ACHAR* t_TotalLengthLabel = NULL;
	ACHAR* t_DiameterLengthLabel = NULL;
	ACHAR* t_UnitWeightLabel = NULL;
	ACHAR* t_WeightLabel = NULL;
	ACHAR* t_GrossWeightLabel = NULL;
	double t_HeadingScale = 0;
	double t_RowSpacing = 0;
	ACHAR* t_Heading = NULL;
	ACHAR* t_Footing = NULL;
	AcDbObjectId t_TextStyleId = AcDbObjectId::kNull;
	AcDbObjectId t_HeadingStyleId = AcDbObjectId::kNull;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("name"), t_Name)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("prevision"), t_Precision)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("display unit"), t_DisplayUnit)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("columns"), t_Columns)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("text color"), t_TextColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("pos color"), t_PosColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("line color"), t_LineColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("border color"), t_BorderColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("heading color"), t_HeadingColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("footing color"), t_FootingColor)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("pos label"), t_PosLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("count label"), t_CountLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("diameter label"), t_DiameterLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("length label"), t_LengthLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("shape label"), t_ShapeLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("total length label"), t_TotalLengthLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("diameter length label"), t_DiameterLengthLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("unit weight label"), t_UnitWeightLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("weight label"), t_WeightLabel)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("gross weight label"), t_GrossWeightLabel)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("heading scale"), t_HeadingScale)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("row spacing"), t_RowSpacing)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("heading"), t_Heading)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("footing"), t_Footing)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("text style"), t_TextStyleId)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("heading style"), t_HeadingStyleId)) != Acad::eOk) return es;

	// Successfully read DXF codes; set object properties.
	setName(t_Name);
	m_Precision = t_Precision;
    m_DisplayUnit = (DrawingUnits)t_DisplayUnit;
	setColumns(t_Columns);

	m_TextColor = t_TextColor;
	m_PosColor = t_PosColor;
	m_LineColor = t_LineColor;
	m_BorderColor = t_BorderColor;
	m_HeadingColor = t_HeadingColor;
	m_FootingColor = t_FootingColor;

	m_HeadingScale = t_HeadingScale;
	m_RowSpacing = t_RowSpacing;
	setHeading(t_Heading);
	setFooting(t_Footing);
	m_TextStyleId = t_TextStyleId;
	m_HeadingStyleId = t_HeadingStyleId;

	acutDelString(t_Name);
	acutDelString(t_Columns);
	acutDelString(t_Heading);
	acutDelString(t_Footing);

	acutDelString(t_PosLabel);
	acutDelString(t_CountLabel);
	acutDelString(t_DiameterLabel);
	acutDelString(t_LengthLabel);
	acutDelString(t_ShapeLabel);
	acutDelString(t_TotalLengthLabel);
	acutDelString(t_DiameterLengthLabel);
	acutDelString(t_UnitWeightLabel);
	acutDelString(t_WeightLabel);
	acutDelString(t_GrossWeightLabel);

	return es;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************

ACHAR* CBOQStyle::GetTableName()
{
	return Table_Name;
}
