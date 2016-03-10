//-----------------------------------------------------------------------------
//----- RebarPos.h : Declaration of the CRebarPos
//-----------------------------------------------------------------------------
#pragma once

#pragma warning( push )
#pragma warning( disable : 4100 )
#include "acgi.h"
#pragma warning( pop )

#include <vector>
#include <map>

#include "DrawParams.h"
#include "PosShape.h"
#include "PosGroup.h"
#include "Shape.h"

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

typedef std::vector<CDrawParams> DrawList;
typedef std::vector<CDrawParams>::size_type DrawListSize;
typedef std::vector<CDrawParams>::iterator DrawListIt;
typedef std::vector<CDrawParams>::const_iterator DrawListConstIt;

/// ---------------------------------------------------------------------------
/// The CRebarPos represents a rebar marker in the drawing. It contains
/// marker number, rebar shape, diameter, spacing, piece lengths and some
/// metadata as well.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CRebarPos : public  AcDbEntity
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CRebarPos);
    
protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
    /// Constructors and destructor    
    CRebarPos();
    virtual ~CRebarPos();
       
public:
	enum PosSubEntityType
	{ 
		NONE = 0,
		POS = 1,
		COUNT = 2,
		DIAMETER = 3,
		SPACING = 4,
		MULTIPLIER = 6,
		LENGTH = 7,
		NOTE = 8
	};
	enum DisplayStyle
	{ 
		ALL = 0,
		WITHOUTLENGTH = 1,
		MARKERONLY = 2,
	};
	enum SubTextAlignment
	{ 
		FREE = 0,
		LEFT = 1,
		RIGHT = 2,
		TOP = 3,
		BOTTOM = 4,
	};

public:
	struct CCalculatedProperties
	{
	public:
		int Generation;

		int Count;
		double Diameter;
		int Precision;
		int FieldCount;
		bool Bending;

		int MinCount;
		int MaxCount;
		bool IsMultiCount;

		double MinA, MinB, MinC, MinD, MinE, MinF;
		double MaxA, MaxB, MaxC, MaxD, MaxE, MaxF;
		bool IsVarA, IsVarB, IsVarC, IsVarD, IsVarE, IsVarF;

		double MinLength, MaxLength;
		bool IsVarLength;

		double MinSpacing, MaxSpacing;
		bool IsVarSpacing;

		CPosGroup::DrawingUnits DrawingUnit;
		CPosGroup::DrawingUnits DisplayUnit;

	public:
		CCalculatedProperties() : Generation(0), Count(0), Diameter(0), Precision(0), FieldCount(0), Bending(0),
			MinCount(0), MaxCount(0), IsMultiCount(false),
			MinA(0),  MinB(0),  MinC(0),  MinD(0),  MinE(0),  MinF(0),
			MaxA(0),  MaxB(0),  MaxC(0),  MaxD(0),  MaxE(0),  MaxF(0),
			IsVarA(false),  IsVarB(false),  IsVarC(false),  IsVarD(false),  IsVarE(false),  IsVarF(false), 
			MinLength(0), MaxLength(0), IsVarLength(false),
			MinSpacing(0), MaxSpacing(0), IsVarSpacing(false),
			DrawingUnit(CPosGroup::MM), DisplayUnit(CPosGroup::MM)
		{ }

		void Reset()
		{
			Count = 0;
			Diameter = 0.0;
			Precision = 0;
			FieldCount = 0;
			Bending = false;

			MinCount = 0; MaxCount = 0;
			IsMultiCount = false;

			MinA = 0; MinB = 0; MinC = 0; MinD = 0; MinE = 0; MinF = 0;
			MaxA = 0; MaxB = 0; MaxC = 0; MaxD = 0; MaxE = 0; MaxF = 0;
			IsVarA = false; IsVarB = false; IsVarC = false; IsVarD = false; IsVarE = false; IsVarF = false;

			MinLength = 0; MaxLength = 0;
			IsVarLength = false;

			MinSpacing = 0; MaxSpacing = 0;
			IsVarSpacing = false;

			DrawingUnit = CPosGroup::MM;
			DisplayUnit = CPosGroup::MM;
		}
	};

private:
	/// Used to cache last draw params
	DrawList lastDrawList;
	CDrawParams lastNoteDraw;
	CDrawParams lastMultiplierDraw;
	CDrawParams lastLengthDraw;
    AcGiTextStyle lastTextStyle;
    AcGiTextStyle lastNoteStyle;
	Adesk::UInt16 lastCircleColor;
	AcDbObjectId defpointsLayer;
	double lastNoteScale;
	double circleRadius;
	double tauSize;
	double partSpacing;
	CCalculatedProperties m_CalcProps;

