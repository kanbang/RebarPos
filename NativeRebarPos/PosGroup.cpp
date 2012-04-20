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
	|Product Desc:     PosStyle Entity\
	|Company:          OZOZ");

//-----------------------------------------------------------------------------
CPosGroup::CPosGroup () : m_Bending(Adesk::kFalse), m_MaxBarLength(12),
	m_DrawingUnit(CPosGroup::MM), m_DisplayUnit(CPosGroup::MM), m_StyleID(AcDbObjectId::kNull)
{ }

CPosGroup::~CPosGroup () { }

//*************************************************************************
// Properties
//*************************************************************************
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

const AcDbObjectId& CPosGroup::StyleId(void) const
{
	assertReadEnabled();
	return m_StyleID;
}

Acad::ErrorStatus CPosGroup::setStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_StyleID = newVal;
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
	pFiler->writeItem(m_Bending);
	pFiler->writeDouble(m_MaxBarLength);
	pFiler->writeInt32(m_DrawingUnit);
	pFiler->writeInt32(m_DisplayUnit);

    // Style
    pFiler->writeHardPointerId(m_StyleID);

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
		// Properties
		pFiler->readItem(&m_Bending);
		pFiler->readItem(&m_MaxBarLength);
		int drawingunit = 0;
		pFiler->readItem(&drawingunit);
		m_DrawingUnit = (DrawingUnits)drawingunit;
		int displayunit = 0;
		pFiler->readItem(&displayunit);
		m_DisplayUnit = (DrawingUnits)displayunit;

		// Style
		pFiler->readHardPointerId(&m_StyleID);
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
	pFiler->writeItem(AcDb::kDxfBool, m_Bending);
	pFiler->writeDouble(AcDb::kDxfXReal, m_MaxBarLength);
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_DrawingUnit);
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, m_DisplayUnit);

    // Style
    pFiler->writeItem(AcDb::kDxfHardPointerId, m_StyleID);

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
	Adesk::Boolean t_Bending;
	double t_MaxBarLength;
	int t_DrawingUnit;
	int t_DisplayUnit;
	AcDbObjectId t_StyleID;
	
    while ((es == Acad::eOk) && ((es = pFiler->readResBuf(&rb)) == Acad::eOk))
    {
        switch (rb.restype) 
		{
        case AcDb::kDxfBool:
			t_Bending = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
            break;
		case AcDb::kDxfXReal:
			t_MaxBarLength = rb.resval.rreal;
			break;
        case AcDb::kDxfInt32 + 1:
			t_DrawingUnit = rb.resval.rlong;
            break;
        case AcDb::kDxfInt32 + 2:
            t_DisplayUnit = rb.resval.rlong;
            break;

        case AcDb::kDxfHardPointerId:
            acdbGetObjectId(t_StyleID, rb.resval.rlname);
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
    m_Bending = t_Bending;
    m_MaxBarLength = t_MaxBarLength;
    m_DrawingUnit = (DrawingUnits)t_DrawingUnit;
    m_DisplayUnit = (DrawingUnits)t_DisplayUnit;
    m_StyleID = t_StyleID;

	return es;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************

ACHAR* CPosGroup::GetTableName()
{
	return Table_Name;
}

AcDbDictionary* CPosGroup::GetDictionary()
{
	assert(Table_Name != NULL);

	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();

	// Create the dictionary if not present
	AcDbDictionary *pNamedobj = NULL;
	pDb->getNamedObjectsDictionary(pNamedobj, AcDb::kForRead);
	AcDbDictionary *pDict;
	if (pNamedobj->getAt(Table_Name, (AcDbObject*&) pDict, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		pDict = new AcDbDictionary;
		AcDbObjectId DictId;
		pNamedobj->upgradeOpen();
		pNamedobj->setAt(Table_Name, pDict, DictId);
		pNamedobj->downgradeOpen();
		pDict->downgradeOpen();
	}
	pNamedobj->close();

	return pDict;
}

AcDbObjectId CPosGroup::CreateDefault(void)
{
	AcDbDictionary* pDict = GetDictionary();

	AcDbObjectId id;
	// Create a new entry if not present
	if(pDict->numEntries() == 0)
	{
		CPosGroup *pObject = new CPosGroup();
		pDict->upgradeOpen();
		pDict->setAt(_T("0"), pObject, id);
		pDict->downgradeOpen();
		pObject->close();
	}
	else
	{
		AcDbDictionaryIterator* it = pDict->newIterator();
		it->next();
		id = it->objectId();
		delete it;
	}
	pDict->close();

	return id;
}
