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

ACRX_DXF_DEFINE_MEMBERS(CBOQTable, CGenericTable,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, BOQTABLE,
	"OZOZRebarPos\
	|Product Desc:     Rebar BOQ Table Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CBOQTable::CBOQTable() :
	m_Multiplier(1), m_StyleID(AcDbObjectId::kNull), m_Heading(NULL), m_Footing(NULL)
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
const Adesk::Int32 CBOQTable::Multiplier(void) const
{
	assertReadEnabled();
	return m_Multiplier;
}

Acad::ErrorStatus CBOQTable::setMultiplier(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Multiplier = newVal;
	UpdateTable();
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
	UpdateTable();
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
	UpdateTable();
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
	UpdateTable();
	return Acad::eOk;
}

//*************************************************************************
// Class Methods
//*************************************************************************
void CBOQTable::AddRow(CBOQRow* const row)
{
	assertWriteEnabled();
	m_List.push_back(row);
	UpdateTable();
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
	UpdateTable();
}

void CBOQTable::RemoveRow(const RowListSize index)
{
	assertWriteEnabled();
	delete m_List[index];
	RowListIt it = m_List.begin();
	m_List.erase(it + index);
	UpdateTable();
}

void CBOQTable::ClearRows()
{
	assertWriteEnabled();
	for(RowListIt it = m_List.begin(); it != m_List.end(); it++)
	{
		delete *it;
	}
	m_List.clear();
	UpdateTable();
}

const RowListSize CBOQTable::GetRowCount() const
{
	assertReadEnabled();
	return m_List.size();
}

//*************************************************************************
// Helper methods
//*************************************************************************

void CBOQTable::UpdateTable(void)
{
	assertReadEnabled();

	// Open style
	Acad::ErrorStatus es;
	AcDbObjectPointer<CBOQStyle> pStyle (m_StyleID, AcDb::kForRead);
	if((es = pStyle.openStatus()) != Acad::eOk)
	{
		return;
	}

	ACHAR* lastColumns = NULL;
	acutUpdString(pStyle->Columns(), lastColumns);

	// Parse columns
	std::vector<CBOQTable::ColumnType> columns = ParseColumns(lastColumns);

	// Create text styles
	AcDbObjectId lastTextStyleId = pStyle->TextStyleId();
	AcDbObjectId lastHeadingStyleId = pStyle->HeadingStyleId();
	AcDbObjectId lastFootingStyleId = pStyle->FootingStyleId();

	// Set colors
	Adesk::UInt16 lastTextColor = pStyle->TextColor();
	Adesk::UInt16 lastPosColor = pStyle->PosColor();
	Adesk::UInt16 lastLineColor = pStyle->LineColor();
	Adesk::UInt16 lastBorderColor = pStyle->BorderColor();
	Adesk::UInt16 lastHeadingColor = pStyle->HeadingColor();
	Adesk::UInt16 lastFootingColor = pStyle->FootingColor();

	// Get texts
	ACHAR* lastHeading = NULL;
	ACHAR* lastFooting = NULL;
	acutUpdString(pStyle->Heading(), lastHeading);
	acutUpdString(pStyle->Footing(), lastFooting);
	if(m_Heading != NULL && m_Heading[0] != _T('\0'))
		acutUpdString(m_Heading, lastHeading);
	if(m_Footing != NULL && m_Footing[0] != _T('\0'))
		acutUpdString(m_Footing, lastFooting);

	// Get other styles
	double lastHeadingScale = pStyle->HeadingScale();
	double lastFootingScale = pStyle->FootingScale();
	double lastRowSpacing = pStyle->RowSpacing();

	Adesk::Int32 lastPrecision = pStyle->Precision();
	CBOQStyle::DrawingUnits lastDisplayUnit = pStyle->DisplayUnit();

	// Create diamater list
	std::map<double, int> diameters;
	for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CBOQRow* row = *it;
		diameters[row->diameter] = 0;
	}
	// Set column position
	int diacol = 0;
	for(std::map<double, int>::iterator it = diameters.begin(); it != diameters.end(); it++)
	{
		it->second = diacol;
		diacol++;
	}

	// Heading/footing
	int headingLines = 0;
	if(lastHeading != NULL && lastHeading[0] != _T('\0'))
	{
		headingLines = 1;
	}
	int footingLines = 0;
	if(lastFooting != NULL && lastFooting[0] != _T('\0'))
	{
		footingLines = 1;
	}

	// Create base table
	int rows = (int)m_List.size() + headingLines + footingLines;

	SetSize(rows, (int)columns.size() + (int)diameters.size() - 1);
	setCellMargin(lastRowSpacing);

	// Set cell properties
	for(int i = headingLines; i < headingLines + (int)m_List.size(); i++)
	{
		for(int j = 0; j < Columns(); j++)
		{
			setCellHorizontalAlignment(i, j, CTableCell::eCENTER);
			setCellVerticalAlignment(i, j, CTableCell::eCENTER);
			setCellTextHeight(i, j, 1.0);
			setCellLeftBorder(i, j, true, lastLineColor);
			setCellRightBorder(i, j, true, lastLineColor);
			setCellTopBorder(i, j, true, lastLineColor);
			setCellBottomBorder(i, j, true, lastLineColor);
		}
	}

	// Set rows
	int j = 0;
	for(std::vector<CBOQTable::ColumnType>::iterator tit = columns.begin(); tit != columns.end(); tit++)
	{
		int i = headingLines;
		CBOQTable::ColumnType type = *tit;

		for(RowListConstIt it = m_List.begin(); it != m_List.end(); it++)
		{
			CBOQRow* row = *it;

			unsigned short color;
			std::wstring text;
			double len;
			int doff = 0;

			if(type == CBOQTable::POS)
				color = lastPosColor;
			else
				color = lastTextColor;

			switch(type)
			{
			case CBOQTable::POS:
				Utility::IntToStr(row->pos, text);
				break;
			case CBOQTable::POSDD:
				Utility::IntToStr(row->pos, text);
				if(text.length() == 1) text.insert(0, L"0");
				break;
			case CBOQTable::COUNT:
				Utility::IntToStr(m_Multiplier * row->count, text);
				break;
			case CBOQTable::DIAMETER:
				Utility::DoubleToStr(row->diameter, text);
				break;
			case CBOQTable::LENGTH:
				len = (row->length1 + row->length2) / 2.0;
				if(lastDisplayUnit == CBOQStyle::CM) len /= 10.0;
				Utility::DoubleToStr(len, lastPrecision, text);
				break;
			case CBOQTable::SHAPE:
				// TODO: Add shape cell
				text = L"SHAPE";
				break;
			case CBOQTable::TOTALLENGTH:
				Utility::DoubleToStr(m_Multiplier * row->count * (row->length1 + row->length2) / 2.0 / 1000.0, lastPrecision, text);
				doff = diameters[row->diameter];
				break;
			}

			setCellTextColor(i, j + doff, color);
			setCellTextStyleId(i, j + doff, lastTextStyleId);
			setCellText(i, j + doff, text);

			i++;
		}
		j++;
	}

	// Set heading
	int hi = 0;
	setCellText(hi, 0, lastHeading);
	setCellTextColor(hi, 0, lastHeadingColor);
	setCellTextStyleId(hi, 0, lastHeadingStyleId);
	setCellHorizontalAlignment(hi, 0, CTableCell::eCENTER);
	setCellVerticalAlignment(hi, 0, CTableCell::eCENTER);
	setCellTextHeight(hi, 0, lastHeadingScale);
	MergeAcross(hi, 0, Columns());

	// Set footing
	int fi = (int)m_List.size() + headingLines;
	setCellText(fi, 0, lastFooting);
	setCellTextColor(fi, 0, lastFootingColor);
	setCellTextStyleId(fi, 0, lastFootingStyleId);
	setCellHorizontalAlignment(fi, 0, CTableCell::eNEAR);
	setCellVerticalAlignment(fi, 0, CTableCell::eCENTER);
	setCellTextHeight(fi, 0, lastFootingScale);
	MergeAcross(fi, 0, Columns());

	acutDelString(lastColumns);
	acutDelString(lastHeading);
	acutDelString(lastFooting);
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
    AcDbIntArray&         geomIds) const
{
	assertReadEnabled();

	return CGenericTable::subGetOsnapPoints(osnapMode, gsSelectionMark, pickPoint, lastPoint, viewXform, snapPoints, geomIds);
}

