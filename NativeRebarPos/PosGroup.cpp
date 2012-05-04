//-----------------------------------------------------------------------------
//----- PosGroup.cpp : Implementation of CPosGroup
//-----------------------------------------------------------------------------
#define WIN32_LEAN_AND_MEAN
#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <windows.h>
#include <objbase.h>

#include "rxregsvc.h"

#include "assert.h"
#include "math.h"

#include "gepnt3d.h"
#include "gevec3d.h"
#include "gelnsg3d.h"
#include "gearc3d.h"

#include "dbents.h"
#include "dbsymtb.h"
#include "dbcfilrs.h"
#include "dbspline.h"
#include "dbproxy.h"
#include "dbxutil.h"
#include "acutmem.h"

#include "acdb.h"
#include "dbidmap.h"
#include "adesk.h"

#include "dbapserv.h"
#include "appinfo.h"
#include "tchar.h"

#include "PosGroup.h"

//-----------------------------------------------------------------------------
Adesk::UInt32 CPosGroup::kCurrentVersionNumber = 1;

ACHAR* CPosGroup::Table_Name = _T("OZOZ_REBAR_GROUPS");

//-----------------------------------------------------------------------------
ACRX_DXF_DEFINE_MEMBERS(CPosGroup, AcDbObject,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, POSGROUP,
	"RebarPos2.0\
	|Product Desc:     PosGroup Entity\
	|Company:          OZOZ");

//-----------------------------------------------------------------------------
CPosGroup::CPosGroup () : m_Name(NULL), m_Bending(Adesk::kFalse), m_MaxBarLength(12), m_Precision(0),
	m_DrawingUnit(CPosGroup::MM), m_DisplayUnit(CPosGroup::MM), m_Current(Adesk::kFalse), 
	m_Formula(NULL), m_FormulaWithoutLength(NULL), m_FormulaPosOnly(NULL), m_StandardDiameters(NULL),
	m_TextColor(2), m_PosColor(4), m_CircleColor(1), m_MultiplierColor(33), m_GroupColor(9), 
	m_NoteColor(30), m_CurrentGroupHighlightColor(8), m_NoteScale(0.75), 
	m_TextStyleID(AcDbObjectId::kNull), m_NoteStyleID(AcDbObjectId::kNull)
{ }

CPosGroup::~CPosGroup () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Formula);
	acutDelString(m_FormulaWithoutLength);
	acutDelString(m_FormulaPosOnly);
}

//*************************************************************************
// Properties
//*************************************************************************
const ACHAR* CPosGroup::Name(void) const
{
	assertReadEnabled();
	return m_Name;
}
Acad::ErrorStatus CPosGroup::setName(const ACHAR* newVal)
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

const Adesk::Boolean CPosGroup::Bending(void) const
{
	assertReadEnabled();
	return m_Bending;
}
Acad::ErrorStatus CPosGroup::setBending(const Adesk::Boolean newVal)
{
	assertWriteEnabled();
	m_Bending = newVal;
	return Acad::eOk;
}

const double CPosGroup::MaxBarLength(void) const
{
	assertReadEnabled();
	return m_MaxBarLength;
}
Acad::ErrorStatus CPosGroup::setMaxBarLength(const double newVal)
{
	assertWriteEnabled();
	m_MaxBarLength = newVal;
	return Acad::eOk;
}

const Adesk::Int32 CPosGroup::Precision(void) const
{
	assertReadEnabled();
	return m_Precision;
}
Acad::ErrorStatus CPosGroup::setPrecision(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Precision = newVal;
	return Acad::eOk;
}

const CPosGroup::DrawingUnits CPosGroup::DrawingUnit(void) const
{
	assertReadEnabled();
	return m_DrawingUnit;
}
Acad::ErrorStatus CPosGroup::setDrawingUnit(const CPosGroup::DrawingUnits newVal)
{
	assertWriteEnabled();
	m_DrawingUnit = newVal;
	return Acad::eOk;
}

const CPosGroup::DrawingUnits CPosGroup::DisplayUnit(void) const
{
	assertReadEnabled();
	return m_DisplayUnit;
}
Acad::ErrorStatus CPosGroup::setDisplayUnit(const CPosGroup::DrawingUnits newVal)
{
	assertWriteEnabled();
	m_DisplayUnit = newVal;
	return Acad::eOk;
}

const ACHAR* CPosGroup::Formula(void) const
{
	assertReadEnabled();
	return m_Formula;
}

