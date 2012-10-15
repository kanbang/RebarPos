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
class DLLIMPEXP CPosShape
{

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

	ShapeList m_List;

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

private:
	static std::map<std::wstring, CPosShape*> m_PosShapes;

public:
	/// Gets the shape with the given name
	static CPosShape* GetPosShape(std::wstring name);

	/// Gets the number of pos shapes
	static std::map<std::wstring, CPosShape*>::size_type GetPosShapeCount();

	/// Gets the underlying maps
	static std::map<std::wstring, CPosShape*> GetMap();

	/// Reads all shapes defined in the resource
	static void MakePosShapesFromResource(HINSTANCE hInstance);

	/// Removes all shapes
	static void ClearPosShapes();
};
