//-----------------------------------------------------------------------------
//----- GenericTable.h : Declaration of the CGenericTable
//-----------------------------------------------------------------------------
#pragma once

#include <vector>

#include "TableCell.h"

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

/// ---------------------------------------------------------------------------
/// The CGenericTable represents a table in the drawing. It contains
/// cells with formatting and borders.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CGenericTable : public  AcDbEntity
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CGenericTable);
    
protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
    /// Constructors and destructor
	CGenericTable(void);
	~CGenericTable(void);

	
private:
	/// Property backing fields
	int m_Rows;
	int m_Columns;
	std::vector<CTableCell*> m_Cells;

	/// Locals

public:
	/// Matrix operators
	const CTableCell* operator()(int i, int j) const
	{
		return m_Cells[i * m_Columns + j];
	}

	CTableCell* operator()(int i, int j)
	{
		return m_Cells[i * m_Columns + j];
	}

private:
    // These are here because otherwise dllexport tries to export the
    // private methods of AcDbObject. They're private in AcDbObject
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