private:
	/// Property backing fields
	AcGeVector3d m_Direction, m_Up;
	AcGePoint3d m_BasePoint;
	AcGePoint3d m_NoteGrip;
	AcGePoint3d m_LengthGrip;
	ACHAR* m_Key;
	ACHAR* m_Length;
	ACHAR* m_DisplayedSpacing;
	ACHAR* m_Pos;
	ACHAR* m_Count;
	ACHAR* m_DisplayedCount;
	ACHAR* m_Diameter;
	ACHAR* m_Spacing;
	Adesk::Boolean m_IncludeInBOQ;
	Adesk::Int32 m_Multiplier;
	ACHAR* m_Note;
	ACHAR* m_Shape;
	ACHAR* m_A;
	ACHAR* m_B;
	ACHAR* m_C;
	ACHAR* m_D;
	ACHAR* m_E;
	ACHAR* m_F;
	Adesk::Boolean m_Detached;

	DisplayStyle m_DisplayStyle;

	SubTextAlignment m_LengthAlignment;
	SubTextAlignment m_NoteAlignment;

	std::map<AcDbObjectId, double> m_BoundDimensions;

	CPosGroup* m_GroupForDisplay;

	/// Locals
	bool geomInit;
	AcGeMatrix3d ucs;

private:
	int suspendCount;
	bool needsUpdate;

public:
	virtual void SuspendUpdate(void);
	virtual void ResumeUpdate(void);

protected:
	/// Calculates pos properties when the pos is modified.
	void Calculate(void);

	/// Parses formula text and creates the draw list
	const DrawList ParseFormula(const ACHAR* formula) const;

protected:
	/// Calculates lengths
	static bool CalcConsLength(const ACHAR* str, const double diameter, const CPosGroup::DrawingUnits inputUnit, double& value);
	static bool GetTotalLengths(const ACHAR* formula, CCalculatedProperties& props);

public:
	/// Gets piece lengths
	static bool GetLengths(const ACHAR* str, const double diameter, const CPosGroup::DrawingUnits inputUnit, double& minLength, double& maxLength, bool& isVar);

	// Gets total lengths
	static bool GetTotalLengths(const ACHAR* formula, const int fieldCount, const CPosGroup::DrawingUnits inputUnit, const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f, const ACHAR* diameter, double& minLength, double& maxLength, bool& isVar);

	// Unit conversion
	static double ConvertLength(const double length, const CPosGroup::DrawingUnits fromUnit, const CPosGroup::DrawingUnits toUnit);

	/// Gets bending radius
	static double BendingRadius(const double d);

public:
	/// Get direction vector
	const AcGeVector3d& DirectionVector(void) const;
	/// Get up vector
	const AcGeVector3d& UpVector(void) const;
	/// Get normal vector
	const AcGeVector3d NormalVector(void) const;

	/// Object scale
	const double Scale(void) const;
	Acad::ErrorStatus setScale(const double newVal);

	/// Get extents
	const double Width(void) const;
	const double Height(void) const;
	const void TextBox(AcGePoint3d& ptmin, AcGePoint3d& ptmax);

	/// Determines which part is under the given point
	const CRebarPos::PosSubEntityType HitTest(const AcGePoint3d& pt0) const;

	/// Gets or sets the base grip point
	const AcGePoint3d& BasePoint(void) const;
	Acad::ErrorStatus setBasePoint(const AcGePoint3d& newVal);

	/// Gets or sets the note grip point
	const AcGePoint3d& NoteGrip(void) const;
	Acad::ErrorStatus setNoteGrip(const AcGePoint3d& newVal);

	/// Gets or sets the length grip point
	const AcGePoint3d& LengthGrip(void) const;
	Acad::ErrorStatus setLengthGrip(const AcGePoint3d& newVal);

	/// Gets or sets pos marker number
	const ACHAR* Pos(void) const;
	Acad::ErrorStatus setPos(const ACHAR* newVal);

	/// Gets or sets the note text
	const ACHAR* Note(void) const;
	Acad::ErrorStatus setNote(const ACHAR* newVal);

	/// Gets or sets the count. Simple arithmetic allowed, ie. 4x2, 2x3x4
	const ACHAR* Count(void) const;
	Acad::ErrorStatus setCount(const ACHAR* newVal);

	/// Gets or sets the rebar diameter
	const ACHAR* Diameter(void) const;
	Acad::ErrorStatus setDiameter(const ACHAR* newVal);

	/// Gets or sets the bar spacing
	const ACHAR* Spacing(void) const;
	Acad::ErrorStatus setSpacing(const ACHAR* newVal);

	/// Gets or sets whether the pos is included in the BOQ
	const Adesk::Boolean IncludeInBOQ(void) const;
	Acad::ErrorStatus setIncludeInBOQ(const Adesk::Boolean newVal);

	/// Gets or sets the BOQ multiplier
	const Adesk::Int32 Multiplier(void) const;
	Acad::ErrorStatus setMultiplier(const Adesk::Int32 newVal);

	/// Gets or sets whether length text is shown
	const DisplayStyle Display(void) const;
	Acad::ErrorStatus setDisplay(const DisplayStyle newVal);

	/// Gets or sets the shape name
	const ACHAR* Shape(void) const;
	Acad::ErrorStatus setShape(const ACHAR* newVal);

	/// Gets or sets the A piece length
	const ACHAR* A(void) const;
	Acad::ErrorStatus setA(const ACHAR* newVal);

	/// Gets or sets the B piece length
	const ACHAR* B(void) const;
	Acad::ErrorStatus setB(const ACHAR* newVal);

	/// Gets or sets the C piece length
	const ACHAR* C(void) const;
	Acad::ErrorStatus setC(const ACHAR* newVal);

	/// Gets or sets the D piece length
	const ACHAR* D(void) const;
	Acad::ErrorStatus setD(const ACHAR* newVal);

	/// Gets or sets the E piece length
	const ACHAR* E(void) const;
	Acad::ErrorStatus setE(const ACHAR* newVal);

	/// Gets or sets the F piece length
	const ACHAR* F(void) const;
	Acad::ErrorStatus setF(const ACHAR* newVal);

	/// Gets the total length
	const ACHAR* Length(void) const;

	/// Gets or sets whether the pos is detached from shape definitions
	const Adesk::Boolean Detached(void) const;
	Acad::ErrorStatus setDetached(const Adesk::Boolean newVal);

	/// Gets or sets the alignment of length text
	const SubTextAlignment LengthAlignment(void) const;
	Acad::ErrorStatus setLengthAlignment(const SubTextAlignment newVal);

	/// Gets or sets the alignment of note text
	const SubTextAlignment NoteAlignment(void) const;
	Acad::ErrorStatus setNoteAlignment(const SubTextAlignment newVal);

	/// Gets calculated properties
	const CCalculatedProperties& CalcProps(void) const;

