//-----------------------------------------------------------------------------
//----- DictEntry.h : Declaration of the CDictEntry
//-----------------------------------------------------------------------------
#pragma once

#include "dbmain.h"
#include <vector>

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

typedef std::vector<AcDbObjectId> ObjectIdList;
typedef std::vector<AcDbObjectId>::iterator ObjectIdListIt;

/// ---------------------------------------------------------------------------
/// The CDictEntry represents a template entry derived from AcDbObject.
/// ---------------------------------------------------------------------------
template <class T>
class DLLIMPEXP CDictEntry : public AcDbObject
{
public:
	CDictEntry()
	{
	}
	virtual ~CDictEntry()
	{
	}

protected:
	/// Static members
	static ACHAR* Table_Name;

public:
	/// Static members
	static ACHAR* GetTableName()
	{
		return CDictEntry<T>::Table_Name;
	}

protected:
	static AcDbDictionary* GetDictionary()
	{
		AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();

		// Create the dictionary if not present
		AcDbDictionary *pNamedobj = NULL;
		pDb->getNamedObjectsDictionary(pNamedobj, AcDb::kForRead);
		AcDbDictionary *pDict;
		if (pNamedobj->getAt(CDictEntry<T>::Table_Name, (AcDbObject*&) pDict, AcDb::kForRead) == Acad::eKeyNotFound)
		{
			pDict = new AcDbDictionary;
			AcDbObjectId DictId;
			pNamedobj->upgradeOpen();
			pNamedobj->setAt(CDictEntry<T>::Table_Name, pDict, DictId);
			pNamedobj->downgradeOpen();
		}
		pNamedobj->close();

		return pDict;
	}

public:
	/// Saves the current entry in the table.
	static AcDbObjectId Save(const ACHAR* name, T* pEntry)
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();

		pDict->upgradeOpen();
		AcDbObjectId id;
		pEntry->upgradeOpen();
		pDict->setAt(name, pEntry, id);
		pDict->downgradeOpen();
		pDict->close();

		return id;
	}

	/// Renames an entry in the table.
	static bool Rename(const ACHAR* oldName, const ACHAR* newName)
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();

		pDict->upgradeOpen();
		bool ret = pDict->setName(oldName, newName);
		pDict->downgradeOpen();
		pDict->close();

		return ret;
	}

	static void Remove(const ACHAR* name)
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();

		pDict->upgradeOpen();
		pDict->remove(name);
		pDict->downgradeOpen();

		pDict->close();
	}

	/// Gets the entry with the given name.
	static AcDbObjectId GetByName(const ACHAR* name)
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();

		AcDbObjectId id;
		if(pDict->getAt(name, id) == Acad::eKeyNotFound)
		{
			id = AcDbObjectId::kNull;
		}
		pDict->close();

		return id;
	}
	
	/// Determines whether the entry specified by entryName is contained in the dictionary.
	static bool Contains(const ACHAR* name)
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();

		bool ret = pDict->has(name);
		pDict->close();

		return ret;
	}

	/// Gets the count of entries in the dictionary.
	static Adesk::UInt32 Count()
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();

		Adesk::UInt32 ret = pDict->numEntries();
		pDict->close();

		return ret;
	}

	/// Reads all entries in the database
	static AcDbDictionaryIterator* GetIterator(void)
	{
		AcDbDictionary* pDict = CDictEntry::GetDictionary();
		return pDict->newIterator();
	}
};

#define GETTABLENAMEFORTYPE(T) _T("OZOZ_DICT_") _T(#T)

template <class T> ACHAR* CDictEntry<T>::Table_Name = GETTABLENAMEFORTYPE(T);
