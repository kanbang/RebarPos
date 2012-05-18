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

ACRX_DXF_DEFINE_MEMBERS(CBOQTable, AcDbEntity,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, BOQTABLE,
	"OZOZRebarPos\
	|Product Desc:     Rebar BOQ Table Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CBOQTable::CBOQTable() :
	m_BasePoint(0, 0, 0), m_Direction(1, 0, 0), m_Up(0, 1, 0), m_Normal(0, 0, 1),
	m_Multiplier(1),m_StyleID(AcDbObjectId::kNull), m_Heading(NULL), m_Footing(NULL)
{
}

CBOQTable::~CBOQTable()
{
	ClearRows();
	acutDelString(m_Heading);
	acutDelString(m_Footing);
}


//*************************************************************************
// Properties
//*************************************************************************
const AcGeVector3d& CBOQTable::DirectionVector(void) const
{
	assertReadEnabled();
	return m_Direction;
}

const AcGeVector3d& CBOQTable::UpVector(void) const
{
	assertReadEnabled();
	return m_Up;
}

const AcGeVector3d& CBOQTable::NormalVector(void) const
{
	assertReadEnabled();
	return m_Normal;
}

const double CBOQTable::Scale(void) const
{
	assertReadEnabled();
	return m_Direction.length();
}

Acad::ErrorStatus CBOQTable::setScale(const double newVal)
{
	assertWriteEnabled();
	AcGeMatrix3d xform = AcGeMatrix3d::scaling(newVal / m_Direction.length(), m_BasePoint);
	return transformBy(xform);
}

const double CBOQTable::Width(void) const
{
    assertReadEnabled();
	// TODO
	return 0.0;
}

const double CBOQTable::Height(void) const
{
    assertReadEnabled();
	// TODO
	return 0.0;
}

const AcGePoint3d& CBOQTable::BasePoint(void) const
{
	assertReadEnabled();
	return m_BasePoint;
}

Acad::ErrorStatus CBOQTable::setBasePoint(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_BasePoint = newVal;
	return Acad::eOk;
}

const Adesk::Int32 CBOQTable::Multiplier(void) const
{
	assertReadEnabled();
	return m_Multiplier;
}

Acad::ErrorStatus CBOQTable::setMultiplier(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Multiplier = newVal;
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
	return Acad::eOk;
}

const AcDbObjectId& CBOQTable::StyleId(void) const
{
	assertReadEnabled();
	return m_StyleID;
}

Acad::ErrorStatus CBOQTable::setStyleId(const AcDbObjectId& newVal)
{
	assertWriteEnabled();
	m_StyleID = newVal;
	return Acad::eOk;
}

//*************************************************************************
// Class Methods
//*************************************************************************
void CBOQTable::AddRow(CBOQRow* const row)
{
	assertWriteEnabled();
	m_List.push_back(row);
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
}

void CBOQTable::RemoveRow(const RowListSize index)
{
	assertWriteEnabled();
	delete m_List[index];
	RowListIt it = m_List.begin();
	m_List.erase(it + index);
}

void CBOQTable::ClearRows()
{
	assertWriteEnabled();
	for(RowListIt it = m_List.begin(); it != m_List.end(); it++)
	{
		delete *it;
	}
	m_List.clear();
}

const RowListSize CBOQTable::GetRowCount() const
{
	assertReadEnabled();
	return m_List.size();
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
    AcDbIntArray&         /*geomIds*/) const
{
	assertReadEnabled();

	if(gsSelectionMark == 0)
        return Acad::eOk;

	if(osnapMode == AcDb::kOsModeIns)
		snapPoints.append(m_BasePoint);

	return Acad::eOk;
}

Acad::ErrorStatus CBOQTable::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
	assertReadEnabled();
	gripPoints.append(m_BasePoint);
	return Acad::eOk;
}

Acad::ErrorStatus CBOQTable::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
    if(indices.length() == 0 || offset.isZeroLength())
        return Acad::eOk;

	assertWriteEnabled();

	return transformBy(AcGeMatrix3d::translation(offset));
}

Acad::ErrorStatus CBOQTable::subTransformBy(const AcGeMatrix3d& xform)
{
	assertWriteEnabled();
	
	m_BasePoint.transformBy(xform);
	m_Direction.transformBy(xform);
	m_Up.transformBy(xform);
	m_Normal = m_Direction.crossProduct(m_Up);

	// Correct direction vectors
	double scale = m_Direction.length();
	m_Direction = m_Direction.normal() * scale;
	m_Up = m_Up.normal() * scale;
	m_Normal = m_Normal.normal() * scale;

	return Acad::eOk;
}

void CBOQTable::subList() const
{
    assertReadEnabled();

	// Call parent first
    AcDbEntity::subList();

	// Base point in UCS
    acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Base Point:"));
    AcGePoint3d pt(m_BasePoint);
    acdbWcs2Ucs(asDblArray(pt), asDblArray(pt), false);
    acutPrintf(_T("X = %-9.16q0, Y = %-9.16q0, Z = %-9.16q0\n"), pt.x, pt.y, pt.z);

	// Group
	if(!m_StyleID.isNull())
	{
		Acad::ErrorStatus es;
		AcDbObjectPointer<CBOQStyle> pStyle (m_StyleID, AcDb::kForRead);
		if((es = pStyle.openStatus()) == Acad::eOk)
		{
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Style:"), pStyle->Name());
		}
	}

	// List all properties
	// TODO
}

