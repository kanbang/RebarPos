//-----------------------------------------------------------------------------
//----- GenericTable.cpp : Implementation of CGenericTable
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "GenericTable.h"
#include "Utility.h"

#include <algorithm>

Adesk::UInt32 CGenericTable::kCurrentVersionNumber = 1;

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_DXF_DEFINE_MEMBERS(CGenericTable, AcDbEntity,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, GENERICTABLE,
	"OZOZRebarPos\
	|Product Desc:     Geberic Table Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CGenericTable::CGenericTable(void) : m_BasePoint(0, 0, 0), 
	m_Direction(1, 0, 0), m_Up(0, 1, 0), m_Normal(0, 0, 1),
	m_Rows(0), m_Columns(0), m_Cells(0),
	lastTexts(), lastLines(), columnWidths(), rowHeights(), isModified(true),
	m_CellMargin(0.2)
{
}

CGenericTable::~CGenericTable(void)
{
	Clear();
	ResetDrawParams();
}

//*************************************************************************
// Properties
//*************************************************************************
const AcGeVector3d& CGenericTable::DirectionVector(void) const
{
	assertReadEnabled();
	return m_Direction;
}

const AcGeVector3d& CGenericTable::UpVector(void) const
{
	assertReadEnabled();
	return m_Up;
}

const AcGeVector3d& CGenericTable::NormalVector(void) const
{
	assertReadEnabled();
	return m_Normal;
}

const double CGenericTable::Scale(void) const
{
	assertReadEnabled();
	return m_Direction.length();
}
Acad::ErrorStatus CGenericTable::setScale(const double newVal)
{
	assertWriteEnabled();
	AcGeMatrix3d xform = AcGeMatrix3d::scaling(newVal / m_Direction.length(), m_BasePoint);
	return transformBy(xform);
}

const int CGenericTable::Columns(void) const
{
	assertReadEnabled();
	return m_Columns;
}
const int CGenericTable::Rows(void) const
{
	assertReadEnabled();
	return m_Rows;
}

const double CGenericTable::CellMargin(void) const
{
	assertReadEnabled();
	return m_CellMargin;
}
Acad::ErrorStatus CGenericTable::setCellMargin(const double newVal)
{
	assertWriteEnabled();
	m_CellMargin = newVal;
	isModified = true;
	return Acad::eOk;
}

const AcGePoint3d& CGenericTable::BasePoint(void) const
{
	assertReadEnabled();
	return m_BasePoint;
}
Acad::ErrorStatus CGenericTable::setBasePoint(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_BasePoint = newVal;
	return Acad::eOk;
}

const double CGenericTable::Width(void) const
{
	if(isModified)
	{
		Calculate();
	}
	double w = 0;
	for(std::vector<double>::iterator it = columnWidths.begin(); it != columnWidths.end(); it++)
	{
		w += (*it);
	}
	return w * m_Direction.length();
}

const double CGenericTable::Height(void) const
{
	if(isModified)
	{
		Calculate();
	}
	double h = 0;
	for(std::vector<double>::iterator it = rowHeights.begin(); it != rowHeights.end(); it++)
	{
		h += (*it);
	}
	return h * m_Direction.length();
}

//*************************************************************************
// Methods
//*************************************************************************

void CGenericTable::SetSize(int rows, int columns)
{
	assertWriteEnabled();

	Clear();

	m_Cells.resize(rows * columns);

	// Create new cells
	for(int i = 0; i < rows; i++)
	{
		for(int j = 0; j < columns; j++)
		{
			m_Cells[i * columns + j] = new CTableCell();
		}
	}

	m_Rows = rows;
	m_Columns = columns;

	isModified = true;
}

void CGenericTable::Clear()
{
	assertWriteEnabled();

	for(std::vector<CTableCell*>::iterator it = m_Cells.begin(); it != m_Cells.end(); it++)
		delete (*it);

	m_Cells.clear();

	isModified = true;
}

//*************************************************************************
// Cell Access Methods
//*************************************************************************
void CGenericTable::setCellText(const int i, const int j, const std::wstring& newVal)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	std::wstring scopy(newVal);
	Utility::ReplaceString(scopy, L"\r\n", L"\\P");
	Utility::ReplaceString(scopy, L"\r", L"\\P");
	Utility::ReplaceString(scopy, L"\n", L"\\P");
	cell->setText(scopy);
	isModified = true;
}
void CGenericTable::setCellTextColor(const int i, const int j, const unsigned short newVal)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setTextColor(newVal);
	isModified = true;
}
void CGenericTable::setCellTextStyleId(const int i, const int j, const AcDbObjectId& newVal)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setTextStyleId(newVal);
	isModified = true;
}
void CGenericTable::setCellTextHeight(const int i, const int j, const double newVal)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setTextHeight(newVal);
	isModified = true;
}