Acad::ErrorStatus CPosGroup::setFormula(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_Formula != NULL)
		acutDelString(m_Formula);
    m_Formula = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Formula);
    }

	return Acad::eOk;
}

const ACHAR* CPosGroup::FormulaWithoutLength(void) const
{
	assertReadEnabled();
	return m_FormulaWithoutLength;
}

Acad::ErrorStatus CPosGroup::setFormulaWithoutLength(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_FormulaWithoutLength != NULL)
		acutDelString(m_FormulaWithoutLength);
    m_FormulaWithoutLength = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_FormulaWithoutLength);
    }

	return Acad::eOk;
}

const ACHAR* CPosGroup::FormulaPosOnly(void) const
{
	assertReadEnabled();
	return m_FormulaPosOnly;
}

Acad::ErrorStatus CPosGroup::setFormulaPosOnly(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_FormulaPosOnly != NULL)
		acutDelString(m_FormulaPosOnly);
    m_FormulaPosOnly = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_FormulaPosOnly);
    }

	return Acad::eOk;
}

const ACHAR* CPosGroup::StandardDiameters(void) const
{
	assertReadEnabled();
	return m_StandardDiameters;
}

Acad::ErrorStatus CPosGroup::setStandardDiameters(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_StandardDiameters != NULL)
		acutDelString(m_StandardDiameters);
    m_StandardDiameters = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_StandardDiameters);
    }

	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::TextColor(void) const
{
	assertReadEnabled();
	return m_TextColor;
}

Acad::ErrorStatus CPosGroup::setTextColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_TextColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::PosColor(void) const
{
	assertReadEnabled();
	return m_PosColor;
}

Acad::ErrorStatus CPosGroup::setPosColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_PosColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::CircleColor(void) const
{
	assertReadEnabled();
	return m_CircleColor;
}

Acad::ErrorStatus CPosGroup::setCircleColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_CircleColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::MultiplierColor(void) const
{
	assertReadEnabled();
	return m_MultiplierColor;
}

Acad::ErrorStatus CPosGroup::setMultiplierColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_MultiplierColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::GroupColor(void) const
{
	assertReadEnabled();
	return m_GroupColor;
}

Acad::ErrorStatus CPosGroup::setGroupColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_GroupColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::NoteColor(void) const
{
	assertReadEnabled();
	return m_NoteColor;
}

Acad::ErrorStatus CPosGroup::setNoteColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_NoteColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosGroup::CurrentGroupHighlightColor(void) const
{
	assertReadEnabled();
	return m_CurrentGroupHighlightColor;
}

Acad::ErrorStatus CPosGroup::setCurrentGroupHighlightColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_CurrentGroupHighlightColor = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CPosGroup::TextStyleId(void) const
{
	assertReadEnabled();
	return m_TextStyleID;
}

Acad::ErrorStatus CPosGroup::setTextStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_TextStyleID = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CPosGroup::NoteStyleId(void) const
{
	assertReadEnabled();
	return m_NoteStyleID;
}

Acad::ErrorStatus CPosGroup::setNoteStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_NoteStyleID = newVal;
	return Acad::eOk;
}

const double CPosGroup::NoteScale(void) const
{
	assertReadEnabled();
	return m_NoteScale;
}

Acad::ErrorStatus CPosGroup::setNoteScale(const double newVal)
{
	assertWriteEnabled();
	m_NoteScale = newVal;
	return Acad::eOk;
}

const Adesk::Boolean CPosGroup::Current(void) const
{
	assertReadEnabled();
	return m_Current;
}
Acad::ErrorStatus CPosGroup::setCurrent(const Adesk::Boolean newVal)
{
	assertWriteEnabled();
	m_Current = newVal;
	return Acad::eOk;
}

//*************************************************************************
// Overrides
//*************************************************************************

