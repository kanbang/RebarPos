//-----------------------------------------------------------------------------
//----- RebarPos.cpp : Implementation of CRebarPos
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

#include "RebarPos.h"

#include <initguid.h>
#include "..\COMRebarPos\COMRebarPos_i.c"
#include <basetsd.h>
#include "ac64bithelpers.h"

Adesk::UInt32 CRebarPos::kCurrentVersionNumber = 1;

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_DXF_DEFINE_MEMBERS(CRebarPos, AcDbEntity,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, REBARPOS,
	"RebarPos2.0\
	|Product Desc:     RebarPos Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CRebarPos::CRebarPos() :
	m_BasePoint(0, 0, 0), direction(1, 0, 0), up(0, 1, 0), norm(0, 0, 1), m_NoteGrip(-4.2133, 0.75 * -1.075, 0),
	m_ShowLength(Adesk::kTrue), m_ShowMarkerOnly(Adesk::kFalse), isModified(true), m_Text(NULL), m_Length(NULL), m_Key(NULL),
	m_Pos(NULL), m_Count(NULL), m_Diameter(NULL), m_Spacing(NULL), m_Note(NULL), m_Multiplier(1), 
	m_A(NULL), m_B(NULL), m_C(NULL), m_D(NULL), m_E(NULL), m_F(NULL),
	m_ShapeID(AcDbObjectId::kNull), m_GroupID(AcDbObjectId::kNull)
{
}

CRebarPos::~CRebarPos()
{
    acutDelString(m_Key);
    acutDelString(m_Text);
    acutDelString(m_Length);
    acutDelString(m_Pos);
    acutDelString(m_Count);
    acutDelString(m_Diameter);
    acutDelString(m_Spacing);
    acutDelString(m_Note);
    acutDelString(m_A);
    acutDelString(m_B);
    acutDelString(m_C);
    acutDelString(m_D);
    acutDelString(m_E);
    acutDelString(m_F);
}


//*************************************************************************
// Properties
//*************************************************************************
const AcGePoint3d CRebarPos::BasePoint(void) const
{
	assertReadEnabled();
	return m_BasePoint;
}

Acad::ErrorStatus CRebarPos::setBasePoint(const AcGePoint3d newVal)
{
	assertWriteEnabled();
	m_BasePoint = newVal;
	return Acad::eOk;
}

const AcGePoint3d CRebarPos::NoteGrip(void) const
{
	assertReadEnabled();
	return m_NoteGrip;
}

Acad::ErrorStatus CRebarPos::setNoteGrip(const AcGePoint3d newVal)
{
	assertWriteEnabled();
	m_NoteGrip = newVal;
	return Acad::eOk;
}

const ACHAR* CRebarPos::Text(void) const
{
	Calculate();
	return m_Text;
}

const ACHAR* CRebarPos::Length(void) const
{
	Calculate();
	return m_Length;
}

const ACHAR* CRebarPos::Pos(void) const
{
	assertReadEnabled();
	return m_Pos;
}

Acad::ErrorStatus CRebarPos::setPos(const ACHAR* newVal)
{
	assertWriteEnabled();

    acutDelString(m_Pos);
    m_Pos = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Pos);
    }

	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::Note(void) const
{
	assertReadEnabled();
	return m_Note;
}

Acad::ErrorStatus CRebarPos::setNote(const ACHAR* newVal)
{
	assertWriteEnabled();

	acutDelString(m_Note);
    m_Note = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Note);
    }

	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::Count(void) const
{
	assertReadEnabled();
	return m_Count;
}

Acad::ErrorStatus CRebarPos::setCount(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Count);
    m_Count = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Count);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::Diameter(void) const
{
	assertReadEnabled();
	return m_Diameter;
}

Acad::ErrorStatus CRebarPos::setDiameter(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Diameter);
    m_Diameter = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Diameter);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::Spacing(void) const
{
	assertReadEnabled();
	return m_Spacing;
}

Acad::ErrorStatus CRebarPos::setSpacing(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Spacing);
    m_Spacing = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Spacing);
    }
	isModified = true;
	return Acad::eOk;
}

const Adesk::Int32 CRebarPos::Multiplier(void) const
{
	assertReadEnabled();
	return m_Multiplier;
}

Acad::ErrorStatus CRebarPos::setMultiplier(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Multiplier = newVal;
	isModified = true;
	return Acad::eOk;
}

