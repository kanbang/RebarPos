//-----------------------------------------------------------------------------
//----- TableCell.cpp : Implementation of CTableCell
//-----------------------------------------------------------------------------
#include "StdAfx.h"

#include "TableCell.h"

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
CTableCell::CTableCell(void) : m_TextColor(0), 
	m_TopBorderColor(0), m_LeftBorderColor(0), m_BottomBorderColor(0), m_RightBorderColor(0),
	m_TopBorder(false), m_LeftBorder(false), m_BottomBorder(false), m_RightBorder(false),
	m_MergeRight(false), m_MergeDown(false),
	m_TextStyleId(AcDbObjectId::kNull),
	m_TextHeight(1.0),
	m_HorizontalAlignment(CTableCell::eNEAR), m_VerticalAlignment(CTableCell::eNEAR)
{
}

CTableCell::~CTableCell(void)
{
}

//*************************************************************************
// Properties
//*************************************************************************
const std::wstring& CTableCell::Text() const         { return m_Text; }
void CTableCell::setText(const std::wstring& newVal) { m_Text = newVal; }

const unsigned short CTableCell::TextColor() const         { return m_TextColor; }
void CTableCell::setTextColor(const unsigned short newVal) { m_TextColor = newVal; }

const unsigned short CTableCell::TopBorderColor() const         { return m_TopBorderColor; }
void CTableCell::setTopBorderColor(const unsigned short newVal) { m_TopBorderColor = newVal; }

const unsigned short CTableCell::LeftBorderColor() const         { return m_LeftBorderColor; }
void CTableCell::setLeftBorderColor(const unsigned short newVal) { m_LeftBorderColor = newVal; }

const unsigned short CTableCell::BottomBorderColor() const         { return m_BottomBorderColor; }
void CTableCell::setBottomBorderColor(const unsigned short newVal) { m_BottomBorderColor = newVal; }

const unsigned short CTableCell::RightBorderColor() const         { return m_RightBorderColor; }
void CTableCell::setRightBorderColor(const unsigned short newVal) { m_RightBorderColor = newVal; }

const bool CTableCell::TopBorder() const         { return m_TopBorder; }
void CTableCell::setTopBorder(const bool newVal) { m_TopBorder = newVal; }

const bool CTableCell::LeftBorder() const         { return m_LeftBorder; }
void CTableCell::setLeftBorder(const bool newVal) { m_LeftBorder = newVal; }

const bool CTableCell::BottomBorder() const         { return m_BottomBorder; }
void CTableCell::setBottomBorder(const bool newVal) { m_BottomBorder = newVal; }

const bool CTableCell::RightBorder() const         { return m_RightBorder; }
void CTableCell::setRightBorder(const bool newVal) { m_RightBorder = newVal; }

const int CTableCell::MergeRight() const         { return m_MergeRight; }
void CTableCell::setMergeRight(const int newVal) { m_MergeRight = newVal; }

const int CTableCell::MergeDown() const         { return m_MergeDown; }
void CTableCell::setMergeDown(const int newVal) { m_MergeDown = newVal; }

const AcDbObjectId& CTableCell::TextStyleId() const          { return m_TextStyleId; }
void CTableCell::setTextStyleId(const AcDbObjectId& newVal)  { m_TextStyleId = newVal; }

const double CTableCell::TextHeight() const         { return m_TextHeight; }
void CTableCell::setTextHeight(const double newVal) { m_TextHeight = newVal; }

const CTableCell::Alignment CTableCell::HorizontalAlignment() const         { return m_HorizontalAlignment; }
void CTableCell::setHorizontalAlignment(const CTableCell::Alignment newVal) { m_HorizontalAlignment = newVal; }

const CTableCell::Alignment CTableCell::VerticalAlignment() const         { return m_VerticalAlignment; }
void CTableCell::setVerticalAlignment(const CTableCell::Alignment newVal) { m_VerticalAlignment = newVal; }