public:
	/// Sets the group to be used for displaying the object. (Not persisted, for temporary display only)
	Acad::ErrorStatus setGroupForDisplay(const CPosGroup* newVal);

public:
	/// Gets a string key identifying a pos with a certain diameter,
	/// shape and piece lengths
	const ACHAR* PosKey() const;
 
public:
	/// Forces a view update.
	const void Update(void);

	/// Adds/removes a bound dimension
	Acad::ErrorStatus AddBoundDimension(AcDbObjectId id);
	Acad::ErrorStatus RemoveBoundDimension(AcDbObjectId id);
	std::vector<AcDbObjectId> GetBoundDimensions(void);
	Acad::ErrorStatus ClearBoundDimensions(void);

public:
	/// Updates all objects
	static void UpdateAll(void);

public:
	/// AcDbEntity overrides: database    
    virtual Acad::ErrorStatus	dwgInFields(AcDbDwgFiler* filer) override;
    virtual Acad::ErrorStatus	dwgOutFields(AcDbDwgFiler* filer) const override;
    
    virtual Acad::ErrorStatus	dxfInFields(AcDbDxfFiler* filer) override;
    virtual Acad::ErrorStatus	dxfOutFields(AcDbDxfFiler* filer) const override;

	virtual void saveAs(AcGiWorldDraw *pWd, AcDb::SaveType saveType) override;

	// AcDbObject overrides: reactors
	virtual void modified(const AcDbObject* dbObj) override;
	virtual void erased(const AcDbObject* dbObj, Adesk::Boolean pErasing) override;
	
protected:
	/// AcDbEntity overrides: geometry
    virtual Acad::ErrorStatus subGetOsnapPoints(
        AcDb::OsnapMode       osnapMode,
        Adesk::GsMarker       gsSelectionMark,
        const AcGePoint3d&    pickPoint,
        const AcGePoint3d&    lastPoint,
        const AcGeMatrix3d&   viewXform,
        AcGePoint3dArray&     snapPoints,
        AcDbIntArray&         geomIds) const override;

    virtual Acad::ErrorStatus   subGetGripPoints(AcGePoint3dArray&     gripPoints,
        AcDbIntArray&  osnapModes,
        AcDbIntArray&  geomIds) const override;

    virtual Acad::ErrorStatus   subMoveGripPointsAt(const AcDbIntArray& indices,
        const AcGeVector3d&     offset) override;

    virtual Acad::ErrorStatus   subTransformBy(const AcGeMatrix3d& xform) override;

    virtual void                subList() const override;

    virtual Acad::ErrorStatus	subExplode(AcDbVoidPtrArray& entitySet) const override;

    virtual Adesk::Boolean      subWorldDraw(AcGiWorldDraw*	mode) override;
    
	virtual Acad::ErrorStatus   subGetGeomExtents(AcDbExtents& extents) const override;

protected:
    /// Overridden methods from AcDbObject    
    virtual Acad::ErrorStatus subDeepClone(AcDbObject* pOwnerObject,
        AcDbObject*& pClonedObject,
        AcDbIdMapping& idMap,
        Adesk::Boolean isPrimary
        = Adesk::kTrue) const override;
    
    virtual Acad::ErrorStatus subWblockClone(AcRxObject* pOwnerObject,
        AcDbObject*& pClonedObject,
        AcDbIdMapping& idMap,
        Adesk::Boolean isPrimary
        = Adesk::kTrue) const override;
    
    virtual Acad::ErrorStatus subGetClassID(CLSID* pClsid) const override;
};
