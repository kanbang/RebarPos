//-----------------------------------------------------------------------------
//----- BOQTable.h : Declaration of the CBOQTable
//-----------------------------------------------------------------------------
#pragma once

#pragma warning( push )
#pragma warning( disable : 4100 )
#include "acgi.h"
#pragma warning( pop )

#include <vector>
#include <map>
#include "BOQRow.h"
#include "BOQStyle.h"
#include "DrawParams.h"
#include "GenericTable.h"

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

typedef std::vector<CBOQRow*> RowList;
typedef std::vector<CBOQRow*>::size_type RowListSize;
typedef std::vector<CBOQRow*>::iterator RowListIt;
typedef std::vector<CBOQRow*>::const_iterator RowListConstIt;

/// ---------------------------------------------------------------------------
/// The CBOQTable represents the BOQ list of rebar markers in the drawing.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CBOQTable : public  CGenericTable
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CBOQTable);
    
protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
    /// Constructors and destructor    
    CBOQTable();
    virtual ~CBOQTable();
       
public:
	enum ColumnType
	{ 
		NONE = 0,
		POS = 1,
		POSDD = 2,
		COUNT = 3,
		DIAMETER = 4,
		LENGTH = 5,
		SHAPE = 6,
		TOTALLENGTH = 7
	};

private:
	/// Property backing fields
	Adesk::Int32 m_Multiplier;

	ACHAR* m_Heading;
	ACHAR* m_Footing;

	AcDbHardPointerId m_StyleID;

	RowList m_List;

protected:
	/// Calculates table when modified.
	void UpdateTable(void);

private:
	/// Parses column definition text
	const std::vector<ColumnType> ParseColumns(const ACHAR* columns) const;

public:
	/// Gets or sets the BOQ multiplier
	const Adesk::Int32 Multiplier(void) const;
	Acad::ErrorStatus setMultiplier(const Adesk::Int32 newVal);

	/// Gets or sets heading text
	const ACHAR* Heading(void) const;
	Acad::ErrorStatus setHeading(const ACHAR* newVal);

	/// Gets or sets heading text
	const ACHAR* Footing(void) const;
	Acad::ErrorStatus setFooting(const ACHAR* newVal);

	/// Gets or sets the BOQ style
	const AcDbObjectId& StyleId(void) const;
	Acad::ErrorStatus setStyleId(const AcDbObjectId& newVal);

public:
	/// Adds a row.
	void AddRow(CBOQRow* const row);

	/// Gets the row at the given index.
	const CBOQRow* GetRow(const RowListSize index) const;

	/// Sets the row at the given index.
	void SetRow(const RowListSize index, CBOQRow* const row);

	/// Gets the count of rows.
	const RowListSize GetRowCount() const;

	/// Removes the row at the given index.
	void RemoveRow(const RowListSize index);

	/// Clears all rows.
	void ClearRows();

public:
	/// AcDbEntity overrides: database    
    virtual Acad::ErrorStatus	dwgInFields(AcDbDwgFiler* filer);
    virtual Acad::ErrorStatus	dwgOutFields(AcDbDwgFiler* filer) const;
    
    virtual Acad::ErrorStatus	dxfInFields(AcDbDxfFiler* filer);
    virtual Acad::ErrorStatus	dxfOutFields(AcDbDxfFiler* filer) const;

protected:
	/// AcDbEntity overrides: geometry
    virtual void                subList() const;

    /// Overridden methods from AcDbObject    
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
    void *operator new[](size_t /* nSize */) { return 0;}
    void operator delete[](void* /* p */) {};
    void *operator new[](size_t /* nSize */, const TCHAR* /* file*/ , int /* line */) { return 0;}
#ifdef MEM_DEBUG
#define new DEBUG_NEW
#define delete DEBUG_DELETE
#endif
};