void CGenericTable::setCellLeftBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setLeftBorder(hasBorder);
	cell->setLeftBorderColor(borderColor);
	isModified = true;
}
void CGenericTable::setCellRightBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setRightBorder(hasBorder);
	cell->setRightBorderColor(borderColor);
	isModified = true;
}
void CGenericTable::setCellTopBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setTopBorder(hasBorder);
	cell->setTopBorderColor(borderColor);
	isModified = true;
}
void CGenericTable::setCellBottomBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setBottomBorder(hasBorder);
	cell->setBottomBorderColor(borderColor);
	isModified = true;
}

void CGenericTable::setCellHorizontalAlignment(const int i, const int j, const CTableCell::Alignment newVal)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setHorizontalAlignment(newVal);
	isModified = true;
}
void CGenericTable::setCellVerticalAlignment(const int i, const int j, const CTableCell::Alignment newVal)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setVerticalAlignment(newVal);
	isModified = true;
}

void CGenericTable::MergeAcross(const int i, const int j, const int span)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setMergeRight(span);
	
	if(span > 1)
	{
		// Clear texts
		for(int k = j + 1; k < j + span; k++)
		{
			m_Cells[i * m_Columns + k]->setText(L"");
		}
		// Clear interior borders
		for(int k = j + 1; k < j + span; k++)
		{
			m_Cells[i * m_Columns + k]->setLeftBorder(false);
		}
		for(int k = j; k < j + span - 1; k++)
		{
			m_Cells[i * m_Columns + k]->setRightBorder(false);
		}
	}
	isModified = true;
}
void CGenericTable::MergeDown(const int i, const int j, const int span)
{
	CTableCell* cell = m_Cells[i * m_Columns + j];
	cell->setMergeDown(span);
	
	if(span > 1)
	{
		// Clear texts
		for(int k = i + 1; k < i + span; k++)
		{
			m_Cells[k * m_Columns + j]->setText(L"");
		}
		// Clear interior borders
		for(int k = i + 1; k < i + span; k++)
		{
			m_Cells[k * m_Columns + j]->setTopBorder(false);
		}
		for(int k = i; k < i + span - 1; k++)
		{
			m_Cells[k * m_Columns + j]->setBottomBorder(false);
		}
	}
	isModified = true;
}

//*************************************************************************
// Helper Methods
//*************************************************************************
const void CGenericTable::ResetDrawParams(void) const
{
	for(std::vector<CDrawTextParams*>::iterator it = lastTexts.begin(); it != lastTexts.end(); it++)
		delete *it;
	for(std::vector<CDrawLineParams*>::iterator it = lastLines.begin(); it != lastLines.end(); it++)
		delete *it;
	lastTexts.clear();
	lastLines.clear();

	columnWidths.clear();
	rowHeights.clear();
}

