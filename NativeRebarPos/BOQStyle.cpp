//-----------------------------------------------------------------------------
//----- BOQStyle.cpp : Implementation of CBOQStyle
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "BOQStyle.h"

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
	m_HeadingScale(1.5), m_RowSpacing(0.2)
{ }

CBOQStyle::~CBOQStyle () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Columns);
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

    // Scales
    pFiler->writeDouble(m_HeadingScale);
    pFiler->writeDouble(m_RowSpacing);

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

		// Properties
		pFiler->readItem(&m_Name);

		pFiler->readInt32(&m_Precision);
		Adesk::Int32 displayunit = 0;
		pFiler->readInt32(&displayunit);
		m_DisplayUnit = (DrawingUnits)displayunit;

		pFiler->readString(&m_Columns);

        pFiler->readUInt16(&m_TextColor);
        pFiler->readUInt16(&m_PosColor);
        pFiler->readUInt16(&m_LineColor);
        pFiler->readUInt16(&m_BorderColor);
        pFiler->readUInt16(&m_HeadingColor);
        pFiler->readUInt16(&m_FootingColor);

        pFiler->readDouble(&m_HeadingScale);
        pFiler->readDouble(&m_RowSpacing);

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
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_Precision);
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, m_DisplayUnit);

	if(m_Columns)
		pFiler->writeString(AcDb::kDxfXTextString + 1, m_Columns);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 1, _T(""));

    // Colors
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_TextColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 1, m_PosColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 2, m_LineColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 3, m_BorderColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 4, m_HeadingColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 5, m_FootingColor);

    // Note scale
    pFiler->writeDouble(AcDb::kDxfXReal, m_HeadingScale);
    pFiler->writeDouble(AcDb::kDxfXReal + 1, m_RowSpacing);

    // Styles
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_TextStyleId);
    pFiler->writeItem(AcDb::kDxfHardPointerId + 1, m_HeadingStyleId);

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
	double t_HeadingScale = 0;
	double t_RowSpacing = 0;
	AcDbObjectId t_TextStyleId = AcDbObjectId::kNull;
	AcDbObjectId t_HeadingStyleId = AcDbObjectId::kNull;

    while ((es == Acad::eOk) && ((es = pFiler->readResBuf(&rb)) == Acad::eOk))
    {
        switch (rb.restype) 
		{
        case AcDb::kDxfXTextString:
            acutUpdString(rb.resval.rstring, t_Name);
            break;
        case AcDb::kDxfInt32 + 1:
			t_Precision = rb.resval.rlong;
            break;
        case AcDb::kDxfInt32 + 2:
            t_DisplayUnit = rb.resval.rlong;
            break;
        case AcDb::kDxfXTextString + 1:
			acutUpdString(rb.resval.rstring, t_Columns);
			break;
		case AcDb::kDxfXInt16:
			t_TextColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 1:
			t_PosColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 2:
			t_LineColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 3:
			t_BorderColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 4:
			t_HeadingColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 5:
			t_FootingColor = rb.resval.rint;
			break;
        case AcDb::kDxfXReal:
			t_HeadingScale = rb.resval.rreal;
			break;
        case AcDb::kDxfXReal + 1:
			t_RowSpacing = rb.resval.rreal;
			break;
        case AcDb::kDxfHardPointerId:
			acdbGetObjectId(t_TextStyleId, rb.resval.rlname);
			break;
        case AcDb::kDxfHardPointerId + 1:
			acdbGetObjectId(t_HeadingStyleId, rb.resval.rlname);
			break;

        default:
            // An unrecognized group. Push it back so that
            // the subclass can read it again.
            pFiler->pushBackItem();
            es = Acad::eEndOfFile;
            break;
        }
    }

    // At this point the es variable must contain eEndOfFile
    // - either from readResBuf() or from pushback. If not,
    // it indicates that an error happened and we should
    // return immediately.
    //
    if (es != Acad::eEndOfFile)
        return Acad::eInvalidResBuf;

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
	m_TextStyleId = t_TextStyleId;
	m_HeadingStyleId = t_HeadingStyleId;

	acutDelString(t_Name);
	acutDelString(t_Columns);

	return es;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************

ACHAR* CBOQStyle::GetTableName()
{
	return Table_Name;
}