//-----------------------------------------------------------------------------
//----- AcDbObject protocols
//- Dwg Filing protocol
Acad::ErrorStatus CPosGroup::dwgOutFields(AcDbDwgFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CPosGroup::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeItem(m_Name);
	else
		pFiler->writeItem(_T(""));

	pFiler->writeBoolean(m_Bending);
	pFiler->writeDouble(m_MaxBarLength);
	pFiler->writeInt32(m_Precision);
	pFiler->writeInt32(m_DrawingUnit);
	pFiler->writeInt32(m_DisplayUnit);

	// Formula
	if (m_Formula)
		pFiler->writeString(m_Formula);
	else
		pFiler->writeString(_T(""));
	if (m_FormulaWithoutLength)
		pFiler->writeString(m_FormulaWithoutLength);
	else
		pFiler->writeString(_T(""));
	if (m_FormulaPosOnly)
		pFiler->writeString(m_FormulaPosOnly);
	else
		pFiler->writeString(_T(""));
	if (m_StandardDiameters)
		pFiler->writeString(m_StandardDiameters);
	else
		pFiler->writeString(_T(""));

    // Colors
    pFiler->writeUInt16(m_TextColor);
    pFiler->writeUInt16(m_PosColor);
    pFiler->writeUInt16(m_CircleColor);
    pFiler->writeUInt16(m_MultiplierColor);
    pFiler->writeUInt16(m_GroupColor);
    pFiler->writeUInt16(m_NoteColor);
	pFiler->writeUInt16(m_CurrentGroupHighlightColor);

    // Note scale
    pFiler->writeDouble(m_NoteScale);

    // Styles
    pFiler->writeHardPointerId(m_TextStyleID);
    pFiler->writeHardPointerId(m_NoteStyleID);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CPosGroup::dwgInFields(AcDbDwgFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CPosGroup::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		acutDelString(m_Name);
		acutDelString(m_Formula);
		acutDelString(m_FormulaWithoutLength);
		acutDelString(m_FormulaPosOnly);

		// Properties
		pFiler->readItem(&m_Name);

		pFiler->readBoolean(&m_Bending);
		pFiler->readDouble(&m_MaxBarLength);
		pFiler->readInt32(&m_Precision);
		Adesk::Int32 drawingunit = 0;
		pFiler->readInt32(&drawingunit);
		m_DrawingUnit = (DrawingUnits)drawingunit;
		Adesk::Int32 displayunit = 0;
		pFiler->readInt32(&displayunit);
		m_DisplayUnit = (DrawingUnits)displayunit;

		pFiler->readString(&m_Formula);
		pFiler->readString(&m_FormulaWithoutLength);
		pFiler->readString(&m_FormulaPosOnly);
		pFiler->readString(&m_StandardDiameters);

        pFiler->readUInt16(&m_TextColor);
        pFiler->readUInt16(&m_PosColor);
        pFiler->readUInt16(&m_CircleColor);
        pFiler->readUInt16(&m_MultiplierColor);
        pFiler->readUInt16(&m_GroupColor);
        pFiler->readUInt16(&m_NoteColor);
		pFiler->readUInt16(&m_CurrentGroupHighlightColor);

        pFiler->readDouble(&m_NoteScale);

		pFiler->readHardPointerId(&m_TextStyleID);
		pFiler->readHardPointerId(&m_NoteStyleID);
	}

	return pFiler->filerStatus();
}