const void CGenericTable::Calculate(void) const
{
	if(!isModified)
	{
		return;
	}

	assertReadEnabled();

	// Reset old params
	ResetDrawParams();

	lastTexts.resize(m_Columns * m_Rows);

	// Create text styles
	for(int i = 0; i < m_Rows; i++)
	{
		for(int j = 0; j < m_Columns; j++)
		{
			CTableCell* cell = m_Cells[i * m_Columns + j];

			AcDbMText* mtext = new AcDbMText();
			mtext->setTextStyle(cell->TextStyleId());
			mtext->setTextHeight(cell->TextHeight());
			mtext->setContents(cell->Text().c_str());

			CDrawTextParams* param = new CDrawTextParams();
			param->w = mtext->actualWidth();
			param->h = mtext->actualHeight();
			param->text = cell->Text();
			param->color = cell->TextColor();
			param->styleId = cell->TextStyleId();
			param->height = cell->TextHeight();
			param->halign = (CDrawTextParams::Alignment)cell->HorizontalAlignment();
			param->valign = (CDrawTextParams::Alignment)cell->VerticalAlignment();

			mtext->close();
			delete mtext;
			lastTexts[i * m_Columns + j] = param;
		}
	}

	// Set minimum columns sizes
	columnWidths.resize(m_Columns);
	for(int j = 0; j < m_Columns; j++)
	{
		columnWidths[j] = 2.0 * m_CellMargin;
	}
	for(int j = 0; j < m_Columns; j++)
	{
		for(int i = 0; i < m_Rows; i++)
		{
			if(m_Cells[i * m_Columns + j]->MergeRight() == 0)
			{
				columnWidths[j] = max(columnWidths[j], lastTexts[i * m_Columns + j]->w + 2.0 * m_CellMargin);
			}
			else
			{
				int span = m_Cells[i * m_Columns + j]->MergeRight();
				if(j + span - 1 > m_Columns - 1) span = m_Columns - j;
				double w = (lastTexts[i * m_Columns + j]->w + 2.0 * m_CellMargin) / (double)span;
				for(int k = j; k < j + span; k++)
				{
					columnWidths[k] = max(columnWidths[k], w);
				}
			}
		}
	}

	// Set minimum row sizes
	rowHeights.resize(m_Rows);
	for(int i = 0; i < m_Rows; i++)
	{
		rowHeights[i] = 2.0 * m_CellMargin;
	}
	for(int i = 0; i < m_Rows; i++)
	{
		for(int j = 0; j < m_Columns; j++)
		{
			if(m_Cells[i * m_Columns + j]->MergeDown() == 0)
			{
				rowHeights[i] = max(rowHeights[i], lastTexts[i * m_Columns + j]->h + 2.0 * m_CellMargin);
			}
			else
			{
				int span = m_Cells[i * m_Columns + j]->MergeDown();
				if(i + span - 1 > m_Rows - 1) span = m_Rows - i;
				double h = (lastTexts[i * m_Columns + j]->h + 2.0 * m_CellMargin) / (double)span;
				for(int k = i; k < i + span; k++)
				{
					rowHeights[k] = max(rowHeights[k], h);
				}
			}
		}
	}

	// Set text location
	double x = 0;
	double y = 0;
	for(int i = 0; i < m_Rows; i++)
	{
		double h = rowHeights[i];
		for(int j = 0; j < m_Columns; j++)
		{
			double w = columnWidths[j];
			if(m_Cells[i * m_Columns + j]->MergeRight() != 0)
			{
				int span = m_Cells[i * m_Columns + j]->MergeRight();
				if(j + span - 1 > m_Columns - 1) span = m_Columns - j;
				w = 0;
				for(int k = j ; k < j + span; k++)
				{
					w += columnWidths[k];
				}
			}
			if(m_Cells[i * m_Columns + j]->MergeDown() != 0)
			{
				int span = m_Cells[i * m_Columns + j]->MergeDown();
				if(i + span - 1 > m_Rows - 1) span = m_Rows - i;
				h = 0;
				for(int k = i ; k < i + span; k++)
				{
					h += rowHeights[k];
				}
			}
			double cx = m_CellMargin;
			double cy = m_CellMargin;
			if(m_Cells[i * m_Columns + j]->HorizontalAlignment() == CTableCell::eNEAR)
			{
				cx = m_CellMargin;
			}
			else if(m_Cells[i * m_Columns + j]->HorizontalAlignment() == CTableCell::eFAR)
			{
				cx = w - m_CellMargin;
			}
			else if(m_Cells[i * m_Columns + j]->HorizontalAlignment() == CTableCell::eCENTER)
			{
				cx = w / 2.0;
			}
			if(m_Cells[i * m_Columns + j]->VerticalAlignment() == CTableCell::eNEAR)
			{
				cy = m_CellMargin;
			}
			else if(m_Cells[i * m_Columns + j]->VerticalAlignment() == CTableCell::eFAR)
			{
				cy = h - m_CellMargin;
			}
			else if(m_Cells[i * m_Columns + j]->VerticalAlignment() == CTableCell::eCENTER)
			{
				cy = h / 2.0;
			}

			lastTexts[i * m_Columns + j]->x = x + cx;
			lastTexts[i * m_Columns + j]->y = y - cy;

			x += columnWidths[j];
		}
		x = 0;
		y -= rowHeights[i];
	}

	// Set lines
	x = 0;
	y = 0;
	for(int i = 0; i < m_Rows; i++)
	{
		double h = rowHeights[i];
		for(int j = 0; j < m_Columns; j++)
		{
			double w = columnWidths[j];

			CTableCell* cell = m_Cells[i * m_Columns + j];
			if(cell->TopBorder())
			{
				CDrawLineParams* line = new CDrawLineParams();
				line->color = cell->TopBorderColor();
				line->x1 = x;
				line->x2 = x + columnWidths[j];
				line->y1 = y;
				line->y2 = y;
				lastLines.push_back(line);
			}
			if(cell->BottomBorder())
			{
				CDrawLineParams* line = new CDrawLineParams();
				line->color = cell->BottomBorderColor();
				line->x1 = x;
				line->x2 = x + columnWidths[j];
				line->y1 = y - rowHeights[i];
				line->y2 = y - rowHeights[i];
				lastLines.push_back(line);
			}
			if(cell->LeftBorder())
			{
				CDrawLineParams* line = new CDrawLineParams();
				line->color = cell->LeftBorderColor();
				line->x1 = x;
				line->x2 = x;
				line->y1 = y;
				line->y2 = y - rowHeights[i];
				lastLines.push_back(line);
			}
			if(cell->RightBorder())
			{
				CDrawLineParams* line = new CDrawLineParams();
				line->color = cell->RightBorderColor();
				line->x1 = x + columnWidths[j];
				line->x2 = x + columnWidths[j];
				line->y1 = y;
				line->y2 = y - rowHeights[i];
				lastLines.push_back(line);
			}

			x += columnWidths[j];
		}
		x = 0;
		y -= rowHeights[i];
	}

	// Done update
	isModified = false;
}

