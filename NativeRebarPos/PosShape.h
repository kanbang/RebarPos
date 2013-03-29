//-----------------------------------------------------------------------------
//----- PosShape.h : Declaration of the CPosShape
//-----------------------------------------------------------------------------
#pragma once

#include <vector>
#include <map>

#include "Shape.h"
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

typedef std::vector<CShape*> ShapeList;
typedef std::vector<CShape*>::size_type ShapeSize;
typedef std::vector<CShape*>::iterator ShapeListIt;
typedef std::vector<CShape*>::const_iterator ShapeListConstIt;

/// ---------------------------------------------------------------------------
/// The CPosShape represents the shape definitions of rebar markers.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CPosShape : public  AcGiDrawable
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CPosShape);

public:
	CPosShape ();
	virtual ~CPosShape ();

protected:
	/// Property backing fields
	ACHAR* m_Name;

	Adesk::Int32 m_Fields;

	ACHAR* m_Formula;
	ACHAR* m_FormulaBending;

	Adesk::Int32 m_Priority;

	Adesk::Boolean m_IsBuiltIn;
	Adesk::Boolean m_IsUnknown;
	Adesk::Boolean m_IsInternal;

	ShapeList m_List;

	AcGsNode* m_GsNode;

	ACHAR* m_A;
	ACHAR* m_B;
	ACHAR* m_C;
	ACHAR* m_D;
	ACHAR* m_E;
	ACHAR* m_F;
	AcGiTextStyle m_Style;

public:
	/// Gets or sets item name
	const ACHAR* Name(void) const;
	Acad::ErrorStatus setName(const ACHAR* newVal);

	/// Gets or sets the field count.
    const Adesk::Int32 Fields(void) const;
	Acad::ErrorStatus setFields(const Adesk::Int32 newVal);

	/// Gets or sets the formula used to calculate the total length from piece lengths.
	const ACHAR* Formula(void) const;
	Acad::ErrorStatus setFormula(const ACHAR* newVal);

	/// Gets or sets the formula used to calculate the total length from piece lengths
	/// including bending allowance.
	const ACHAR* FormulaBending(void) const;
	Acad::ErrorStatus setFormulaBending(const ACHAR* newVal);

	/// Gets or sets the priority of the shape in BOM table.
	const Adesk::Int32 Priority(void) const;
	Acad::ErrorStatus setPriority(const Adesk::Int32 newVal);

	/// Gets or sets whether this is an unknown shape
public:
	const Adesk::Boolean IsUnknown(void) const;
protected:
	Acad::ErrorStatus setIsUnknown(const Adesk::Boolean newVal);

	/// Gets or sets whether this is a built-in shape
public:
	const Adesk::Boolean IsBuiltIn(void) const;
protected:
	Acad::ErrorStatus setIsBuiltIn(const Adesk::Boolean newVal);

	/// Gets or sets whether this is an internal shape
public:
	const Adesk::Boolean IsInternal(void) const;
protected:
	Acad::ErrorStatus setIsInternal(const Adesk::Boolean newVal);

public:
	/// Adds a shape.
	void AddShape(CShape* const shape);

	/// Gets the shape at the given index.
	const CShape* GetShape(const ShapeSize index) const;

	/// Sets the shape at the given index.
	void SetShape(const ShapeSize index, CShape* const shape);

	/// Gets the count of shapes.
	const ShapeSize GetShapeCount() const;

	/// Removes the shape at the given index.
	void RemoveShape(const ShapeSize index);

	/// Clears all shapes.
	void ClearShapes();

	/// Get shape extents
	const AcDbExtents GetShapeExtents() const;

	/// Replaces piece names with given strings
	Acad::ErrorStatus setShapeTexts(const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f);
	Acad::ErrorStatus clearShapeTexts(void);

private:
	static std::map<std::wstring, CPosShape*> m_BuiltInPosShapes;
	static std::map<std::wstring, CPosShape*> m_InternalPosShapes;
	static std::map<std::wstring, CPosShape*> m_CustomPosShapes;

public:
	/// Add a new shape
	static void AddPosShape(CPosShape* shape);

	/// Gets the shape with the given name
	static CPosShape* GetPosShape(const std::wstring name);

	/// Gets the shape representing an unkown shape
	static CPosShape* GetUnknownPosShape();

	/// Gets the number of pos shapes
	static int GetPosShapeCount(const bool builtin, const bool isinternal, const bool custom);

	/// Removes all shapes
	static void ClearPosShapes(const bool builtin, const bool isinternal, const bool custom);

	/// Gets the names of all shapes
	static std::vector<std::wstring> GetAllShapes(const bool builtin, const bool isinternal, const bool custom);

	/// Reads all shapes defined in the resource
	static void ReadPosShapesFromResource(HINSTANCE hInstance, const int resid, const bool isinternal);

	/// Reads all shapes defined in the given text file
	static void ReadPosShapesFromFile(const std::wstring filename);

	/// Saves all shapes to the given text file
	static void SavePosShapesToFile(const std::wstring filename);

	/// Reads all shapes defined in the string
	static void ReadPosShapesFromString(const std::wstring source, const bool builtin, const bool isinternal);

public:
    // Drawable implementation
    virtual Adesk::Boolean  isPersistent    (void) const;
    virtual AcDbObjectId    id              (void) const;

    virtual void            setGsNode       (AcGsNode* gsnode);
    virtual AcGsNode*       gsNode          (void) const;

    virtual bool            bounds          (AcDbExtents& bounds) const;

public:
	// Drawable implementation
    virtual Adesk::UInt32   subSetAttributes   (AcGiDrawableTraits* traits);
    virtual Adesk::Boolean  subWorldDraw       (AcGiWorldDraw* worldDraw);
    virtual void            subViewportDraw    (AcGiViewportDraw* vd);
	virtual Adesk::UInt32   subViewportDrawLogicalFlags (AcGiViewportDraw * vd);

private:
		// Helper functions
	static bool SortShapeNames(const std::wstring p1, const std::wstring p2);
};