//-----------------------------------------------------------------------------
//----- AcDbObject protocols
//- Dxf Filing protocol
Acad::ErrorStatus CPosGroup::dxfOutFields(AcDbDxfFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("PosGroup"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CPosGroup::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeString(AcDb::kDxfXTextString, m_Name);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	pFiler->writeItem(AcDb::kDxfBool, m_Bending);
	pFiler->writeDouble(AcDb::kDxfXReal, m_MaxBarLength);
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_Precision);
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, m_DrawingUnit);
	pFiler->writeInt32(AcDb::kDxfInt32 + 3, m_DisplayUnit);

	if(m_Formula)
		pFiler->writeString(AcDb::kDxfXTextString + 1, m_Formula);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 1, _T(""));
	if(m_FormulaWithoutLength)
		pFiler->writeString(AcDb::kDxfXTextString + 2, m_FormulaWithoutLength);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 2, _T(""));
	if(m_FormulaPosOnly)
		pFiler->writeString(AcDb::kDxfXTextString + 3, m_FormulaPosOnly);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 3, _T(""));
	if(m_StandardDiameters)
		pFiler->writeString(AcDb::kDxfXTextString + 4, m_StandardDiameters);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 4, _T(""));

    // Colors
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_TextColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 1, m_PosColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 2, m_CircleColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 3, m_MultiplierColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 4, m_GroupColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 5, m_NoteColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16 + 6, m_CurrentGroupHighlightColor);

    // Note scale
    pFiler->writeDouble(AcDb::kDxfXReal + 1, m_NoteScale);

    // Styles
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_TextStyleID);
    pFiler->writeItem(AcDb::kDxfHardPointerId + 1, m_NoteStyleID);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CPosGroup::dxfInFields(AcDbDxfFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbObject::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("PosGroup")))
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
	if (version > CPosGroup::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Properties
	ACHAR* t_Name = NULL;
	Adesk::Boolean t_Bending;
	double t_MaxBarLength;
	int t_Precision;
	int t_DrawingUnit;
	int t_DisplayUnit;
	ACHAR* t_Formula = NULL;
	ACHAR* t_FormulaWithoutLength = NULL;
	ACHAR* t_FormulaPosOnly = NULL;
	ACHAR* t_StandardDiameters = NULL;
	Adesk::UInt16 t_TextColor;
	Adesk::UInt16 t_PosColor;
	Adesk::UInt16 t_CircleColor;
	Adesk::UInt16 t_MultiplierColor;
	Adesk::UInt16 t_GroupColor;
	Adesk::UInt16 t_NoteColor;
	Adesk::UInt16 t_CurrentGroupHighlightColor;
	double t_NoteScale;
	AcDbObjectId t_TextStyleID, t_NoteStyleID;

    while ((es == Acad::eOk) && ((es = pFiler->readResBuf(&rb)) == Acad::eOk))
    {
        switch (rb.restype) 
		{
        case AcDb::kDxfXTextString:
            acutUpdString(rb.resval.rstring, t_Name);
            break;
        case AcDb::kDxfBool:
			t_Bending = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
            break;
		case AcDb::kDxfXReal:
			t_MaxBarLength = rb.resval.rreal;
			break;
        case AcDb::kDxfInt32 + 1:
			t_Precision = rb.resval.rlong;
            break;
        case AcDb::kDxfInt32 + 2:
			t_DrawingUnit = rb.resval.rlong;
            break;
        case AcDb::kDxfInt32 + 3:
            t_DisplayUnit = rb.resval.rlong;
            break;
        case AcDb::kDxfXTextString + 1:
			acutUpdString(rb.resval.rstring, t_Formula);
			break;
        case AcDb::kDxfXTextString + 2:
			acutUpdString(rb.resval.rstring, t_FormulaWithoutLength);
			break;
        case AcDb::kDxfXTextString + 3:
			acutUpdString(rb.resval.rstring, t_FormulaPosOnly);
			break;
        case AcDb::kDxfXTextString + 4:
			acutUpdString(rb.resval.rstring, t_StandardDiameters);
			break;
		case AcDb::kDxfXInt16:
			t_TextColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 1:
			t_PosColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 2:
			t_CircleColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 3:
			t_MultiplierColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 4:
			t_GroupColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 5:
			t_NoteColor = rb.resval.rint;
			break;
        case AcDb::kDxfXInt16 + 6:
			t_CurrentGroupHighlightColor = rb.resval.rint;
			break;
        case AcDb::kDxfXReal + 1:
			t_NoteScale = rb.resval.rreal;
			break;
        case AcDb::kDxfHardPointerId:
			acdbGetObjectId(t_TextStyleID, rb.resval.rlname);
			break;
        case AcDb::kDxfHardPointerId + 1:
			acdbGetObjectId(t_NoteStyleID, rb.resval.rlname);
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
    m_Bending = t_Bending;
    m_MaxBarLength = t_MaxBarLength;
	m_Precision = t_Precision;
    m_DrawingUnit = (DrawingUnits)t_DrawingUnit;
    m_DisplayUnit = (DrawingUnits)t_DisplayUnit;
	setFormula(t_Formula);
	setFormulaWithoutLength(t_FormulaWithoutLength);
	setFormulaPosOnly(t_FormulaPosOnly);
	setStandardDiameters(t_StandardDiameters);
	m_TextColor = t_TextColor;
	m_PosColor = t_PosColor;
	m_CircleColor = t_CircleColor;
	m_MultiplierColor = t_MultiplierColor;
	m_GroupColor = t_GroupColor;
	m_NoteColor = t_NoteColor;
	m_CurrentGroupHighlightColor = t_CurrentGroupHighlightColor;
	m_NoteScale = t_NoteScale;
	m_TextStyleID = t_TextStyleID;
	m_NoteStyleID = t_NoteStyleID;

	acutDelString(t_Name);
	acutDelString(t_Formula);
	acutDelString(t_FormulaWithoutLength);
	acutDelString(t_FormulaPosOnly);
	acutDelString(t_StandardDiameters);

	return es;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************

ACHAR* CPosGroup::GetTableName()
{
	return Table_Name;
}