//*************************************************************************
// Overridden methods from AcDbEntity
//*************************************************************************
Acad::ErrorStatus CGenericTable::subGetOsnapPoints(
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

Acad::ErrorStatus CGenericTable::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
	assertReadEnabled();
	gripPoints.append(m_BasePoint);
	return Acad::eOk;
}

Acad::ErrorStatus CGenericTable::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
    if(indices.length() == 0 || offset.isZeroLength())
        return Acad::eOk;

	assertWriteEnabled();

	transformBy(AcGeMatrix3d::translation(offset));

	return Acad::eOk;
}

Acad::ErrorStatus CGenericTable::subTransformBy(const AcGeMatrix3d& xform)
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

Acad::ErrorStatus CGenericTable::subExplode(AcDbVoidPtrArray& entitySet) const
{
    assertReadEnabled();

	if(isModified)
	{
		Calculate();
	}

	if(lastTexts.size() == 0 && lastLines.size() == 0)
	{
		return Acad::eInvalidInput;
	}

	Acad::ErrorStatus es;

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);

	// Texts
	for(std::vector<CDrawTextParams*>::iterator it = lastTexts.begin(); it != lastTexts.end(); it++)
	{
		CDrawTextParams* p = (*it);
		if(!p->text.empty())
		{
			AcDbMText* text = new AcDbMText();
			text->setTextHeight(p->height);
			text->setTextStyle(p->styleId);
			text->setLocation(AcGePoint3d(p->x, p->y, 0));
			text->setColorIndex(p->color);
			if(p->halign == CDrawTextParams::eNEAR)
			{
				if(p->valign == CDrawTextParams::eNEAR)
					text->setAttachment(AcDbMText::kTopLeft);
				else if(p->valign == CDrawTextParams::eCENTER)
					text->setAttachment(AcDbMText::kMiddleLeft);
				else if(p->valign == CDrawTextParams::eFAR)
					text->setAttachment(AcDbMText::kBottomLeft);
			}
			else if(p->halign == CDrawTextParams::eCENTER)
			{
				if(p->valign == CDrawTextParams::eNEAR)
					text->setAttachment(AcDbMText::kTopCenter);
				else if(p->valign == CDrawTextParams::eCENTER)
					text->setAttachment(AcDbMText::kMiddleCenter);
				else if(p->valign == CDrawTextParams::eFAR)
					text->setAttachment(AcDbMText::kBottomCenter);
			}
			else if(p->halign == CDrawTextParams::eFAR)
			{
				if(p->valign == CDrawTextParams::eNEAR)
					text->setAttachment(AcDbMText::kTopRight);
				else if(p->valign == CDrawTextParams::eCENTER)
					text->setAttachment(AcDbMText::kMiddleRight);
				else if(p->valign == CDrawTextParams::eFAR)
					text->setAttachment(AcDbMText::kBottomRight);
			}
			text->setContents(p->text.c_str());
			if((es = text->transformBy(trans)) != Acad::eOk)
			{
				return es;
			}
			entitySet.append(text);
		}
	}

	// Lines
	for(std::vector<CDrawLineParams*>::iterator it = lastLines.begin(); it != lastLines.end(); it++)
	{
		CDrawLineParams* p = (*it);
		AcDbLine* line = new AcDbLine(AcGePoint3d(p->x1, p->y1, 0), AcGePoint3d(p->x2, p->y2, 0));
		line->setColorIndex(p->color);
		if((es = line->transformBy(trans)) != Acad::eOk)
		{
			return es;
		}
		entitySet.append(line);
	}

    return Acad::eOk;
}

