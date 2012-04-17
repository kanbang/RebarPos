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

#include "Utility.h"
#include "RebarPos.h"

#include "acdb.h"
#include "dbidmap.h"
#include "adesk.h"

#include "dbapserv.h"
#include "appinfo.h"
#include "tchar.h"

#include <initguid.h>
#include "..\COMRebarPos\COMRebarPos_i.c"
#include <basetsd.h>
#include "ac64bithelpers.h"

#define VERSION 1

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_DXF_DEFINE_MEMBERS(CRebarPos, AcDbCurve,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, REBARPOS,
	"RebarPos2.0\
	|Product Desc:     RebarPos Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CRebarPos::CRebarPos() :
    mCenter(0, 0, 0), mTextStyle(AcDbObjectId::kNull), mpName(NULL)
{
}

CRebarPos::~CRebarPos()
{
    acutDelString(mpName);
}


//*************************************************************************
// Methods specific to CRebarPos 
//*************************************************************************
const AcGePoint3d& CRebarPos::getCenter() const
{
    assertReadEnabled();
	return mCenter;
}
Acad::ErrorStatus CRebarPos::setCenter(const AcGePoint3d& cen)
{
    assertWriteEnabled();
    mCenter = cen;
    return Acad::eOk;
}

const TCHAR* CRebarPos::name() const
{
    assertReadEnabled();
    return mpName;
}
Acad::ErrorStatus CRebarPos::setName(const TCHAR* pName)
{
    assertWriteEnabled();
    
    acutDelString(mpName);
    mpName = NULL;

    if(pName != NULL)
    {
        acutUpdString(pName, mpName);
    }

    return Acad::eOk;
}

const AcDbObjectId& CRebarPos::styleId() const
{
    assertReadEnabled();
    return mTextStyle;
}

Acad::ErrorStatus CRebarPos::setTextStyle(const AcDbObjectId& tsId)
{
    assertWriteEnabled();
    mTextStyle = tsId;
    return Acad::eOk;
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
    snapPoints.append(mCenter);
    return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
    assertReadEnabled();
    gripPoints.append(mCenter);
    return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
    if (indices.length()== 0 || offset.isZeroLength())
        return Acad::eOk;

    assertWriteEnabled();
    return transformBy(AcGeMatrix3d::translation(offset));
}

void CRebarPos::subList() const
{
    assertReadEnabled();

	// Call parent first
    AcDbEntity::subList();

    acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Center:"));

    //AcGePoint3d cent(center().x,center().y,elevation());
    //acdbEcs2Ucs(asDblArray(cent), asDblArray(cent), asDblArray(normal()),Adesk::kFalse);
    //acutPrintf(_T("X = %-9.16q0, Y = %-9.16q0, Z = %-9.16q0\n"), 
    //                  cent.x, cent.y);
}

Acad::ErrorStatus CRebarPos::subTransformBy(const AcGeMatrix3d& xform)
{
    assertWriteEnabled();
    mCenter.transformBy(xform);
    return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subExplode(AcDbVoidPtrArray& entitySet) const
{
    assertReadEnabled();

    Acad::ErrorStatus es = Acad::eOk;

    AcDbText *text ;

    if ((mpName != NULL) && (mpName[0] != _T('\0')))
    {
        AcGeVector3d direction(1, 0, 0);

        if (mTextStyle != AcDbObjectId::kNull)
            text = new AcDbText (mCenter, mpName, mTextStyle, 0, direction.angleTo (AcGeVector3d (1, 0, 0))) ;
        else
            text =new AcDbText (mCenter, mpName, mTextStyle, direction.length() / 20, direction.angleTo (AcGeVector3d (1, 0, 0))) ;

        entitySet.append (text) ;
    }

    return es;
}

Adesk::Boolean CRebarPos::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();

    if (worldDraw->regenAbort()) {
        return Adesk::kTrue;
    }

    const TCHAR *pName = name();
    AcDbObjectId id = styleId();

    AcGiTextStyle textStyle;

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

    return Adesk::kTrue; // Don't call viewportDraw().
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CRebarPos::dwgInFields(AcDbDwgFiler* filer)
{
    assertWriteEnabled();
    Acad::ErrorStatus es;

    if ((es = AcDbEntity::dwgInFields(filer)) != Acad::eOk) 
    {
        return es;
    }

    // Object Version - must always be the first item.
    //
    Adesk::Int16 version;
    filer->readItem(&version);
    if (version > VERSION)
      return Acad::eMakeMeProxy;

    switch (version)
    {
    case 1:
        filer->readPoint3d(&mCenter);
        acutDelString(mpName);
        filer->readString(&mpName);
        filer->readHardPointerId(&mTextStyle);
        break;
    default:
        assert(false);
    }
    return filer->filerStatus();
}

Acad::ErrorStatus CRebarPos::dwgOutFields(AcDbDwgFiler* filer) const
{
    assertReadEnabled();
    Acad::ErrorStatus es;

    if ((es = AcDbEntity::dwgOutFields(filer)) != Acad::eOk)
    {
        return es;
    }

    // Object Version - must always be the first item.
    // 
    Adesk::Int16 version = VERSION;
    filer->writeItem(version);

    filer->writePoint3d(mCenter);
    if (mpName)
        filer->writeString(mpName);
    else
        filer->writeString(_T(""));

    // mTextStyle is a hard pointer id, so filing it out to
    // the purge filer (kPurgeFiler) prevents purging of
    // this object.
    //
    filer->writeHardPointerId(mTextStyle);
    return filer->filerStatus();
}

Acad::ErrorStatus CRebarPos::dxfInFields(AcDbDxfFiler* filer)
{
    assertWriteEnabled();
    Acad::ErrorStatus es = Acad::eOk;
    resbuf rb;

    if ((AcDbEntity::dxfInFields(filer) != Acad::eOk) || !filer->atSubclassData(_T("RebarPos")))
    {
        return filer->filerStatus();
    }

    // Object Version
    Adesk::Int16 version;
    filer->readItem(&rb);
    if (rb.restype != AcDb::kDxfInt16) 
    {
        filer->pushBackItem();
        filer->setError(Acad::eInvalidDxfCode,
            _T("\nError: expected group code %d (version)"), AcDb::kDxfInt16);
        return filer->filerStatus();
    }
    version = rb.resval.rint;
    if (version > VERSION)
        return Acad::eMakeMeProxy;

    AcGePoint3d cen3d;
    AcDbObjectId textStyle;
    TCHAR * pName = NULL;
    while ((es == Acad::eOk) && ((es = filer->readResBuf(&rb)) == Acad::eOk))
    {
        switch (rb.restype) {

        case AcDb::kDxfXCoord:
            cen3d = asPnt3d(rb.resval.rpoint);
            break;

        case AcDb::kDxfText:
            acutUpdString(rb.resval.rstring,pName);
            break;

        case AcDb::kDxfHardPointerId:
            acdbGetObjectId(textStyle, rb.resval.rlname);
            break;

        default:
            // An unrecognized group. Push it back so that
            // the subclass can read it again.
            filer->pushBackItem();
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

    mTextStyle = textStyle;
    setName(pName);
    acutDelString(pName);
    mCenter.set(cen3d.x, cen3d.y, cen3d.z);

    return es;
}

Acad::ErrorStatus CRebarPos::dxfOutFields(AcDbDxfFiler* filer) const
{
    assertReadEnabled();
    Acad::ErrorStatus es;

    if ((es = AcDbEntity::dxfOutFields(filer)) != Acad::eOk)
    {
        return es;
    }
    filer->writeItem(AcDb::kDxfSubclass, _T("RebarPos"));

    // Object Version
    //
    Adesk::Int16 version = VERSION;
    filer->writeInt16(AcDb::kDxfInt16, version);
    filer->writePoint3d(AcDb::kDxfXCoord, mCenter);
    //always use max precision when writing out the normal
    //filer->writeVector3d(AcDb::kDxfNormalX, mPlaneNormal,16);
    filer->writeString(AcDb::kDxfText, mpName);
    filer->writeItem(AcDb::kDxfHardPointerId, mTextStyle);
    return filer->filerStatus();
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

    // If this is an Xref bind operation and this AsdkPoly
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
    *pClsid = CLSID_ComPolygon;
    return Acad::eOk;
}
