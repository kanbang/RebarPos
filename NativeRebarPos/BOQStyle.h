//-----------------------------------------------------------------------------
//----- BOQStyle.h : Declaration of the CBOQStyle
//-----------------------------------------------------------------------------
#pragma once

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
class DLLIMPEXP CBOQStyle : public AcDbObject
{
public:
	/// Define additional RTT information for AcRxObject base type.
	ACRX_DECLARE_MEMBERS(CBOQStyle);

protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
	CBOQStyle ();
	virtual ~CBOQStyle ();

public:
	enum DrawingUnits
	{ 
		MM = 0,
		CM = 1,
		DM = 2,
		M = 3,
	};

public:
	/// AcDbEntity overrides: database
	virtual Acad::ErrorStatus dwgOutFields(AcDbDwgFiler *pFiler) const;
	virtual Acad::ErrorStatus dwgInFields(AcDbDwgFiler *pFiler);

	virtual Acad::ErrorStatus dxfOutFields(AcDbDxfFiler *pFiler) const;
	virtual Acad::ErrorStatus dxfInFields(AcDbDxfFiler *pFiler);

protected:
	/// Property backing fields
	ACHAR* m_Name;

	Adesk::Int32 m_Precision;

	DrawingUnits m_DisplayUnit;

	ACHAR* m_Columns;

	Adesk::UInt16 m_TextColor;
	Adesk::UInt16 m_PosColor;
	Adesk::UInt16 m_LineColor;
	Adesk::UInt16 m_BorderColor;
	Adesk::UInt16 m_HeadingColor;
	Adesk::UInt16 m_FootingColor;

	ACHAR* m_Heading;
	ACHAR* m_Footing;

	ACHAR* m_PosLabel;
	ACHAR* m_CountLabel;
	ACHAR* m_DiameterLabel;
	ACHAR* m_LengthLabel;
	ACHAR* m_ShapeLabel;
	ACHAR* m_TotalLengthLabel;
	ACHAR* m_DiameterLengthLabel;
	ACHAR* m_UnitWeightLabel;
	ACHAR* m_WeightLabel;
	ACHAR* m_GrossWeightLabel;

    AcDbHardPointerId m_TextStyleId;
    AcDbHardPointerId m_HeadingStyleId;
    AcDbHardPointerId m_FootingStyleId;

	double m_HeadingScale;
	double m_FootingScale;
	double m_RowSpacing;

public:
	/// Gets or sets item name
	const ACHAR* Name(void) const;
	Acad::ErrorStatus setName(const ACHAR* newVal);

	/// Gets or sets the maximum bar length
    const double MaxBarLength(void) const;
	Acad::ErrorStatus setMaxBarLength(const double newVal);

	/// Gets or sets the display precision
    const Adesk::Int32 Precision(void) const;
	Acad::ErrorStatus setPrecision(const Adesk::Int32 newVal);

	/// Gets or sets the display unit
    const DrawingUnits DisplayUnit(void) const;
	Acad::ErrorStatus setDisplayUnit(const DrawingUnits newVal);

	/// Gets or sets the column definition.
    const ACHAR* Columns(void) const;
	Acad::ErrorStatus setColumns(const ACHAR* newVal);

	/// Gets or sets the text color.
	const Adesk::UInt16 TextColor(void) const;
	Acad::ErrorStatus setTextColor(const Adesk::UInt16 newVal);

	/// Gets or sets the pos text color.
	const Adesk::UInt16 PosColor(void) const;
	Acad::ErrorStatus setPosColor(const Adesk::UInt16 newVal);

	/// Gets or sets the line color.
	const Adesk::UInt16 LineColor(void) const;
	Acad::ErrorStatus setLineColor(const Adesk::UInt16 newVal);

	/// Gets or sets the border color.
	const Adesk::UInt16 BorderColor(void) const;
	Acad::ErrorStatus setBorderColor(const Adesk::UInt16 newVal);

	/// Gets or sets the heading text color.
	const Adesk::UInt16 HeadingColor(void) const;
	Acad::ErrorStatus setHeadingColor(const Adesk::UInt16 newVal);

	/// Gets or sets the footing text color.
	const Adesk::UInt16 FootingColor(void) const;
	Acad::ErrorStatus setFootingColor(const Adesk::UInt16 newVal);

	/// Gets or sets heading text
	const ACHAR* Heading(void) const;
	Acad::ErrorStatus setHeading(const ACHAR* newVal);

	/// Gets or sets heading text
	const ACHAR* Footing(void) const;
	Acad::ErrorStatus setFooting(const ACHAR* newVal);

	// Get labels
	const ACHAR* PosLabel(void) const;
	const ACHAR* CountLabel(void) const;
	const ACHAR* DiameterLabel(void) const;
	const ACHAR* LengthLabel(void) const;
	const ACHAR* ShapeLabel(void) const;
	const ACHAR* TotalLengthLabel(void) const;
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
	Acad::ErrorStatus setUnitWeightLabel(const ACHAR* newVal);
	Acad::ErrorStatus setWeightLabel(const ACHAR* newVal);
	Acad::ErrorStatus setGrossWeightLabel(const ACHAR* newVal);

	/// Gets or sets pointer to the text style.
	const AcDbObjectId& TextStyleId(void) const;
	Acad::ErrorStatus setTextStyleId(const AcDbObjectId& newVal);

	/// Gets or sets pointer to the heading style.
	const AcDbObjectId& HeadingStyleId(void) const;
	Acad::ErrorStatus setHeadingStyleId(const AcDbObjectId& newVal);

	/// Gets or sets pointer to the footing style.
	const AcDbObjectId& FootingStyleId(void) const;
	Acad::ErrorStatus setFootingStyleId(const AcDbObjectId& newVal);

	/// Gets or sets the heading scale relative to text height.
	const double HeadingScale(void) const;
	Acad::ErrorStatus setHeadingScale(const double newVal);

	/// Gets or sets the footing scale relative to text height.
	const double FootingScale(void) const;
	Acad::ErrorStatus setFootingScale(const double newVal);

	/// Gets or sets the row spacing.
	const double RowSpacing(void) const;
	Acad::ErrorStatus setRowSpacing(const double newVal);

private:
	static ACHAR* Table_Name;

public:
	/// Gets the table name
	static ACHAR* GetTableName();

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