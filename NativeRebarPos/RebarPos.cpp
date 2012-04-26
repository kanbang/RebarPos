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
#include "dbobjptr.h"

#include "acdb.h"
#include "dbidmap.h"
#include "adesk.h"

#include "dbapserv.h"
#include "appinfo.h"
#include "tchar.h"
#include "AcString.h"

#include "Utility.h"
#include "Calculator.h"
#include "RebarPos.h"
#include "PosShape.h"
#include "PosGroup.h"
#include "PosStyle.h"

#include <initguid.h>
#include "..\COMRebarPos\COMRebarPos_i.c"
#include <basetsd.h>
#include "ac64bithelpers.h"

#include <sstream>

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
	m_BasePoint(0, 0, 0), direction(1, 0, 0), up(0, 1, 0), norm(0, 0, 1), m_NoteGrip(0, -1.6, 0),
	m_DisplayStyle(CRebarPos::ALL), isModified(true), m_Length(NULL), m_Key(NULL),
	m_Pos(NULL), m_Count(NULL), m_Diameter(NULL), m_Spacing(NULL), m_Note(NULL), m_Multiplier(1), 
	m_A(NULL), m_B(NULL), m_C(NULL), m_D(NULL), m_E(NULL), m_F(NULL), m_IsVarLength(false),
	m_ShapeID(AcDbObjectId::kNull), m_GroupID(AcDbObjectId::kNull), 
	circleRadius(1.125), partSpacing(0.15), m_MinLength(0), m_MaxLength(0)
{
}