const Adesk::Boolean CRebarPos::ShowLength(void) const
{
	assertReadEnabled();
	return m_ShowLength;
}

Acad::ErrorStatus CRebarPos::setShowLength(const Adesk::Boolean newVal)
{
	assertWriteEnabled();
	m_ShowLength = newVal;
	isModified = true;
	return Acad::eOk;
}

const Adesk::Boolean CRebarPos::ShowMarkerOnly(void) const
{
	assertReadEnabled();
	return m_ShowMarkerOnly;
}

Acad::ErrorStatus CRebarPos::setShowMarkerOnly(const Adesk::Boolean newVal)
{
	assertWriteEnabled();
	m_ShowMarkerOnly = newVal;
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::A(void) const
{
	assertReadEnabled();
	return m_A;
}

Acad::ErrorStatus CRebarPos::setA(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_A);
    m_A = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_A);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::B(void) const
{
	assertReadEnabled();
	return m_B;
}

Acad::ErrorStatus CRebarPos::setB(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_B);
    m_B = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_B);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::C(void) const
{
	assertReadEnabled();
	return m_C;
}

Acad::ErrorStatus CRebarPos::setC(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_C);
    m_C = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_C);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::D(void) const
{
	assertReadEnabled();
	return m_D;
}

Acad::ErrorStatus CRebarPos::setD(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_D);
    m_D = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_D);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::E(void) const
{
	assertReadEnabled();
	return m_E;
}

Acad::ErrorStatus CRebarPos::setE(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_E);
    m_E = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_E);
    }
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::F(void) const
{
	assertReadEnabled();
	return m_F;
}

Acad::ErrorStatus CRebarPos::setF(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_F);
    m_F = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_F);
    }
	isModified = true;
	return Acad::eOk;
}

const AcDbObjectId& CRebarPos::ShapeId(void) const
{
	assertReadEnabled();
	return m_ShapeID;
}

Acad::ErrorStatus CRebarPos::setShapeId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_ShapeID = newVal;
	isModified = true;
	return Acad::eOk;
}

const AcDbObjectId& CRebarPos::GroupId(void) const
{
	assertReadEnabled();
	return m_GroupID;
}

Acad::ErrorStatus CRebarPos::setGroupId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_GroupID = newVal;
	isModified = true;
	return Acad::eOk;
}

const ACHAR* CRebarPos::PosKey() const
{
	Calculate();
	return m_Key;
}

//*************************************************************************
// Overridden methods from AcDbEntity
//*************************************************************************

Acad::ErrorStatus CRebarPos::subGetOsnapPoints(
    AcDb::OsnapMode       osnapMode,
    Adesk::GsMarker       gsSelectionMark,
    const AcGePoint3d&    pickPoint,
    const AcGePoint3d&    lastPoint,
    const AcGeMatrix3d&   viewXform,
    AcGePoint3dArray&     snapPoints,
    AcDbIntArray&         /*geomIds*/) const
{
	assertReadEnabled();

	if(gsSelectionMark == 0)
        return Acad::eOk;

	if(osnapMode == AcDb::kOsModeIns)
		snapPoints.append(m_BasePoint);

	return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
	assertReadEnabled();
	gripPoints.append(m_BasePoint);
	gripPoints.append(m_NoteGrip);
	return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
    if(indices.length()== 0 || offset.isZeroLength())
        return Acad::eOk;

	assertWriteEnabled();

	// If there are more than one hot points or base point is hot, transform the entire entity
	if(indices.length() > 1 || (indices.length() == 1 && indices[0] == 0))
		transformBy(AcGeMatrix3d::translation(offset));

	// Transform the note grip
	if(indices.length() == 1 && indices[0] == 1)
		m_NoteGrip.transformBy(AcGeMatrix3d::translation(offset));

	return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subTransformBy(const AcGeMatrix3d& xform)
{
	assertWriteEnabled();
	
	m_BasePoint.transformBy(xform);
	m_NoteGrip.transformBy(xform);
	direction.transformBy(xform);
	up.transformBy(xform);

	// Text always left to right
	if(direction.x < 0)
	{
		AcGeMatrix3d mirror = AcGeMatrix3d::kIdentity;
		mirror.setToMirroring(AcGeLine3d(m_BasePoint, up));
		m_BasePoint.transformBy(mirror);
		m_NoteGrip.transformBy(mirror);
		direction.transformBy(mirror);
		up.transformBy(mirror);
	}

	// Text always upright
	if(up.y < 0)
	{
		AcGeMatrix3d mirror = AcGeMatrix3d::kIdentity;
		mirror.setToMirroring(AcGeLine3d(m_BasePoint, direction));
		m_BasePoint.transformBy(mirror);
		m_NoteGrip.transformBy(mirror);
		direction.transformBy(mirror);
		up.transformBy(mirror);
	}

	// Calculate normal
	norm = direction.crossProduct(up);

	return Acad::eOk;
}

void CRebarPos::subList() const
{
    assertReadEnabled();

	// Call parent first
    AcDbEntity::subList();

    AcGePoint3d pt;

	// Base point
    acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Base Point:"));
	pt = m_BasePoint;
    acdbEcs2Ucs(asDblArray(pt), asDblArray(pt), asDblArray(norm), Adesk::kFalse);
    acutPrintf(_T("X = %-9.16q0, Y = %-9.16q0, Z = %-9.16q0\n"), pt.x, pt.y, pt.z);

	// TODO: List all properties
	// Pos
	if ((m_Pos != NULL) && (m_Pos[0] != _T('\0')))
	{
		acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Pos Marker:"));
		acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), m_Pos);
	}
}

