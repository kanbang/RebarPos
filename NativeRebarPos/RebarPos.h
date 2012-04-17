#pragma once

#include "dbmain.h"
#include "dbcurve.h"
#include "geassign.h"
#include "gegbl.h"
#include "gegblabb.h"
#include "gemat3d.h"
#include "acestext.h"
#include "gept2dar.h"
#include "windows.h"
#include "dbxutil.h"

// The following is part of the code used to export a poly API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef POLYSAMP
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

// The "DLLIMPEXP" is only required for exporting a poly API or using
// the exported API.  It is not necessary for any custom classes that
// are not exporting an API of their own.
//
class DLLIMPEXP CRebarPos: public  AcDbEntity
{
public:
    ACRX_DECLARE_MEMBERS(CRebarPos);
    
    //*************************************************************************
    // Constructors and destructor
    //*************************************************************************
    
    CRebarPos();
    virtual ~CRebarPos();
       
    //*************************************************************************
    // Methods specific to AsdkPoly 
    //*************************************************************************
    
    const AcGePoint3d& getCenter() const;
    Acad::ErrorStatus  setCenter     (const AcGePoint3d& cen);

    const TCHAR*	       name()	     const;
    Acad::ErrorStatus  setName	     (const TCHAR* pName);

    const AcDbObjectId&	styleId()     const;
    
    Acad::ErrorStatus  setTextStyle  (const AcDbObjectId& );

    //*************************************************************************
    // Overridden methods from AcDbObject
    //*************************************************************************
    
    virtual Acad::ErrorStatus	dwgInFields(AcDbDwgFiler* filer);
    virtual Acad::ErrorStatus	dwgOutFields(AcDbDwgFiler* filer) const;
    
    virtual Acad::ErrorStatus	dxfInFields(AcDbDxfFiler* filer);
    virtual Acad::ErrorStatus	dxfOutFields(AcDbDxfFiler* filer) const;


protected:
    virtual Acad::ErrorStatus subGetOsnapPoints(
        AcDb::OsnapMode       osnapMode,
        Adesk::GsMarker       gsSelectionMark,
        const AcGePoint3d&    pickPoint,
        const AcGePoint3d&    lastPoint,
        const AcGeMatrix3d&   viewXform,
        AcGePoint3dArray&     snapPoints,
        AcDbIntArray&         geomIds) const;

    virtual Acad::ErrorStatus   subGetGripPoints(AcGePoint3dArray&     gripPoints,
        AcDbIntArray&  osnapModes,
        AcDbIntArray&  geomIds) const;

    virtual Acad::ErrorStatus   subMoveGripPointsAt(const AcDbIntArray& indices,
        const AcGeVector3d&     offset);

    virtual void                subList() const;

    virtual Acad::ErrorStatus   subTransformBy(const AcGeMatrix3d& xform);

    virtual Acad::ErrorStatus	subExplode(AcDbVoidPtrArray& entitySet) const;

    virtual Adesk::Boolean      subWorldDraw(AcGiWorldDraw*	mode);
    
    //*************************************************************************
    // Overridden methods from AcDbObject
    //*************************************************************************
    
    virtual Acad::ErrorStatus subDeepClone(AcDbObject* pOwnerObject,
        AcDbObject*& pClonedObject,
        AcDbIdMapping& idMap,
        Adesk::Boolean isPrimary
        = Adesk::kTrue) const;
    
    virtual Acad::ErrorStatus subWblockClone(AcRxObject* pOwnerObject,
        AcDbObject*& pClonedObject,
        AcDbIdMapping& idMap,
        Adesk::Boolean isPrimary
        = Adesk::kTrue) const;
    
    virtual Acad::ErrorStatus subGetClassID(CLSID* pClsid) const;

private:

    // These are here because otherwise dllexport tries to export the
    // private methods of AcDbObject.  They're private in AcDbObject
    // because vc5 and vc6 do not properly support array new and delete.
    // The "vector deleting dtor" gets optimized into a scalar deleting
    // dtor by the linker if the server dll does not call vector delete.
    // The linker shouldn't do that, because it doesn't know that the
    // object won't be passed to some other dll which *does* do vector
    // delete.
    //
#ifdef MEM_DEBUG
#undef new
#undef delete
#endif
    void *operator new[](size_t nSize) { return 0;}
    void operator delete[](void *p) {};
    void *operator new[](size_t nSize, const TCHAR *file, int line) { return 0;}
#ifdef MEM_DEBUG
#define new DEBUG_NEW
#define delete DEBUG_DELETE
#endif

    AcGePoint3d   mCenter;
    
    TCHAR*	  mpName;
    AcDbHardPointerId  mTextStyle;       
};


//- This line allows us to get rid of the .def file in ARX projects
#ifndef _WIN64
#pragma comment(linker, "/export:_acrxGetApiVersion,PRIVATE")
#else
#pragma comment(linker, "/export:acrxGetApiVersion,PRIVATE")
#endif