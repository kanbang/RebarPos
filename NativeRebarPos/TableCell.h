//-----------------------------------------------------------------------------
//----- TableCell.h : Declaration of the CTableCell
//-----------------------------------------------------------------------------
#pragma once

#include <string>

/// ---------------------------------------------------------------------------
/// The CTableCell represents a cell in the CGenericTable. It contains
/// cell contents with formatting and borders.
/// ---------------------------------------------------------------------------
class CTableCell
{
public:
	CTableCell(void);
	virtual ~CTableCell(void);

public:
	enum Alignment
	{
		eNEAR = 0,
		eCENTER = 1,
		eFAR = 2
	};

private:
	/// Property backing fields
	std::wstring m_Text;

	unsigned short m_TextColor;
	unsigned short m_TopBorderColor;
	unsigned short m_LeftBorderColor;
	unsigned short m_BottomBorderColor;
	unsigned short m_RightBorderColor;

	bool m_TopBorder;
	bool m_LeftBorder;
	bool m_BottomBorder;
	bool m_RightBorder;

	bool m_TopBorderDouble;
	bool m_LeftBorderDouble;
	bool m_BottomBorderDouble;
	bool m_RightBorderDouble;

	int m_MergeRight;
	int m_MergeDown;

	AcDbHardPointerId m_TextStyleId;

	double m_TextHeight;

	Alignment m_HorizontalAlignment;
	Alignment m_VerticalAlignment;

public:
	/// Properties
	const std::wstring& Text() const;
	void setText(const std::wstring& newVal);

	const unsigned short TextColor() const;
	void setTextColor(const unsigned short newVal);

	const unsigned short TopBorderColor() const;
	void setTopBorderColor(const unsigned short newVal);

	const unsigned short LeftBorderColor() const;
	void setLeftBorderColor(const unsigned short newVal);

	const unsigned short BottomBorderColor() const;
	void setBottomBorderColor(const unsigned short newVal);

	const unsigned short RightBorderColor() const;
	void setRightBorderColor(const unsigned short newVal);

	const bool TopBorder() const;
	void setTopBorder(const bool newVal);

	const bool LeftBorder() const;
	void setLeftBorder(const bool newVal);

	const bool BottomBorder() const;
	void setBottomBorder(const bool newVal);

	const bool RightBorder() const;
	void setRightBorder(const bool newVal);

	const bool TopBorderDouble() const;
	void setTopBorderDouble(const bool newVal);

	const bool LeftBorderDouble() const;
	void setLeftBorderDouble(const bool newVal);

	const bool BottomBorderDouble() const;
	void setBottomBorderDouble(const bool newVal);

	const bool RightBorderDouble() const;
	void setRightBorderDouble(const bool newVal);

	const int MergeRight() const;
	void setMergeRight(const int newVal);

	const int MergeDown() const;
	void setMergeDown(const int newVal);

	const AcDbObjectId& TextStyleId() const;
	void setTextStyleId(const AcDbObjectId& newVal);

	const double TextHeight() const;
	void setTextHeight(const double newVal);

	const Alignment HorizontalAlignment() const;
	void setHorizontalAlignment(const Alignment newVal);

	const Alignment VerticalAlignment() const;
	void setVerticalAlignment(const Alignment newVal);
};