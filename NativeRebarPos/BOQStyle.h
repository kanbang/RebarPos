//-----------------------------------------------------------------------------
//----- BOQStyle.h : Declaration of the CBOQStyle
//-----------------------------------------------------------------------------
#pragma once

#include <vector>
#include <map>

#include "Utility.h"

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
/// The CBOQStyle represents the settings for BOQ tables.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CBOQStyle
{
public:
	CBOQStyle ();
	virtual ~CBOQStyle ();

protected:
	/// Property backing fields
	ACHAR* m_Name;

	ACHAR* m_Columns;

	ACHAR* m_PosLabel;
	ACHAR* m_CountLabel;
	ACHAR* m_DiameterLabel;
	ACHAR* m_LengthLabel;
	ACHAR* m_ShapeLabel;
	ACHAR* m_TotalLengthLabel;
	ACHAR* m_DiameterListLabel;
	ACHAR* m_DiameterLengthLabel;
	ACHAR* m_UnitWeightLabel;
	ACHAR* m_WeightLabel;
	ACHAR* m_GrossWeightLabel;
	ACHAR* m_MultiplierHeadingLabel;

    AcDbHardPointerId m_TextStyleId;
    AcDbHardPointerId m_HeadingStyleId;
    AcDbHardPointerId m_FootingStyleId;

	Adesk::Boolean m_IsBuiltIn;

public:
	/// Gets or sets item name
	const ACHAR* Name(void) const;
	Acad::ErrorStatus setName(const ACHAR* newVal);

	/// Gets or sets the column definition.
    const ACHAR* Columns(void) const;
	Acad::ErrorStatus setColumns(const ACHAR* newVal);

	// Get labels
	const ACHAR* PosLabel(void) const;
	const ACHAR* CountLabel(void) const;
	const ACHAR* DiameterLabel(void) const;
	const ACHAR* LengthLabel(void) const;
	const ACHAR* ShapeLabel(void) const;
	const ACHAR* TotalLengthLabel(void) const;
	const ACHAR* DiameterListLabel(void) const;
	const ACHAR* DiameterLengthLabel(void) const;
	const ACHAR* UnitWeightLabel(void) const;
	const ACHAR* WeightLabel(void) const;
	const ACHAR* GrossWeightLabel(void) const;
	// Set labels
	Acad::ErrorStatus setPosLabel(const ACHAR* newVal);
	Acad::ErrorStatus setCountLabel(const ACHAR* newVal);
	Acad::ErrorStatus setDiameterLabel(const ACHAR* newVal);
	Acad::ErrorStatus setLengthLabel(const ACHAR* newVal);
	Acad::ErrorStatus setShapeLabel(const ACHAR* newVal);
	Acad::ErrorStatus setTotalLengthLabel(const ACHAR* newVal);
	Acad::ErrorStatus setDiameterLengthLabel(const ACHAR* newVal);
	Acad::ErrorStatus setDiameterListLabel(const ACHAR* newVal);
	Acad::ErrorStatus setUnitWeightLabel(const ACHAR* newVal);
	Acad::ErrorStatus setWeightLabel(const ACHAR* newVal);
	Acad::ErrorStatus setGrossWeightLabel(const ACHAR* newVal);

	/// Gets or sets the heading for tables with multiplier > 1
	const ACHAR* MultiplierHeadingLabel(void) const;
	Acad::ErrorStatus setMultiplierHeadingLabel(const ACHAR* newVal);

	/// Gets or sets pointer to the text style.
	const AcDbObjectId& TextStyleId(void) const;
	Acad::ErrorStatus setTextStyleId(const AcDbObjectId& newVal);

	/// Gets or sets pointer to the heading style.
	const AcDbObjectId& HeadingStyleId(void) const;
	Acad::ErrorStatus setHeadingStyleId(const AcDbObjectId& newVal);

	/// Gets or sets pointer to the footing style.
	const AcDbObjectId& FootingStyleId(void) const;
	Acad::ErrorStatus setFootingStyleId(const AcDbObjectId& newVal);

	/// Gets or sets whether this is a built-in style
	const Adesk::Boolean IsBuiltIn(void) const;
	Acad::ErrorStatus setIsBuiltIn(const Adesk::Boolean newVal);

private:
	static std::map<std::wstring, CBOQStyle*> m_BOQStyles;

public:
	/// Add a new style
	static void AddBOQStyle(CBOQStyle* style);

	/// Gets the style with the given name
	static CBOQStyle* GetBOQStyle(const std::wstring name);

	/// Gets the number of styles
	static std::map<std::wstring, CBOQStyle*>::size_type GetBOQStyleCount();

	/// Gets the underlying map
	static std::map<std::wstring, CBOQStyle*> GetMap();

	/// Removes all styles
	static void ClearBOQStyles(const bool builtin, const bool custom);

	/// Reads all styles defined in the resource
	static void ReadBOQStylesFromResource(HINSTANCE hInstance, const int resid);

	/// Reads all styles defined in the given text file
	static void ReadBOQStylesFromFile(const std::wstring filename, const bool builtin);

	/// Reads all styles defined in the string
	static void ReadBOQStylesFromString(const std::wstring source, const bool builtin);

	/// Saves all styles to the given text file
	static void SaveBOQStylesToFile(const std::wstring filename, const bool builtin, const bool custom);
};