CRebarPos::~CRebarPos()
{
    acutDelString(m_Key);
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
const AcGeVector3d& CRebarPos::DirectionVector(void) const
{
	assertReadEnabled();
	return direction;
}

const AcGeVector3d& CRebarPos::UpVector(void) const
{
	assertReadEnabled();
	return up;
}

const AcGeVector3d& CRebarPos::NormalVector(void) const
{
	assertReadEnabled();
	return norm;
}

const AcGePoint3d& CRebarPos::BasePoint(void) const
{
	assertReadEnabled();
	return m_BasePoint;
}

Acad::ErrorStatus CRebarPos::setBasePoint(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_BasePoint = newVal;
	return Acad::eOk;
}

const AcGePoint3d& CRebarPos::NoteGrip(void) const
{
	assertReadEnabled();
	return m_NoteGrip;
}

Acad::ErrorStatus CRebarPos::setNoteGrip(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_NoteGrip = newVal;
	return Acad::eOk;
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

const CRebarPos::DisplayStyle CRebarPos::Display(void) const
{
	assertReadEnabled();
	return m_DisplayStyle;
}

Acad::ErrorStatus CRebarPos::setDisplay(const CRebarPos::DisplayStyle newVal)
{
	assertWriteEnabled();
	m_DisplayStyle = newVal;
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

const ACHAR* CRebarPos::Length(void) const
{
	assertReadEnabled();
	return m_Length;
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
	assertReadEnabled();
	Calculate();
	return m_Key;
}

const bool CRebarPos::IsVarLength(void) const
{
	assertReadEnabled();
	return m_IsVarLength;
}

const double CRebarPos::MinLength(void) const
{
	assertReadEnabled();
	return m_MinLength;
}

const double CRebarPos::MaxLength(void) const
{
	assertReadEnabled();
	return m_MaxLength;
}

//*************************************************************************
// Methods
//*************************************************************************

/// Determines which part is under the given point
const CRebarPos::PosSubEntityType CRebarPos::HitTest(const AcGePoint3d& pt0) const
{
	// Transform to text coordinate system
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, direction, up, norm);
	if(trans.isSingular())
	{
		return CRebarPos::NONE;
	}
	trans.invert();
	AcGePoint3d pt(pt0);
	pt.transformBy(trans);
		
	CDrawParams p;
	if(lastDrawList.size() != 0)
	{
		for(DrawListSize i = 0; i < lastDrawList.size(); i++)
		{
			p = lastDrawList[i];
			if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
			{
				return (CRebarPos::PosSubEntityType)p.type;
			}
		}
	}
	// Check group text
	p = lastGroupDraw;
	if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
	{
		return CRebarPos::GROUP;
	}
	// Check multiplier text
	p = lastMultiplierDraw;
	if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
	{
		return CRebarPos::MULTIPLIER;
	}

	// Transform to note coordinate system
	trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_NoteGrip, direction, up, norm);
	if(trans.isSingular())
	{
		return CRebarPos::NONE;
	}
	trans.invert();
	pt = pt0;
	pt.transformBy(trans);
	p = lastNoteDraw;
	if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
	{
		return CRebarPos::NOTE;
	}

	return CRebarPos::NONE;
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
    if(indices.length() == 0 || offset.isZeroLength())
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

	// TODO: Fix mirroring around midpoint
	// TODO: Correction should be based on UCS
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

	// List all properties
	if (m_Pos != NULL)
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Pos Marker:"), m_Pos);
	if (m_Count != NULL)
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Count:"), m_Count);
	if (m_Diameter != NULL)
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Diameter:"), m_Diameter);
	if (m_Spacing != NULL)
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Spacing:"), m_Spacing);
	acutPrintf(_T("%18s%16s %i\n"), _T(/*MSG0*/""), _T("Multiplier:"), m_Multiplier);

	if(!m_ShapeID.isNull())
	{
		Acad::ErrorStatus es;
		AcDbObjectPointer<AcDbDictionary> pNamedObj (database()->namedObjectsDictionaryId(), AcDb::kForRead);
		if((es = pNamedObj.openStatus()) == Acad::eOk)
		{
			AcDbDictionary* pDict = NULL;
			if((es = pNamedObj->getAt(CPosShape::GetTableName(), (AcDbObject *&)pDict, AcDb::kForRead)) == Acad::eOk)
			{
				ACHAR* shapeName = NULL;
				pDict->nameAt(m_ShapeID, shapeName);
				if (shapeName != NULL)
					acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Shape:"), shapeName);
				acutDelString(shapeName);
				pDict->close();
			}
			pNamedObj->close();
		}
	}

	if ((m_A != NULL) && (m_A[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("A Length:"), m_A);
	if ((m_B != NULL) && (m_B[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("B Length:"), m_B);
	if ((m_C != NULL) && (m_C[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("C Length:"), m_C);
	if ((m_D != NULL) && (m_D[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("D Length:"), m_D);
	if ((m_E != NULL) && (m_E[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("E Length:"), m_E);
	if ((m_F != NULL) && (m_F[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("F Length:"), m_F);
	if ((m_Length != NULL) && (m_Length[0] != _T('\0')))
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Total Length:"), m_Length);
}

Acad::ErrorStatus CRebarPos::subExplode(AcDbVoidPtrArray& entitySet) const
{
    assertReadEnabled();

	if(lastDrawList.size() == 0)
	{
		return Acad::eInvalidInput;
	}

	Acad::ErrorStatus es;

	// Open group and style
	AcDbObjectPointer<CPosGroup> pGroup (m_GroupID, AcDb::kForRead);
	if((es = pGroup.openStatus()) != Acad::eOk)
	{
		return es;
	}
	AcDbObjectPointer<CPosStyle> pStyle (pGroup->StyleId(), AcDb::kForRead);
	if((es = pStyle.openStatus()) != Acad::eOk)
	{
		return es;
	}
	AcDbObjectId textStyle = pStyle->TextStyleId();
	AcDbObjectId noteStyle = pStyle->NoteStyleId();

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, direction, up, norm);
	AcGeMatrix3d noteTrans = AcGeMatrix3d::kIdentity;
	noteTrans.setCoordSystem(m_NoteGrip, direction, up, norm);

    AcDbText* text;
	CDrawParams p;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		p = lastDrawList[i];
		text = new AcDbText(AcGePoint3d(p.x, p.y, 0), p.text, textStyle, 1.0);
		text->setColorIndex(p.color);
		text->transformBy(trans);
		entitySet.append(text);
		if(p.hasCircle)
		{
			AcDbCircle* circle;
			circle = new AcDbCircle(AcGePoint3d(p.x + p.w / 2, p.y + p.h / 2, 0), AcGeVector3d::kZAxis, circleRadius);
			circle->setColorIndex(lastCircleColor);
			circle->transformBy(trans);
			entitySet.append(circle);
		}
	}
	p = lastNoteDraw;
	text = new AcDbText(AcGePoint3d(p.x, p.y, 0), p.text, noteStyle, 1.0 * pStyle->NoteScale());
	text->setColorIndex(p.color);
	text->transformBy(noteTrans);
	entitySet.append(text);

    return Acad::eOk;
}

Adesk::Boolean CRebarPos::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();

    if(worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }

	if(m_GroupID.isNull())
	{
		return Adesk::kTrue;
	}

	Acad::ErrorStatus es;

	// Get layers
	AcDbObjectId zero = Utility::GetZeroLayer();
	AcDbObjectId defpoints = Utility::GetDefpointsLayer();

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, direction, up, norm);
	AcGeMatrix3d noteTrans = AcGeMatrix3d::kIdentity;
	noteTrans.setCoordSystem(m_NoteGrip, direction, up, norm);

	// Update if required
	if(!worldDraw->isDragging())
	{
		// Get group name
		lastGroupDraw.text.setEmpty();
		AcDbObjectPointer<AcDbDictionary> pNamedObj (database()->namedObjectsDictionaryId(), AcDb::kForRead);
		if(pNamedObj->has(CPosGroup::GetTableName()))
		{
			AcDbDictionary* pDict;
			if((es = pNamedObj->getAt(CPosGroup::GetTableName(), (AcDbObject *&)pDict, AcDb::kForRead)) == Acad::eOk)
			{
				pDict->nameAt(m_GroupID, lastGroupDraw.text);
				pDict->close();
			}
		}

		// Open group and get style id
		AcDbObjectId styleID = AcDbObjectId::kNull;
		AcDbObjectPointer<CPosGroup> pGroup (m_GroupID, AcDb::kForRead);
		if((es = pGroup.openStatus()) != Acad::eOk)
		{
			return es;
		}
		styleID = pGroup->StyleId();
		lastCurrentGroup = pGroup->Current();
		if(styleID.isNull())
		{
			return Adesk::kTrue;
		}

		// Open style
		AcDbObjectPointer<CPosStyle> pStyle (styleID, AcDb::kForRead);
		if((es = pStyle.openStatus()) != Acad::eOk)
		{
			return es;
		}

		// Create text styles
		if (pStyle->TextStyleId() != AcDbObjectId::kNull)
			Utility::MakeGiTextStyle(lastTextStyle, pStyle->TextStyleId());
		if (pStyle->NoteStyleId() != AcDbObjectId::kNull)
			Utility::MakeGiTextStyle(lastNoteStyle, pStyle->NoteStyleId());
		lastTextStyle.setTextSize(1.0);
		lastNoteStyle.setTextSize(1.0 * pStyle->NoteScale());
		lastTextStyle.loadStyleRec();
		lastNoteStyle.loadStyleRec();

		// Calculate lengths if modified
		if(isModified)
		{
			Calculate();

			// Rebuild draw list
			lastDrawList.clear();
			if(m_DisplayStyle == CRebarPos::ALL && pStyle->Formula() != NULL)
			{
				lastDrawList = ParseFormula(pStyle->Formula());
			}
			else if(m_DisplayStyle == CRebarPos::WITHOUTLENGTH && pStyle->FormulaWithoutLength() != NULL)
			{
				lastDrawList = ParseFormula(pStyle->FormulaWithoutLength());
			}
			else if(m_DisplayStyle == CRebarPos::MARKERONLY && pStyle->FormulaPosOnly() != NULL)
			{
				lastDrawList = ParseFormula(pStyle->FormulaPosOnly());
			}
			lastNoteDraw.text = m_Note;

			if(m_Multiplier == 0)
				lastMultiplierDraw.text = _T("-");
			else
				lastMultiplierDraw.text.format(_T("%dx"), m_Multiplier);
		}

		// Set colors
		lastCircleColor = pStyle->CircleColor();
		lastGroupDraw.color = pStyle->GroupColor();
		lastMultiplierDraw.color = pStyle->MultiplierColor();
		lastGroupHighlightColor = pStyle->CurrentGroupHighlightColor();
		for(DrawListSize i = 0; i < lastDrawList.size(); i++)
		{
			CDrawParams p = lastDrawList[i];
			switch(p.type)
			{
			case CRebarPos::POS:
				p.color = pStyle->PosColor();
				break;
			case CRebarPos::GROUP:
				p.color = pStyle->GroupColor();
				break;
			case CRebarPos::MULTIPLIER:
				p.color = pStyle->MultiplierColor();
				break;
			default:
				p.color = pStyle->TextColor();
			}
			lastDrawList[i] = p;
		}
		lastNoteDraw.color = pStyle->NoteColor();
	}

	// Transform to match object orientation
	worldDraw->geometry().pushModelTransform(trans);
	// Measure items
	double x = 0;
	double y = 0;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		CDrawParams p = lastDrawList[i];
		AcGePoint2d ext = lastTextStyle.extents(p.text, Adesk::kTrue, -1, Adesk::kFalse);
		if(p.hasCircle)
		{
			p.x = x + (2.0 * circleRadius - ext.x ) / 2.0;
		}
		else
		{
			p.x = x;
		}
		p.y = y;
		p.w = ext.x;
		p.h = ext.y;
		if(p.hasCircle)
		{
			x += 2.0 * circleRadius + partSpacing;
		}
		else
		{
			x += ext.x + partSpacing;
		}
		lastDrawList[i] = p;
	}
	// Measure group text
	lastTextStyle.setTextSize(0.4);
	lastTextStyle.loadStyleRec();
	AcGePoint2d gext = lastTextStyle.extents(lastGroupDraw.text, Adesk::kTrue, -1, Adesk::kFalse);
	lastGroupDraw.x = 0;
	lastGroupDraw.y = -0.8;
	lastGroupDraw.w = gext.x;
	lastGroupDraw.h = gext.y;
    // Measure multiplier text
	AcGePoint2d mext = lastTextStyle.extents(lastMultiplierDraw.text, Adesk::kTrue, -1, Adesk::kFalse);
	lastMultiplierDraw.x = 0;
	lastMultiplierDraw.y = 1.4;
	lastMultiplierDraw.w = gext.x;
	lastMultiplierDraw.h = gext.y;

	// Reset transform
	worldDraw->geometry().popModelTransform();

	// Transform to match note orientation
	worldDraw->geometry().pushModelTransform(noteTrans);
	// Measure note text
	AcGePoint2d noteExt = lastNoteStyle.extents(lastNoteDraw.text, Adesk::kTrue, -1, Adesk::kFalse);
	lastNoteDraw.x = 0;
	lastNoteDraw.y = 0;
	lastNoteDraw.w = noteExt.x;
	lastNoteDraw.h = noteExt.y;

	// Reset transform
	worldDraw->geometry().popModelTransform();

	// Nothing to draw
	if(lastDrawList.empty())
	{
		return Adesk::kTrue;
	}

	// Transform to match object orientation
	worldDraw->geometry().pushModelTransform(trans);
	// Highlight current group
	if(lastCurrentGroup == Adesk::kTrue)
	{
		AcGiFillType filltype = worldDraw->subEntityTraits().fillType();
		worldDraw->subEntityTraits().setFillType(kAcGiFillAlways);
		worldDraw->subEntityTraits().setLayer(defpoints);
		worldDraw->subEntityTraits().setColor(lastGroupHighlightColor);
		AcGePoint3d rec[4];
		CDrawParams p = lastDrawList.at(lastDrawList.size() - 1);
		rec[0].set(-partSpacing, -partSpacing, 0);
		rec[1].set(p.x + p.w + partSpacing, -partSpacing, 0);
		rec[2].set(p.x + p.w + partSpacing, 1.0 + partSpacing, 0);
		rec[3].set(-partSpacing, 1.0 + partSpacing, 0);
		worldDraw->geometry().polygon(4, rec);
		worldDraw->subEntityTraits().setFillType(filltype);
	}
	// Draw items
	worldDraw->subEntityTraits().setLayer(zero);
	lastTextStyle.setTextSize(1.0);
	lastTextStyle.loadStyleRec();
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		CDrawParams p = lastDrawList.at(i);
		worldDraw->subEntityTraits().setColor(p.color);
		worldDraw->geometry().text(AcGePoint3d(p.x, p.y, 0), AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, p.text, -1, Adesk::kFalse, lastTextStyle);
		if(p.hasCircle)
		{
			worldDraw->subEntityTraits().setColor(lastCircleColor);
			worldDraw->geometry().circle(AcGePoint3d(p.x + p.w / 2, p.y + p.h / 2, 0), circleRadius, AcGeVector3d::kZAxis);
		}
	}
	// Group name
	worldDraw->subEntityTraits().setLayer(defpoints);
	lastTextStyle.setTextSize(0.4);
	lastTextStyle.loadStyleRec();
	worldDraw->subEntityTraits().setColor(lastGroupDraw.color);
	worldDraw->geometry().text(AcGePoint3d(lastGroupDraw.x, lastGroupDraw.y, 0), AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, lastGroupDraw.text, -1, Adesk::kFalse, lastTextStyle);
	// Multiplier
	worldDraw->subEntityTraits().setColor(lastMultiplierDraw.color);
	worldDraw->geometry().text(AcGePoint3d(lastMultiplierDraw.x, lastMultiplierDraw.y, 0), AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, lastMultiplierDraw.text, -1, Adesk::kFalse, lastTextStyle);
	// Reset transform
	worldDraw->geometry().popModelTransform();

	// Transform to match note orientation
	worldDraw->geometry().pushModelTransform(noteTrans);
	// Draw note text
	worldDraw->subEntityTraits().setColor(lastNoteDraw.color);
	worldDraw->subEntityTraits().setLayer(zero);
	worldDraw->geometry().text(AcGePoint3d(lastNoteDraw.x, lastNoteDraw.y, 0), AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, m_Note, -1, Adesk::kFalse, lastNoteStyle);
	// Reset transform
	worldDraw->geometry().popModelTransform();

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
		int display = 0;
		pFiler->readItem(&display);
		m_DisplayStyle = (CRebarPos::DisplayStyle)display;
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
	pFiler->writeInt32(m_DisplayStyle);
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
	int t_Display;
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

        case AcDb::kDxfInt32 + 2:
            t_Display = rb.resval.rlong;
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
	m_DisplayStyle = (CRebarPos::DisplayStyle)t_Display;
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
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, m_DisplayStyle);
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

//*************************************************************************
// Helper methods
//*************************************************************************

const void CRebarPos::Calculate(void) const
{
	if(!isModified)
		return;

	assertReadEnabled();

	// Open group and shape
	Acad::ErrorStatus es;
	AcDbObjectPointer<CPosGroup> pGroup (m_GroupID, AcDb::kForRead);
	if((es = pGroup.openStatus()) != Acad::eOk)
	{
		return;
	}
	bool bending = (pGroup->Bending() == Adesk::kTrue);
	int precision = pGroup->Precision();
	CPosGroup::DrawingUnits drawingUnits = pGroup->DrawingUnit();
	CPosGroup::DrawingUnits displayUnits = pGroup->DisplayUnit();
	AcDbObjectPointer<CPosShape> pShape (m_ShapeID, AcDb::kForRead);
	const ACHAR* formula;
	if((es = pShape.openStatus()) != Acad::eOk)
	{
		return;
	}
	if(bending)
	{
		formula = pShape->FormulaBending();
	}
	else
	{
		formula = pShape->Formula();
	}
	int fieldCount = pShape->Fields();

	// Scale from drawing units to MM
	double scale = 1.0;
	switch(drawingUnits)
	{
	case CPosGroup::MM:
		scale *= 1.0;
		break;
	case CPosGroup::CM:
		scale *= 10.0;
		break;
	}

	// Calculate length
	CalcTotalLength(formula, fieldCount, scale, precision, m_MinLength, m_MaxLength, m_IsVarLength);

	// Scale from MM to display units
	scale = 1.0;
	switch(displayUnits)
	{
	case CPosGroup::MM:
		scale /= 1.0;
		break;
	case CPosGroup::CM:
		scale /= 10.0;
		break;
	}
	m_MinLength *= scale;
	m_MaxLength *= scale;

	// Set text
	std::wstring strL1;
	Utility::DoubleToStr(m_MinLength, precision, strL1);
	std::wstring strL2;
	Utility::DoubleToStr(m_MaxLength, precision, strL2);
	std::wstring strL;
	if(m_IsVarLength)
	{
		strL = strL1 + L"~" + strL2;
	}
	else
	{
		strL = strL1;
	}
	acutUpdString(strL.c_str(), m_Length);

	// Shape code
	AcString shape;
	shape.format(_T("_%i_%i_"), m_ShapeID.handle().low(), m_ShapeID.handle().high());

	AcString key(_T(""));
	key = key.concat(m_Diameter);
	key = key.concat(shape);
	key = key.concat(m_A).concat(m_B).concat(m_C).concat(m_D).concat(m_E).concat(m_F);
	acutUpdString(key, m_Key);

	isModified = false;
}

void CRebarPos::CalcTotalLength(const ACHAR* str, int fieldCount, double scale, int precision, double& minLength, double& maxLength, bool& isVar) const
{
	AcString length(str);
	std::wstring length1(length);
	std::wstring length2(length);

	// Calculate piece lengths
	double A1 = 0.0, A2 = 0.0, B1 = 0.0, B2 = 0.0, C1 = 0.0, C2 = 0.0, D1 = 0.0, D2 = 0.0, E1 = 0.0, E2 = 0.0, F1 = 0.0, F2 = 0.0;
	bool Avar = false, Bvar = false, Cvar = false, Dvar = false, Evar = false, Fvar = false;
	if(fieldCount >= 1)
		CalcLength(m_A, scale, A1, A2, Avar);
	if(fieldCount >= 2)
		CalcLength(m_B, scale, B1, B2, Bvar);
	if(fieldCount >= 3)
		CalcLength(m_C, scale, C1, C2, Cvar);
	if(fieldCount >= 4)
		CalcLength(m_D, scale, D1, D2, Dvar);
	if(fieldCount >= 5)
		CalcLength(m_E, scale, E1, E2, Evar);
	if(fieldCount >= 6)
		CalcLength(m_F, scale, F1, F2, Fvar);

	isVar = Avar || Bvar || Cvar || Dvar || Evar || Fvar;

	// Replace lengths
	std::wstring strA1, strA2, strB1, strB2, strC1, strC2;
	std::wstring strD1, strD2, strE1, strE2, strF1, strF2;
	Utility::DoubleToStr(A1, precision, strA1);
	Utility::DoubleToStr(A2, precision, strA2);
	Utility::DoubleToStr(B1, precision, strB1);
	Utility::DoubleToStr(C2, precision, strB2);
	Utility::DoubleToStr(C1, precision, strC1);
	Utility::DoubleToStr(A2, precision, strC2);
	Utility::DoubleToStr(D1, precision, strD1);
	Utility::DoubleToStr(D2, precision, strD2);
	Utility::DoubleToStr(E1, precision, strE1);
	Utility::DoubleToStr(E2, precision, strE2);
	Utility::DoubleToStr(F1, precision, strF1);
	Utility::DoubleToStr(F2, precision, strF2);
	Utility::ReplaceString(length1, L"A", strA1);
	Utility::ReplaceString(length2, L"A", strA2);
	Utility::ReplaceString(length1, L"B", strB1);
	Utility::ReplaceString(length2, L"B", strB2);
	Utility::ReplaceString(length1, L"C", strC1);
	Utility::ReplaceString(length2, L"C", strC2);
	Utility::ReplaceString(length1, L"D", strD1);
	Utility::ReplaceString(length2, L"D", strD2);
	Utility::ReplaceString(length1, L"E", strE1);
	Utility::ReplaceString(length2, L"E", strE2);
	Utility::ReplaceString(length1, L"F", strF1);
	Utility::ReplaceString(length2, L"F", strF2);

	// Replace diameter and radius
	double d = 0.0;
	if(m_Diameter != NULL && m_Diameter[0] == _T('\0'))
		d = Utility::StrToDouble(m_Diameter);
	double r = BendingRadius(d);
	std::wstring strD, strR;
	Utility::DoubleToStr(d, precision, strD);
	Utility::DoubleToStr(r, precision, strR);
	Utility::ReplaceString(length1, L"d", strD);
	Utility::ReplaceString(length2, L"d", strD);
	Utility::ReplaceString(length1, L"r", strR);
	Utility::ReplaceString(length2, L"r", strR);

	// Calculate lengths
	minLength = CCalculator::evaluate(length1);
	maxLength = CCalculator::evaluate(length2);
}

void CRebarPos::CalcLength(const ACHAR* str, double scale, double& minLength, double& maxLength, bool& isVar) const
{
	AcString length(str);
	std::wstring length1, length2;

	// Get variable lengths
	int i = length.find(_T("~"));
	if(i != -1)
	{
		length1 = length.substr(0, i);
		length2 = length.substr(i + 1, length.length() - i - 1);
		isVar = true;
	}
	else
	{
		length1 = length;
		length2 = length;
		isVar = false;
	}
	
	// Calculate lengths
	minLength = CalcConsLength(length1.c_str(), scale);
	maxLength = CalcConsLength(length2.c_str(), scale);
}

double CRebarPos::CalcConsLength(const ACHAR* str, double scale) const
{
	std::wstring length(str);

	// Replace diameter and radius
	double d = 0.0;
	if(m_Diameter != NULL && m_Diameter[0] == _T('\0'))
		d = Utility::StrToDouble(m_Diameter);
	double r = BendingRadius(d);
	// Convert units
	std::wstring strD, strR;
	d /= scale;
	r /= scale;
	Utility::DoubleToStr(d, strD);
	Utility::DoubleToStr(r, strR);
	Utility::ReplaceString(length, L"d", strD);
	Utility::ReplaceString(length, L"r", strR);
	
	// Calculate length
	return CCalculator::evaluate(length) * scale;
}
const double CRebarPos::BendingRadius(const double d) const
{
	if(d <= 16.0)
		return (2.0 * d);
	else
		return (3.5 * d);
}

const DrawList CRebarPos::ParseFormula(const ACHAR* formula) const
{
	assertReadEnabled();

	// Formula text
	// [M] Pos marker
	// [MM] Pos marker with double digits
	// [N] Bar count
	// [D] Bar diameter
	// [S] Spacing
	// [L] Total length
	// [M:C] Draw a circle around the item
	// Anything else is considered plain text and printed as-is
	// ["T":D] Prints the letter T before the diameter
	// e.g. [M:C][N]["T":D]["/":S] ["L=":L]

	DrawList list;
	list.clear();

	// First pass: separate parts
	AcString str(formula);
	AcString part;
	bool indeco = false;
	for(unsigned int i = 0; i < str.length(); i++)
	{
		AcString c = str.substr(i, 1);
		if((!indeco && c == _T('[')) || (indeco && (c == _T(']'))) || (i == str.length() - 1))
		{
			if((i == str.length() - 1) && (c != _T('[')) && (c != _T(']')))
			{
				part += c;
			}
			if(!indeco && !part.isEmpty())
			{
				CDrawParams p(0, part);
				list.push_back(p);
			}
			else if(indeco && !part.isEmpty())
			{
				CDrawParams p(1, part);
				list.push_back(p);
			}
			indeco = (c == _T('['));

			part.setEmpty();
		}
		else
		{
			part += c;
		}
	}

	// Second pass: separate format strings and decorators
	DrawList finallist;
	finallist.clear();
	for(DrawListSize j = 0; j < list.size(); j++)
	{
		CDrawParams p = list[j];
		if(p.type == 0)
		{
			p.type = CRebarPos::NONE;
			p.hasCircle = false;
			finallist.push_back(p);
		}
		else if(p.type == 1)
		{
			p.type = CRebarPos::NONE;
			part.setEmpty();
			std::vector<AcString> parts;
			bool inliteral = false;
			for(unsigned int i = 0; i < p.text.length(); i++)
			{
				AcString c = p.text.substr(i, 1);
				if(!inliteral && (c == _T('"')))
				{
					inliteral = true;
				}
				else if(inliteral && (c == _T('"')))
				{
					parts.push_back(part);
					part.setEmpty();
					inliteral = false;
				}
				else if(c == _T(':') || (i == p.text.length() - 1))
				{
					if(i == p.text.length() - 1)
					{
						part += c;
					}
					if(part == _T("M"))
					{
						p.type = CRebarPos::POS;
						AcString pos(m_Pos);
						if(pos.length() == 0) pos = _T(" ");
						parts.push_back(pos);
					}
					else if(part == _T("MM"))
					{
						p.type = CRebarPos::POS;
						AcString pos(m_Pos);
						if(pos.length() == 0) pos = _T("  ");
						if(pos.length() == 1) pos.precat(_T("0"));
						parts.push_back(pos);
					}
					else if(part == _T("N") && m_Count != NULL && m_Count[0] != _T('\0'))
					{
						p.type = CRebarPos::COUNT;
						parts.push_back(m_Count);
					}
					else if(part == _T("D") && m_Diameter != NULL && m_Diameter[0] != _T('\0'))
					{
						p.type = CRebarPos::DIAMETER;
						parts.push_back(m_Diameter);
					}
					else if(part == _T("S") && m_Spacing != NULL && m_Spacing[0] != _T('\0'))
					{
						p.type = CRebarPos::SPACING;
						parts.push_back(m_Spacing);
					}
					else if(part == _T("L") && m_Length != NULL && m_Length[0] != _T('\0'))
					{
						p.type = CRebarPos::LENGTH;
						parts.push_back(m_Length);
					}
					else if(part == _T("C"))
					{
						p.hasCircle = true;
					}
					part.setEmpty();
				}
				else
				{
					part += c;
				}
			}
			if(p.type != CRebarPos::NONE)
			{
				p.text.setEmpty();
				for(std::vector<AcString>::size_type k = 0; k < parts.size(); k++)
				{
					p.text.append(parts[k]);
				}
				finallist.push_back(p);
			}
		}
	}

	return finallist;
}