Acad::ErrorStatus CBOQTable::subExplode(AcDbVoidPtrArray& entitySet) const
{
    assertReadEnabled();
	// TODO
    return Acad::eOk;
}

Adesk::Boolean CBOQTable::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();
	// TODO
    if(worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);
	
	// Transform to match orientation
	worldDraw->geometry().pushModelTransform(trans);
	// Draw items
	worldDraw->subEntityTraits().setSelectionMarker(1);
	AcGiTextStyle style;
	style.setTextSize(1.0);
	style.loadStyleRec();
	worldDraw->geometry().text(AcGePoint3d(0, 0, 0), AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, _T("BOQTable"), -1, Adesk::kFalse, style);

	worldDraw->geometry().popModelTransform();

    return Adesk::kTrue; // Don't call viewportDraw().
}

void CBOQTable::saveAs(AcGiWorldDraw *worldDraw, AcDb::SaveType saveType)
{
    assertReadEnabled();

    if(worldDraw->regenAbort())
	{
        return;
    }

	// TODO

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);
	double scale = m_Direction.length();

	// Draw items
	AcGiTextStyle style;
	style.setTextSize(1.0);
	style.loadStyleRec();

	AcGePoint3d textpt(0, 0, 0);
	textpt.transformBy(trans);
	worldDraw->geometry().text(AcGePoint3d(0, 0, 0), m_Normal, m_Direction, _T("BOQTable"), -1, Adesk::kFalse, style);
}

Acad::ErrorStatus CBOQTable::subGetGeomExtents(AcDbExtents& extents) const
{
    assertReadEnabled();
	// TODO
	// Get ECS extents

	// Transform to WCS

	return Acad::eOk;
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CBOQTable::dwgOutFields(AcDbDwgFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CBOQTable::kCurrentVersionNumber);

	pFiler->writePoint3d(m_BasePoint);
	pFiler->writeVector3d(m_Direction);
	pFiler->writeVector3d(m_Up);

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

	// Style
	pFiler->writeHardPointerId(m_StyleID);

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
		pFiler->writeHardPointerId(row->shapeId);
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dwgInFields(AcDbDwgFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CBOQTable::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		acutDelString(m_Heading);
		acutDelString(m_Footing);

		pFiler->readPoint3d(&m_BasePoint);
		pFiler->readVector3d(&m_Direction);
		pFiler->readVector3d(&m_Up);
		m_Normal = m_Direction.crossProduct(m_Up);
		
		pFiler->readInt32(&m_Multiplier);

		pFiler->readString(&m_Heading);
		pFiler->readString(&m_Footing);

		// Style
		pFiler->readHardPointerId(&m_StyleID);


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
			pFiler->readHardPointerId(&row->shapeId);
			m_List.push_back(row);
		}
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dxfOutFields(AcDbDxfFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("BOQTable"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CBOQTable::kCurrentVersionNumber);

	// Geometry
	pFiler->writePoint3d(AcDb::kDxfXCoord, m_BasePoint);
	// Use max precision when writing out direction vectors
	pFiler->writeVector3d(AcDb::kDxfXCoord + 1, m_Direction, 16);
	pFiler->writeVector3d(AcDb::kDxfXCoord + 2, m_Up, 16);

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

    // Style
	pFiler->writeItem(AcDb::kDxfHardPointerId, m_StyleID);

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
		pFiler->writeObjectId(AcDb::kDxfHardPointerId, row->shapeId);
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dxfInFields(AcDbDxfFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbEntity::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("BOQTable")))
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
	AcGePoint3d t_BasePoint;
	AcGeVector3d t_Direction, t_Up;
	Adesk::Int32 t_Multiplier = 0;
	ACHAR* t_Heading = NULL;
	ACHAR* t_Footing = NULL;
	AcDbObjectId t_StyleID = AcDbObjectId::kNull;
	RowList t_List;
	long count;

	if((es = Utility::ReadDXFPoint(pFiler, AcDb::kDxfXCoord, _T("base point"), t_BasePoint)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFVector(pFiler, AcDb::kDxfXCoord + 1, _T("direction vector"), t_Direction)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFVector(pFiler, AcDb::kDxfXCoord + 2, _T("up vector"), t_Up)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32 + 1, _T("multiplier"), t_Multiplier)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("heading"), t_Heading)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString + 1, _T("footing"), t_Footing)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("style id"), t_StyleID)) != Acad::eOk) return es;
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
		if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("row shape id"), row->shapeId)) != Acad::eOk) return es;
		t_List.push_back(row);
    }

	// Successfully read DXF codes; set object properties.
	m_BasePoint = t_BasePoint;
	m_Direction = t_Direction;
	m_Up = t_Up;
	m_Normal = m_Direction.crossProduct(m_Up);

	m_Multiplier = t_Multiplier;

	setHeading(t_Heading);
	setFooting(t_Footing);
	m_StyleID = t_StyleID;

	ClearRows();
	m_List.clear();
	m_List = t_List;

	acutDelString(t_Heading);
	acutDelString(t_Footing);

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
        return AcDbEntity::subWblockClone(pOwner, pClonedObject, idMap, isPrimary);

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

//return the CLSID of the class here
Acad::ErrorStatus CBOQTable::subGetClassID(CLSID* pClsid) const
{
    assertReadEnabled();
	// See the interface definition file for the CLASS ID
	CLSID clsid = { 0xba77cfff, 0x274, 0x4d4c, { 0xbf, 0xe2, 0x64, 0xa5, 0x73, 0x1b, 0xad, 0x37 } };
    *pClsid = clsid;
    return Acad::eOk;
}