Acad::ErrorStatus CRebarPos::subExplode(AcDbVoidPtrArray& entitySet) const
{
    assertReadEnabled();

	// TODO: Fix this
    Acad::ErrorStatus es = Acad::eOk;

    AcDbText *text ;

    if ((m_Pos != NULL) && (m_Pos[0] != _T('\0')))
    {
		/*
        if (mTextStyle != AcDbObjectId::kNull)
            text = new AcDbText(m_BasePoint, m_Pos, mTextStyle, 0, direction.angleTo (AcGeVector3d (1, 0, 0)));
        else
            text = new AcDbText(m_BasePoint, m_Pos, mTextStyle, direction.length() / 20, direction.angleTo (AcGeVector3d (1, 0, 0)));
		*/
		text = new AcDbText(m_BasePoint, m_Pos, AcDbObjectId::kNull, direction.length() / 20, direction.angleTo(AcGeVector3d(1, 0, 0)));
        entitySet.append(text) ;
    }

    return es;
}

Adesk::Boolean CRebarPos::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();

	// TODO: Fix this
    if (worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }


	/*
    AcGiTextStyle textStyle;
    AcDbObjectId id = styleId();

    if (id != AcDbObjectId::kNull)
        if (rx_getTextStyle(textStyle, id) != Acad::eOk)
            id = AcDbObjectId::kNull;

    if ((pName != NULL) && (pName[0] != _T('\0')))
    {
        worldDraw->subEntityTraits().setSelectionMarker(1);
        AcGeVector3d direction(1, 0, 0);
        AcGeVector3d normal(0, 0, 1);

        if (id != AcDbObjectId::kNull)
            worldDraw->geometry().text(mCenter, normal, direction,
                 pName, -1, 0, textStyle);
        else
            worldDraw->geometry().text(mCenter, normal, direction,
                 direction.length() / 20, 1, 0, pName);
    }
	*/
	if ((m_Pos != NULL) && (m_Pos[0] != _T('\0')))
	{
		worldDraw->geometry().text(m_BasePoint, norm, direction, direction.length() / 20, 1, 0, m_Pos);
	}

	// Direction markers
	worldDraw->subEntityTraits().setColor(6);
	worldDraw->geometry().circle(m_BasePoint, 0.2, AcGeVector3d::kZAxis);

    return Adesk::kTrue; // Don't call viewportDraw().
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CRebarPos::dwgInFields(AcDbDwgFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CRebarPos::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		pFiler->readItem(&m_BasePoint);
		pFiler->readItem(&m_NoteGrip);
		pFiler->readItem(&direction);
		pFiler->readItem(&up);
		norm = direction.crossProduct(up);
		
		acutDelString(m_Pos);
		acutDelString(m_Note);
		acutDelString(m_Count);
		acutDelString(m_Diameter);
		acutDelString(m_Spacing);
		acutDelString(m_A);
		acutDelString(m_B);
		acutDelString(m_C);
		acutDelString(m_D);
		acutDelString(m_E);
		acutDelString(m_F);

		pFiler->readString(&m_Pos);
		pFiler->readString(&m_Note);
		pFiler->readString(&m_Count);
		pFiler->readString(&m_Diameter);
		pFiler->readString(&m_Spacing);
		pFiler->readItem(&m_Multiplier);
		pFiler->readItem(&m_ShowLength);
		pFiler->readItem(&m_ShowMarkerOnly);
		pFiler->readString(&m_A);
		pFiler->readString(&m_B);
		pFiler->readString(&m_C);
		pFiler->readString(&m_D);
		pFiler->readString(&m_E);
		pFiler->readString(&m_F);

		// Styles
		pFiler->readHardPointerId(&m_ShapeID);
		pFiler->readHardPointerId(&m_GroupID);
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CRebarPos::dwgOutFields(AcDbDwgFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CRebarPos::kCurrentVersionNumber);

	pFiler->writeItem(m_BasePoint);
	pFiler->writeItem(m_NoteGrip);
	pFiler->writeItem(direction);
	pFiler->writeItem(up);

	if (m_Pos)
		pFiler->writeString(m_Pos);
	else
		pFiler->writeString(_T(""));
	if(m_Note)
		pFiler->writeString(m_Note);
	else
		pFiler->writeString(_T(""));
	if(m_Count)
		pFiler->writeString(m_Count);
	else
		pFiler->writeString(_T(""));
	if(m_Diameter)
		pFiler->writeString(m_Diameter);
	else
		pFiler->writeString(_T(""));
	if(m_Spacing)
		pFiler->writeString(m_Spacing);
	else
		pFiler->writeString(_T(""));
	pFiler->writeItem(m_Multiplier);
	pFiler->writeItem(m_ShowLength);
	pFiler->writeItem(m_ShowMarkerOnly);
	if(m_A)
		pFiler->writeString(m_A);
	else
		pFiler->writeString(_T(""));
	if(m_B)
		pFiler->writeString(m_B);
	else
		pFiler->writeString(_T(""));
	if(m_C)
		pFiler->writeString(m_C);
	else
		pFiler->writeString(_T(""));
	if(m_D)
		pFiler->writeString(m_D);
	else
		pFiler->writeString(_T(""));
	if(m_E)
		pFiler->writeString(m_E);
	else
		pFiler->writeString(_T(""));
	if(m_F)
		pFiler->writeString(m_F);
	else
		pFiler->writeString(_T(""));

	// Style
	pFiler->writeHardPointerId(m_ShapeID);
	pFiler->writeHardPointerId(m_GroupID);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CRebarPos::dxfInFields(AcDbDxfFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbEntity::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("RebarPos")))
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
	if (version > CRebarPos::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	AcGePoint3d t_BasePoint, t_NoteGrip;
	AcGeVector3d t_Direction, t_Up;
	ACHAR* t_Pos = NULL;
	ACHAR* t_Note = NULL;
	ACHAR* t_Count = NULL;
	ACHAR* t_Diameter = NULL;
	ACHAR* t_Spacing = NULL;
	Adesk::Int32 t_Multiplier;
	Adesk::Boolean t_ShowLength;
	Adesk::Boolean t_ShowMarkerOnly;
	ACHAR* t_A = NULL;
	ACHAR* t_B = NULL;
	ACHAR* t_C = NULL;
	ACHAR* t_D = NULL;
	ACHAR* t_E = NULL;
	ACHAR* t_F = NULL;
	AcDbObjectId t_ShapeID, t_GroupID;

    while ((es == Acad::eOk) && ((es = pFiler->readResBuf(&rb)) == Acad::eOk))
    {
        switch (rb.restype) 
		{
        case AcDb::kDxfXCoord:
            t_BasePoint = asPnt3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 1:
            t_NoteGrip = asPnt3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 2:
			t_Direction = asVec3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 3:
            t_Up = asVec3d(rb.resval.rpoint);
            break;

        case AcDb::kDxfXTextString:
            acutUpdString(rb.resval.rstring, t_Pos);
            break;
        case AcDb::kDxfXTextString + 1:
            acutUpdString(rb.resval.rstring, t_Note);
            break;
        case AcDb::kDxfXTextString + 2:
            acutUpdString(rb.resval.rstring, t_Count);
            break;
        case AcDb::kDxfXTextString + 3:
            acutUpdString(rb.resval.rstring, t_Diameter);
            break;
        case AcDb::kDxfXTextString + 4:
            acutUpdString(rb.resval.rstring, t_Spacing);
            break;

        case AcDb::kDxfInt32 + 1:
            t_Multiplier = rb.resval.rlong;
            break;

        case AcDb::kDxfBool:
			t_ShowLength = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
            break;
        case AcDb::kDxfBool + 1:
			t_ShowMarkerOnly = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
            break;

        case AcDb::kDxfXTextString + 5:
            acutUpdString(rb.resval.rstring, t_A);
            break;
        case AcDb::kDxfXTextString + 6:
            acutUpdString(rb.resval.rstring, t_B);
            break;
        case AcDb::kDxfXTextString + 7:
            acutUpdString(rb.resval.rstring, t_C);
            break;
        case AcDb::kDxfXTextString + 8:
            acutUpdString(rb.resval.rstring, t_D);
            break;
        case AcDb::kDxfXTextString + 9:
            acutUpdString(rb.resval.rstring, t_E);
            break;
        case AcDb::kDxfXTextString + 10:
            acutUpdString(rb.resval.rstring, t_F);
            break;

        case AcDb::kDxfHardPointerId:
            acdbGetObjectId(t_ShapeID, rb.resval.rlname);
            break;
        case AcDb::kDxfHardPointerId + 1:
            acdbGetObjectId(t_GroupID, rb.resval.rlname);
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
	m_BasePoint = t_BasePoint;
	m_NoteGrip = t_NoteGrip;
	direction = t_Direction;
	up = t_Up;
	setPos(t_Pos);
	setNote(t_Note);
	setCount(t_Count);
	setDiameter(t_Diameter);
	setSpacing(t_Spacing);
	m_Multiplier = t_Multiplier;
	m_ShowLength = t_ShowLength;
	m_ShowMarkerOnly = t_ShowMarkerOnly;
	setA(t_A);
	setB(t_B);
	setC(t_C);
	setD(t_D);
	setE(t_E);
	setF(t_F);
	m_ShapeID = t_ShapeID;
	m_GroupID = t_GroupID;

	acutDelString(t_Pos);
	acutDelString(t_Note);
	acutDelString(t_Count);
	acutDelString(t_Diameter);
	acutDelString(t_Spacing);
	acutDelString(t_A);
	acutDelString(t_B);
	acutDelString(t_C);
	acutDelString(t_D);
	acutDelString(t_E);
	acutDelString(t_F);

    return es;
}

Acad::ErrorStatus CRebarPos::dxfOutFields(AcDbDxfFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("RebarPos"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CRebarPos::kCurrentVersionNumber);

	// Geometry
	pFiler->writePoint3d(AcDb::kDxfXCoord, m_BasePoint);
	pFiler->writePoint3d(AcDb::kDxfXCoord + 1, m_NoteGrip);
	// Use max precision when writing out direction vectors
	pFiler->writeVector3d(AcDb::kDxfXCoord + 2, direction, 16);
	pFiler->writeVector3d(AcDb::kDxfXCoord + 3, up, 16);

	// Properties
	if(m_Pos)
		pFiler->writeString(AcDb::kDxfXTextString, m_Pos);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if(m_Note)
		pFiler->writeString(AcDb::kDxfXTextString + 1, m_Note);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 1, _T(""));
	if(m_Count)
		pFiler->writeString(AcDb::kDxfXTextString + 2, m_Count);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 2, _T(""));
	if(m_Diameter)
		pFiler->writeString(AcDb::kDxfXTextString + 3, m_Diameter);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 3, _T(""));
	if(m_Spacing)
		pFiler->writeString(AcDb::kDxfXTextString + 4, m_Spacing);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 4, _T(""));
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_Multiplier);
	pFiler->writeItem(AcDb::kDxfBool, m_ShowLength);
	pFiler->writeItem(AcDb::kDxfBool + 1, m_ShowMarkerOnly);
	if(m_A)
		pFiler->writeString(AcDb::kDxfXTextString + 5, m_A);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 5, _T(""));
	if(m_B)
		pFiler->writeString(AcDb::kDxfXTextString + 6, m_B);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 6, _T(""));
	if(m_C)
		pFiler->writeString(AcDb::kDxfXTextString + 7, m_C);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 7, _T(""));
	if(m_D)
		pFiler->writeString(AcDb::kDxfXTextString + 8, m_D);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 8, _T(""));
	if(m_E)
		pFiler->writeString(AcDb::kDxfXTextString + 9, m_E);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 9, _T(""));
	if(m_F)
		pFiler->writeString(AcDb::kDxfXTextString + 10, m_F);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 10, _T(""));
	
    // Styles
	pFiler->writeItem(AcDb::kDxfHardPointerId, m_ShapeID);
    pFiler->writeItem(AcDb::kDxfHardPointerId + 1, m_GroupID);

	return pFiler->filerStatus();
}