Acad::ErrorStatus CBOQTable::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
	assertReadEnabled();

	return CGenericTable::subGetGripPoints(gripPoints, osnapModes, geomIds);
}

Acad::ErrorStatus CBOQTable::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
	assertWriteEnabled();

	return CGenericTable::subMoveGripPointsAt(indices, offset);
}

Acad::ErrorStatus CBOQTable::subTransformBy(const AcGeMatrix3d& xform)
{
	assertWriteEnabled();
	
	return CGenericTable::subTransformBy(xform);
}

void CBOQTable::subList() const
{
    assertReadEnabled();

	// Call parent first
    CGenericTable::subList();

	// Base point in UCS
    acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Base Point:"));
    AcGePoint3d pt(BasePoint());
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

	return CGenericTable::subExplode(entitySet);
}

Adesk::Boolean CBOQTable::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();

	return CGenericTable::subWorldDraw(worldDraw);
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CBOQTable::dwgOutFields(AcDbDwgFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = CGenericTable::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CBOQTable::kCurrentVersionNumber);

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
	if((es = CGenericTable::dwgInFields(pFiler)) != Acad::eOk)
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

	UpdateTable();

	return pFiler->filerStatus();
}

Acad::ErrorStatus CBOQTable::dxfOutFields(AcDbDxfFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = CGenericTable::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("BOQTable"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CBOQTable::kCurrentVersionNumber);

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
	if(((es = CGenericTable::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("BOQTable")))
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
	Adesk::Int32 t_Multiplier = 0;
	ACHAR* t_Heading = NULL;
	ACHAR* t_Footing = NULL;
	AcDbObjectId t_StyleID = AcDbObjectId::kNull;
	RowList t_List;
	long count;

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
	m_Multiplier = t_Multiplier;

	setHeading(t_Heading);
	setFooting(t_Footing);
	m_StyleID = t_StyleID;

	ClearRows();
	m_List.clear();
	m_List = t_List;

	acutDelString(t_Heading);
	acutDelString(t_Footing);

	UpdateTable();

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
        return CGenericTable::subWblockClone(pOwner, pClonedObject, idMap, isPrimary);

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

// Return the CLSID of the class here
Acad::ErrorStatus CBOQTable::subGetClassID(CLSID* pClsid) const
{
    assertReadEnabled();
	// See the interface definition file for the CLASS ID
	CLSID clsid = { 0xba77cfff, 0x0274, 0x4d4c, { 0xbf, 0xe2, 0x64, 0xa5, 0x73, 0x1b, 0xad, 0x37 } };
    *pClsid = clsid;
    return Acad::eOk;
}

const std::vector<CBOQTable::ColumnType> CBOQTable::ParseColumns(const ACHAR* columns) const
{
	assertReadEnabled();

	// Column definition text
	// [M] Pos marker
	// [MM] Pos marker with double digits
	// [N] Bar count
	// [D] Bar diameter
	// [L] Length
	// [SH] Shape
	// [TL] Total length
	// e.g. [MM][D][N][L][SH][TL]

	std::vector<CBOQTable::ColumnType> list;

	// First pass: separate parts
	std::wstring str;
	if(columns != NULL)
		str.assign(columns);
	std::wstring part;
	bool indeco = false;
	for(unsigned int i = 0; i < str.length(); i++)
	{
		wchar_t c = str.at(i);
		if((!indeco && c == L'[') || (indeco && (c == L']')) || (i == str.length() - 1))
		{
			if((i == str.length() - 1) && (c != L'[') && (c != L']'))
			{
				part += c;
			}
			if(indeco && !part.empty())
			{
				CBOQTable::ColumnType type = CBOQTable::NONE;

				if(part == L"M")
					type = CBOQTable::POS;
				else if(part == L"MM")
					type = CBOQTable::POSDD;
				else if(part == L"N")
					type = CBOQTable::COUNT;
				else if(part == L"D")
					type = CBOQTable::DIAMETER;
				else if(part == L"L")
					type = CBOQTable::LENGTH;
				else if(part == L"SH")
					type = CBOQTable::SHAPE;
				else if(part == L"TL")
					type = CBOQTable::TOTALLENGTH;

				if(type != CBOQTable::NONE)
					list.push_back(type);
			}
			indeco = (c == L'[');

			part.clear();
		}
		else
		{
			part += c;
		}
	}

	return list;
}
