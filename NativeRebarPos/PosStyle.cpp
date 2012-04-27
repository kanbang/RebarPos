//-----------------------------------------------------------------------------
//----- PosStyle.cpp : Implementation of CPosStyle
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

#include "PosStyle.h"

//-----------------------------------------------------------------------------
Adesk::UInt32 CPosStyle::kCurrentVersionNumber = 1;

ACHAR* CPosStyle::Table_Name = _T("OZOZ_REBAR_STYLES");

//-----------------------------------------------------------------------------
ACRX_DXF_DEFINE_MEMBERS(CPosStyle, AcDbObject,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, POSSTYLE,
	"RebarPos2.0\
	|Product Desc:     PosStyle Entity\
	|Company:          OZOZ");

//-----------------------------------------------------------------------------
CPosStyle::CPosStyle () : m_Name(NULL), m_Formula(NULL), m_FormulaWithoutLength(NULL), m_FormulaPosOnly(NULL),
	m_TextColor(2), m_PosColor(4), m_CircleColor(1), m_MultiplierColor(33), m_GroupColor(9), 
	m_NoteColor(30), m_CurrentGroupHighlightColor(8), m_NoteScale(0.75), 
	m_TextStyleID(AcDbObjectId::kNull), m_NoteStyleID(AcDbObjectId::kNull)
{}

CPosStyle::~CPosStyle () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Formula);
	acutDelString(m_FormulaWithoutLength);
	acutDelString(m_FormulaPosOnly);
}

//*************************************************************************
// Properties
//*************************************************************************
const ACHAR* CPosStyle::Name(void) const
{
	assertReadEnabled();
	return m_Name;
}
Acad::ErrorStatus CPosStyle::setName(const ACHAR* newVal)
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

const ACHAR* CPosStyle::Formula(void) const
{
	assertReadEnabled();
	return m_Formula;
}

Acad::ErrorStatus CPosStyle::setFormula(const ACHAR* newVal)
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

const ACHAR* CPosStyle::FormulaWithoutLength(void) const
{
	assertReadEnabled();
	return m_FormulaWithoutLength;
}

Acad::ErrorStatus CPosStyle::setFormulaWithoutLength(const ACHAR* newVal)
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

const ACHAR* CPosStyle::FormulaPosOnly(void) const
{
	assertReadEnabled();
	return m_FormulaPosOnly;
}

Acad::ErrorStatus CPosStyle::setFormulaPosOnly(const ACHAR* newVal)
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

const Adesk::UInt16 CPosStyle::TextColor(void) const
{
	assertReadEnabled();
	return m_TextColor;
}

Acad::ErrorStatus CPosStyle::setTextColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_TextColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosStyle::PosColor(void) const
{
	assertReadEnabled();
	return m_PosColor;
}

Acad::ErrorStatus CPosStyle::setPosColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_PosColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosStyle::CircleColor(void) const
{
	assertReadEnabled();
	return m_CircleColor;
}

Acad::ErrorStatus CPosStyle::setCircleColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_CircleColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosStyle::MultiplierColor(void) const
{
	assertReadEnabled();
	return m_MultiplierColor;
}

Acad::ErrorStatus CPosStyle::setMultiplierColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_MultiplierColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosStyle::GroupColor(void) const
{
	assertReadEnabled();
	return m_GroupColor;
}

Acad::ErrorStatus CPosStyle::setGroupColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_GroupColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosStyle::NoteColor(void) const
{
	assertReadEnabled();
	return m_NoteColor;
}

Acad::ErrorStatus CPosStyle::setNoteColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_NoteColor = newVal;
	return Acad::eOk;
}

const Adesk::UInt16 CPosStyle::CurrentGroupHighlightColor(void) const
{
	assertReadEnabled();
	return m_CurrentGroupHighlightColor;
}

Acad::ErrorStatus CPosStyle::setCurrentGroupHighlightColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_CurrentGroupHighlightColor = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CPosStyle::TextStyleId(void) const
{
	assertReadEnabled();
	return m_TextStyleID;
}

Acad::ErrorStatus CPosStyle::setTextStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_TextStyleID = newVal;
	return Acad::eOk;
}

const AcDbObjectId& CPosStyle::NoteStyleId(void) const
{
	assertReadEnabled();
	return m_NoteStyleID;
}

Acad::ErrorStatus CPosStyle::setNoteStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_NoteStyleID = newVal;
	return Acad::eOk;
}

const double CPosStyle::NoteScale(void) const
{
	assertReadEnabled();
	return m_NoteScale;
}

Acad::ErrorStatus CPosStyle::setNoteScale(const double newVal)
{
	assertWriteEnabled();
	m_NoteScale = newVal;
	return Acad::eOk;
}

//*************************************************************************
// Overrides
//*************************************************************************

//-----------------------------------------------------------------------------
//----- AcDbObject protocols
//- Dwg Filing protocol
Acad::ErrorStatus CPosStyle::dwgOutFields(AcDbDwgFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CPosStyle::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeItem(m_Name);
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

Acad::ErrorStatus CPosStyle::dwgInFields(AcDbDwgFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CPosStyle::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		acutDelString(m_Name);
		acutDelString(m_Formula);
		acutDelString(m_FormulaWithoutLength);
		acutDelString(m_FormulaPosOnly);

		pFiler->readItem(&m_Name);
		pFiler->readString(&m_Formula);
		pFiler->readString(&m_FormulaWithoutLength);
		pFiler->readString(&m_FormulaPosOnly);

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
Acad::ErrorStatus CPosStyle::dxfOutFields(AcDbDxfFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("PosStyle"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CPosStyle::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeString(AcDb::kDxfXTextString, m_Name);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
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

    // Colors
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_TextColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 1, m_PosColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 2, m_CircleColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 3, m_MultiplierColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 4, m_GroupColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 5, m_NoteColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16 + 6, m_CurrentGroupHighlightColor);

    // Note scale
    pFiler->writeDouble(AcDb::kDxfXReal, m_NoteScale);

    // Styles
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_TextStyleID);
    pFiler->writeItem(AcDb::kDxfHardPointerId + 1, m_NoteStyleID);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CPosStyle::dxfInFields(AcDbDxfFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbObject::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("PosStyle")))
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
	if (version > CPosStyle::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Properties
	ACHAR* t_Name = NULL;
	ACHAR* t_Formula = NULL;
	ACHAR* t_FormulaWithoutLength = NULL;
	ACHAR* t_FormulaPosOnly = NULL;
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
        case AcDb::kDxfXTextString + 1:
			acutUpdString(rb.resval.rstring, t_Formula);
			break;
        case AcDb::kDxfXTextString + 2:
			acutUpdString(rb.resval.rstring, t_FormulaWithoutLength);
			break;
        case AcDb::kDxfXTextString + 3:
			acutUpdString(rb.resval.rstring, t_FormulaPosOnly);
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
        case AcDb::kDxfXReal:
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
	setFormula(t_Formula);
	setFormulaWithoutLength(t_FormulaWithoutLength);
	setFormulaPosOnly(t_FormulaPosOnly);
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

	return es;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************

ACHAR* CPosStyle::GetTableName()
{
	return Table_Name;
}
