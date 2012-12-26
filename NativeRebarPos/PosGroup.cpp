//-----------------------------------------------------------------------------
//----- PosGroup.cpp : Implementation of CPosGroup
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "PosGroup.h"
#include "Utility.h"

//-----------------------------------------------------------------------------
Adesk::UInt32 CPosGroup::kCurrentVersionNumber = 1;

ACHAR* CPosGroup::Table_Name = _T("OZOZ_REBAR_GROUPS");

//-----------------------------------------------------------------------------
ACRX_DXF_DEFINE_MEMBERS(CPosGroup, AcDbObject,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyObject::kEraseAllowed, POSGROUP,
	"OZOZRebarPos\
	|Product Desc:     PosGroup Entity\
	|Company:          OZOZ");

//-----------------------------------------------------------------------------
CPosGroup::CPosGroup () : m_Name(NULL), m_Bending(Adesk::kFalse), m_MaxBarLength(12), m_Precision(0),
	m_DrawingUnit(CPosGroup::MM), m_DisplayUnit(CPosGroup::MM), 
	m_Formula(NULL), m_FormulaVariableLength(NULL), m_FormulaLengthOnly(NULL), m_FormulaPosOnly(NULL), m_StandardDiameters(NULL),
	m_TextColor(2), m_PosColor(4), m_CircleColor(1), m_MultiplierColor(33), m_GroupColor(9), 
	m_NoteColor(30), m_CurrentGroupHighlightColor(8), m_CountColor(5), m_NoteScale(0.75), 
	m_TextStyleID(AcDbObjectId::kNull), m_NoteStyleID(AcDbObjectId::kNull)
{ }

CPosGroup::~CPosGroup () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Formula);
	acutDelString(m_FormulaVariableLength);
	acutDelString(m_FormulaLengthOnly);
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

const ACHAR* CPosGroup::FormulaVariableLength(void) const
{
	assertReadEnabled();
	return m_FormulaVariableLength;
}

Acad::ErrorStatus CPosGroup::setFormulaVariableLength(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_FormulaVariableLength != NULL)
		acutDelString(m_FormulaVariableLength);
    m_FormulaVariableLength = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_FormulaVariableLength);
    }

	return Acad::eOk;
}

const ACHAR* CPosGroup::FormulaLengthOnly(void) const
{
	assertReadEnabled();
	return m_FormulaLengthOnly;
}

Acad::ErrorStatus CPosGroup::setFormulaLengthOnly(const ACHAR* newVal)
{
	assertWriteEnabled();

	if(m_FormulaLengthOnly != NULL)
		acutDelString(m_FormulaLengthOnly);
    m_FormulaLengthOnly = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_FormulaLengthOnly);
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

const Adesk::UInt16 CPosGroup::CountColor(void) const
{
	assertReadEnabled();
	return m_CountColor;
}