Acad::ErrorStatus CRebarPos::subDeepClone(AcDbObject*    pOwner,
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
    AcDbIdPair idPair(objectId(), (AcDbObjectId)NULL,
                      false, isPrim);
    if (idMap.compute(idPair) && (idPair.value() != NULL))
        return Acad::eOk;    

    // Create the clone
    //
    CRebarPos *pClone = (CRebarPos*)isA()->create();
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
        AcDbBlockTableRecord *pBTR =
            AcDbBlockTableRecord::cast(pOwner);
        if (pBTR != NULL)
        {
            pBTR->appendAcDbEntity(pClone);
            bOwnerXlated = true;
        }
        else
        {
            pOwner->database()->addAcDbObject(pClone);
        }
    } else {
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
    while (filer.getNextOwnedObject(id)) {

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
        pSubObject->deepClone(pClonedObject,
                              pClonedSubObject,
                              idMap, Adesk::kFalse);

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


Acad::ErrorStatus CRebarPos::subWblockClone(AcRxObject*    pOwner,
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
        return AcDbEntity::subWblockClone(pOwner, pClonedObject,
            idMap, isPrimary);

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

    if (   idMap.deepCloneContext() == AcDb::kDcXrefBind
        && ownerId() == pspace)
        return Acad::eOk;
    
    // If this object is in the idMap and is already
    // cloned, then return.
    //
    bool isPrim = false;
    if (isPrimary)
        isPrim = true;

    AcDbIdPair idPair(objectId(), (AcDbObjectId)NULL,
                      false, isPrim);
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
    CRebarPos *pClone = (CRebarPos*)isA()->create();
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
    if (pBTR != NULL && isPrimary) {
        pBTR->appendAcDbEntity(pClone);
    } else {
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

    idMap.assign(AcDbIdPair(objectId(), pClonedObject->objectId(), Adesk::kTrue,
        isPrim, (Adesk::Boolean)(pOwn != NULL) ));

   pClonedObject->setOwnerId((pOwn != NULL) ?
        pOwn->objectId() : ownerId());

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
    while (filer.getNextHardObject(id)) {

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
        if (pSubObject->database() != database()) {
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
        if (pSubObject->ownerId() == objectId()) {
            pSubObject->wblockClone(pClone,
                                    pClonedSubObject,
                                    idMap, Adesk::kFalse);
        } else {
            pSubObject->wblockClone(pClone->database(),
                                    pClonedSubObject,
                                    idMap, Adesk::kFalse);
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


//return the CLSID of the class here
Acad::ErrorStatus   CRebarPos::subGetClassID(CLSID* pClsid) const
{
    assertReadEnabled();
    *pClsid = CLSID_ComRebarPos;
    return Acad::eOk;
}

const void CRebarPos::Calculate(void) const
{
	if(isModified)
	{
		assertReadEnabled();
/*
		// TODO: Fix this
		m_Length = "1200";

		AcDbObjectPointer<CPosGroup> group(m_GroupID, AcDb::kForRead);
		AcDbObjectPointer<CPosStyle> pstyle(group->Style(), AcDb::kForRead);
		m_Text = m_Pos + m_Count + "T" + m_Diameter;
		if(!m_Spacing.isEmpty())
			m_Text += "/" + m_Spacing;
		if(m_ShowLength && !m_Length.isEmpty())
			m_Text += " L=" + m_Length;
		pstyle->close();
		group->close();

		// Shape code
		AcString shape;
		shape.format(_T("_%i_%i_"), m_ShapeID.handle().low(), m_ShapeID.handle().high());

		m_Key = _T("");
		m_Key = m_Key.concat(m_Diameter);
		m_Key = m_Key.concat(shape);
		m_Key = m_Key.concat(m_A).concat(m_B).concat(m_C).concat(m_D).concat(m_E).concat(m_F);

		// Check if variable length
		m_IsVarLength = (m_A.findOneOf(_T("-~")) != -1) || (m_B.findOneOf(_T("-~")) != -1) ||
			(m_C.findOneOf(_T("-~")) != -1) || (m_D.findOneOf(_T("-~")) != -1) ||
			(m_E.findOneOf(_T("-~")) != -1) || (m_F.findOneOf(_T("-~")) != -1);
*/
		isModified = false;
	}
}