Adesk::Boolean CGenericTable::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    assertReadEnabled();

    if(worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }

	// Update if modified
	if(isModified)
	{
		Calculate();
	}

	// Transform to match orientation
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);

	worldDraw->subEntityTraits().setSelectionMarker(1);

	// Draw texts
	for(std::vector<CDrawTextParams*>::iterator it = lastTexts.begin(); it != lastTexts.end(); it++)
	{
		CDrawTextParams* p = (*it);
		AcDbMText* text = new AcDbMText();
		text->setTextHeight(p->height);
		text->setTextStyle(p->styleId);
		text->setLocation(AcGePoint3d(p->x, p->y, 0));
		text->setColorIndex(p->color);
		if(p->halign == CDrawTextParams::eNEAR)
		{
			if(p->valign == CDrawTextParams::eNEAR)
				text->setAttachment(AcDbMText::kTopLeft);
			else if(p->valign == CDrawTextParams::eCENTER)
				text->setAttachment(AcDbMText::kMiddleLeft);
			else if(p->valign == CDrawTextParams::eFAR)
				text->setAttachment(AcDbMText::kBottomLeft);
		}
		else if(p->halign == CDrawTextParams::eCENTER)
		{
			if(p->valign == CDrawTextParams::eNEAR)
				text->setAttachment(AcDbMText::kTopCenter);
			else if(p->valign == CDrawTextParams::eCENTER)
				text->setAttachment(AcDbMText::kMiddleCenter);
			else if(p->valign == CDrawTextParams::eFAR)
				text->setAttachment(AcDbMText::kBottomCenter);
		}
		else if(p->halign == CDrawTextParams::eFAR)
		{
			if(p->valign == CDrawTextParams::eNEAR)
				text->setAttachment(AcDbMText::kTopRight);
			else if(p->valign == CDrawTextParams::eCENTER)
				text->setAttachment(AcDbMText::kMiddleRight);
			else if(p->valign == CDrawTextParams::eFAR)
				text->setAttachment(AcDbMText::kBottomRight);
		}
		text->setContents(p->text.c_str());
		text->transformBy(trans);
		worldDraw->geometry().draw(text);
		delete text;
	}

	// Draw lines
	AcGePoint3d pts[2];
	for(std::vector<CDrawLineParams*>::iterator it = lastLines.begin(); it != lastLines.end(); it++)
	{
		CDrawLineParams* p = (*it);
		AcDbLine* line = new AcDbLine(AcGePoint3d(p->x1, p->y1, 0.0), AcGePoint3d(p->x2, p->y2, 0.0));
		line->setColorIndex(p->color);
		line->transformBy(trans);
		worldDraw->geometry().draw(line);
		delete line;
	}


    return Adesk::kTrue; // Don't call viewportDraw().
}

Acad::ErrorStatus CGenericTable::subGetGeomExtents(AcDbExtents& extents) const
{
    assertReadEnabled();

	if(isModified)
	{
		Calculate();
	}

	// Get ECS extents
	double w = Width();
	double h = Height();
	AcGePoint3d pt1(0, 0, 0);
	AcGePoint3d pt2(w, 0, 0);
	AcGePoint3d pt3(w, h, 0);
	AcGePoint3d pt4(0, h, 0);

	// Transform to WCS
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);
	pt1.transformBy(trans);
	pt2.transformBy(trans);
	pt3.transformBy(trans);
	pt4.transformBy(trans);

	extents.addPoint(pt1);
	extents.addPoint(pt2);
	extents.addPoint(pt3);
	extents.addPoint(pt4);
	
	return Acad::eOk;
}