Acad::ErrorStatus CPosGroup::setCountColor(const Adesk::UInt16 newVal)
{
	assertWriteEnabled();
	m_CountColor = newVal;
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
	if (m_FormulaVariableLength)
		pFiler->writeString(m_FormulaVariableLength);
	else
		pFiler->writeString(_T(""));
	if (m_FormulaLengthOnly)
		pFiler->writeString(m_FormulaLengthOnly);
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
	pFiler->writeUInt16(m_CountColor);

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
		acutDelString(m_FormulaLengthOnly);
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
		pFiler->readString(&m_FormulaVariableLength);
		pFiler->readString(&m_FormulaLengthOnly);
		pFiler->readString(&m_FormulaPosOnly);
		pFiler->readString(&m_StandardDiameters);

        pFiler->readUInt16(&m_TextColor);
        pFiler->readUInt16(&m_PosColor);
        pFiler->readUInt16(&m_CircleColor);
        pFiler->readUInt16(&m_MultiplierColor);
        pFiler->readUInt16(&m_GroupColor);
        pFiler->readUInt16(&m_NoteColor);
		pFiler->readUInt16(&m_CurrentGroupHighlightColor);
		pFiler->readUInt16(&m_CountColor);

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
	if(m_FormulaVariableLength)
		pFiler->writeString(AcDb::kDxfXTextString + 2, m_FormulaVariableLength);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 2, _T(""));
	if(m_FormulaLengthOnly)
		pFiler->writeString(AcDb::kDxfXTextString + 3, m_FormulaLengthOnly);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 3, _T(""));
	if(m_FormulaPosOnly)
		pFiler->writeString(AcDb::kDxfXTextString + 4, m_FormulaPosOnly);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 4, _T(""));
	if(m_StandardDiameters)
		pFiler->writeString(AcDb::kDxfXTextString + 5, m_StandardDiameters);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 5, _T(""));

    // Colors
    pFiler->writeUInt16(AcDb::kDxfXInt16, m_TextColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 1, m_PosColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 2, m_CircleColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 3, m_MultiplierColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 4, m_GroupColor);
    pFiler->writeUInt16(AcDb::kDxfXInt16 + 5, m_NoteColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16 + 6, m_CurrentGroupHighlightColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16 + 7, m_CountColor);

    // Note scale
    pFiler->writeDouble(AcDb::kDxfXReal + 1, m_NoteScale);

    // Styles
    pFiler->writeItem(AcDb::kDxfHardPointerId + 1, m_TextStyleID);
    pFiler->writeItem(AcDb::kDxfHardPointerId + 2, m_NoteStyleID);

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
	Adesk::Boolean t_Bending = Adesk::kFalse;
	double t_MaxBarLength = 0;
	int t_Precision = 0;
	int t_DrawingUnit = 0;
	int t_DisplayUnit = 0;
	ACHAR* t_Formula = NULL;
	ACHAR* t_FormulaVariableLength = NULL;
	ACHAR* t_FormulaLengthOnly = NULL;
	ACHAR* t_FormulaPosOnly = NULL;
	ACHAR* t_StandardDiameters = NULL;
	Adesk::UInt16 t_TextColor = 0;
	Adesk::UInt16 t_PosColor = 0;
	Adesk::UInt16 t_CircleColor = 0;
	Adesk::UInt16 t_MultiplierColor = 0;
	Adesk::UInt16 t_GroupColor = 0;
	Adesk::UInt16 t_NoteColor = 0;
	Adesk::UInt16 t_CurrentGroupHighlightColor = 0;
	Adesk::UInt16 t_CountColor = 0;
	double t_NoteScale = 0;
	AcDbObjectId t_TextStyleID = AcDbObjectId::kNull;
	AcDbObjectId t_NoteStyleID = AcDbObjectId::kNull;

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
			acutUpdString(rb.resval.rstring, t_FormulaVariableLength);
			break;
        case AcDb::kDxfXTextString + 3:
			acutUpdString(rb.resval.rstring, t_FormulaLengthOnly);
			break;
        case AcDb::kDxfXTextString + 4:
			acutUpdString(rb.resval.rstring, t_FormulaPosOnly);
			break;
        case AcDb::kDxfXTextString + 5:
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
        case AcDb::kDxfXInt16 + 7:
			t_CountColor = rb.resval.rint;
			break;
        case AcDb::kDxfXReal + 1:
			t_NoteScale = rb.resval.rreal;
			break;
        case AcDb::kDxfHardPointerId + 1:
			acdbGetObjectId(t_TextStyleID, rb.resval.rlname);
			break;
        case AcDb::kDxfHardPointerId + 2:
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
	setFormulaVariableLength(t_FormulaVariableLength);
	setFormulaLengthOnly(t_FormulaLengthOnly);
	setFormulaPosOnly(t_FormulaPosOnly);
	setStandardDiameters(t_StandardDiameters);

	m_TextColor = t_TextColor;
	m_PosColor = t_PosColor;
	m_CircleColor = t_CircleColor;
	m_MultiplierColor = t_MultiplierColor;
	m_GroupColor = t_GroupColor;
	m_NoteColor = t_NoteColor;
	m_CurrentGroupHighlightColor = t_CurrentGroupHighlightColor;
	m_CountColor = t_CountColor;

	m_NoteScale = t_NoteScale;

	m_TextStyleID = t_TextStyleID;
	m_NoteStyleID = t_NoteStyleID;

	acutDelString(t_Name);
	acutDelString(t_Formula);
	acutDelString(t_FormulaVariableLength);
	acutDelString(t_FormulaLengthOnly);
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

AcDbObjectId CPosGroup::GetGroupId()
{
	AcDbObjectId id = AcDbObjectId::kNull;

	AcDbDictionary* pNamedObj = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	pDb->getNamedObjectsDictionary(pNamedObj, AcDb::kForRead);

	AcDbDictionary *pDict = NULL;
	if (pNamedObj->getAt(GetTableName(), (AcDbObject*&) pDict, AcDb::kForRead) == Acad::eKeyNotFound)
	{
        pDict = new AcDbDictionary();
		pNamedObj->upgradeOpen();
		AcDbObjectId pid;
		pNamedObj->setAt(GetTableName(), pDict, pid);
		pNamedObj->downgradeOpen();
	}

	if(pDict->getAt(_T("0"), id) == Acad::eKeyNotFound)
	{
        CPosGroup* group = new CPosGroup();
        group->setName(_T("0"));
        group->setFormula(_T("[M:C][N][\"T\":D][\"/\":S]"));
		group->setFormulaVariableLength(_T("[M:C][N][\"T\":D][\"/\":S]"));
        group->setFormulaLengthOnly(_T("[\"L=\":L]"));
        group->setFormulaPosOnly(_T("[M:C]"));
        group->setStandardDiameters(_T("8 10 12 14 16 18 20 22 25 26 32 36"));
		group->setTextStyleId(Utility::CreateTextStyle(_T("Rebar Text Style"), _T("romans.shx"), 0.65));
		group->setNoteStyleId(Utility::CreateTextStyle(_T("Rebar Note Style"), _T("simplxtw.shx"), 0.9));
        pDict->upgradeOpen();
        pDict->setAt(_T("0"), group, id);
		group->close();
        pDict->downgradeOpen();
	}

	pDict->close();
	pNamedObj->close();

	return id;
}