//-----------------------------------------------------------------------------
//----- PosGroup.h : Declaration of the CPosGroup
//-----------------------------------------------------------------------------
#pragma once

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

class CRebarPos;

/// ---------------------------------------------------------------------------
/// The CPosGroup represents the settings for groups of rebar markers.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CPosGroup : public AcDbObject
{
public:
	/// Define additional RTT information for AcRxObject base type.
	ACRX_DECLARE_MEMBERS(CPosGroup);

protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
	CPosGroup ();
	virtual ~CPosGroup ();

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

	Adesk::Boolean m_Bending;
	double m_MaxBarLength;
	Adesk::Int32 m_Precision;

	DrawingUnits m_DrawingUnit;
	DrawingUnits m_DisplayUnit;

	ACHAR* m_Formula;
	ACHAR* m_FormulaVariableLength;
	ACHAR* m_FormulaLengthOnly;
	ACHAR* m_FormulaPosOnly;
	ACHAR* m_StandardDiameters;

	Adesk::UInt16 m_TextColor;
	Adesk::UInt16 m_PosColor;
	Adesk::UInt16 m_CircleColor;
	Adesk::UInt16 m_MultiplierColor;
	Adesk::UInt16 m_GroupColor;
	Adesk::UInt16 m_NoteColor;
	Adesk::UInt16 m_CurrentGroupHighlightColor;
	Adesk::UInt16 m_CountColor;

    double m_NoteScale;

    AcDbHardPointerId m_TextStyleID;
    AcDbHardPointerId m_NoteStyleID;

	AcGsNode* m_GsNode;

public:
	/// Gets or sets item name
	const ACHAR* Name(void) const;
	Acad::ErrorStatus setName(const ACHAR* newVal);

	/// Gets or sets the bending option.
    const Adesk::Boolean Bending(void) const;
	Acad::ErrorStatus setBending(const Adesk::Boolean newVal);

	/// Gets or sets the maximum bar length
    const double MaxBarLength(void) const;
	Acad::ErrorStatus setMaxBarLength(const double newVal);

	/// Gets or sets the display precision
    const Adesk::Int32 Precision(void) const;
	Acad::ErrorStatus setPrecision(const Adesk::Int32 newVal);

	/// Gets or sets the drawing unit
    const DrawingUnits DrawingUnit(void) const;
	Acad::ErrorStatus setDrawingUnit(const DrawingUnits newVal);

	/// Gets or sets the display unit
    const DrawingUnits DisplayUnit(void) const;
	Acad::ErrorStatus setDisplayUnit(const DrawingUnits newVal);

	/// Gets or sets the formula text.
    const ACHAR* Formula(void) const;
	Acad::ErrorStatus setFormula(const ACHAR* newVal);

	/// Gets or sets the formula text with only length displayed.
    const ACHAR* FormulaVariableLength(void) const;
	Acad::ErrorStatus setFormulaVariableLength(const ACHAR* newVal);

	/// Gets or sets the formula text with only length displayed.
    const ACHAR* FormulaLengthOnly(void) const;
	Acad::ErrorStatus setFormulaLengthOnly(const ACHAR* newVal);

	/// Gets or sets the formula text with only pos marker displayed.
    const ACHAR* FormulaPosOnly(void) const;
	Acad::ErrorStatus setFormulaPosOnly(const ACHAR* newVal);

	/// Gets or sets the list of standard diameters separated with spaces.
    const ACHAR* StandardDiameters(void) const;
	Acad::ErrorStatus setStandardDiameters(const ACHAR* newVal);

	/// Gets or sets the text color.
	const Adesk::UInt16 TextColor(void) const;
	Acad::ErrorStatus setTextColor(const Adesk::UInt16 newVal);

	/// Gets or sets the pos text color.
	const Adesk::UInt16 PosColor(void) const;
	Acad::ErrorStatus setPosColor(const Adesk::UInt16 newVal);

	/// Gets or sets the pos circle color.
	const Adesk::UInt16 CircleColor(void) const;
	Acad::ErrorStatus setCircleColor(const Adesk::UInt16 newVal);

	/// Gets or sets the multiplier text color.
	const Adesk::UInt16 MultiplierColor(void) const;
	Acad::ErrorStatus setMultiplierColor(const Adesk::UInt16 newVal);

	/// Gets or sets the group text color.
	const Adesk::UInt16 GroupColor(void) const;
	Acad::ErrorStatus setGroupColor(const Adesk::UInt16 newVal);

	/// Gets or sets the note text color.
	const Adesk::UInt16 NoteColor(void) const;
	Acad::ErrorStatus setNoteColor(const Adesk::UInt16 newVal);

	/// Gets or sets the highlight color of the pos objects in the current group.
	const Adesk::UInt16 CurrentGroupHighlightColor(void) const;
	Acad::ErrorStatus setCurrentGroupHighlightColor(const Adesk::UInt16 newVal);

	/// Gets or sets the highlight color of the pos objects with 0 counts
	const Adesk::UInt16 CountColor(void) const;
	Acad::ErrorStatus setCountColor(const Adesk::UInt16 newVal);

	/// Gets or sets the note height relative to text height.
	const double NoteScale(void) const;
	Acad::ErrorStatus setNoteScale(const double newVal);

	/// Gets or sets pointer to the text style.
	const AcDbObjectId& TextStyleId(void) const;
	Acad::ErrorStatus setTextStyleId(const AcDbObjectId& newVal);

	/// Gets or sets pointer to the note style.
	const AcDbObjectId& NoteStyleId(void) const;
	Acad::ErrorStatus setNoteStyleId(const AcDbObjectId& newVal);

private:
	static ACHAR* Table_Name;
	static ACHAR* TextStyle_Name;
	static ACHAR* TextStyle_Font;
	static double TextStyle_Width;
	static ACHAR* NoteStyle_Name;
	static ACHAR* NoteStyle_Font;
	static double NoteStyle_Width;

public:
	/// Gets the table name
	static ACHAR* GetTableName();

	/// Gets the one and only group
	static Acad::ErrorStatus GetGroupId(AcDbObjectId& id);

	/// Creates the one and only group
	static Acad::ErrorStatus CreateGroup(void);

public:
	// Drawable implementation
    virtual AcGiDrawable*       drawable();

    virtual void            setGsNode       (AcGsNode* gsnode);
    virtual AcGsNode*       gsNode          (void) const;

    virtual bool            bounds          (AcDbExtents& bounds) const;

    virtual Adesk::UInt32   subSetAttributes   (AcGiDrawableTraits* traits);
    virtual Adesk::Boolean  subWorldDraw       (AcGiWorldDraw* worldDraw);
    virtual void            subViewportDraw    (AcGiViewportDraw* vd);
	virtual Adesk::UInt32   subViewportDrawLogicalFlags (AcGiViewportDraw * vd);

private:
	const std::vector<CRebarPos*> GetDisplayedPos(void) const;
};
